using System;
using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Дочерняя организация
    /// </summary>
    public class ChildOrganization : TransneftObject, IWithName, IHierarchy
    {
        public ChildOrganization()
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parent">Guid организации-владельца</param>
        public ChildOrganization(Guid parent) :base() => ParentId = parent;

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guid организации-владельца
        /// </summary>
        public Guid ParentId { get; set; } 
    }
}
