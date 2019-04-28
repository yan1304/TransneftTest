using System;

namespace Transneft.Model.Interfaces
{
    /// <summary>
    /// Класс с владельцем
    /// </summary>
    public interface IHierarchy
    {
        /// <summary>
        /// Владелец объекта
        /// </summary>
        Guid ParentId { get; set; }
    }
}
