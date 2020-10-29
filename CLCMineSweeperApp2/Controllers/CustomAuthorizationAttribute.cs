using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLCMineSweeperApp2.Controllers
{
    public class CustomAuthorizationAttribute
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            SecurityService service = new SecurityService();

            //get user from a session variable

            UserLogin user = (UserLogin)filterContext.HttpContext.Session["user"];
            bool success = false;

            //checking if user is null
            if (user != null)
            {
                success = service.Authenticate(user);
            }

            if (success)
            {
                //do nothing, allow events to continue as normal as user has been logged in!!
            }
            else
            {
                filterContext.Result = new RedirectResult("/login");

            }
        }
    }
}