using Aggregator.Factory;

namespace Aggregator.Services
{
    public class AggregatorService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAggregatorFactory _aggregatorFactory;

        public AggregatorService(IServiceProvider serviceProvider, IAggregatorFactory aggregatorFactory) 
        {
            _serviceProvider = serviceProvider;
            _aggregatorFactory = aggregatorFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Aggregate!, null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void Aggregate(object state)
        {
            using (_serviceProvider.CreateScope())
            {
                _aggregatorFactory.CreateInstance();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        public void Dispose() => _timer?.Dispose();
    }
}
