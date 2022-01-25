using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.AppSettings
{
    public class AppSettings
    {
        public string EquipmentId { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string AuthorizationName { get; set; }
        public string AuthorizationKey { get; set; }
        public string GetSessionUrl { get; set; }
        public string GetBusLocationsUrl { get; set; }
        public string GetJourneysUrl { get; set; }
    }
}
