using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Transneft.Core;
using Transneft.Model;

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
        /// POST CalcEnergyPoint (задание 1.2 п.1)
        /// Добавить новую точку измерения напряжения с указанием счётчика, трансформатора тока и трансформатора напряжения
        /// </summary>
        /// <param name="data">CalcEnergyPoint в json-формате</param>
        /// <returns>JSON-отклик</returns>
        [HttpPost]
        public JsonResult Post([FromBody] object data)
        {
            Logger.Write("Start POST CalcEnergyPoint");
            CalcEnergyPoint param = null;
            try
            {
                var json = $"{data}";
                if (string.IsNullOrWhiteSpace(json))
                {
                    return WriteLogAndReturn(new JsonResponse { ErrMsg = "Отсутствует параметр!" });
                }

                param = json.FromJson<CalcEnergyPoint>();
                if (param.Name.IsNull())
                {
                    return WriteLogAndReturn(new JsonResponse { ErrMsg = "Отсутствует параметр Name!" });
                }
                else if (param.VoltTransformatorId.IsNull())
                {
                    return WriteLogAndReturn(new JsonResponse { ErrMsg = "Отсутствует параметр VoltTransformatorId!" });
                }
                else if (param.CurTransformatorId.IsNull())
                {
                    return WriteLogAndReturn(new JsonResponse { ErrMsg = "Отсутствует параметр CurTransformatorId!" });
                }
                else if (param.ElectricEnergyMeterId.IsNull())
                {
                    return WriteLogAndReturn(new JsonResponse { ErrMsg = "Отсутствует параметр ElectricEnergyMeterId!" });
                }
                else if (param.Id.IsNull())
                {
                    param.Id = Guid.NewGuid();
                }

            }
            catch
            {
                return WriteLogAndReturn(new JsonResponse { ErrMsg = "Некорректный JSON-параметр" });
            }

            return new JsonResult("ОК");
        }
    }
}
