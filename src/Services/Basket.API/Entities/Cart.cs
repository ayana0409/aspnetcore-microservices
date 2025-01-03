﻿namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = [];
        public Cart()
        { 
        }
        public Cart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);

        public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    }
}
