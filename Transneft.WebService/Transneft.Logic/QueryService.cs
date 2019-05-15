using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taransneft.Logic.Interfaces;
using Transneft.Core;
using Transneft.Logic.Contexts;
using Transneft.Model;

namespace Taransneft.Logic
{
    /// <summary>
    /// Класс для работы с контекстом БД 
    /// </summary>
    public class QueryService : IQueryService
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        public TransneftDbContext Context { get; set; }

        /// <summary>
        /// Добавить новую точку измерения с указанием счетчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="json">JSON-данные для добавления</param>
        public void AddCalcEnergyPoint(string json)
        {
            if (json.IsNullOrWhiteSpace())
            {
                throw new Exception("Отсутствует JSON-параметр!");
            }

            var param = json.FromJson<CalcEnergyPoint>();
            if (param.Name.IsNull())
            {
                throw new Exception("Отсутствует параметр Name!");
            }
            else if (Context.CalcEnergyPoints.Any(z => z.Name == param.Name))
            {
                throw new Exception($"Точка измерения электроэнергии с именем {param.Name} уже существует!");
            }
            else if (param.ElectricEnergyMeterId.IsNull())
            {
                throw new Exception("Отсутствует параметр ElectricEnergyMeterId!");
            }
            else if (param.CurTransformatorId.IsNull())
            {
                throw new Exception("Отсутствует параметр CurTransformatorId!");
            }
            else if (param.VoltTransformatorId.IsNull())
            {
                throw new Exception("Отсутствует параметр VoltTransformatorId!");
            }
            else if (param.ConsObjectId.IsNull())
            {
                throw new Exception("Отсутствует параметр ConsObjectId!");
            }

            param.ConsObject = Context.ConsObjects
                .FirstOrDefault(z => z.Id == param.ConsObjectId) ?? throw new Exception("Некорректный параметр ConsObjectId!");
            param.ElectricEnergyMeter = Context.ElectricEnergyMeters
                .FirstOrDefault(z => z.Id == param.ElectricEnergyMeterId) ?? throw new Exception("Некорректный параметр ElectricEnergyMeters!");
            param.CurTransformator = Context.CurTransformators
                .FirstOrDefault(z => z.Id == param.CurTransformatorId) ?? throw new Exception("Некорректный параметр CurTransformatorId!");
            param.VoltTransformator = Context.VoltTransformators
                .FirstOrDefault(z => z.Id == param.VoltTransformatorId) ?? throw new Exception("Некорректный параметр VoltTransformators!");
            Context.CalcEnergyPoints.Add(param);
            Context.SaveChanges();
        }

