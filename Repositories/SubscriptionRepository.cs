using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ASUSport.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private ApplicationContext db;

        public SubscriptionRepository(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Получить список всех абонементов для этого объекта
        /// </summary>
        /// <param name="objectId">Название спортивного объекта</param>
        /// <returns>Список абонементов</returns>
        public List<SubscriptionDTO> GetSubscriptions(int objectId)
        {
            var subscriptions = db.Subscriptions.Where(s => s.SportObject.Id == objectId).ToList();

            if (!subscriptions.Any())
            {
                return null;
            }

            var result = new List<SubscriptionDTO>();

            foreach (var s in subscriptions)
            {
                var sub = new SubscriptionDTO()
                {
                    SportObjectName = s.SportObject.Name,
                    Type = s.Type,
                    Name = s.Name,
                    NumOfVisits = s.NumOfVisits,
                    Price = s.Price,
                    StartingTime = s.StartingTime,
                    ClosingTime = s.ClosingTime
                };

                result.Add(sub);
            }

            return result;
        }
    }
}
