using Newtonsoft.Json;
using System;
using System.Linq;

namespace Transneft.Core
{
    /// <summary>
    /// Базовый Helper для Transneft
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Проверить, что объект == null
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Результат проверки</returns>
        public static bool IsNull(this object obj) => obj == null;
        
        /// <summary>
        /// Проверить, что объект != null
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Результат проверки</returns>
        public static bool IsNotNull(this object obj) => !obj.IsNull();

        /// <summary>
        /// Проверить, что строка пуста
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Результат проверки</returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Проверить, что строка пуста или содержит пробельные символы
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Результат проверки</returns>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// Проверить, что id входит в массив
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="ids">Массив</param>
        /// <returns>Результат проверки</returns>
        public static bool In(this Guid id, params Guid[] ids) => ids.Any(z => z == id);

        /// <summary>
        /// Конвертировать данные в JSON-строку
        /// </summary>
        /// <param name="obj">Данные</param>
        /// <returns>JSON-строка</returns>
        public static string ToJson(this object obj) => obj.IsNotNull() ? JsonConvert.SerializeObject(obj) : null;

        /// <summary>
        /// Получить данные из JSON
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="json">JSON-строка</param>
        /// <returns>Данные</returns>
        public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
