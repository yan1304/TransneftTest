using System;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Связь между CalculatedDevice и CalcEnergyPoint
    /// </summary>
    public class CalcPointAndDeviceConnection : TransneftObject
    {
        /// <summary>
        /// Guid точки измерения электроэнергии
        /// </summary>
        public Guid PointGuid { get; set; }

        /// <summary>
        /// Guid расчетного прибора учёта
        /// </summary>
        public Guid DeviceGuid { get; set; }

        /// <summary>
        /// Дата начала связи
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания связи
        /// </summary>
        public DateTime DateTo { get; set; }
    }
}
