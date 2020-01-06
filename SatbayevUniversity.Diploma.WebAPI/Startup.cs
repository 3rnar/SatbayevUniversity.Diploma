using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SatbayevUniversity.Diploma.WebAPI.Repositories;
using SatbayevUniversity.Diploma.WebAPI.Services;
using SatbayevUniversity.Diploma.WebAPI.Services.Interfaces;
using SatbayevUniversity.Diploma.WebAPI.Utilities;

namespace SatbayevUniversity.Diploma.WebAPI
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
            services.AddControllers();
            services.Configure<DBConfig>(options =>
            {
                options.ConnectionString = Configuration.GetSection("DB:ConnectionString").Value;
            });
            services.AddSwaggerDocumentation();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IDiplomaWorkService, DiplomaWorkService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IDiplomaRepository, DiplomaRepository>();
            services.AddTransient<IDiplomaWorkJournalRepository, DiplomaWorkJournalRepository>();
            services.AddTransient<IDiplomaWorksCalendarRepository, DiplomaWorksCalendarRepository>();
            services.AddTransient<IPresentationFormRepository, PresentationFormRepository>();
            services.AddTransient<INotificationService, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }

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
