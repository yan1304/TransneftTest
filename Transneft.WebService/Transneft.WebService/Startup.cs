using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Transneft.Logic.Contexts;
using Transneft.WebService.Helpers;

namespace Transneft.WebService
{
    /// <summary>
    /// Sturtup приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNodeServices();
            services.AddDbContext<TransneftDbContext>(options => options.UseSqlServer(Program.Configuration["ConnectionString"]));
            services.AddMvc(options =>
            {
                options.Filters.Add(new JsonParamFilter());
            });

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "Transneft.WebService",
                        Description = "Swagger for Transneft.WebService" 
                    });

                    c.IncludeXmlComments(GetXmlCommentsPath());
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Warning);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/Swagger/v1/swagger.json", "Transneft.WebService"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetXmlCommentsPath() => $@"{Directory.GetCurrentDirectory()}\Transneft.WebService.XML";
    }
}
