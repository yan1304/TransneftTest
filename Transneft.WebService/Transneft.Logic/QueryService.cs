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
        /// <param name="energyMeterId">Id счетчика электрической энергии</param>
        /// <param name="curTrId">Id трансформатора тока</param>
        /// <param name="voltTrId">Id трансформатора напряжения</param>
        public async Task AddCalcEnergyPoint(string json, string energyMeterId, string curTrId, string voltTrId)
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
            else if (energyMeterId.IsNullOrWhiteSpace())
            {
                throw new Exception("Отсутствует параметр energyMeterId!");
            }
            else if (curTrId.IsNullOrWhiteSpace())
            {
                throw new Exception("Отсутствует параметр curTrId!");
            }
            else if (voltTrId.IsNullOrWhiteSpace())
            {
                throw new Exception("Отсутствует параметр voltTrId!");
            }

            if (!Guid.TryParse(energyMeterId, out var energytMeter))
            {
                throw new Exception("Некорректный параметр energyMeterId!");
            }

            if (!Guid.TryParse(curTrId, out var curTr))
            {
                throw new Exception("Некорректный параметр curTrId!");
            }

            if (!Guid.TryParse(voltTrId, out var voltTr))
            {
                throw new Exception("Некорректный параметр voltTrId!");
            }

            param.ElectricEnergyMeter = Context.ElectricEnergyMeters
                .FirstOrDefault(z => z.Id == energytMeter) ?? throw new Exception("Некорректный параметр energyMeterId!");
            param.CurTransformator = Context.CurTransformators
                .FirstOrDefault(z => z.Id == curTr) ?? throw new Exception("Некорректный параметр curTrId!");
            param.VoltTransformator = Context.VoltTransformators
                .FirstOrDefault(z => z.Id == voltTr) ?? throw new Exception("Некорректный параметр voltTrId!");

            await Context.CalcEnergyPoints.AddAsync(param);
            await Context.SaveChangesAsync();
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
                .Where(z => z.DateFrom.Year <= year && z.DateTo.Year >= year)
                .Select(z => z.CalculatedDeviceId)
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
            var energyMeters = obj.CalcEnergyPoints.Select(z => z.ElectricEnergyMeter).ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Where(z => z.CalcEnergyPointId.In(energyMeters.Select(x => x.CalcEnergyPointId).ToArray()))
                .GroupBy(z => z.CalcEnergyPointId)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return energyMeters.Where(z => z.CalcEnergyPointId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
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
            var voltTransformators = obj.CalcEnergyPoints.Select(z => z.VoltTransformator).ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Where(z => z.CalcEnergyPointId.In(voltTransformators.Select(x => x.CalcEnergyPointId).ToArray()))
                .GroupBy(z => z.CalcEnergyPointId)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return voltTransformators.Where(z => z.CalcEnergyPointId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
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
            var curTransformators = obj.CalcEnergyPoints.Select(z => z.CurTransformator).ToArray();
            var deadlinedPoints = Context.CalcPointAndDevices
                .Where(z => z.CalcEnergyPointId.In(curTransformators.Select(x => x.CalcEnergyPointId).ToArray()))
                .GroupBy(z => z.CalcEnergyPointId)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return curTransformators.Where(z => z.CalcEnergyPointId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
        }

        /// <summary>
        /// Получить все дочерние организации (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetAllChildOrganizations() 
            => Context.ChildOrganizations
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Name })
            .ToArray();

        /// <summary>
        /// Получить все не используемые счетчики электроэнергии (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledElectricEnergyMeters()
            => Context.ElectricEnergyMeters
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Number })
            .ToArray();

        /// <summary>
        /// Получить все не используемые трансформаторы тока (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledCurTransformators()
            => Context.CurTransformators
            .Select(z => new ItemInfo { Id = z.Id.ToString(), Name = z.Number })
            .ToArray();

        /// <summary>
        /// Получить все не используемые трансформаторы напряжения (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        public IEnumerable<ItemInfo> GetDisabledVoltTransformators()
            => Context.VoltTransformators
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
            => Context.ConsObjects.FirstOrDefault(z => z.Id == id)
                ?? throw new Exception($"Не найден объект потребления с Id = {id}");
    }
}
