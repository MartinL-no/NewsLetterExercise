using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewsLetterExercise.API.ViewModel;
using NewsLetterExercise.Core.ApplicationServices;

namespace NewsLetterExercise.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService; 
        }

        [HttpGet("create")]
        public async Task<ContentResult> CreateSubscription([FromQuery] string email, [FromQuery] string name)
        {
            var isCreated = await _subscriptionService.CreateSubscription(name, email);

            var html = isCreated ? "<p>Thanks for subscribing, you should receive an email soon with a link to confirm your subscription</p>" : "<p>There was a issue with your submission please try again</p>";
            
            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }

        [HttpGet("confirm")]
        public async Task<IActionResult>  ConfirmSubscription([FromQuery] string email, [FromQuery] string confirmationCode)
        {
            var isConfirmed = await _subscriptionService.ConfirmSubscription(email, new Guid(confirmationCode));

            var html = isConfirmed ? "<p>Thanks! your subscription is now confirmed</p>" : "<p>The link has expired</p>";
            
            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}