using Aggregator.Aggregators;

namespace Aggregator.Factory
{
    public interface IAggregatorFactory
    {
        DbAggregator CreateInstance();
    }
}