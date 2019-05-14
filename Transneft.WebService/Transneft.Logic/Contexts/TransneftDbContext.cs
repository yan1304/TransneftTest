using Microsoft.EntityFrameworkCore;
using System;
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
        public TransneftDbContext(DbContextOptions<TransneftDbContext> options) : base(options) => Database.EnsureCreated();

        ///// <summary>
        ///// Конструктор класса
        ///// </summary>
        ///// <param name="connectionString">Строка подключения</param>
        //public TransneftDbContext(string connectionString = null)
        //{
        //    Database.EnsureCreated();
        //}

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
        /// Выполняется при создании модели в БД
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddUniqueKeys(modelBuilder);
            AddRelationships(modelBuilder);

            //Id объектов
            var id = Guid.NewGuid();
            var childId = Guid.NewGuid();
            var consObjId = Guid.NewGuid();
            var delPointId1 = Guid.NewGuid();
            var delPointId2 = Guid.NewGuid();
            var cId1 = Guid.NewGuid();
            var cId2 = Guid.NewGuid();
            var eId1 = Guid.NewGuid();
            var eId2 = Guid.NewGuid();
            var vId1 = Guid.NewGuid();
            var vId2 = Guid.NewGuid();
            var calcDeviceId1 = Guid.NewGuid();
            var calcDeviceId2 = Guid.NewGuid();
            var calcEngPointId1 = Guid.NewGuid();
            var calcEngPointId2 = Guid.NewGuid();

            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = id,
                    Name = "Транснефть",
                    Address = "Москва, Пресненская наб."
                });

            modelBuilder.Entity<ChildOrganization>().HasData(
                new ChildOrganization
                {
                    Id = childId,
                    OrganizationId = id,
                    Name = "Дочерняя организация",
                    Address = "Москва, Цветной бульвар 34"
                });

            modelBuilder.Entity<ConsObject>().HasData(
                new ConsObject
                {
                    Id = consObjId,
                    ChildOrganizationId = childId,
                    Name = "ПС 110/10 Весна",
                    Address = "Москва, Новый Арбат, 19"
                });

            modelBuilder.Entity<DeliveryEnergyPoint>().HasData(
                new DeliveryEnergyPoint()
                {
                    Id = delPointId1,
                    ConsObjectId = consObjId,
                    Name = "Весна 1",
                    MaxPower = 200.0d
                },
                new DeliveryEnergyPoint()
                {
                    Id = delPointId2,
                    ConsObjectId = consObjId,
                    Name = "Весна 2",
                    MaxPower = 1200.0d
                });

            modelBuilder.Entity<CalculatedDevice>().HasData(
                new CalculatedDevice
                {
                    Id = calcDeviceId1,
                    DeliveryEnergyPointId = delPointId1
                },
                new CalculatedDevice
                {
                    Id = calcDeviceId2,
                    DeliveryEnergyPointId = delPointId2
                });

            modelBuilder.Entity<ElectricEnergyMeter>().HasData(
                new ElectricEnergyMeter
                {
                    Id = eId1,
                    Number = "МС1001",
                    Type = "Тестовый тип"
                },
                new ElectricEnergyMeter
                {
                    Id = eId2,
                    Number = "МС1002",
                    Type = "Тестовый тип",
                    CheckDate = new DateTime(2018, 5, 1)
                });

            modelBuilder.Entity<CurTransformator>().HasData(
                new CurTransformator
                {
                    Id = cId1,
                    Number = "МТ1001",
                    Type = "Тестовый тип",
                    KT = 0.89d,
                    CheckDate = new DateTime(2019, 02, 01)
                },
                new CurTransformator
                {
                    Id = cId2,
                    Number = "МТ1002",
                    Type = "Тестовый тип",
                    KT = 0.89d
                });

            modelBuilder.Entity<VoltTransformator>().HasData(
                new VoltTransformator
                {
                    Id = vId1,
                    Number = "МН1001",
                    Type = "Тестовый тип",
                    KT = 0.77d,
                    CheckDate = new DateTime(2019, 09, 01)
                },
                new VoltTransformator
                {
                    Id = vId2,
                    Number = "МН1002",
                    Type = "Тестовый тип",
                    KT = 0.04d,
                    CheckDate = new DateTime(2018, 1, 1)
                });

            modelBuilder.Entity<CalcEnergyPoint>().HasData(
                new CalcEnergyPoint
                {
                    Id = calcEngPointId1,
                    ConsObjectId = consObjId,
                    ElectricEnergyMeterId = eId1,
                    CurTransformatorId = cId1,
                    VoltTransformatorId = vId1,
                    Name = "Точка измерения Москва 1.0"

                },
                new CalcEnergyPoint
                {
                    Id = calcEngPointId2,
                    ConsObjectId = consObjId,
                    ElectricEnergyMeterId = eId2,
                    CurTransformatorId = cId2,
                    VoltTransformatorId = vId2,
                    Name = "Точка измерения Москва 2.0"
                });

            modelBuilder.Entity<CalcPointAndDeviceConnection>()
                .HasData(
                new CalcPointAndDeviceConnection
                {
                    CalculatedDeviceId = calcDeviceId1,
                    CalcEnergyPointId = calcEngPointId1,
                    DateFrom = new DateTime(2018, 1, 9),
                    DateTo = new DateTime(2019, 2, 2)
                },
                new CalcPointAndDeviceConnection
                {
                    CalculatedDeviceId = calcDeviceId2,
                    CalcEnergyPointId = calcEngPointId2,
                    DateFrom = new DateTime(2018, 1, 9),
                    DateTo = new DateTime(2019, 2, 2)
                });
        }

        ///// <summary>
        ///// Настройка подключения к базе данных
        ///// </summary>
        ///// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConnectionString);

        /// <summary>
        /// Добавить в модель связи
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        private void AddRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CalcEnergyPoint>()
                .HasOne(z => z.ConsObject)
                .WithMany(z => z.CalcEnergyPoints)
                .HasForeignKey(p => p.ConsObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<CalcEnergyPoint>()
                .HasOne(z => z.CurTransformator)
                .WithOne(z => z.CalcEnergyPoint)
                .HasForeignKey<CalcEnergyPoint>(p => p.CurTransformatorId);

            modelBuilder
                .Entity<CalcEnergyPoint>()
                .HasOne(z => z.VoltTransformator)
                .WithOne(z => z.CalcEnergyPoint)
                .HasForeignKey<CalcEnergyPoint>(p => p.VoltTransformatorId);

            modelBuilder
                .Entity<CalcEnergyPoint>()
                .HasOne(z => z.ElectricEnergyMeter)
                .WithOne(z => z.CalcEnergyPoint)
                .HasForeignKey<CalcEnergyPoint>(p => p.ElectricEnergyMeterId);

            modelBuilder.Entity<CalculatedDevice>()
                .HasOne(z => z.DeliveryEnergyPoint)
                .WithOne(z => z.CalculatedDevice)
                .HasForeignKey<CalculatedDevice>(p => p.DeliveryEnergyPointId);

            modelBuilder.Entity<CalcPointAndDeviceConnection>()
                .HasKey(t => new { t.CalcEnergyPointId, t.CalculatedDeviceId, t.DateFrom, t.DateTo });

            modelBuilder.Entity<CalcPointAndDeviceConnection>()
                .HasOne(z => z.CalcEnergyPoint)
                .WithMany(z => z.CalcPointAndDeviceConnections)
                .HasForeignKey(z => z.CalcEnergyPointId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CalcPointAndDeviceConnection>()
                .HasOne(z => z.CalculatedDevice)
                .WithMany(z => z.CalcPointAndDeviceConnections)
                .HasForeignKey(z => z.CalculatedDeviceId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// Сделать поля уникальными
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        private void AddUniqueKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasIndex(z => z.Name)
                .IsUnique();

            modelBuilder.Entity<ChildOrganization>()
                .HasIndex(z => new { z.Name })
                .IsUnique();

            modelBuilder.Entity<ConsObject>()
                .HasIndex(z => new { z.Name })
                .IsUnique();

            modelBuilder.Entity<CalcEnergyPoint>()
                .HasIndex(z => new { z.Name })
                .IsUnique();

            modelBuilder.Entity<DeliveryEnergyPoint>()
                .HasIndex(z => new { z.Name })
                .IsUnique();

            modelBuilder.Entity<ElectricEnergyMeter>()
                .HasIndex(z => new { z.Number })
                .IsUnique();

            modelBuilder.Entity<VoltTransformator>()
                .HasIndex(z => new { z.Number })
                .IsUnique();

            modelBuilder.Entity<CurTransformator>()
                .HasIndex(z => new { z.Number })
                .IsUnique();
        }
    }
}
