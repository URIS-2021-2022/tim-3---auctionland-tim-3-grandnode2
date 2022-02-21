using AutoMapper;
using JavnoNadmetanje.Auth;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JavnoNadmetanje
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
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters();

            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IJavnoNadmetanjeRepository, JavnoNadmetanjeRepository>();
            services.AddScoped<ISluzbeniListRepository, SluzbeniListRepository>();
            services.AddScoped<IPrijavaZaNadmetanjeRepository, PrijavaZaNadmetanjeRepository>();
            services.AddScoped<IOglasRepository, OglasRepository>();
            services.AddScoped<IDokumentPrijavaZaNadmetanjeRepository, DokumentPrijavaZaNadmetanjeRepository>();
            services.AddScoped<IUplataService, UplataService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("JavnoNadmetanjeOpenApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Javno Nadmetanje API",
                    Version = "1",
                    Description = "Pomoću ovog API-ja mogu da se vrše sve CRUD operacije u okviru agregata Javno Nadmetanje.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Lenka Subotin",
                        Email = "subotinlenka@gmail.com",
                        Url = new Uri("http://www.ftn.uns.ac.rs/")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "FTN licence",
                        Url = new Uri("http://www.ftn.uns.ac.rs/")
                    },
                    TermsOfService = new Uri("http://www.ftn.uns.ac.rs/javnoNadmetanjeTermsOfService")
                });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            // Dodat DbContext koji ce se koristiti
            services.AddDbContextPool<JavnoNadmetanjeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("JavnoNadmetanjeDB")));
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
                setupAction.SwaggerEndpoint("/swagger/JavnoNadmetanjeOpenApiSpecification/swagger.json", "Javno Nadmetanje API");
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
