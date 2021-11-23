using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.ViewModels;
using ASUSport.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;

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
        public Response SignUpForAnEvent(EventDTO data, string login)
        {
            var trainer = GetTrainer(data.TrainerName);

            var selectedEvent = db.Events.FirstOrDefault(
                e => e.Trainer == trainer && e.Section.Name == data.SectionName && e.Time == DateTime.Parse(data.Time));

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
            var section = db.Sections.FirstOrDefault(s => s.Name == data.SectionName);

            if (section == null)
            {
                return new Response()
                {
                    Status = false,
                    Type = "SectionNotFound",
                    Message = "Секция не найдена"
                };
            }

            var selectedTrainer = GetTrainer(data.TrainerName);

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
                Time = DateTime.Parse(data.Time)
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
        public User GetTrainer(string fullName)
        {
            string firstName = fullName.Split(' ')[0];
            string middleName = fullName.Split(' ')[1];
            string lastName = fullName.Split(' ')[2];

            var trainer = db.UserData.FirstOrDefault(
                t => t.FirstName == firstName && t.MiddleName == middleName && t.LastName == lastName)
                .User;

            return trainer;
        }

        /// <inheritdoc/>
        public List<EventDTO> GetEvents(EventDTO parametres)
        {
            var result = new List<EventDTO>();

            List<Event> events = db.Events.Select(e => e).ToList();

            if (parametres.SectionName != null)
            {
                _ = events.Where(e => e.Section.Name == parametres.SectionName);
            }

            if (parametres.Time != null)
            {
                _ = events.Where(e => e.Time.Date == DateTime.Parse(parametres.Time));
            }

            if (parametres.TrainerName != null)
            {
                var trainer = GetTrainer(parametres.TrainerName);

                _ = events.Where(e => e.Trainer == trainer);
            }

            foreach (Event ev in events.ToList())
            {
                var dto = new EventDTO()
                {
                    SectionName = ev.Section.Name,
                    TrainerName = parametres.TrainerName,
                    Time = ev.Time.ToString(),
                    Duration = ev.Section.Duration,
                    FreeSpaces = ev.Section.SportObject.Capacity - ev.Clients.Count
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
