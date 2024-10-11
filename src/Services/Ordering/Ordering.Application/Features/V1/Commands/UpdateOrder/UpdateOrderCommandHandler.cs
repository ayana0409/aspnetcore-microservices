using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOrderRepository _repository;

        public UpdateOrderCommandHandler(ILogger logger, IOrderRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        private const string MethodName = "UpdateOrderCommandHandler";
        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {request.UserName}");

            Order orderEntity = _mapper.Map<Order>(request);
            await _repository.UpdateAsync(orderEntity);
            await _repository.SaveChangeAsync();

            _logger.Information($"END: {MethodName} - Username: {request.UserName}");

            return new ApiSuccessResult<OrderDto>(_mapper.Map<OrderDto>(orderEntity), "Success");
        }
    }
}
