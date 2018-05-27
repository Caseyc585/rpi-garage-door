using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rpi_garage_door.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;

namespace rpi_garage_door
{
    public class DoorEventService : IDoorEventService
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;

        public DoorEventService(IOptions<AppSettings> options, ILogger<DoorEventService> logger)
        {
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task PostDoorServer(DoorEventBody body)
        {
            using(HttpClient client = new HttpClient())
            {
                try	
                {
                    var json = JsonConvert.SerializeObject(body,
                    new JsonSerializerSettings 
                    { 
                        ContractResolver = new CamelCasePropertyNamesContractResolver() 
                    });
                    var content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync(_appSettings.DoorEventURL,  content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                }  
                catch(HttpRequestException e)
                {
                    _logger.LogError(e, "Error while post data");
                }
            }
        }
    }
}