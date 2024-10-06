using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ordering.Application.Common.Behavious
{
    public class PerformanceBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds <= 500) return response;

            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Application long running request: {Name} ({ElapsedMilliseconds} millieseconds) {@Request}",
                requestName, elapsedMilliseconds, request);

            return response;
        }
    }
}
