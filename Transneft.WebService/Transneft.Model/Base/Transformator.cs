using Transneft.Model.Interfaces;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс транформатора
    /// </summary>
    public abstract class Transformator : ElectricEnergyMeter
    {
        /// <summary>
        /// Коэффицент трансформации
        /// </summary>
        public double KT { get; set; }
    }
}
