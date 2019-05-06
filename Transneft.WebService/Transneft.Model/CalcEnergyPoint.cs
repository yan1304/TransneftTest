using System;
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
        /// Трансформатор напряжения
        /// </summary>
        public VoltTransformator VoltTransformator { get; set; }

        /// <summary>
        /// Счетчик электрической энергии
        /// </summary>
        public ElectricEnergyMeter ElectricEnergyMeter { get; set; }
    }
}
