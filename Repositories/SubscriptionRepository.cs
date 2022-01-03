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

        /// <inheritdoc/>
        public Response AddSubscription(SubscriptionDTO data)
        {
            var newSubscription = new Subscription()
            {
                Type = data.Type,
                Name = data.Name,
                SportObjectId = data.SportObjectId,
                StartingTime = data.StartingTime,
                ClosingTime = data.ClosingTime,
                NumOfVisits = data.NumOfVisits,
                Price = data.Price
            };

            db.Subscriptions.Add(newSubscription);
            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
        public Response UpdateSubscription(UpdateSubscriptionDTO data)
        {
            var subscription = db.Subscriptions.FirstOrDefault(s => s.Id == data.Id);

            if (data.Type != null)
                subscription.Type = data.Type;

            if (data.Name != null)
                subscription.Name = data.Name;

            if (data.NumOfVisits != null)
                subscription.NumOfVisits = (int)data.NumOfVisits;

            if (data.Price != null)
                subscription.Price = (int)data.Price;

            if (data.StartingTime != null)
                subscription.StartingTime = data.StartingTime;

            if (data.ClosingTime != null)
                subscription.ClosingTime = data.ClosingTime;

            db.Subscriptions.Update(subscription);
            db.SaveChanges();

            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Response DeleteSubscription(int id)
        {
            var subscription = db.Subscriptions.FirstOrDefault(s => s.Id == id);

            db.Subscriptions.Remove(subscription);
            db.SaveChanges();
            
            return new Response()
            {
                Status = true,
                Type = "success",
                Message = "OK"
            };
        }
    }
}
