using Customer.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public static class CustomersController
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            app.MapGet("/", () => "Welcome to Customer Minimal API!");

            app.MapGet("/api/customers/{username}",
                async (string username, ICustomerRepository repository) =>
                {
                    var customer = await repository.GetCustomerByUserNameAsync(username);
                    return customer != null ? Results.Ok(customer) : Results.NotFound();
                });
        }
    }
}
