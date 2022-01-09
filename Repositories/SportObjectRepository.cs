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
        public Response AddSportObject(SportObjectDTO data)
        {
            var sportObject = new SportObject()
            {
                Name = data.Name,
                Capacity = data.Capacity,
                StartingTime = data.StartingTime,
                ClosingTime = data.ClosingTime,
                Location = data.Location
            };

            db.SportObjects.Add(sportObject);
            //db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
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
        public List<SportObject> GetSportObjectIds()
        {
            var objects = db.SportObjects.Select(s => s);

            var result = new List<SportObject>();

            foreach (var obj in objects)
            {
                result.Add(obj);
            }

            return result;
        }

        /// <inheritdoc/>
        public Response DeleteSportObject(int id)
        {
            var sportObject = db.SportObjects.FirstOrDefault(s => s.Id == id);

            db.SportObjects.Remove(sportObject);
            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UpdateSportObject(UpdateSportObjectDTO data)
        {
            var sportObject = db.SportObjects.FirstOrDefault(s => s.Id == (int)data.Id);

            if (data.Name != null)
                sportObject.Name = data.Name;

            if (data.Location != null)
                sportObject.Location = data.Location;

            if (data.Capacity != null)
                sportObject.Capacity = (int)data.Capacity;

            if (data.StartingTime != null)
                sportObject.StartingTime = data.StartingTime;

            if (data.ClosingTime != null)
                sportObject.ClosingTime = data.ClosingTime;

            db.SportObjects.Update(sportObject);
            //db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UpdateSportObjects(List<UpdateSportObjectDTO> data)
        {
            foreach (var sportObject in data)
            {
                if (sportObject.Id != null)
                    UpdateSportObject(sportObject);

                else
                {
                    var newSportObject = new SportObjectDTO()
                    {
                        Name = sportObject.Name,
                        Capacity = (int)sportObject.Capacity,
                        StartingTime = sportObject.StartingTime,
                        ClosingTime = sportObject.ClosingTime,
                        Location = sportObject.Location
                    };

                    AddSportObject(newSportObject);
                }
            }

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
            return db.SportObjects.Count();
        }
    }
}
