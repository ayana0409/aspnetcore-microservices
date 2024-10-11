using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOrderRepository _repository;
        public GetOrderQueryHandler(IOrderRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private const string MethodName = "GetOrderQueryHandler";
        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {request.UserName}");

            var orderEntities = await _repository.GetOrderByUserName(request.UserName);
            var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

            _logger.Information($"END: {MethodName} - Username: {request.UserName}");

            return new ApiSuccessResult<List<OrderDto>>(orderList, "Success");
        }
    }
}
