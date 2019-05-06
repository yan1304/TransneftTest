using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;

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
        public ChildOrganizationController(ILogger logger, IQueryService service) : base(logger, service) { }

        /// <summary>
        /// GET ChildOrganization (задание 1.2 п.5)
        /// Получить дочерние организации
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
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
