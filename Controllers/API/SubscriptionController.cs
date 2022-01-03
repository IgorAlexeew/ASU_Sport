using Microsoft.AspNetCore.Mvc;
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
    }
}
