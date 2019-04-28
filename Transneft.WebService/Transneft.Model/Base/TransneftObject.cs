using System;
using System.ComponentModel.DataAnnotations;

namespace Transneft.Model.Base
{
    /// <summary>
    /// Базовый класс модели для БД
    /// </summary>
    public abstract class TransneftObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TransneftObject() => Id = Guid.NewGuid();

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
