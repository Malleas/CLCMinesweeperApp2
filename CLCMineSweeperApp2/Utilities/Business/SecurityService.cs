using CLCMinesweeperApp.Models;
using CLCMinesweeperApp.Services.Data;
using CLCMineSweeperApp2.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace CLCMinesweeperApp.Services.Business
{
    public class SecurityService
    {
        [Dependency]
        public SecurityDAO securityDAO { get; set; }

        public bool Authenticate(UserLogin user)
        {
        SecurityDAO service = new SecurityDAO();
            return securityDAO.FindByUser(user);
        }
    }
}