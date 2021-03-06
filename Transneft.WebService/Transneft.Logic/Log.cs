﻿using Microsoft.Extensions.Logging;
using System;

namespace Transneft.Logic
{
    /// <summary>
    /// Логгер приложения
    /// </summary>
    public class Log 
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
    }
}
