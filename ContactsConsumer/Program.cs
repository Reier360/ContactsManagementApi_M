using ContactsConsumer.EventProcessor;
using ContactsConsumer.Interfaces;
using DataAccess.Interfaces;
using DataAccess.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsConsumer
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
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddHostedService<Worker>();
                    services.AddSingleton<ICustomerDBContext>(new ContactsDBContext(configuration["ConnectionStrings:PostgreSQL"]));
                    services.AddSingleton<IEventProcessor, EventProcessor.EventProcessor>();
                });
    }
}
