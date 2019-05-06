using System;
using System.Collections.Generic;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Дочерняя организация
    /// </summary>
    public class ChildOrganization : OrganizationBase, IHierarchy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ChildOrganization() => ConsObjects = new List<ConsObject>();

        /// <summary>
        /// Guid организации-владельца
        /// </summary>
        public Guid ParentId { get; set; } 

        /// <summary>
        /// Организация-владелец
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// Объекты потребления
        /// </summary>
        public IEnumerable<ConsObject> ConsObjects { get; set; }
    }
}
