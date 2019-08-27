using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace APIOrientacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Referência ao banco de dados
            services.AddDbContext<Contexto>(options => options.UseSqlServer(Configuration.GetConnectionString("ProjetoOrientacao")));

            //Adiciona a informação de criação do Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Title = "API - Orientação",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Define o uso do Swagger
            app.UseSwagger();

            //Define a Endpoint da Interface do Swagger
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("v1/swagger.json", "API - Orientação v1");
                c.DefaultModelExpandDepth(-1);
            });

            app.UseMvc();
        }
    }
}
