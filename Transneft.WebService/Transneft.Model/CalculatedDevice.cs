using System;
using System.Collections.Generic;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Расчетный прибор учёта
    /// </summary>
    public class CalculatedDevice : TransneftObject, IHierarchy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CalculatedDevice() => Connections = new List<CalcPointAndDeviceConnection>();

        /// <summary>
        /// Guid Точки поставки электроэнергии
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Точка поставки электроэнергии
        /// </summary>
        public DeliveryEnergyPoint DeliveryEnergyPoint { get; set; }

        /// <summary>
        /// Связи с CalcEnergyPoint
        /// </summary>
        public IEnumerable<CalcPointAndDeviceConnection> Connections { get; set; }
    }
}
