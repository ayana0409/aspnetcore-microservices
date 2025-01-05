namespace Shared.DTOs.Basket
{
    public class CartDto
    {
        public string UserName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public List<CartItemDto> Items { get; set; } = [];
        public CartDto()
        {
        }
        public CartDto(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
    }
}
