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
using Uplata.Auth;
using Uplata.Data;
using Uplata.Entities;

namespace Uplata
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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUplataRepository, UplataRepository>();
            services.AddScoped<IBankaRepository, BankaRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("UplataOpenApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Uplata API",
                    Version = "1",
                    Description = "Pomoću ovog API-ja mogu da se vrše sve CRUD operacije u okviru agregata Uplata.",
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
                    TermsOfService = new Uri("http://www.ftn.uns.ac.rs/uplataTermsOfService")
                });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            // Dodar DbContext koji ce se koristiti 
            services.AddDbContextPool<UplataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UplataDB")));

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
                setupAction.SwaggerEndpoint("/swagger/UplataOpenApiSpecification/swagger.json", "Uplata API");
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
