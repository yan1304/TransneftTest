using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер дляработы с точками измерения
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalcEnergyPointController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        public CalcEnergyPointController(ILogger logger) : base(logger) { }

        /// <summary>
        /// GET CalcEnergyPoint
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("ОК");
        }

        /// <summary>
        /// POST CalcEnergyPoint
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>JSON-отклик</returns>
        [HttpPost]
        public JsonResult Post([FromBody] object data)
        {
            return new JsonResult("ОК");
        }
    }
}
