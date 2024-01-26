using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Models.ViewModel;
//using TicketingTool.oAuthService;

namespace TicketingTool.Controllers
{
    public class CommonController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult TopBarUserPanel()
        {
            var profile = JsonConvert.DeserializeObject<vm_employee>(Session["user_profile"].ToString());
            return PartialView("~/Views/Shared/PartialView/TopBarUserPanel.cshtml", profile);
        }

        [ChildActionOnly]
        public PartialViewResult SideBarUserPanel()
        {
            var profile = JsonConvert.DeserializeObject<vm_employee>(Session["user_profile"].ToString());
            return PartialView("~/Views/Shared/PartialView/SideBarUserPanel.cshtml", profile);
        }

        [ChildActionOnly]
        public PartialViewResult SideBarMenu()
        {
            List<vm_menu> menuList = (List<vm_menu>)Session["menu"];
            return PartialView("~/Views/Shared/PartialView/SideBar.cshtml", menuList);
        }
    }
}