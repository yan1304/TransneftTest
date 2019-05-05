using Microsoft.EntityFrameworkCore;
using Transneft.Core;
using Transneft.Model;

namespace Transneft.Logic.Contexts
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class TransneftDbContext : DbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TransneftDbContext() => Database.EnsureCreated();

        /// <summary>
        /// Строка подключения
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Организации
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// Дочернии организации
        /// </summary>
        public DbSet<ChildOrganization> ChildOrganizations { get; set; }

        /// <summary>
        /// Объекты потребления
        /// </summary>
        public DbSet<ConsObject> ConsObjects { get; set; }

        /// <summary>
        /// Точки измерения электроэнергии
        /// </summary>
        public DbSet<CalcEnergyPoint> CalcEnergyPoints { get; set; }

        /// <summary>
        /// Счетчики электрической энергии
        /// </summary>
        public DbSet<ElectricEnergyMeter> ElectricEnergyMeters { get; set; }

        /// <summary>
        /// Точки поставки электроэнергии
        /// </summary>
        public DbSet<DeliveryEnergyPoint> DeliveryEnergyPoints { get; set; }

        /// <summary>
        /// Трансформаторы тока
        /// </summary>
        public DbSet<CurTransformator> CurTransformators { get; set; }

        /// <summary>
        /// Трансформаторы напряжения
        /// </summary>
        public DbSet<VoltTransformator> VoltTransformators { get; set; }

        /// <summary>
        /// Расчётные приборы учета
        /// </summary>
        public DbSet<CalculatedDevice> CalculatedDevices { get; set; }

        /// <summary>
        /// Связи между CalculatedDevice и CalcEnergyPoint
        /// </summary>
        public DbSet<CalcPointAndDeviceConnection> CalcPointAndDevices { get; set; }

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
            //Тест
            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Name = "First",
                    Adress = "Moscow"
                });
        }

        /// <summary>
        /// Настройка подключения к базе данных
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConnectionString);
    }
}
