using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Transneft.WebApplication
{
    /// <summary>
    /// Главное окно приложения
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args">Аргументы</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Настройка хоста
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>Хост</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
