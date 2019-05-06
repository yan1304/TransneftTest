using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
