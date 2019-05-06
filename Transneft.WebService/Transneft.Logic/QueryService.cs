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
        private readonly TransneftDbContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ctx">Контекст БД</param>
        public QueryService(TransneftDbContext ctx)
        {
            _context = ctx;
        }

        /// <summary>
        /// Добавить новую точку измерения с указанием счетчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="json">JSON-данные для добавления</param>
        public async Task AddCalcEnergyPoint(string json)
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
            else if (_context.CalcEnergyPoints.Any(z => z.Name == param.Name))
            {
                throw new Exception($"Точка измерения электроэнергии с именем {param.Name} уже существует!");
            }
            else if (param.VoltTransformator.IsNull())
            {
                throw new Exception("Отсутствует параметр VoltTransformator!");
            }
            else if (param.CurTransformator.IsNull())
            {
                throw new Exception("Отсутствует параметр CurTransformator!");
            }
            else if (param.ElectricEnergyMeter.IsNull())
            {
                throw new Exception("Отсутствует параметр ElectricEnergyMeter!");
            }
            
            await _context.CalcEnergyPoints.AddAsync(param);
            await _context.SaveChangesAsync();
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

            var deviceIds = _context.CalcPointAndDevices
                .Where(z => z.DateFrom.Year <= year && z.DateTo.Year >= year)
                .Select(z => z.DeviceGuid)
                .ToArray();

            return _context.CalculatedDevices
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
            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(energyMeters.Select(x => x.ParentId).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return energyMeters.Where(z => z.ParentId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
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
            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(voltTransformators.Select(x => x.ParentId).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return voltTransformators.Where(z => z.ParentId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
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
            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(curTransformators.Select(x => x.ParentId).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            return curTransformators.Where(z => z.ParentId.In(deadlinedPoints) && z.CheckDate.IsNull()).ToArray();
        }

        /// <summary>
        /// Получить все дочерние организации
        /// </summary>
        /// <returns>Дочерние организации</returns>
        public IEnumerable<ChildOrganization> GetAllChildOrganizations() => _context.ChildOrganizations.ToArray();

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
            => _context.ConsObjects.FirstOrDefault(z => z.Id == id)
                ?? throw new Exception($"Не найден объект потребления с Id = {id}");
    }
}
