using Parser.Factory;
using Parser.Watcher_Factory;
using System.Text.RegularExpressions;
using Watcher.Services;

namespace Parser.Watcher
{
    public class FileWatcher : IHostedService
    {
        private readonly static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private HashSet<string>         logs;
        private bool                    isProcessable;
        private string                  loaderRoute;

        private readonly HttpService        _service;
        private readonly IServiceProvider   _serviceProvider;
        private readonly IParserFactory     _parserFactory;
        private readonly IWatcherFactory    _watcherFactory;
        private readonly IConfiguration     _configuration;

        public FileWatcher(HttpService service , IParserFactory parserFactory, IWatcherFactory watcherFactory, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            logs = new();
            _service = service;
            _parserFactory = parserFactory;
            _watcherFactory = watcherFactory;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            loaderRoute = _configuration["Routes:Loader"]!;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (_serviceProvider.CreateScope())
            {
                _watcherFactory.CreateWatcher(rootDirectory, process);
            }

            return Task.CompletedTask;
        }

        private void process(string fileName)
        {
            isProcessable = false;

            isProcessable = isFileProcessable(fileName);

            if (isProcessable)
            {
                startParser(fileName);

                _service.SendMessage(loaderRoute, fileName);
            }
        }

        private bool isFileProcessable(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern) || Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                if (!logs.Contains(fileName))
                {
                    logs.Add(fileName);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Sorry, file {fileName} has been processed already, skipping...");
                    File.Delete(Path.Combine(rootDirectory, fileName));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void startParser(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern))
            {
                using (_serviceProvider.CreateScope())
                {
                    _parserFactory.CreateRadioLinkParser(fileName);
                }
            }

            else if (Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                using (_serviceProvider.CreateScope())
                {
                    _parserFactory.CreateRFInputParser(fileName);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
