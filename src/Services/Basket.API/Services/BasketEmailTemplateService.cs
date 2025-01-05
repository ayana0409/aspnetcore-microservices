using Basket.API.Services.Interfaces;
using Shared.Configurations;

namespace Basket.API.Services
{
    public class BasketEmailTemplateService : EmailTemplateService, IEmailTemplateService
    {
        public BasketEmailTemplateService(BackgroundJobSettings backgroundJobSettings) : base(backgroundJobSettings)
        {
        }

        public string GenerateReminderCheckoutOrderEmail(string username)
        {
            var _checkoutUrl = $"{BackgroundJobSettings.ApiGwUrl}/{BackgroundJobSettings.CheckoutUrl}/{username}";
            var emailText = ReadEmailTemplateContent("reminder-checkout-order");
            var emailReplaceText = emailText.Replace("[username]", username)
                .Replace("[checkoutUrl]", _checkoutUrl);

            return emailReplaceText;

        }
    }
}
