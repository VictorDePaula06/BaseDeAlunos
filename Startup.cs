using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseAlunos.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BaseAlunos
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=BaseDeAlunos;Trusted_Connection=True;TrustServerCertificate=True"));


            services.AddControllers();

            services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "API de Alunos",
                            Version = "v1",
                            Description = "API para gerenciamento de alunos de academia",
                        });

                        // Corrigir conflitos e descrever rotas
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Alunos v1");
                c.RoutePrefix = string.Empty; // Swagger na raiz
            });



            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // mapear os controladores

            });
        }
    }
}
