using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
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
            var orderEntity = await _repository.GetByIdAsync(request.Id)
                                ?? throw new NotFoundException(nameof(Order), request.Id);

            _logger.Information($"BEGIN: {MethodName} - Order: {request.Id}");

            orderEntity = _mapper.Map(request, orderEntity);
            
            var updatedOrder = await _repository.UpdateOrderAsync(orderEntity);
            await _repository.SaveChangeAsync();
            _logger.Information($"Updated Order: {updatedOrder.Id}");
            var result = _mapper.Map<OrderDto>(orderEntity);

            _logger.Information($"END: {MethodName} - Order: {request.Id}");

            return new ApiSuccessResult<OrderDto>(result, "Success");
        }
    }
}
