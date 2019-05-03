using Newtonsoft.Json;

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
