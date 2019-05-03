using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Transneft.Logic.Contexts;

namespace Transneft.WebService
{
    /// <summary>
    /// Основной класс приложения
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">Аргументы</param>
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                      .AddJsonFile("settings.json", true)
                                                      .Build();
            TransneftDbContext.ConnectionString = Configuration["ConnectionString"];
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Создание WebHostBuilder
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, int.Parse(Configuration["ServicePort"]));
                });
    }
}
