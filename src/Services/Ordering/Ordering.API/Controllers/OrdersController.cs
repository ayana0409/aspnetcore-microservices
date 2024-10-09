using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders;
using Shared.Services.Email;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
        }

        [HttpGet("{userName}", Name = RouteNames.GetOrders)]
        [ProducesDefaultResponseType(typeof(IEnumerable<OrderDto>))]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderByUserName([Required] string userName)
        {
            var query = new GetOrdersQuery(userName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}
