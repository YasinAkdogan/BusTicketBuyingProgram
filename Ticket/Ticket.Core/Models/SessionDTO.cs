using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class SessionDTO
    {
        public string status { get; set; }
        public Data2 data { get; set; }
        public object message { get; set; }
        [JsonProperty("user-message")]
        public object usermessage { get; set; }
        [JsonProperty("api-request-id")]
        public object apirequestid { get; set; }
        public object controller { get; set; }
        [JsonProperty("client-request-id")]
        public object clientrequestid { get; set; }
    }

    public class Data2
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }
        [JsonProperty("device-id")]
        public string deviceid { get; set; }
        public object affiliate { get; set; }
        [JsonProperty("device-type")]
        public int devicetype { get; set; }
        public object device { get; set; }
    }
}
