using AutoMapper;
using komisijaService.Auth;
using komisijaService.Data;
using komisijaService.DBContexts;
using komisijaService.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace komisijaService
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
            services.AddDbContext<KomisijaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseForKomisija")));

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters();


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("AdApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo()
                     {
                         Title = "Komisija API",
                         Version = "1.0",
                         Description = "Ovaj API pruza informacije o komisiji, kao i o licnostima te komisije",
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact
                         {
                             Name = "Dunja Zamaklar",
                             Email = "dunjazamaklar1@gmail.com"
                         },
                         License = new Microsoft.OpenApi.Models.OpenApiLicense
                         {
                             Name = "FTN licence",
                             Url = new Uri("http://www.ftn.uns.ac.rs/")
                         }
                     });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //setupAction.IncludeXmlComments(xmlCommentsPath);
                
            });

            services.AddScoped<IKomisijaRepository, KomisijaRepository>();
            services.AddScoped<ILicnostKomisijeRepository, LicnostiKomisijeRepository>();
            services.AddScoped<ILoggerMockRepository, LoggerMockRepository>();
            services.AddScoped<IAuthHelper, AuthHelper>();

            services.AddHttpContextAccessor();

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
                        await context.Response.WriteAsync("Molim vas pokusajte kasnije.");
                    });
                });
            }

            app.UseSwagger();
         
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/AdApiSpecification/swagger.json", "komisijaService v1");
                setupAction.RoutePrefix = "";
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
