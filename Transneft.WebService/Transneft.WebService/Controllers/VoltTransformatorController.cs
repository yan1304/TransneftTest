using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с трансформаторами напряжения
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class VoltTransformatorController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public VoltTransformatorController(ILogger<VoltTransformatorController> logger, IQueryService service, TransneftDbContext context)
            : base(logger, service, context) { }

        /// <summary>
        /// GET VoltTransformator(id) (задание 1.2 п.4)
        /// По указанному объекту потребления выбрать все трансформаторы напряжения с закончишившимся сроком проверки
        /// </summary>
        /// <param name="id">Id объекта потребления</param>
        /// <returns>JSON-отклик</returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Log("Start GET VoltTransformator");
            try
            {
                Log("Start QueryService.GetDeadlinedVoltTransformators");
                var result = QueryService.GetDeadlinedVoltTransformators(id);
                Log("End QueryService.GetDeadlinedVoltTransformators");
                Log("End GET VoltTransformator");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                return new BadRequestObjectResult($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// GET VoltTransformator
        /// Выбрать все не используемые трансформаторы напряжения 
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET VoltTransformator (All)");
            try
            {
                Log("Start QueryService.GetDisabledVoltTransformators");
                var result = QueryService.GetDisabledVoltTransformators();
                Log("End QueryService.GetDisabledVoltTransformators");
                Log("End GET VoltTransformator (All)");
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
