using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

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

            if (selectedEvent.Section.SportObject.Capacity - selectedEvent.Clients.Count == 0)
            {
                return new Response()
                {
                    Status = false,
                    Type = "no_free_spaces",
                    Message = "Свободных мест нет"
                };
            }

            var user = db.Users.First(u => u.Login == login);

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

            var selectedTrainer = GetTrainer(data.Trainer);

            if (selectedTrainer == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "trainer_not_found",
                    Message = "Тренер не найден"
                };
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
            var trainer = db.UserData.FirstOrDefault(u => u.User.Id == id);

            if (trainer == null)
                return null;

            return trainer.User;
        }

        /// <inheritdoc/>
        public List<EventModelDTO> GetEvents(int? section, int? trainer, string date, string time)
        {
            var result = new List<EventModelDTO>();

            List<Event> events = db.Events.Where(e => e.Time > DateTime.Now).Select(e => e).ToList();

            if (section != null)
            {
                events = events.Where(e => e.Section.Id == section).ToList();
            }

            if (date != "")
            {
                events = events.Where(e => e.Time.Date == DateTime.Parse(date)).ToList();
            }

            if (time != "")
            {
                events = events.Where(e => e.Time.ToString("HH:mm") == time).ToList();
            }

            if (trainer != null)
            {
                var trainerUser = GetTrainer((int)trainer);

                events = events.Where(e => e.Trainer == trainerUser).ToList();
            }

            foreach (Event ev in events.ToList())
            {
                var trainerUser = db.UserData.First(u => u.User == ev.Trainer);

                string trainerName = trainerUser.FirstName + " " + trainerUser.MiddleName + " " + trainerUser.LastName;

                var capacity = ev.Section.SportObject.Capacity;

                var model = new EventModelDTO()
                {
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
        public int GetEvent(int? section, int? trainer, string date, string time)
        {
            var trainerUser = GetTrainer((int)trainer);

            if (trainer == null)
            {
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
    }
}
