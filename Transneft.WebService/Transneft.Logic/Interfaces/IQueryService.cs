using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transneft.Logic.Contexts;
using Transneft.Model;

namespace Taransneft.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с запросами к БД
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        TransneftDbContext Context { get; set; }

        /// <summary>
        /// Добавить новую точку измерения с указанием счетчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="json">JSON-данные для добавления</param>
        /// <param name="energyMeterId">Id счетчика электрической энергии</param>
        /// <param name="curTrId">Id трансформатора тока</param>
        /// <param name="voltTrId">Id трансформатора напряжения</param>
        Task AddCalcEnergyPoint(string json, string energyMeterId, string curTrId, string voltTrId);

        /// <summary>
        /// Выбрать все расчетные приборы в year году
        /// </summary>
        /// <param name="year">Год</param>
        /// <returns>Расчетные приборы учета</returns>
        IEnumerable<CalculatedDevice> GetCalculatedDevices(int year);

        /// <summary>
        /// По указанному объекту потребления выбрать все счетчики с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Счетчики электрической энергии</returns>
        IEnumerable<ElectricEnergyMeter> GetDeadlinedEnergyMeters(string consObjectId);

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы напряжения с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы напряжения</returns>
        IEnumerable<VoltTransformator> GetDeadlinedVoltTransformators(string consObjectId);

        /// <summary>
        /// По указанному объекту потребления выбрать все трансформаторы тока с закончишившимся сроком проверки
        /// </summary>
        /// <param name="consObjectId">Id объекта потребления</param>
        /// <returns>Трансформаторы тока</returns>
        IEnumerable<CurTransformator> GetDeadlinedCurTransformators(string consObjectId);

        /// <summary>
        /// Получить все дочерние организации (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        IEnumerable<ItemInfo> GetAllChildOrganizations();

        /// <summary>
        /// Получить все не используемые счетчики электроэнергии (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        IEnumerable<ItemInfo> GetDisabledElectricEnergyMeters();

        /// <summary>
        /// Получить все не используемые трансформаторы тока (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        IEnumerable<ItemInfo> GetDisabledCurTransformators();

        /// <summary>
        /// Получить все не используемые трансформаторы напряжения (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        IEnumerable<ItemInfo> GetDisabledVoltTransformators();

        /// <summary>
        /// Получить все объекты потребления (id и имя)
        /// </summary>
        /// <returns>Id и имя</returns>
        IEnumerable<ItemInfo> GetAllConsObjects();
    }
}
