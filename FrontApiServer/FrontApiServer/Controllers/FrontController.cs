using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FrontApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontController : ControllerBase
    {

        public FrontController()
        {

        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        { 
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
                    
                        foreach (var x in data)
                        {
                            //Call your store method and pass in your own object
                        }

                        return data;
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
    }
}
