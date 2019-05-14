using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transneft.Core;
using Transneft.Model;
using Transneft.WebApplication.Model;
using Transneft.WebApplication.Model.Enum;

namespace Transneft.WebApplication.Controllers
{
    /// <summary>
    /// Контроллер для отправления/получения с WebService
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Index() => await Index(null);

        /// <summary>
        /// Главная страница
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{msg}")]
        public async Task<IActionResult> Index(string msg)
        {
            try
            {
                var param = new IndexModel
                {
                    Message = msg
                };

                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:8050") })
                {
                    var resp = await client.SendAsync(
                        new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri($"{client.BaseAddress}/ConsObject")
                        });
                    if (resp.IsSuccessStatusCode)
                    {
                        param.ConsObjects = (await resp.Content.ReadAsStringAsync()).FromJson<ItemInfo[]>();
                    }
                    else
                    {
                        param.Message = $"Список объектов потребления не загрузился. {(await resp.Content.ReadAsStringAsync()).FromJson<string>()}";
                    }
                }

                return View(param);
            }
            catch (Exception ex)
            {
                return View(new IndexModel { Message = ex.Message });
            }
        }

        /// <summary>
        /// Получить расчетные приборы в 2018г
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Table()
        {
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:8050") })
                {
                    var resp = await client.SendAsync(
                        new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri($"{client.BaseAddress}/CalculatedDevice")
                        });
                    if (resp.IsSuccessStatusCode)
                    {
                        return View((await resp.Content.ReadAsStringAsync()).FromJson<CalculatedDevice[]>());
                    }
                    else
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { msg = ex.Message });
            }
        }

        /// <summary>
        /// Получить табличные данные
        /// </summary>
        /// <param name="param">Параметр для поиска</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Table(TableParam param)
        {
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:8050") })
                {
                    HttpResponseMessage resp = null;
                    switch (param.Type)
                    {
                        case TypeDevice.ElectricEnergyMeter:
                            resp = await client.SendAsync(
                                new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri($"{client.BaseAddress}/ElectricEnergyMeter/{param.Id}")
                                });
                            break;
                        case TypeDevice.CurTransformator:
                            resp = await client.SendAsync(
                                new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri($"{client.BaseAddress}/CurTransformator/{param.Id}")
                                });
                            break;
                        case TypeDevice.VoltTransformator:
                            resp = await client.SendAsync(
                                new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri($"{client.BaseAddress}/VoltTransformator/{param.Id}")
                                });
                            break;
                    }

                    if (resp.IsNotNull() && resp.IsSuccessStatusCode)
                    {
                        switch (param.Type)
                        {
                            case TypeDevice.ElectricEnergyMeter:
                                return View((await resp.Content.ReadAsStringAsync()).FromJson<ElectricEnergyMeter[]>());
                            case TypeDevice.CurTransformator:
                                return View((await resp.Content.ReadAsStringAsync()).FromJson<CurTransformator[]>());
                            case TypeDevice.VoltTransformator:
                                return View((await resp.Content.ReadAsStringAsync()).FromJson<VoltTransformator[]>());
                            default:
                                return View(null);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { msg = ex.Message });
            }
        }

        /// <summary>
        /// Добавить точку измерения электроэнергии
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> AddPoint()
        {
            try
            {
                var pointParam = new AddCalcEnergyPoint();
                using (var client = new HttpClient { BaseAddress = new Uri("http://localhost:8050") })
                {
                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{client.BaseAddress}/ChildOrganization")
                    };

                    var resp = await client.SendAsync(req);
                    if (!resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }

                    pointParam.ChildOrganizations = (await resp.Content.ReadAsStringAsync()).FromJson<ItemInfo[]>();
                    req.RequestUri = new Uri($"{client.BaseAddress}/ElectricEnergyMeter");
                    resp = await client.SendAsync(req);
                    if (!resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }

                    pointParam.ElectricEnergyMeters = (await resp.Content.ReadAsStringAsync()).FromJson<ItemInfo[]>();
                    req.RequestUri = new Uri($"{client.BaseAddress}/CurTransformator");
                    resp = await client.SendAsync(req);
                    if (!resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }

                    pointParam.CurTransformators = (await resp.Content.ReadAsStringAsync()).FromJson<ItemInfo[]>();
                    req.RequestUri = new Uri($"{client.BaseAddress}/VoltTransformator");
                    resp = await client.SendAsync(req);
                    if (!resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                    }

                    pointParam.VoltTransformators = (await resp.Content.ReadAsStringAsync()).FromJson<ItemInfo[]>();
                    return View(pointParam);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { msg = ex.Message });
            }
        }

        /// <summary>
        /// Добавить точку измерения электроэнергии
        /// </summary>
        /// <param name="point">Точка измерения электроэнергии</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPoint(AddCalcEnergyPoint point)
        {
            try
            {
                var engPoint = new CalcEnergyPoint
                {
                    Name = point.Name,
                    ConsObjectId = Guid.Parse(point.ChildOrganizationId),
                };

                using (var client = new HttpClient() { BaseAddress = new Uri("http://localhost:8050") })
                {
                    var req = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{client.BaseAddress}/CalcEnergyPoint/{point.ElectricEnergyMeterId}/{point.CurTransformatorId}/{point.VoltTransformatorId}")
                    };

                    req.Content = new StringContent(engPoint.ToJson(), Encoding.Unicode);
                    var resp = await client.SendAsync(req);
                    if (resp.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index", new { msg = (await resp.Content.ReadAsStringAsync()).FromJson<string>() });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { msg = ex.Message });
            }
        }
    }
}