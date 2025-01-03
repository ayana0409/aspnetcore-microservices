using Basket.API.Services.Interfaces;
using Shared.Configurations;

namespace Basket.API.Services
{
    public class BasketEmailTemplateService : EmailTemplateService, IEmailTemplateService
    {
        public BasketEmailTemplateService(BackgroundJobSettings backgroundJobSettings) : base(backgroundJobSettings)
        {
        }

        public string GenerateReminderCheckoutOrderEmail(string username, string checkouUrl = "baskets")
        {
            var _checkoutUrl = $"{BackgroundJobSettings.ApiGwUrl}/{checkouUrl}/{username}";
            var emailText = ReadEmailTemplateContent("reminder-checkout-order");
            var emailReplaceText = emailText.Replace("[username]", username)
                .Replace("[checkoutUrl]", _checkoutUrl);

            return emailReplaceText;

        }
    }
}
