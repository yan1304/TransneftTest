using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Transneft.Model.Contexts;

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
            TransneftDbContext.ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=TransneftTest;Trusted_Connection=True;";
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Создание WebHostBuilder
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
