using Parser.Watcher;

namespace Parser.Watcher_Factory
{
    public interface IWatcherFactory
    {
        BaseWatcher CreateWatcher (string directory, Action<string> followUpMethod);
    }
}
