using Contracts.ScheduledJobs;
using Contracts.Services;
using Hangfire.API.Services.Interfaces;
using Shared.Services.Email;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Hangfire.API.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IScheduledJobService _scheduledJobService;
        private readonly ISmtpEmailService _smtpEmailService;
        private readonly ILogger _logger;
        public BackgroundJobService(ILogger logger, ISmtpEmailService smtpEmailService, IScheduledJobService scheduledJobService)
        {
            _logger = logger;
            _smtpEmailService = smtpEmailService;
            _scheduledJobService = scheduledJobService;
        }
        public string? SendEmailContent(string email, string subject, string emailContent, DateTimeOffset enqueueAt)
        {
            var emailRequest = new MailRequest
            {
                ToAddress = email,
                Subject = subject,
                Body = emailContent
            };

            try
            {
                var jobId = _scheduledJobService.Schedule(() => _smtpEmailService.SendEmail(emailRequest), enqueueAt);
                _logger.Information($"Sent email to {email} with subject: {subject} - Job Id: {jobId}");
                return jobId;
            }
            catch (Exception e)
            {
                _logger.Error($"Failed due to an error with the email service: {e.Message}");
            }

            return null;
        }
    }
}
