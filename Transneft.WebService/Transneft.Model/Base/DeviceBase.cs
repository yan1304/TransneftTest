using System;
using Transneft.Model.Interfaces;

namespace Transneft.Model.Base
{
    public abstract class DeviceBase : TransneftObject, IHierarchy
    {
        /// <summary>
        /// Guid точки измерения электроэнергии 
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата проверки
        /// </summary>
        public DateTime CheckDate { get; set; } 
    }
}
