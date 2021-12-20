using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<SportObjectForMainDTO> GetInfo(int? id)
        {
            var objects = db.SportObjects.Select(s => s).ToList();

            if (id != null)
                objects = objects.Where(o => o.Id == id).ToList();

            DateTime today = DateTime.Now;

            if (today.DayOfWeek == DayOfWeek.Sunday)
                today.AddDays(1);

            DateTime nearestSaturday = GetNearestDay(today, DayOfWeek.Saturday);

            var result = new List<SportObjectForMainDTO>();

            foreach (var obj in objects)
            {
                var events = db.Events.Where(e => e.Section.SportObject.Id == obj.Id
                    && e.Time > today && e.Time < nearestSaturday).ToList();

                var subscription = db.Subscriptions.First(s => s.SportObject.Id == obj.Id && s.Type == "Разовое занятие");

                var info = new SportObjectForMainDTO()
                {
                    Id = obj.Id,
                    ObjectName = obj.Name,
                    ServiceName = subscription.Type,
                    WorkingHours = subscription.StartingTime != null ? subscription.StartingTime + " - " + subscription.ClosingTime : null,
                    Price = subscription.Price
                };

                if (events.Any())
                {
                    var grouping = events.GroupBy(e => e.Time.Date)
                        .Select(g => new { date = g.Key, num = g.Sum(e => e.Clients.Count) })
                        .OrderBy(key => key.num).Take(3).Select(s => s.date.ToString("yyyy-MM-dd")).ToList();

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
