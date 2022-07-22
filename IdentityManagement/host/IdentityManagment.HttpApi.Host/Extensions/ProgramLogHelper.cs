using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace IdentityManagment.Extensions
{
    // ReSharper disable All
    public static class ProgramLogHelper
    {
        public static void AddLog(IConfigurationBuilder builder,
            bool enableProductionConsoleLog = true, 
            LogEventLevel productionLogLevel = LogEventLevel.Information)
        {
            Add_Debug_Log();

            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //if (environment == Environments.Development)
            //{
            //    Add_Debug_Log();
            //}
            //else
            //{
            //    Add_Production_Log(builder, enableProductionConsoleLog, productionLogLevel);
            //}
        }

        public static void Add_Debug_Log()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.Console())
                .CreateLogger();
        }

        public static void Add_Production_Log(IConfigurationBuilder builder, bool enableProductionConsoleLog, LogEventLevel productionLogLevel = LogEventLevel.Information)
        {
            // log to elastic
        }

        //private static ElasticsearchSinkOptions _getElasticsearchSinkOptions(ElasticsearchOptionModel opt)
        //{
        //    var indexFormat = $"{opt.IndexName}-{DateTime.UtcNow.ToString(opt.IndexNameTimeFormat)}";

        //    return new ElasticsearchSinkOptions(new Uri(opt.GetConnectionString()))
        //    {
        //        AutoRegisterTemplate = true,
        //        IndexFormat = indexFormat
        //    };
        //}
    }
}