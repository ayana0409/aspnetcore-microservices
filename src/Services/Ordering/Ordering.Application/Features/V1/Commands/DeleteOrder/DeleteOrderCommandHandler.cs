using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Application.Features.V1.Orders
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _repository;
        public DeleteOrderCommandHandler(IOrderRepository repository, ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private const string MethodName = "DeleteOrderCommandHandler";
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - OrderId: {request.Id}");

            Order orderEntity = await _repository.GetByIdAsync(request.Id)
                                    ?? throw new NotFoundException($"Not found order {request.Id}.");
            await _repository.DeleteAsync(orderEntity);
            await _repository.SaveChangeAsync();

            _logger.Information($"END: {MethodName} - OrderId: {request.Id}");

        }
    }
}
