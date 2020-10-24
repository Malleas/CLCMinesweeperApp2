using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLCMinesweeperApp.Models
{
    public class UserLogin
    {

        [Required]
        [DisplayName("User Name")]
        [StringLength(20, MinimumLength = 6)]
        [DefaultValue("")]
        public string userName { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5)]
        [DefaultValue("")]
        public string password { get; set; }
    }
}