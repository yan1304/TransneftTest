using Microsoft.AspNetCore.Mvc;
using Transneft.Model.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер дляработы с точками измерения
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalcEnergyPointController
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private TransneftDbContext context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_logger"></param>
        public CalcEnergyPointController()
        {
            context = new TransneftDbContext();
        }

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
