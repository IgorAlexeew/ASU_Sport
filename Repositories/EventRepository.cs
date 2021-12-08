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
            var selectedEvent = db.Events.FirstOrDefault(e => e.Id == eventId);

            if (selectedEvent == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "EventNotFound",
                    Message = "События не существует"
                };
            }

            if (selectedEvent.Section.SportObject.Capacity - selectedEvent.Clients.Count == 0)
            {
                return new Response()
                {
                    Status = false,
                    Type = "NoFreeSpaces",
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
                    Type = "SectionNotFound",
                    Message = "Секция не найдена"
                };
            }

            var selectedTrainer = GetTrainer(data.Trainer);

            if (selectedTrainer == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "TrainerNotFound",
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
        public List<EventModelDTO> GetEvents(string section, string trainer, string date, string time)
        {
            var result = new List<EventModelDTO>();

            List<Event> events = db.Events.Where(e => e.Time > DateTime.Now).Select(e => e).ToList();

            if (section != null)
            {
                _ = events.Where(e => e.Section.Id == int.Parse(section));
            }

            if (date != null)
            {
                _ = events.Where(e => e.Time.Date == DateTime.Parse(date));
            }

            if (time != null)
            {
                _ = events.Where(e => e.Time.ToString("HH:mm") == time);
            }

            if (trainer != null)
            {
                var trainerUser = GetTrainer(int.Parse(trainer));

                _ = events.Where(e => e.Trainer == trainerUser);
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
        public int GetEvent(string section, string trainer, string date, string time)
        {
            var trainerUser = GetTrainer(int.Parse(trainer));

            if (trainer == null)
            {
                return 0;
            }

            var selectedEvent = db.Events.FirstOrDefault(
                e => e.Trainer == trainerUser && e.Section.Id == int.Parse(section) && e.Time == DateTime.Parse(date + " " + time));

            if (selectedEvent == null)
            {
                return 0;
            }

            return selectedEvent.Id;
        }
    }
}
