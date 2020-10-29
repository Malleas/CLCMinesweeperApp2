using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Business;
using CLCMineSweeperApp2.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace CLCMinesweeperApp.Controllers
{
    public class LoginController : Controller
    {
        [Dependency]
        public SecurityService securityService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(UserLogin user)
        {
            
            if (securityService.Authenticate(user))
            {
                Session["user"] = user;
                return View("../GameBoard/LoadGame");
            }
            else
            {
                Session.Clear();
                return View("LoginFailed");
            }
        }

        
        public ActionResult Protected()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        
    }
}