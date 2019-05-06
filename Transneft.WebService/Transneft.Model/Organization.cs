using System.Collections.Generic;
using Transneft.Model.Base;

namespace Transneft.Model
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization : OrganizationBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Organization() => ChildOrganizations = new List<ChildOrganization>();

        /// <summary>
        /// Дочерние организации
        /// </summary>
        public IEnumerable<ChildOrganization> ChildOrganizations { get; set; }
    }
}
