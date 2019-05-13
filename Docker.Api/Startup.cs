using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Docker.Api
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
            services.AddDbContext<DbUserInfoContext>(
                options =>
                {
                    options.UseMySQL(Configuration.GetConnectionString("mysql"));
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc();
            //UserInit.InitData(app, loggerFactory).Wait();
            //Init(app);
        }

        /// <summary>
        /// 初始化用户名，根据自己需求来
        /// </summary>
        /// <param name="app"></param>
        public void Init(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DbUserInfoContext>();
                /* 
                 * Dockerfile异常
                 * System.InvalidOperationException: No coercion operator is defined between types 'System.Int16' and 'System.Boolean'.
                https://bugs.mysql.com/bug.php?id=92987
    */
                //if (!context.userInfos.Any())
                context.Database.Migrate();

                if (context.userInfos.Count() <= 0)
                {
                    context.userInfos.Add(new Model.UseInfo
                    {
                        name = "admin",
                        address = "博客园"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
