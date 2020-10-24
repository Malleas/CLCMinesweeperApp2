using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLCMinesweeperApp.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Registration(Player player)
        {
            UserService userService = new UserService();


            if (userService.CreateUser(player))
            {
                return View("RegistrationComplete");
            }
            else
            {
                return View("RegistrationFailed");
            }
        }
    }
}