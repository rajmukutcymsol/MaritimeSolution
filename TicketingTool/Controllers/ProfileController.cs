using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        
    }
}