using FrontApiServer.Data;
using Microsoft.EntityFrameworkCore;

namespace FrontApiServer.Extensions
{
    public static class SettingExtensions
    {
        /// <summary>
        /// DB 접속 ConnectionString 적용
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Transient);//, ServiceLifetime.Transient);
        }

    }
}
