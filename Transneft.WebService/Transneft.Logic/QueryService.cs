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
            else if (param.VoltTransformatorId.IsNull())
            {
                throw new Exception("Отсутствует параметр VoltTransformatorId!");
            }
            else if (param.CurTransformatorId.IsNull())
            {
                throw new Exception("Отсутствует параметр CurTransformatorId!");
            }
            else if (param.ElectricEnergyMeterId.IsNull())
            {
                throw new Exception("Отсутствует параметр ElectricEnergyMeterId!");
            }
            else if (!_context.CurTransformators.Any(z => z.Id == param.CurTransformatorId))
            {
                throw new Exception($"Не найден трансформатор тока с Id = {param.CurTransformatorId}!");
            }
            else if (_context.CalcEnergyPoints.Any(z => z.CurTransformatorId == param.CurTransformatorId))
            {
                throw new Exception($"Трансформатор тока с Id = {param.CurTransformatorId} уже имеет привязку к точке измерения электроэнергии!");
            }
            else if (!_context.VoltTransformators.Any(z => z.Id == param.VoltTransformatorId))
            {
                throw new Exception($"Не найден трансформатор напряжения с Id = {param.VoltTransformatorId}!");
            }
            else if (_context.CalcEnergyPoints.Any(z => z.VoltTransformatorId == param.VoltTransformatorId))
            {
                throw new Exception($"Трансформатор напряжения с Id = {param.VoltTransformatorId} уже имеет привязку к точке измерения электроэнергии!");
            }
            else if (!_context.ElectricEnergyMeters.Any(z => z.Id == param.ElectricEnergyMeterId))
            {
                throw new Exception($"Не найден счетчик электрической энергии с Id = {param.ElectricEnergyMeterId}!");
            }
            else if (_context.CalcEnergyPoints.Any(z => z.ElectricEnergyMeterId == param.ElectricEnergyMeterId))
            {
                throw new Exception($"Счетчик электрической энергии с Id = {param.ElectricEnergyMeterId} уже имеет привязку к точке измерения электроэнергии!");
            }

            if (param.Id.IsNull())
            {
                param.Id = Guid.NewGuid();
            }

            await _context.CalcEnergyPoints.AddAsync(param);
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
            CheckConsObjId(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var energyPointIds = _context.CalcEnergyPoints
                .Where(z => z.ParentId == id)
                .Select(z => new { z.Id, z.ElectricEnergyMeterId})
                .ToArray();

            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(energyPointIds.Select(x => x.Id).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            energyPointIds = energyPointIds.Where(z => z.Id.In(deadlinedPoints)).ToArray();

            return _context.ElectricEnergyMeters
                .Where(z => z.CheckDate.IsNull() && z.Id.In(energyPointIds.Select(x => x.ElectricEnergyMeterId).ToArray()))
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
            CheckConsObjId(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var energyPointIds = _context.CalcEnergyPoints
                .Where(z => z.ParentId == id)
                .Select(z => new { z.Id, z.VoltTransformatorId })
                .ToArray();

            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(energyPointIds.Select(x => x.Id).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            energyPointIds = energyPointIds.Where(z => z.Id.In(deadlinedPoints)).ToArray();

            return _context.VoltTransformators
                .Where(z => z.CheckDate.IsNull() && z.Id.In(energyPointIds.Select(x => x.VoltTransformatorId).ToArray()))
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
            CheckConsObjId(id);

            // Выбрать точки измерения электроэнергии и счетчики для данного объекта потребления
            var energyPointIds = _context.CalcEnergyPoints
                .Where(z => z.ParentId == id)
                .Select(z => new { z.Id, z.CurTransformatorId })
                .ToArray();

            var deadlinedPoints = _context.CalcPointAndDevices
                .Where(z => z.PointGuid.In(energyPointIds.Select(x => x.Id).ToArray()))
                .GroupBy(z => z.PointGuid)
                .Where(z => z.Max(x => x.DateTo < DateTime.Now))
                .Select(z => z.Key)
                .ToArray();

            energyPointIds = energyPointIds.Where(z => z.Id.In(deadlinedPoints)).ToArray();

            return _context.CurTransformators
                .Where(z => z.CheckDate.IsNull() && z.Id.In(energyPointIds.Select(x => x.CurTransformatorId).ToArray()))
                .ToArray();
        }

        /// <summary>
        /// Получить Id объекта потребления из строки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        private Guid GetConsObjId(string consObjectId)
        {
            if (!Guid.TryParse(consObjectId, out var id))
            {
                throw new Exception("Неверное значение параметра consObjectId");
            }

            return id;
        }

        /// <summary>
        /// Проверить, что объект потребления существует
        /// </summary>
        /// <param name="id">Guid</param>
        private void CheckConsObjId(Guid id)
        {
            if (!_context.ConsObjects.Any(z => z.Id == id))
            {
                throw new Exception($"Не найден объект потребления с Id = {id}");
            }
        }
    }
}
