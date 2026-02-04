using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Decorators
{
    public class LoggingDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingDecorator<TRequest, TResponse>> _logger;
        public LoggingDecorator(ILogger<LoggingDecorator<TRequest, TResponse>> logger) => _logger = logger;

        

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {Request}", typeof(TRequest).Name);
            var response = await next();
            _logger.LogInformation("Handled {Request}", typeof(TRequest).Name);
            return response;
        }
    }
}