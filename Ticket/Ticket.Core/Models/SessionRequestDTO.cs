using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
    public class SessionRequestDTO
    {
        public int type { get; set; }
        public object connection { get; set; }
        public Application application { get; set; }
    }

    public class Connection
    {
    }

    public class Application
    {
        public string version { get; set; }

        [JsonProperty("equipment-id")]
        public string equipmentid { get; set; }
    }
}
