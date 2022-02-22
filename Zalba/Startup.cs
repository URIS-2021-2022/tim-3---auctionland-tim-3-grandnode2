using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Zalba.Entities;
using Zalba.Auth;
using Zalba.ServiceCalls;

namespace Zalba
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

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            services.AddScoped<IZalbaRepository, ZalbaRepository>();
            services.AddScoped<ITipZalbeRepository, TipZalbeRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILicitacijaService, LicitacijaService>();
            services.AddScoped<IPodnosilacZalbeService, PodnosilacZalbeService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddSwaggerGen(setupAction => //ON JE OVO OBRISAO
            {
                setupAction.SwaggerDoc("ZalbaOpenApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Zalba API",
                    Version = "1",
                    Description = "Pomoću ovog API-ja mogu da se vrše sve CRUD operacije u okviru agregata Zalba.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Maja Cetic",
                        Email = "ceticmaja@gmail.com",
                        Url = new Uri(Configuration["Links:FTN"])
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "FTN licence",
                        Url = new Uri(Configuration["Links:FTN"])
                    },
                    TermsOfService = new Uri(Configuration["Links:TermsOfService"])
                }) ;

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContextPool<ZalbaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ZalbaDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do greške. Molim Vas pokušajte kasnije!");
                    });
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ZalbaOpenApiSpecification/swagger.json", "Zalba API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
