using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLCMinesweeperApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(UserLogin user)
        {
            SecurityService securityService = new SecurityService();


            if (securityService.Authenticate(user))
            {
                return View("../GameBoard/LoadGame");
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}