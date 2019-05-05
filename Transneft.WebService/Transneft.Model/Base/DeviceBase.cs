using System;

namespace Transneft.Model.Base
{
    public abstract class DeviceBase : TransneftObject
    {
        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата проверки
        /// </summary>
        public DateTime? CheckDate { get; set; } 
    }
}
