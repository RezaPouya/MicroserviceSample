using Serilog;
using Serilog.Events;

namespace AuthServer.Host
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == Environments.Development)
            {
                Add_Debug_Log();
            }
            else
            {
                //Add_Production_Log(builder);
            }

            try
            {
                Log.Information("Starting web host.");
                await CreateHostBuilder(args).Build().RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void Add_Debug_Log()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                //.WriteTo.Async(c => c.File("Logs/order_service_logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();
        }

        //private static void Add_Production_Log(IConfigurationBuilder builder)
        //{
        //    var configuration = builder.Build();

        //    var elasticSearchOptions = configuration.GetSection("ElasticSearch:Logging").Get<ElasticsearchOptionModel>();

        //    var loggerConfiguration = new LoggerConfiguration()
        //        .MinimumLevel.Information()
        //        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        //        .Enrich.FromLogContext()
        //        .WriteTo.Async(c => c.Console())
        //        .WriteTo.Elasticsearch(_getElasticsearchSinkOptions(elasticSearchOptions));

        //    Log.Logger = loggerConfiguration.CreateLogger();
        //}

        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseAutofac()
                .UseSerilog();
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