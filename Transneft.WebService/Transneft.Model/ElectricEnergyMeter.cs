using System;
using System.ComponentModel.DataAnnotations;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Счетчик электрической энергии
    /// </summary>
    public class ElectricEnergyMeter : DeviceBase
    {
        /// <summary>
        /// Id точки измерения электроэнергии
        /// </summary>
        public Guid CalcEnergyPointId { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии
        /// </summary>
        public CalcEnergyPoint CalcEnergyPoint { get; set; }
    }
}
