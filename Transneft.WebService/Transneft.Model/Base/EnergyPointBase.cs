using System;
using System.ComponentModel.DataAnnotations;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс точки измерения/поставки электроэнергии
    /// </summary>
    public abstract class EnergyPointBase : TransneftObject
    {
        /// <summary>
        /// Объект потребления
        /// </summary>
        public ConsObject ConsObject { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Guid объекта потребления
        /// </summary>
        public Guid ConsObjectId { get; set; }
    }
}
