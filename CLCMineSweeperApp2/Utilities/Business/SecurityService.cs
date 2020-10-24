using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLCMinesweeperApp.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(UserLogin user)
        {
            SecurityDAO service = new SecurityDAO();
            return service.FindByUser(user);
        }
    }
}