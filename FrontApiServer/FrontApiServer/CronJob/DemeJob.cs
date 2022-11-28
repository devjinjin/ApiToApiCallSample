using FrontApiServer.Data;
using Quartz;
using System.Net.Http.Headers;

namespace FrontApiServer.CronJob
{
    public class DemoJob : IJob
    {
        ApplicationDbContext _dbContext;
        public DemoJob(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string id = Guid.NewGuid().ToString();
            string message = "This job will be executed again at: " +
            context.NextFireTimeUtc.ToString();

            try
            {
                //주기 적으로 다른 API 호출
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7270");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync("WeatherForecast");
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

                            if (data != null) {
                                if (_dbContext.WeatherForecasts != null)
                                {
                                    _dbContext.WeatherForecasts.AddRange(data);

                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                            //Something has gone wrong, handle it here
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
