using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services;
using Basket.API.Services.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Shared.DTOs.ScheduledJob;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly BackgroundJobHttpService _backgroundJobHttpService;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, 
            ILogger logger, IEmailTemplateService emailTemplateService, BackgroundJobHttpService backgroundJobHttpService)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
            _emailTemplateService = emailTemplateService;
            _backgroundJobHttpService = backgroundJobHttpService;
        }
        public async Task<Cart?> GetBasketByUserName(string username)
        {
            _logger.Information($"BEGIN: GetBasketByUserName {username}");
            var basket = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"END: GetBasketByUserName {username}");

            return string.IsNullOrEmpty(basket) ? null : 
                _serializeService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions? options = null)
        {
            _logger.Information($"BEGIN: UpdateBasket {cart.UserName}");

            if (options != null)
                await _redisCacheService.SetStringAsync(cart.UserName, 
                    _serializeService.Serialize(cart), options);
            else
                await _redisCacheService.SetStringAsync(cart.UserName,
                    _serializeService.Serialize(cart));

            _logger.Information($"END: UpdateBasket {cart.UserName}");

            try
            {
                await TriggerSendEmailReminderCheckout(cart);
            }
            catch (Exception ex) 
            {
                _logger.Error(ex.Message);
            }

            return await GetBasketByUserName(cart.UserName);
        }

        private async Task TriggerSendEmailReminderCheckout(Cart cart)
        {
            var emailTemplate = _emailTemplateService.GenerateReminderCheckoutOrderEmail(cart.UserName);

            var model = new ReminderCheckoutOrderDto(cart.EmailAddress, "Reminder checkout", emailTemplate,
                DateTimeOffset.Now.AddSeconds(3));

            const string uri = "api/scheduled-jobs/send-email-reminder-checkout-order";
            var response = await _backgroundJobHttpService.Client.PostAtJson(uri, model);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                var jobId = await response.ReadContentAs<string>();
                if (!string.IsNullOrEmpty(jobId))
                {
                    cart.JobId = jobId;
                    await _redisCacheService.SetStringAsync(cart.UserName, 
                        _serializeService.Serialize(cart));
                }
            }

        }

        public async Task<bool> DeleteBasketFromUserName(string username)
        {
            try
            {
                _logger.Information($"BEGIN: DeleteBasketFromUserName {username}");
                await _redisCacheService.RemoveAsync(username);
                _logger.Information($"END: DeleteBasketFromUserName {username}");

                return true;
            }
            catch (Exception ex) 
            {
                _logger.Error("DeleteBasketFromUserName: ", ex.Message);
                throw;
            }
        }

    }
}
