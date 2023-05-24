using System;
using System.Collections.Generic;
using System.Linq;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using ASUSport.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ASUSport.Repositories
{
    public class EventRepository : IEventRepository
    {
        private ApplicationContext db;

        public EventRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <inheritdoc/>
        public Response SignUpForAnEvent(int eventId, string login)
        {
            if (login == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "not_authorized",
                    Message = "Пользователь не авторизован"
                };
            }
            
            var selectedEvent = db.Events.FirstOrDefault(e => e.Id == eventId);

            if (selectedEvent == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "event_not_found",
                    Message = "События не существует"
                };
            }

            var user = db.Users.First(u => u.Login == login);

            if (user.Events.FirstOrDefault(e => e.Id == eventId) != null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "already_signed_up",
                    Message = "Пользователь уже записан на это событие"
                };
            }

            if (selectedEvent.Section.SportObject.Capacity - selectedEvent.Clients.Count == 0)
            {
                return new Response()
                {
                    Status = false,
                    Type = "no_free_spaces",
                    Message = "Свободных мест нет"
                };
            }

            selectedEvent.Clients.Add(user);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response AddEvent(EventDTO data)
        {
            var section = db.Sections.FirstOrDefault(s => s.Id == data.Section);

            if (section == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "section_not_found",
                    Message = "Секция не найдена"
                };
            }

            User selectedTrainer = null;

            if (data.Trainer != null)
            {
                selectedTrainer = GetTrainer((int)data.Trainer);

                if (selectedTrainer == null)
                {
                    return new Response()
                    {
                        Status = false,
                        Type = "trainer_not_found",
                        Message = "Тренер не найден"
                    };
                }
            }

            var newEvent = new Event()
            {
                Section = section,
                Trainer = selectedTrainer,
                Time = DateTime.Parse(data.Date + " " + data.Time)
            };

            db.Events.Add(newEvent);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public User GetTrainer(int id)
        {
            var trainer = db.Users.FirstOrDefault(u => u.Id == id);

            if (trainer == null)
                return null;

            return trainer;
        }

        /// <inheritdoc/>
        public List<EventModelDTO> GetEvents(int? section, int? trainer, string date, string time)
        {
            var result = new List<EventModelDTO>();

            //.Where(e => e.Time > DateTime.Now)

            List<Event> events = db.Events.Select(e => e).ToList();

            if (section != null)
                events = events.Where(e => e.Section.Id == section).ToList();

            if (date != null)
                events = events.Where(e => e.Time.Date == DateTime.Parse(date)).ToList();

            if (time != null)
                events = events.Where(e => e.Time.ToString("HH:mm") == time).ToList();

            if (trainer != null)
            {
                var trainerUser = GetTrainer((int)trainer);

                events = events.Where(e => e.Trainer == trainerUser).ToList();
            }

            foreach (Event ev in events.ToList())
            {
                string trainerName = string.Empty;
                
                if (ev.Trainer != null)
                {
                    var trainerUser = db.UserData.First(u => u.User == ev.Trainer);

                    trainerName = trainerUser.FirstName + " " + trainerUser.MiddleName + " " + trainerUser.LastName;
                }

                var capacity = ev.Section.SportObject.Capacity;

                var model = new EventModelDTO()
                {
                    Id = ev.Id,
                    SectionName = ev.Section.Name,
                    TrainerName = trainerName,
                    Date = ev.Time.ToString("yyyy-MM-dd"),
                    Time = ev.Time.ToString("HH:mm"),
                    Duration = ev.Section.Duration,
                    FreeSpaces = capacity - ev.Clients.Count,
                    Capacity = capacity
                };

                result.Add(model);
            }

            return result;
        }

        /// <inheritdoc/>
        public EventsForSportobjectDTO GetEventByDateSportObject(int id, string date, string login)
        {
            var events = db.Events
                .Include(ev => ev.Clients)
                .Where(e => e.Section.SportObject.Id == id && e.Time.Date == DateTime.Parse(date)).ToList();

            var eventsList = new List<EventModelDTO>();

            var selectedObject = db.SportObjects.First(s => s.Id == id);

            int capacity = selectedObject.Capacity;
            string name = selectedObject.Name;

            var subscriptions = db.Subscriptions.Where(s => s.SportObject.Id == id).ToList();

            int price;
            if (subscriptions.Any())
                price = subscriptions.First(s => s.Price == subscriptions.Min(p => p.Price)).Price;

            else
                price = 0;

            foreach (var ev in events)
            {
                string trainerName = string.Empty;
                UserShortDTO trainer = null;

                if (ev.Trainer != null)
                {
                    var trainerData = db.UserData.First(u => u.User.Id == ev.Trainer.Id);

                    trainerName = trainerData.FirstName + " " + trainerData.MiddleName + " " + trainerData.LastName;

                    trainer = new UserShortDTO() { 
                        Id = ev.Trainer.Id,
                        FirstName = trainerData.FirstName,
                        MiddleName = trainerData.MiddleName,
                        LastName = trainerData.LastName,
                    };
                }

                bool isSigned = false;

                if (login != null)
                    isSigned = db.Users.First(u => u.Login == login).Events.Select(e => e.Id).Contains(ev.Id);
                
                var clients = ev.Clients;
                List<UserShortDTO> clientsWithData = new List<UserShortDTO>();

                foreach(var client in clients)
            {
                    var userData = db.UserData.FirstOrDefault(u => u.User == client);

                    var userInfoDTO = new UserShortDTO()
                    {
                        Id = client.Id,
                        FirstName = userData?.FirstName,
                        MiddleName = userData?.MiddleName,
                        LastName = userData?.LastName
                    };

                    clientsWithData.Add(userInfoDTO);
                }


                var model = new EventModelDTO()
                {
                    Id = ev.Id,
                    Date = ev.Time.ToString("yyyy-MM-dd HH:mm"),
                    SectionName = ev.Section.Name,
                    Section = new SectionInfoDTO()
                    {
                        Id = ev.Section.Id,
                        Name = ev.Section.Name,
                        Duration = ev.Section.Duration,
                        SportObjectId = ev.Section.SportObjectId,
                    },
                    Time = ev.Time.ToString("HH:mm") + " - " + ev.Time.AddMinutes(ev.Section.Duration).ToString("HH:mm"),
                    Duration = ev.Section.Duration,
                    FreeSpaces = capacity - ev.Clients.Count,
                    Price = price,
                    TrainerName = trainerName,
                    Trainer = trainer,
                    Clients = clientsWithData.ToArray(),
                };

                if (login != null)
                    model.IsSigned = isSigned;

                eventsList.Add(model);
            }

            var dto = new EventsForSportobjectDTO()
            {
                ObjectName = name,
                Capacity = capacity,
                Date = date,
                Events = eventsList
            };

            return dto;
        }

        /// <inheritdoc/>
        public int GetEvent(int? section, int? trainer, string date, string time)
        {
            User trainerUser = null;

            if (trainer != null)
            {
                trainerUser = GetTrainer((int)trainer);

                if (trainer == null)
                    return 0;
            }

            var selectedEvent = db.Events.FirstOrDefault(
                e => e.Trainer == trainerUser && e.Section.Id == section && e.Time == DateTime.Parse(date + " " + time));

            if (selectedEvent == null)
            {
                return 0;
            }

            return selectedEvent.Id;
        }

        /// <inheritdoc/>
        public Response SignUpForUnathorized(SignUpForUnathorizedDTO data)
        {
            var selectedEvent = db.Events.First(o => o.Id == data.EventId);

            var newUser = new User()
            {
                Login = data.Login,
                RoleId = db.Roles.First(r => r.Name == "client").Id
            };

            db.Users.Add(newUser);

            var userData = new UserData()
            {
                FirstName = data.Name,
                User = newUser
            };

            db.UserData.Add(userData);

            selectedEvent.Clients.Add(newUser);

            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UnsubscribeForTheEvent(int id, string login)
        {
            var user = db.Users.FirstOrDefault(o => o.Login == login);

            var ev = db.Events.FirstOrDefault(o => o.Id == id);

            user.Events.Remove(ev);
            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UpdateEvent(UpdateEventDTO data)
        {
            var selectedEvent = db.Events.FirstOrDefault(s => s.Id == data.Id);
            
            if (data.SectionId != null)
            {
                var newSection = db.Sections.FirstOrDefault(s => s.Id == data.SectionId);
                selectedEvent.Section = newSection;
            }

            if (data.TrainerId != null)
            {
                var newTrainer = db.Users.FirstOrDefault(s => s.Id == data.TrainerId);
                selectedEvent.Trainer = newTrainer;
            }

            if (data.Time != null)
                selectedEvent.Time = DateTime.Parse(data.Time);

            if (data.ClientIds != null && data.ClientIds.Length > 0)
            {
                selectedEvent.Clients.Clear();
                var clients = db.Users.Where(user => data.ClientIds.Contains(user.Id)).ToArray();
                foreach (var client in clients)
                {
                    selectedEvent.Clients.Add(client);
                }
            }

            db.Events.Update(selectedEvent);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response DeleteEvent(int id)
        {
            var selectedEvent = db.Events.FirstOrDefault(s => s.Id == id);

            db.Events.Remove(selectedEvent);
            //db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public byte[] GetEventsWithClients(string date, int sportObject)
        {
            var result = new EventsWithClientsDTO()
            {
                Date = date,
                SportObject = db.SportObjects.FirstOrDefault(s => s.Id == sportObject).Name,
                EventParticipants = new List<EventParticipantsDTO>()
            };

            var events = db.Events.Where(e => e.Time.Date == DateTime.Parse(date) && e.Section.SportObjectId == sportObject).ToList();

            foreach (var ev in events)
            {
                var duration = ev.Section.Duration;

                UserDataDTO trainerModel = null;

                if (ev.Trainer != null)
                {
                    var trainer = db.UserData.First(u => u.UserId == ev.Trainer.Id);

                    trainerModel = new UserDataDTO()
                    {
                        FirstName = trainer.FirstName,
                        MiddleName = trainer.MiddleName,
                        LastName = trainer.LastName
                    };
                }

                var clientsAndTimestamps = new EventParticipantsDTO()
                {
                    Timestamp = ev.Time.ToString("HH:mm") + " - " + ev.Time.AddMinutes(duration).ToString("HH:mm"),
                    Clients = new List<UserDataDTO>(),
                    SectionName = ev.Section.Name,
                    Trainer = trainerModel
                };

                foreach (int id in ev.Clients.Select(s => s.Id))
                {
                    var user = db.UserData.FirstOrDefault(s => s.UserId == id);

                    var userInfo = new UserDataDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        PhoneNumber = user.PhoneNumber,
                        DateOfBirth = user.DateOfBirth.ToShortDateString()
                    };

                    clientsAndTimestamps.Clients.Add(userInfo);
                }

                result.EventParticipants.Add(clientsAndTimestamps);
            }

            return ExcelHelper.GetEventsWithCLients(result);
        }

        /// <inheritdoc/>
        public Response UpdateEvents(List<UpdateEventTableDTO> data)
        {
            foreach (var ev in data)
            {
                if (ev.Id != null)
                    UpdateEvent(new UpdateEventDTO()
                    {
                        Id = ev.Id,
                        Time = ev.Time,
                        SectionId = ev.SectionId,
                        TrainerId = ev.TrainerId
                    });

                else
                {
                    User trainer = null;
                    if (ev.TrainerId != null)
                        trainer = db.Users.First(u => u.Id == (int)ev.TrainerId);

                    var section = db.Sections.FirstOrDefault(s => s.Id == (int)ev.SectionId);

                    var newEvent = new Event()
                    {
                        Time = DateTime.Parse(ev.Time),
                        Trainer = trainer,
                        Section = section
                    };

                    db.Events.Add(newEvent);
                }
            }

            var indexes = db.Events.Select(s => s.Id).ToList()
                .Except(data.Where(s => s.Id != null).Select(s => (int)s.Id).ToList());

            foreach (var index in indexes)
                DeleteEvent(index);

            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public int GetNumberOfEntities()
        {
            return db.Events.Count();
        }

        /// <inheritdoc/>
        public List<UpdateEventTableDTO> GetTableData()
        {
            var events = db.Events.Select(s => s).ToList();

            var result = new List<UpdateEventTableDTO>();

            foreach (var ev in events)
            {
                var e = new UpdateEventTableDTO()
                {
                    Id = ev.Id,
                    TrainerId = ev.Trainer?.Id,
                    SectionId = ev.Section?.Id,
                    Time = ev.Time.ToString("yyyy-MM-dd HH:mm")
                };

                result.Add(e);
            }

            return result;
        }
    }
}
