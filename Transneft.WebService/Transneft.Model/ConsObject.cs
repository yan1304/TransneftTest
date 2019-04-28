using System;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Объект потребления
    /// </summary>
    public class ConsObject : AddressObject, IWithName, IHierarchy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parent">Guid дочерней организации</param>
        public ConsObject(Guid parent) : base() => ParentId = parent;

        /// <summary>
        /// Наименование объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guid дочерней организации
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
