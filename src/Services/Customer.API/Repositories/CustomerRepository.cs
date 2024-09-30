using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
    {
        private readonly CustomerContext _dbContext;
        private readonly IUnitOfWork<CustomerContext> _unitOfWork;

        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) 
            : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public Task<Entities.Customer?> GetCustomerByUserNameAsync(string userName) => 
                FindByCondition(x => x.UserName.Equals(userName))
                .SingleOrDefaultAsync();
    }
}
