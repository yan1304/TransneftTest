using System;
using System.Collections.Generic;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Расчетный прибор учёта
    /// </summary>
    public class CalculatedDevice : TransneftObject
    {
        /// <summary>
        /// Guid Точки поставки электроэнергии
        /// </summary>
        public Guid DeliveryEnergyPointId { get; set; }

        /// <summary>
        /// Точка поставки электроэнергии
        /// </summary>
        public DeliveryEnergyPoint DeliveryEnergyPoint { get; set; }

        /// <summary>
        /// Связи между CalculatedDevice и CalcEnergyPoint
        /// </summary>
        public List<CalcPointAndDeviceConnection> CalcPointAndDeviceConnections { get; set; }
    }
}
