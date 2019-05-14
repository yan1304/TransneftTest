using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Transneft.Model;

namespace Transneft.WebApplication.Components
{
    /// <summary>
    /// Компонент для отображения таблиц по устройствам
    /// </summary>
    public class ElectricEnergyMeterList : ViewComponent
    {
        /// <summary>
        /// Получить компонент с отображением устройств
        /// </summary>
        /// <param name="items">Данные</param>
        /// <returns>IViewComponentResult</returns>
        public IViewComponentResult Invoke(IEnumerable<ElectricEnergyMeter> items) => View(items);
    }
}
