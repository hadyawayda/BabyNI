using Aggregator.Aggregation;
using Aggregator.Connection;

namespace Aggregator.Factory
{
    public class AggregatorFactory : IAggregatorFactory
    {
        private readonly IDbConnection _connection;

        public AggregatorFactory(IDbConnection connection)
        {
            _connection = connection;
        }
        public DbAggregator CreateInstance()
        {
            return new DbAggregator(_connection);
        }
    }
}
