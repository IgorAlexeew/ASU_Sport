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
        public List<SportObjectForMainDTO> GetInfo()
        {
            var objects = db.SportObjects.Select(s => s.Name).ToList();

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

        public DateTime GetNearestDay(DateTime today, DayOfWeek day)
        {
            int diff = ((int) day - (int) today.DayOfWeek + 6) % 7;
            return today.AddDays(diff + 1);
        }
    }
}
