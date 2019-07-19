using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProject.API.CustomMiddlewares;
using AspNetCoreProject.API.Logger;
using AspNetCoreProject.Application.BusinessLogic.Services;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.Infrastructure.Data.Context;
using AspNetCoreProject.Infrastructure.Data.Repositories;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AspNetCoreProject.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IQuestionnaireService, QuestionnaireService>();
            services.AddSingleton<ILogger>(new FileLogger(Configuration.GetSection("LogPath").Value));
            services.AddDbContextPool<InquirerContext>( 
                options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 16), ServerType.MySql); 
                    }
                ));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, InquirerContext inquirerContext) {
            inquirerContext.Database.EnsureCreated();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureCustomExceptionMiddleware();
            app.UseMvc();
        }
    }
}
