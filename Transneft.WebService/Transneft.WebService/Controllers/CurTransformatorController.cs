using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Taransneft.Logic.Interfaces;

namespace Transneft.WebService.Controllers
{
    /// <summary>
    /// 
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
        public CurTransformatorController(ILogger logger, IQueryService service) : base(logger, service) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
    }
}
