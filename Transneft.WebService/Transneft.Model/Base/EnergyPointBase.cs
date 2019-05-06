using System;
using System.ComponentModel.DataAnnotations;
using Transneft.Model.Interfaces;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс точки измерения/поставки электроэнергии
    /// </summary>
    public abstract class EnergyPointBase : TransneftObject, IHierarchy
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
        public Guid ParentId { get; set; }
    }
}
