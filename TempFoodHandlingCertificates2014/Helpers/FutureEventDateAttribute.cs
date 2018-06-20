using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TempFoodHandlingCertificates2014.Helpers
{
    public class FutureEventDateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            
           return (value != null) && (((DateTime)value).Date >= DateTime.Now.Date.AddDays(5));
        }
       
    }
}