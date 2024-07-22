using Klabin.Rml.Client.Resources;
using Klabin.Rml.ClientLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Klabin.Rml.Client
{
    static class Program
    {
        public static IConfiguration Configuration;
        public static IServiceProvider ServiceProvider;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogLevel appLogLevel;
            try
            {
                //To register all default providers:
                var builder = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                var loggerFactory = NLog.LogManager.LoadConfiguration("nlog.config");

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(new NLogProviderOptions() { CaptureMessageProperties = true, ParseMessageTemplates = true });
                });

                Configuration = builder.Build();

                appLogLevel = GetAppLogLevel(loggerFactory);

                serviceCollection.AddSingleton(Configuration);
                ServiceProvider = serviceCollection.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o arquivo de configuração. Erro: {ex.Message}");
                return;
            }

            //if (ProcessAlreadyRunning())
            //{
            //    MessageBox.Show("O sistema Klabin.Rml.Client já esta rodando em outro processo nesta máquina!", "Sistema já iniciado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            CultureInfo.CurrentUICulture = new CultureInfo(Configuration.GetSection("cultureInfo").Value);
            AddResourceLabels();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(appLogLevel));
        }

        private static void AddResourceLabels()
        {
            ResourceHelper.AddResource(Labels_ReelLength.ResourceManager, MachineType.ReelLength.ToString());
            ResourceHelper.AddResource(Labels_ReelWeigth.ResourceManager, MachineType.ReelWeigth.ToString());
            ResourceHelper.AddResource(Labels_ReelWeigth.ResourceManager, MachineType.ReelWeightRinnert.ToString());
            ResourceHelper.AddResource(Labels_ReelWeigth.ResourceManager, MachineType.ReelWeightToledo.ToString());
            ResourceHelper.AddResource(Labels_ReelWeigth.ResourceManager, MachineType.ReelWeigthP08.ToString());
        }

        private static LogLevel GetAppLogLevel(NLog.LogFactory loggerFactory)
        {
            try
            {
                var logLevels = loggerFactory.Configuration.LoggingRules.First().Levels;
                var minLevel = logLevels.OrderBy(l => l.Ordinal).FirstOrDefault();
                if (minLevel == null)
                {
                    return LogLevel.Information;
                }

                return Enum.Parse<LogLevel>(minLevel.Ordinal.ToString());

            }
            catch (Exception)
            {
                return LogLevel.Information;
            }
        }

        private static bool ProcessAlreadyRunning()
        {
            var processes = Process.GetProcesses();
            foreach (var p in processes)
            {
                if (p.ProcessName.ToLower() == "Klabin.Rml.Client".ToLower() && p.Id != Process.GetCurrentProcess().Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
