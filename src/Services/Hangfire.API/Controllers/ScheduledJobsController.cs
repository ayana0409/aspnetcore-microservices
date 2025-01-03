using Hangfire.API.Services;
using Hangfire.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.ScheduledJob;

namespace Hangfire.API.Controllers
{
    [Route("api/scheduled-jobs")]
    [ApiController]
    public class ScheduledJobsController : ControllerBase
    {
        private readonly IBackgroundJobService _jobService;
        public ScheduledJobsController(IBackgroundJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route(template: "send-email-reminder-checkout-order")]
        public IActionResult SendReminderCheckoutOrderEmail([FromBody] ReminderCheckoutOrderDto model)
        {
            var jobId = _jobService.SendEmailContent(model.email, model.subject, 
                model.emailContent, model.enqueueAt);

            return Ok(jobId);
        }
    }
}
