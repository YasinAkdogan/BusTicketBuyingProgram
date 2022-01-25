using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class BusLocationsDto
    {
        public string status { get; set; }
        public List<LocationInfo> data { get; set; }
        public object message { get; set; }
        [JsonProperty("user-message")]
        public object usermessage { get; set; }
        [JsonProperty("api-request-id")]
        public object apirequestid { get; set; }
        public string controller { get; set; }
        [JsonProperty("client-request-id")]
        public object clientrequestid { get; set; }
    }

    public class LocationInfo
    {
        public int id { get; set; }
        [JsonProperty("parent-id")]
        public int parentid { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        [JsonProperty("geo-location")]
        public GeoLocation geolocation { get; set; }
        public int zoom { get; set; }
        [JsonProperty("tz-code")]
        public string tzcode { get; set; }
        [JsonProperty("weather-code")]
        public object weathercode { get; set; }
        public int? rank { get; set; }
        [JsonProperty("reference-code")]
        public object referencecode { get; set; }
        public string keywords { get; set; }
    }

    public class GeoLocation
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int zoom { get; set; }
    }
}
