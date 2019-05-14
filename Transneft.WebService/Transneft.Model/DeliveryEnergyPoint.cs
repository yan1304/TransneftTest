using System;
using System.ComponentModel.DataAnnotations;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Точка поставки электроэнергии
    /// </summary>
    public class DeliveryEnergyPoint : EnergyPointBase
    {
        /// <summary>
        /// Максимальная мощность, кВт
        /// </summary>
        [Required]
        [Display(Name = "Максимальная мощность, кВт")]
        public double MaxPower { get; set; }

        /// <summary>
        /// Расчетный прибор учета
        /// </summary>
        public CalculatedDevice CalculatedDevice { get; set; }
    }
}
