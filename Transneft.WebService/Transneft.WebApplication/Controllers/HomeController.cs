using Microsoft.AspNetCore.Mvc;
using Transneft.Model;

namespace Transneft.WebApplication.Controllers
{
    /// <summary>
    /// Контроллер для отправления/получения с WebService
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Получить данные по устройствам с истекшим сроком проверки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceType">Тип устройства</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Table(string id, string deviceType)
        {
            return View();
        }

        /// <summary>
        /// Добавить точку измерения электроэнергии
        /// </summary>
        /// <param name="point">Точка измерения электроэнергии</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult AddPoint()
        {
            return View();
        }

        /// <summary>
        /// Добавить точку измерения электроэнергии
        /// </summary>
        /// <param name="point">Точка измерения электроэнергии</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPoint([Bind("Name,ParentId,CurTransformatorId,VoltTransformatorId,ElectricEnergyMeterId")]CalcEnergyPoint point)
        {
            return View();
        }
    }
}