using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLCMinesweeperApp.Models
{
    public class Player
    {
        public Player()
        {

        }
        public Player(string firstName, string lastname, Gender gender, string age, States state, string emailAddress, string userName, string password)
        {
            this.firstName = firstName;
            this.lastname = lastname;
            this.gender = gender;
            this.age = age;
            this.state = state;
            this.emailAddress = emailAddress;
            this.userName = userName;
            this.password = password;
        }

        [Required]
        [DisplayName("First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must contain between 2 and 20 characters.")]
        [DefaultValue("")]
        public string firstName { get; set; }


        [Required]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must contain between 2 and 20 characters.")]
        [DefaultValue("")]
        public string lastname { get; set; }

        [Required]
  
        public Gender gender { get; set; }

        [Required]
        [DisplayName("Age")]
        [StringLength(3, MinimumLength = 2)]
     
        [DefaultValue("")]
        public string age { get; set; }


        public States state { get; set; }


        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [Required]
        [DisplayName("Username")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be a minimum of 6 characters and no more than 20 characters.")]
        [DefaultValue("")]
        public string userName { get; set; }

        [Required]
        [DisplayName("Password")]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[A-Z])(?=.*\d)(?=.*[a-z])|
        (?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&\/=?_.-])|
        (?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&\/=?_.-])|
        (?=.*\d)(?=.*[a-z])(?=.*[!@#$%&\/=?_.-])).{7,15}$", ErrorMessage = "Password must have: between 7-15 characters, at least 3 of 4 types of characters including, lowercase, uppercase, digits, or special characters !@#$%&/=?_.")]
        [DefaultValue("")]
        public string password { get; set; }

        public enum States
        {
            AK, AL, AR, AS, AZ, CA, CO, CT, DC, DE, FL, GA, GU, HI, IA, ID, IL, IN, KS, KY, LA, MA, MD, ME, MI, MN, MO, MP, MS, MT, NC, ND, NE, NH, NJ, NM, NV, NY, OH, OK, OR, PA, PR, RI, SC, SD, TN, TX, UM, UT, VA, VI, VT, WA, WI, WV, WY
        }

        public enum Gender
        {
            M,
            F
        }
    }
}