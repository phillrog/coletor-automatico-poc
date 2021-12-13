using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace ColetaAutomatica.API.Configuration
{
    public static class ElasticConfiguration
    {
		public static void ConfigureLogging(this IServiceCollection services, IConfigurationRoot configuration)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithMachineName()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
		{
			return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
			};
		}
	}
}
