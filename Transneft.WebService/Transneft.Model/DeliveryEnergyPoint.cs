using System;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Точка поставки электроэнергии
    /// </summary>
    public class DeliveryEnergyPoint : TransneftObject, IWithName, IHierarchy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parent">Guid объекта потребления</param>
        public DeliveryEnergyPoint(Guid parent) : base() => ParentId = parent;

        /// <summary>
        /// Guid объекта потребления
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Наименование точки поставки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Максимальная мощность, кВт
        /// </summary>
        public double MaxPower { get; set; }
    }
}
