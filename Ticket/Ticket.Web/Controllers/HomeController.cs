using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Core.AppSettings;
using Ticket.Core.Businesses;
using Ticket.Core.Models;
using Ticket.Core.ViewModels;

namespace Ticket.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusiness _business;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memCache;
        public HomeController(IBusiness business, IMapper mapper, IMemoryCache memCache)
        {
            _business = business;
            _mapper = mapper;
            _memCache = memCache;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["CookieKey"] != null && Request.Cookies["CookieKey"] != "")
            {
                var CookieValue = Request.Cookies["CookieKey"];
                var DeviceId = Request.Cookies["DeviceId"];
                //Hafızaya kayıt ettirdiğim veriyi çekiyorum.
                var getData = _memCache.Get<CacheModel>(CookieValue);
                //verilerimi viewbag'ler ile View tarafına taşıyorum.
                if (getData != null)
                {
                    ViewBag.originid = getData.OriginId;
                    ViewBag.destinationid = getData.DestinationId;
                }
                //Daha önce eğer sisteme giriş yaptıysa tekrardan sessionid almak yerine hazır olanı gönderiyorum.
                SessionDTO session = new SessionDTO()
                {
                    data = new Data2
                    {
                        sessionid = CookieValue,
                        deviceid = DeviceId,
                        devicetype = 0
                    }
                };
                var busLocations = _business.GetBusLocations(session);
                return View(busLocations.data);
            }
            else
            {
                var session = _business.GetSession();
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("CookieKey", session.data.sessionid, options);
                Response.Cookies.Append("DeviceId", session.data.deviceid, options);
                var CookieValue = Request.Cookies["CookieKey"];
                var DeviceId = Request.Cookies["DeviceId"];
                var busLocations = _business.GetBusLocations(session);
                return View(busLocations.data);
            }
        }
        public IActionResult JourneyIndex(int oid, int did, DateTime TDate)
        {
            //Sistem hata vermemesi için oluşturdum direk buraya geldiği zaman cookie'nin içi boşsa Index sayfasına yönlendiriyorum.
            if (Request.Cookies["CookieKey"] != null && Request.Cookies["CookieKey"] != "")
            {
                var CookieValue = Request.Cookies["CookieKey"];
                var DeviceId = Request.Cookies["DeviceId"];
                CacheModel cacheInfo = new CacheModel()
                {
                    SessionId = CookieValue,
                    DeviceId = DeviceId,
                    OriginId = oid,
                    DestinationId = did
                };
                //En son aranılanı kayıt ettirmek için daha önceki hafızaya kayıt ettirdiğim veriyi siliyorum.
                _business.RemoveInMemory(CookieValue);
                _business.CreateInMemory(cacheInfo);
                LocationsViewModel model = new LocationsViewModel
                {
                    OriginId = oid,
                    DestinationId = did,
                    TicketDate = TDate
                };
                SessionDTO session = new SessionDTO()
                {
                    data = new Data2
                    {
                        sessionid = CookieValue,
                        deviceid = DeviceId,
                        devicetype = 0
                    }
                };
                var busLocations = _business.GetJourneys(session, model);
                return View(busLocations.data);
            }
            else
            {
                return RedirectToAction("Home", "Index");
            }
        }
    }
}
