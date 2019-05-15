using System.Collections.Generic;
using Transneft.Model;

namespace Transneft.WebApplication.Model
{
    /// <summary>
    /// Модель для добавления новой точки измерения электроэнергии
    /// </summary>
    public class AddCalcEnergyPoint : CalcEnergyPoint
    {
        /// <summary>
        /// Список счетчиков электрической энергии
        /// </summary>
        public IEnumerable<ItemInfo> ElectricEnergyMeters { get; set; }

        /// <summary>
        /// Список трансформаторов тока
        /// </summary>
        public IEnumerable<ItemInfo> CurTransformators { get; set; }

        /// <summary>
        /// Список трансформаторов напряжения
        /// </summary>
        public IEnumerable<ItemInfo> VoltTransformators { get; set; }

        /// <summary>
        /// Список дочерних организаций
        /// </summary>
        public IEnumerable<ItemInfo> ChildOrganizations { get; set; }

        /// <summary>
        /// Id Список счетчика электрической энергии
        /// </summary>
        public string EnergyMeterId { get; set; }

        /// <summary>
        /// Id Список трансформатора тока
        /// </summary>
        public string CurTransId { get; set; }

        /// <summary>
        /// Id трансформатора напряжения
        /// </summary>
        public string VoltTransId { get; set; }

        /// <summary>
        /// Id дочерней организации
        /// </summary>
        public string ChildOrganizationId { get; set; }
    }
}
