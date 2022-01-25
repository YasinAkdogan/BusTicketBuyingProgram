using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.Models
{
   public class CacheModel
    {
        public string SessionId { get; set; }
        public string DeviceId { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }      
    }
}
