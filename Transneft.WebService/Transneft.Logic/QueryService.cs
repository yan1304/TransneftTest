using System;
using System.Collections.Generic;
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
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new Exception("Отсутствует JSON-параметр!");
            }

            var param = json.FromJson<CalcEnergyPoint>();
            if (param.Name.IsNull())
            {
                throw new Exception("Отсутствует параметр Name!");
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
            else if (param.Id.IsNull())
            {
                param.Id = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Выбрать все расчетные приборы в year году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Рассчетные приборы учета</returns>
        public async Task<IEnumerable<CalculatedDevice>> GetCalculatedDevices(int year)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все счетчики с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Счетчики электрической энергии</returns>
        public async Task<IEnumerable<ElectricEnergyMeter>> GetDeadlinedEnergyMeters(Guid consObjectId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы напряжения с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы напряжения</returns>
        public async Task<IEnumerable<VoltTransformator>> GetDeadlinedVoltTransformators(Guid consObjectId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы тока с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы тока</returns>
        public async Task<IEnumerable<CurTransformator>> GetDeadlinedCurTransformators(Guid consObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
