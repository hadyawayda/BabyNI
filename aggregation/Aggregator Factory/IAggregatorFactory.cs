using Aggregator.Aggregation;

namespace Aggregator.Factory
{
    public interface IAggregatorFactory
    {
        DbAggregator CreateInstance();
    }
}