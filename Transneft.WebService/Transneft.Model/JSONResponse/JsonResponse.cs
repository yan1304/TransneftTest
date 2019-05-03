﻿using Transneft.Model.Interfaces;

namespace Transneft.Model
{
    /// <summary>
    /// JSON-отклик
    /// </summary>
    public class JsonResponse : IResponse
    {
        /// <summary>
        /// Успешно ли
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// JSON-данные
        /// </summary>
        public string Data { get; set; }
    }
}
