using Microsoft.AspNetCore.Mvc;
using ASUSport.Repositories.Impl;
using ASUSport.DTO;
using System.Collections.Generic;

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
        public IActionResult GetSubscriptions(int? objectId)
        {
            var result = subscriptionRepository.GetSubscriptions(objectId);

            return Ok(result);
        }

        [HttpPost("add-subscription")]
        public IActionResult AddSubscription([FromBody] SubscriptionDTO data)
        {
            var result = subscriptionRepository.AddSubscription(data);

            return Ok(result);
        }

        [HttpPut("update-subscription")]
        public IActionResult UpdateSubscription([FromBody] UpdateSubscriptionDTO data)
        {
            var result = subscriptionRepository.UpdateSubscription(data);

            return Ok(result);
        }

        [HttpDelete("delete-subscriptions")]
        public IActionResult DeleteSubscription(int id)
        {
            var result = subscriptionRepository.DeleteSubscription(id);

            return Ok(result);
        }

        [HttpPost("update-subscriptions")]
        public IActionResult UpdateSubscriptions([FromBody] List<UpdateSubscriptionDTO> data)
        {
            var result = subscriptionRepository.UpdateSubscriptions(data);

            return Ok(result);
        }

        [HttpGet("get-number-of-entities")]
        public IActionResult GetNumberOfEntities()
        {
            var result = subscriptionRepository.GetNumberOfEntities();

            return Ok(result);
        }
    }
}
