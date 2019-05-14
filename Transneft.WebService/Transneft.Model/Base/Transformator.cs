using System;
using System.ComponentModel.DataAnnotations;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс транформатора
    /// </summary>
    public abstract class Transformator : DeviceBase
    {
        /// <summary>
        /// Коэффицент трансформации
        /// </summary>
        public double KT { get; set; }
    }
}
