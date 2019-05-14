using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;
using Transneft.Logic.Contexts;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// Контроллер для работы с трансформаторами тока
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CurTransformatorController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        /// <param name="context">Контекст БД</param>
        public CurTransformatorController(ILogger<CurTransformatorController> logger, IQueryService service, TransneftDbContext context)
            : base(logger, service, context) { }

        /// <summary>
        /// GET CurTransformator (задание 1.2 п.5)
        /// По указанному объекту потребления выбрать все трансформаторы тока с закончишившимся сроком проверки
        /// </summary>
        /// <param name="id">Id объекта потребления</param>
        /// <returns>JSON-отклик</returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Log("Start GET CurTransformator");
            try
            {
                Log("Start QueryService.GetDeadlinedCurTransformators");
                var result = QueryService.GetDeadlinedCurTransformators(id);
                Log("End QueryService.GetDeadlinedCurTransformators");
                Log("End GET CurTransformator");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
                return new BadRequestObjectResult($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// GET CurTransformator
        /// Выбрать все не используемые трансформаторы тока 
        /// </summary>
        /// <returns>JSON-отклик</returns>
        [HttpGet]
        public IActionResult Get()
        {
            Log("Start GET CurTransformator (All)");
            try
            {
                Log("Start QueryService.GetDisabledCurTransformators");
                var result = QueryService.GetDisabledCurTransformators();
                Log("End QueryService.GetDisabledCurTransformators");
                Log("End GET CurTransformator (All)");
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
