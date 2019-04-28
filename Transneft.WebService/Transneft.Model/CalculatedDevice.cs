using System;
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
        /// Guid Точки поставки электроэнергии
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
