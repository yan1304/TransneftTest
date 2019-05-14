using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с объектами потребления
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ConsObjectController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public ConsObjectController(ILogger<ConsObjectController> logger, IQueryService service, TransneftDbContext context)
            : base(logger, service, context) { }

        /// <summary>
        /// GET ConsObject
        /// Получить все объекты потребления
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET ConsObject");
            try
            {
                Log("Start QueryService.GetAllConsObjects");
                var result = QueryService.GetAllConsObjects();
                Log("End QueryService.GetAllConsObjects");
                Log("End GET ConsObject");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                return new BadRequestObjectResult($"Ошибка: {ex.Message}");
            }
        }
    }
}