        /// <summary>
        /// Выбрать все расчетные приборы в year году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Рассчетные приборы учета</returns>
        public IEnumerable<CalculatedDevice> GetCalculatedDevices(int year)
        {
            if (year < 1900 || year > 2050)
            {
                throw new Exception("Указан некорректный год");
            }

            var deviceIds = Context.CalcPointAndDevices
                .Include(z => z.CalculatedDevice)
                .Where(z => z.DateFrom.Year <= year && z.DateTo.Year >= year)
                .Select(z => z.CalculatedDevice.Id)
                .ToArray();

            return Context.CalculatedDevices
                .Where(z => z.Id.In(deviceIds))
                .ToArray();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все счетчики с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Счетчики электрической энергии</returns>
        public IEnumerable<ElectricEnergyMeter> GetDeadlinedEnergyMeters(string consObjectId)
        {
            var id = GetConsObjId(consObjectId);
            var obj = GetConsObject(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var energyMeters = Context.ElectricEnergyMeters
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.IsNotNull() && z.CalcEnergyPoint.Id.In(obj.CalcEnergyPoints.Select(x => x.Id).ToArray()))
                .ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.Id.In(energyMeters.Select(x => x.CalcEnergyPoint.Id).ToArray()))
                .GroupBy(z => z.CalcEnergyPoint.Id)
                .Select(z => new { z.Key, DateTo = z.Max(x => x.DateTo) })
                .ToArray();

            return energyMeters
                .Where(z => z.CalcEnergyPoint.Id.In(deadlinedPoints.Select(x => x.Key).ToArray())
                    && ((z.CheckDate.IsNull() && deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo < DateTime.Now)
                    || z.CheckDate > deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo))
                .Select(z => new ElectricEnergyMeter
                {
                    Id = z.Id,
                    Number = z.Number,
                    CalcEnergyPointId = z.CalcEnergyPoint.Id,
                    CheckDate = z.CheckDate,
                    Type = z.Type
                })
                .ToArray();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы напряжения с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы напряжения</returns>
        public IEnumerable<VoltTransformator> GetDeadlinedVoltTransformators(string consObjectId)
        {
            var id = GetConsObjId(consObjectId);
            var obj = GetConsObject(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var voltTransformators = Context.VoltTransformators
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.IsNotNull() && z.CalcEnergyPoint.Id.In(obj.CalcEnergyPoints.Select(x => x.Id).ToArray()))
                .ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.Id.In(voltTransformators.Select(x => x.CalcEnergyPoint.Id).ToArray()))
                .GroupBy(z => z.CalcEnergyPoint.Id)
                .Select(z => new { z.Key, DateTo = z.Max(x => x.DateTo) })
                .ToArray();

            return voltTransformators
                .Where(z => z.CalcEnergyPoint.Id.In(deadlinedPoints.Select(x => x.Key).ToArray())
                    && ((z.CheckDate.IsNull() && deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo < DateTime.Now)
                    || z.CheckDate > deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo))
                .Select(z => new VoltTransformator
                {
                    Id = z.Id,
                    Number = z.Number,
                    CalcEnergyPointId = z.CalcEnergyPoint.Id,
                    CheckDate = z.CheckDate,
                    Type = z.Type,
                    KT = z.KT
                })
                .ToArray();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы тока с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы тока</returns>
        public IEnumerable<CurTransformator> GetDeadlinedCurTransformators(string consObjectId)
        {
            var id = GetConsObjId(consObjectId);
            var obj = GetConsObject(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var curTransformators = Context.CurTransformators
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.IsNotNull() && z.CalcEnergyPoint.Id.In(obj.CalcEnergyPoints.Select(x => x.Id).ToArray()))
                .ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Include(z => z.CalcEnergyPoint)
                .Where(z => z.CalcEnergyPoint.Id.In(curTransformators.Select(x => x.CalcEnergyPoint.Id).ToArray()))
                .GroupBy(z => z.CalcEnergyPoint.Id)
                .Select(z => new { z.Key, DateTo = z.Max(x => x.DateTo) })
                .ToArray();

            return curTransformators
                .Where(z => z.CalcEnergyPoint.Id.In(deadlinedPoints.Select(x => x.Key).ToArray())
                    && ((z.CheckDate.IsNull() && deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo < DateTime.Now)
                    || z.CheckDate > deadlinedPoints.First(x => x.Key == z.CalcEnergyPoint.Id).DateTo))
                .Select(z => new CurTransformator
                {
                    Id = z.Id,
                    Number = z.Number,
                    CalcEnergyPointId = z.CalcEnergyPoint.Id,
                    CheckDate = z.CheckDate,
                    Type = z.Type,
                    KT = z.KT
                })
                .ToArray();
        }

        /// <summary>
        /// Получить все не используемые счетчики электроэнергии (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledElectricEnergyMeters()
            => Context.ElectricEnergyMeters
            .Include(z => z.CalcEnergyPoint)
            .Where(z => z.CalcEnergyPoint.IsNull())
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Number })
            .ToArray();

        /// <summary>
        /// Получить все не используемые трансформаторы тока (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledCurTransformators()
            => Context.CurTransformators
            .Include(z => z.CalcEnergyPoint)
            .Where(z => z.CalcEnergyPoint.IsNull())
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Number })
            .ToArray();

        /// <summary>
        /// Получить все не используемые трансформаторы напряжения (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledVoltTransformators()
            => Context.VoltTransformators
            .Include(z => z.CalcEnergyPoint)
            .Where(z => z.CalcEnergyPoint.IsNull())
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Number })
            .ToArray();

        /// <summary>
        /// Получить все объекты потребления (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetAllConsObjects() => Context.ConsObjects
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Name })
            .ToArray();

        /// <summary>
        /// Получить Id объекта потребления из строки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        private Guid GetConsObjId(string consObjectId)
            => !Guid.TryParse(consObjectId, out var id) 
                ? throw new Exception("Неверное значение параметра consObjectId") 
                : id;

        /// <summary>
        /// Получить объект потребления по Id
        /// </summary>
        /// <param name="id">Guid</param>
        private ConsObject GetConsObject(Guid id)
            => Context.ConsObjects
            .Include(z => z.CalcEnergyPoints)
            .FirstOrDefault(z => z.Id == id)
                ?? throw new Exception($"Не найден объект потребления с Id = {id}");
    }
}
