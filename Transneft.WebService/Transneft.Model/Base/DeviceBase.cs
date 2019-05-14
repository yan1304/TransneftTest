using System;
using System.ComponentModel.DataAnnotations;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс устройства
    /// </summary>
    public abstract class DeviceBase :TransneftObject
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

    }
}
