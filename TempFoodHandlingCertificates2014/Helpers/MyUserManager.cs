using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using TempFoodHandlingCertificates2014.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TempFoodHandlingCertificates2014.Helpers
{
    public class MyUserManager : UserManager<ApplicationUser>
    {
        public MyUserManager() :
            base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
                      
            
            PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 4,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false,
              

            };
         
            
            
        }
    }
}