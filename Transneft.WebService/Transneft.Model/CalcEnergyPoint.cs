using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Точка измерения электроэнергии
    /// </summary>
    public class CalcEnergyPoint : TransneftObject, IWithName
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
