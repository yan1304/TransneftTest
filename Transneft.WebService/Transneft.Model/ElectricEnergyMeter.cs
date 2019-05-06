using System;
using System.ComponentModel.DataAnnotations;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Счетчик электрической энергии
    /// </summary>
    public class ElectricEnergyMeter : TransneftObject, IHierarchy
    {
        /// <summary>
        /// Номер
        /// </summary>
        [Required]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        /// <summary>
        /// Дата проверки
        /// </summary>
        [Display(Name = "Дата проверки")]
        [DataType(DataType.Date)]
        public DateTime? CheckDate { get; set; }

        /// <summary>
        /// Тип счетчика
        /// </summary>
        [Required]
        [Display(Name = "Тип счетчика")]
        public string Type { get; set; }

        /// <summary>
        /// Id точки измерения электроэнергии
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии
        /// </summary>
        public CalcEnergyPoint CalcEnergyPoint { get; set; }
    }
}
