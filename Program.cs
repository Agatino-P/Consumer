using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace GettingStarted
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        private const string QueueName = "MyQueue";
        private const string ExchangeName = "MyExchange";

        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((mtContext, services) =>
                {
                    services.AddMassTransit(config =>
                    {
                        config.AddConsumer<PaymentPaidConsumer>().Endpoint(e =>
                        {
                            e.Name = QueueName;
                            e.ConcurrentMessageLimit = 1;
                        });
                        config.UsingRabbitMq((rmqContext, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("admin");
                                h.Password("admin");
                            });
                            cfg.ReceiveEndpoint(
                                QueueName,
                                rabbitMqReceiveEndpointConfigurator => rabbitMqReceiveEndpointConfigurator.ConfigureConsumer<PaymentPaidConsumer>(rmqContext));
                            //cfg.UseMessageRetry(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(20)));
                            cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30)));
                        });
                    });

                });
    }
}
