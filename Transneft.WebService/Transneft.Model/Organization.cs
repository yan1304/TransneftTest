using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization : AddressObject, IWithName
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
