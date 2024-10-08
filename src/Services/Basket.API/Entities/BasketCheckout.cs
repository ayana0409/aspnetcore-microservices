﻿namespace Basket.API.Entities
{
    public class BasketCheckout
    {

        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
