using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempFoodHandlingCertificates2014.ViewModels
{
    public class vmPage1
    {
        public int Id { get; set; }

        public Models.EventDetails EventDetails { get; set; }
        public Models.StallDetails StallDetails { get; set; }
        public Models.ApplicantDetails ApplicantDetails { get; set; }

    }
}