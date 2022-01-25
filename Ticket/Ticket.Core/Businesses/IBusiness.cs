using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Models;
using Ticket.Core.ViewModels;

namespace Ticket.Core.Businesses
{
    public interface IBusiness
    {
        SessionDTO GetSession();
        CacheModel CreateInMemory(CacheModel cacheModel);
        void RemoveInMemory(string CookieValue);
        BusLocationsDto GetBusLocations(SessionDTO sessionDTO);
        JourneysDto GetJourneys(SessionDTO sessionDTO, LocationsViewModel locationsViewModel);
    }
}
