using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBeerHouse.Models
{
    public class UserInformation
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ChangePassword { get; set; }
        public string Email { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public string ReturnUrl { get; set; }
        public string[] Roles { get; set; }
    }
}
