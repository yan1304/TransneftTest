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
        /// Список счетчиков электрической энергии
        /// </summary>
        public new string ElectricEnergyMeterId { get; set; }

        /// <summary>
        /// Список трансформаторов тока
        /// </summary>
        public new string CurTransformatorId { get; set; }

        /// <summary>
        /// Список трансформаторов напряжения
        /// </summary>
        public new string VoltTransformatorId { get; set; }

        /// <summary>
        /// Список дочерняя организация
        /// </summary>
        public string ChildOrganizationId { get; set; }
    }
}
