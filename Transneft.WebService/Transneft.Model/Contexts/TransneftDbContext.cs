using Microsoft.EntityFrameworkCore;
using Transneft.Core;

namespace Transneft.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class TransneftDbContext : DbContext
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public static string ConnectionString { get; private set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        public TransneftDbContext(string connectionString = null)
        {
            if (ConnectionString.IsNull())
            {
                ConnectionString = connectionString;
            }
        }

        /// <summary>
        /// Выполняется при создании модели в БД
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        /// <summary>
        /// Настройка подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConnectionString);
    }
}
