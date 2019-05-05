using System;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Точка измерения электроэнергии
    /// </summary>
    public class CalcEnergyPoint : TransneftObject, IWithName
    {
        /// <summary>
        /// Id объекта потребления
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id трансформатора тока
        /// </summary>
        public Guid CurTransformatorId { get; set; }

        /// <summary>
        /// Id трансформатора напряжения
        /// </summary>
        public Guid VoltTransformatorId { get; set; }

        /// <summary>
        /// Id Счетчика электрической энергии
        /// </summary>
        public Guid ElectricEnergyMeterId { get; set; }
    }
}
