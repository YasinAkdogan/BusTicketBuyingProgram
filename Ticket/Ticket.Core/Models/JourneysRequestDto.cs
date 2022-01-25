using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class JourneysRequestDto
    {
        [JsonProperty("device-session")]
        public DeviceSession devicesession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
        [JsonProperty("data")]
        public Data data { get; set; }
    }

    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }
        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class Data
    {
        [JsonProperty("origin-id")]
        public int originid { get; set; }
        [JsonProperty("destination-id")]
        public int destinationid { get; set; }
        [JsonProperty("departure-date")]
        public DateTime departuredate { get; set; }
    }
}
