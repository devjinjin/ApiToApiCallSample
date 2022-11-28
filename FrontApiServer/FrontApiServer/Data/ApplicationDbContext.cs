using Microsoft.EntityFrameworkCore;

namespace FrontApiServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Microsoft.EntityFrameworkCore.SqlServer 추가
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);         
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<WeatherForecast>? WeatherForecasts { get; set; }
    }
}
