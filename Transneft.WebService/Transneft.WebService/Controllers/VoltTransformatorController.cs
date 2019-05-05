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
    public class VoltTransformatorController : WebServiceControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="service">Сервис для работы с БД</param>
        public VoltTransformatorController(ILogger logger, IQueryService service) : base(logger, service) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
    }
}
