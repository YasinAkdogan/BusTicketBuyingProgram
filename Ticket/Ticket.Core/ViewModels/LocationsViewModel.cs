using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Core.ViewModels
{
   public class LocationsViewModel
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
