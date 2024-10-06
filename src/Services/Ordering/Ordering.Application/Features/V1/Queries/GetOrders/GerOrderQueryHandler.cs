using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class GerOrderQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        public GerOrderQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderEntities = await _repository.GetOrderByUserName(request.UserName);
            var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

            return new ApiSuccessResult<List<OrderDto>>(orderList);
        }
    }
}
