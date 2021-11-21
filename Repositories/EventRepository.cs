using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.ViewModels;
using ASUSport.Repositories.Impl;

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
        public void SignUpForAnEvent(EventDTO data, string login)
        {
            var trainer = GetTrainer(data.TrainerName);

            var selectedEvent = db.Events.First(
                e => e.Trainer == trainer && e.Section.Name == data.SectionName && e.Time == DateTime.Parse(data.Time));

            var user = db.Users.First(u => u.Login == login);

            selectedEvent.Clients.Add(user);
            db.SaveChanges();
        }

        /// <inheritdoc/>
        public void AddEvent(EventDTO data)
        {
            var selectedTrainer = GetTrainer(data.TrainerName);

            var newEvent = new Event()
            {
                Section = db.Sections.First(s => s.Name == data.SectionName),
                Trainer = selectedTrainer,
                Time = DateTime.Parse(data.Time)
            };

            db.Events.Add(newEvent);
            db.SaveChanges();
        }

        /// <inheritdoc/>
        public User GetTrainer(string fullName)
        {
            string firstName = fullName.Split(' ')[0];
            string middleName = fullName.Split(' ')[1];
            string lastName = fullName.Split(' ')[2];

            var trainer = db.UserData.First(
                t => t.FirstName == firstName && t.MiddleName == middleName && t.LastName == lastName)
                .User;

            return trainer;
        }
    }
}
