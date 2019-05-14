using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Transneft.Model;

namespace Transneft.WebApplication.Components
{
    /// <summary>
    /// Компонент по отображению Расчетных приборов учета
    /// </summary>
    public class CalculatedDeviceList : ViewComponent
    {
        /// <summary>
        /// Получить компонент с отображением Расчетных приборов учета
        /// </summary>
        /// <param name="items">Данные</param>
        /// <returns>IViewComponentResult</returns>
        public IViewComponentResult Invoke(IEnumerable<CalculatedDevice> items) => View(items);
    }
}
