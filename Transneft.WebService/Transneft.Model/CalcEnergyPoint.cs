using System;
using System.Collections.Generic;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Точка измерения электроэнергии
    /// </summary>
    public class CalcEnergyPoint : EnergyPointBase
    {
        /// <summary>
        /// Трансформатор тока
        /// </summary>
        public CurTransformator CurTransformator { get; set; }

        /// <summary>
        /// Id трансформатора тока
        /// </summary>
        public Guid CurTransformatorId { get; set; }

        /// <summary>
        /// Трансформатор напряжения
        /// </summary>
        public VoltTransformator VoltTransformator { get; set; }

        /// <summary>
        /// Id трансформатора напряжения
        /// </summary>
        public Guid VoltTransformatorId { get; set; }

        /// <summary>
        /// Счетчик электрической энергии
        /// </summary>
        public ElectricEnergyMeter ElectricEnergyMeter { get; set; }

        /// <summary>
        /// Id счетчика электрической энергии
        /// </summary>
        public Guid ElectricEnergyMeterId { get; set; }

        /// <summary>
        /// Связи между CalculatedDevice и CalcEnergyPoint
        /// </summary>
        public List<CalcPointAndDeviceConnection> CalcPointAndDeviceConnections { get; set; }
    }
}
