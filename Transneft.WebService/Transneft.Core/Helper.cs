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
    }
}
