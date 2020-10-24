using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLCMinesweeperApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public String Index()
        {
            return @"Home Page";
        }

        //GET: /Home/Registration
        [HttpGet]

        public ActionResult RegistrationView()
        {
            return View("Registration");
        }

        //GET: /Home/LoginView
        [HttpGet]

        public ActionResult LoginView()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
    }
}