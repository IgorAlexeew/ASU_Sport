using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.ViewModels;
using ASUSport.Repositories.Impl;

namespace ASUSport.Repositories
{
    public class SportObjectRepository : ISportObjectRepository
    {
        private ApplicationContext db;

        public SportObjectRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <inheritdoc/>
        public List<SportObjectInfo> GetInfo()
        {
            var objects = db.SportObjects.Take(3).Select(s => s.Name).ToList();

            DateTime today = DateTime.Now;

            if (today.DayOfWeek == DayOfWeek.Sunday)
                today.AddDays(1);

            DateTime nearestSaturday = GetNearestDay(today, DayOfWeek.Saturday);

            var result = new List<SportObjectInfo>();

            foreach (var obj in objects)
            {
                var events = db.Events.Where(e => e.Section.SportObject.Name == obj
                    && e.Time > today && e.Time < nearestSaturday).ToList();

                var info = new SportObjectInfo() { ObjectName = obj };

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
