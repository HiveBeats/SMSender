using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SMSender.Processor.Services;
using SMSender.Shared.Models;
using SMSender.Shared.Configuration;

namespace SMSender.Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configurationBuilder = new ConfigurationBuilder();
                    var config = configurationBuilder.SetBasePath(Environment.CurrentDirectory)
                        .AddEnvironmentVariables("ASPNETCORE_")
                        .Build();
                    
                    var connectionString = config.GetConnectionString("SMSDb");
                    services.AddDbContext<AppDbContext>(options => options
                        .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    );
                    
                    var rabbitConfigurationSection = config.GetSection("Rabbit");
                    var rabbitConfig = rabbitConfigurationSection.Get<RabbitConfig>();
                    //todo: fix consumer
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();
                        x.AddConsumer<ShortMessageCreatedConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(rabbitConfig.Host, "/", h =>
                            {
                                h.Username(rabbitConfig.User);
                                h.Password(rabbitConfig.Password);
                            });
                        });
                    });
                    services.AddTransient<IShortMessageProcessingService, ShortMessageProcessingService>();
                    services.AddHostedService<Worker>();
                });
    }
}
