using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.AppSettings;
using Ticket.Core.Businesses;
using Ticket.Core.Models;
using Ticket.Core.ViewModels;

namespace Ticket.Business.Businesses
{
    public class Business : IBusiness
    {
        private readonly AppSettings config;
        private readonly IMemoryCache _memCache;
        public Business(IOptions<AppSettings> config, IMemoryCache memCache)
        {
            this.config = config.Value;
            _memCache = memCache;
        }
        private TEntity HttpPost<TEntity>(string request, string url) where TEntity : class, new()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(config.AuthorizationName, config.AuthorizationKey);
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(url, content).Result;
            string data = result.Content.ReadAsStringAsync().Result;
            var datas = JsonConvert.DeserializeObject<TEntity>(data);
            return datas;
        }
        public BusLocationsDto GetBusLocations(SessionDTO sessionDTO)
        {
            BusLocationsRequestDto busLocationsDto = new BusLocationsRequestDto()
            {
                data = null,
                devicesession = new DeviceSession2()
                {
                    sessionid = sessionDTO.data.sessionid,
                    deviceid = sessionDTO.data.deviceid
                },
                date = DateTime.Now,
                language = config.Language
            };
            string request = JsonConvert.SerializeObject(busLocationsDto);
            var data = HttpPost<BusLocationsDto>(request, config.GetBusLocationsUrl);
            return data;
        }
        public SessionDTO GetSession()
        {
            SessionRequestDTO sessionRequestDTO = new SessionRequestDTO()
            {
                application = new Application
                {
                    equipmentid = config.EquipmentId,
                    version = config.Version

                },
                type = Convert.ToInt32(config.Type),
                connection = new object
                {

                }
            };
            string request = JsonConvert.SerializeObject(sessionRequestDTO);
            var data = HttpPost<SessionDTO>(request, config.GetSessionUrl);
            return data;
        }
        public JourneysDto GetJourneys(SessionDTO sessionDTO, LocationsViewModel locationsViewModel)
        {
            JourneysRequestDto getJourneysRequestDto = new JourneysRequestDto()
            {
                data = new Data()
                {
                    originid = locationsViewModel.OriginId,
                    destinationid = locationsViewModel.DestinationId,
                    departuredate = Convert.ToDateTime(locationsViewModel.TicketDate.ToShortDateString())
                },
                devicesession = new DeviceSession()
                {
                    sessionid = sessionDTO.data.sessionid,
                    deviceid = sessionDTO.data.deviceid
                },
                date = DateTime.Now,
                language = config.Language
            };          
            string request = JsonConvert.SerializeObject(getJourneysRequestDto);
            var data = HttpPost<JourneysDto>(request,config.GetJourneysUrl);
            return data;
           
        }

        public CacheModel CreateInMemory(CacheModel cacheModel)
        {            
            
                //Burada cache için belirli ayarlamaları yapıyoruz.Cache süresi,önem derecesi gibi
                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(30),
                    Priority = CacheItemPriority.Normal
                };
                //Bu satırda belirlediğimiz key'e göre ve ayarladığımız cache özelliklerine göre kategorilerimizi in-memory olarak cache'liyoruz.
                _memCache.Set(cacheModel.SessionId, cacheModel, cacheExpOptions);
            
            return cacheModel;
        }

        public void RemoveInMemory(string CookieValue)
        {
            _memCache.Remove(CookieValue);            
        }
    }
}
