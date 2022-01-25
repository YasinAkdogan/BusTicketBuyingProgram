using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class BusLocationsRequestDto
    {
        public object data { get; set; }
        [JsonProperty("device-session")]
        public DeviceSession2 devicesession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
    }

    public class DeviceSession2
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }
        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }
}
