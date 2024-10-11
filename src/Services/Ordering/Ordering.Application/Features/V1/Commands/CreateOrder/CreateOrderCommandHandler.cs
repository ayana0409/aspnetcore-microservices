using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOrderRepository _repository;
        public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private const string MethodName = "CreateOrderCommandHandler";
        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {request.UserName}");

            Order orderEntity = _mapper.Map<Order>(request);
            await _repository.CreateAsync(orderEntity);
            await _repository.SaveChangeAsync();

            _logger.Information($"END: {MethodName} - Username: {request.UserName}");

            return new ApiSuccessResult<long>(orderEntity.Id, "Success");
        }
    }
}
