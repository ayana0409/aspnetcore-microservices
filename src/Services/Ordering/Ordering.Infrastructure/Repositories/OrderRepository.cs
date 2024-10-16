using Ordering.Domain.Entities;
using Ordering.Domain.Persistence;
using Ordering.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Common;
using Contracts.Common.Interfaces;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order, long, OrderContext>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext, IUnitOfWork<OrderContext> unitOfWork) :
            base(orderContext, unitOfWork)
        { 
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await CreateAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName) => 
            await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await UpdateAsync(order);
            return order;
        }
    }
}
