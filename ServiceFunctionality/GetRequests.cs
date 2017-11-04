using getVehicleLocationAPI.Data;
using getVehicleLocationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace getVehicleLocationAPI.ServiceFunctionality
{
    public class GetRequests
    {
        private readonly LocationContext _context;
        public GetRequests(LocationContext context)
        {
            _context = context;
        }

        public IEnumerable<VehicleLocation> GetVehicleList()
        {
            var answer = _context.VehicleLocations.Where(a => a.Active == 1);
            return answer;
        }

        public async Task<string> ReturnLocation(string latLong)
        {
            string responseBody;
            HttpClient client = new HttpClient();
            Secrets Api = new Secrets();
            var API = Api.API;

            var httpsString = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latLong}&key={API}";

            try
            {

                HttpResponseMessage response = await client.GetAsync(httpsString);

                response.EnsureSuccessStatusCode();
                
                responseBody = await response.Content.ReadAsStringAsync();

                JObject rss = JObject.Parse(responseBody);
                var rssTitle = (JArray)rss["results"];
                var address = (string)rssTitle[0]["formatted_address"];
                return address;
            }
            catch (Exception e)
            {
                return "What is up with this " + e.Message;

            }
        }
    }
}
