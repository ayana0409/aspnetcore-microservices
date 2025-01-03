namespace Basket.API.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        string GenerateReminderCheckoutOrderEmail(string username, string checkouUrl = "baskets");
    }
}
