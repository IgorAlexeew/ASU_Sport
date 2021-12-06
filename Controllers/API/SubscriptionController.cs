using Microsoft.AspNetCore.Mvc;
using ASUSport.Models;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;

namespace ASUSport.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("get-subscriptions")]
        public IActionResult GetSubscriptions(int objectId)
        {
            var result = subscriptionRepository.GetSubscriptions(objectId);

            return Ok(result);
        }
    }
}
