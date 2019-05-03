using Microsoft.Extensions.Logging;
using System;
using Transneft.Logic.Interfaces;
using Transneft.Model.Interfaces;

namespace Transneft.Logic
{
    /// <summary>
    /// Логгер приложения
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// ILogger
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Логгер
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="logger">Логгер</param>
        public Log(ILogger logger) => _logger = logger;

        /// <summary>
        /// Записать лог
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void Write(string msg) => _logger.LogWarning($"{DateTime.Now}: {msg}");

        /// <summary>
        /// Записать лог из отклика
        /// </summary>
        /// <param name="resp">Отклик</param>
        public void WriteResponse(IResponse resp)
        {
            var str = $"Success: {resp.Success}, ErrorMsg: {(string.IsNullOrWhiteSpace(resp.ErrMsg) ? "None" : resp.ErrMsg)}, " 
                + $"Data: {(string.IsNullOrWhiteSpace(resp.Data) ? "None" : resp.Data)}";
            Write(str);
        }
    }
}
