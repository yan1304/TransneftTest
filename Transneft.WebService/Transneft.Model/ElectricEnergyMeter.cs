using Transneft.Model.Base;
using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// Счетчик электрической энергии
    /// </summary>
    public class ElectricEnergyMeter : DeviceBase, IWithType
    {

        /// <summary>
        /// Тип счетчика
        /// </summary>
        public string Type { get; set; }
    }
}
