using System;
using System.Collections.Generic;
using System.Text;
using Transneft.Model.Interfaces;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс транформатора
    /// </summary>
    public abstract class Transformator : DeviceBase, IWithType
    {
        /// <summary>
        /// Тип трансформатора тока
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// КТН (Коэффицент трансформации)
        /// </summary>
        public double KTN { get; set; }
    }
}
