using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с дочерними организациями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ChildOrganizationController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public ChildOrganizationController(ILogger<ChildOrganizationController> logger, IQueryService service, TransneftDbContext context) 
            : base(logger, service, context) { }

        /// <summary>
        /// GET ChildOrganization
        /// Получить все дочерние организации
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET ChildOrganization");
            try
            {
                Log("Start QueryService.GetAllChildOrganizations");
                var result = QueryService.GetAllChildOrganizations();
                Log("End QueryService.GetAllChildOrganizations");
                Log("End GET ChildOrganization");
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
