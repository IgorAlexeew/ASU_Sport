using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

namespace ASUSport.Repositories
{
    public class SportObjectRepository : ISportObjectRepository
    {
        private readonly ApplicationContext db;

        public SportObjectRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <inheritdoc/>
        public List<SportObjectForMainDTO> GetInfo(int num = 3)
        {
            var objects = db.SportObjects.Take(num).Select(s => s.Name).ToList();

            DateTime today = DateTime.Now;

            if (today.DayOfWeek == DayOfWeek.Sunday)
                today.AddDays(1);

            DateTime nearestSaturday = GetNearestDay(today, DayOfWeek.Saturday);

            var result = new List<SportObjectForMainDTO>();

            foreach (var obj in objects)
            {
                var events = db.Events.Where(e => e.Section.SportObject.Name == obj
                    && e.Time > today && e.Time < nearestSaturday).ToList();

                var subscription = db.Subscriptions.First(s => s.SportObject.Name == obj && s.Type == "Разовое занятие");

                var info = new SportObjectForMainDTO()
                {
                    ObjectName = obj,
                    ServiceName = subscription.Type,
                    WorkingHours = subscription.StartingTime != null ? subscription.StartingTime + " - " + subscription.ClosingTime : null,
                    Price = subscription.Price
                };

                if (events.Any())
                {
                    var grouping = events.GroupBy(e => e.Time.Date)
                        .Select(g => new { date = g.Key, num = g.Sum(e => e.Clients.Count) })
                        .OrderBy(key => key.num).Take(3).Select(s => s.date).ToList();

                    info.Days = grouping;
                }

                else
                    info.Days = null;

                result.Add(info);                   
            }

            return result;
        }

        /// <inheritdoc/>
        public DateTime GetNearestDay(DateTime today, DayOfWeek day)
        {
            int diff = ((int) day - (int) today.DayOfWeek + 6) % 7;
            return today.AddDays(diff + 1);
        }

        /// <inheritdoc/>
        public EventsForSportobjectDTO GetEventByDateSportObject(string id, string date)
        {
            var events = db.Events.Where(e => e.Section.SportObject.Id == int.Parse(id) && e.Time.Date == DateTime.Parse(date)).ToList();

            if (!events.Any())
                return null;

            var eventsList = new List<EventModelDTO>();

            var selectedObject = db.SportObjects.First(s => s.Id == int.Parse(id));

            int capacity = selectedObject.Capacity;
            string name = selectedObject.Name;

            foreach (var e in events)
            {
                string trainerName = string.Empty;
                
                if (e.Trainer != null)
                {
                    var trainer = db.UserData.First(u => u.User == e.Trainer);

                    trainerName = trainer.FirstName + " " + trainer.MiddleName + " " + trainer.LastName;
                }

                var model = new EventModelDTO()
                {
                    SectionName = e.Section.Name,
                    Time = e.Time.ToString("HH:mm"),
                    Duration = e.Section.Duration,
                    FreeSpaces = capacity - e.Clients.Count,
                    TrainerName = trainerName
                };

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
        public List<object> GetSportObjectIds()
        {
            var objects = db.SportObjects.Select(s => s);

            var result = new List<object>();

            foreach (var obj in objects)
            {
                result.Add(new { obj.Id, obj.Name });
            }

            return result;
        }
    }
}
