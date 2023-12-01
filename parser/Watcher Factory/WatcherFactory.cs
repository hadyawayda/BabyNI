using Parser.Watcher;

namespace Parser.Watcher_Factory
{
    public class WatcherFactory : IWatcherFactory
    {
        public BaseWatcher CreateWatcher (string directory, Action<string> followUpMethod)
        {
            return new BaseWatcher (directory, followUpMethod);
        }
    }
}
