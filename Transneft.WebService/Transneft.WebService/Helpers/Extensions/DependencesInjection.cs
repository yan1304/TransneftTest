﻿using Microsoft.Extensions.DependencyInjection;
using Taransneft.Logic;
using Taransneft.Logic.Interfaces;
using Transneft.Logic;
using Transneft.Logic.Interfaces;

namespace Transneft.WebService.Helpers
{
    /// <summary>
    /// Класс для добавления зависимостей
    /// </summary>
    public static class DependencesInjection
    {
        /// <summary>
        /// Добавить зависимости
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void AddDependences(this IServiceCollection services)
        {
            services.AddScoped<ILog, Log>();
            services.AddScoped<IQueryService, QueryService>();
        }
    }
}
