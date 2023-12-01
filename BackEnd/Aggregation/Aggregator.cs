using BabyNI.Helpers;

namespace BabyNI.Aggregation
{
    internal class Aggregator
    {
        private readonly string aggregatorScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone\Aggregator\Aggregate Tables.sql";
        private QueryFetcher fetcher;

        public Aggregator()
        {
            fetcher = new QueryFetcher(aggregatorScript, true);
        }
    }
}
