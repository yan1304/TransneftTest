using System;
using System.Collections.Generic;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Объект потребления
    /// </summary>
    public class ConsObject : OrganizationBase, IHierarchy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConsObject() => DeliveryEnergyPoints = new List<DeliveryEnergyPoint>();

        /// <summary>
        /// Guid дочерней организации
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Дочерняя организация
        /// </summary>
        public ChildOrganization ChildOrganization { get; set; }

        /// <summary>
        /// Точки поставки электроэнергии
        /// </summary>
        public IEnumerable<DeliveryEnergyPoint> DeliveryEnergyPoints { get; set; }

        /// <summary>
        /// Точки измерения электроэнергии
        /// </summary>
        public IEnumerable<CalcEnergyPoint> CalcEnergyPoints { get; set; }
    }
}
