using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Api.Data
{
    public class UserInit
    {
        private ILogger<UserInit> _logger;

        public UserInit(ILogger<UserInit> logger)
        {
            _logger = logger;
        }

        public static async Task InitData(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DbUserInfoContext>();
                var logger = scope.ServiceProvider.GetService<ILogger<UserInit>>();
                logger.LogDebug("begin mysql init");
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
            await Task.CompletedTask;
        }
    }
}
