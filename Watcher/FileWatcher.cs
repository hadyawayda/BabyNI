using Parser.Factory;
using Parser.Parsers;
using System.Text.RegularExpressions;

namespace Parser.Watcher
{
    public class FileWatcher : IHostedService
    {
        private readonly static string  parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";

        private readonly IServiceProvider _serviceProvider;
        private readonly IParserFactory _parserFactory;

        public FileWatcher(IServiceProvider serviceProvider, IParserFactory parserFactory)
        {
            _serviceProvider = serviceProvider;
            _parserFactory = parserFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var myScopedService = _parserFactory.CreateRadioLinkParser("radiolink");
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var myScopedService2 = _parserFactory.CreateRFInputParser("rfinput");
            }

            return Task.CompletedTask;
        }

        private void process(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern))
            {
                RadioLinkParser parser1 = new RadioLinkParser(fileName);
            }

            else if (Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                RFInputParser parser2 = new RFInputParser(fileName);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
