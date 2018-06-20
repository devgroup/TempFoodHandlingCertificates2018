using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.ViewModels
{
    [MetadataType(typeof(vmPage4_Metadata))]
    public class vmPage4
    {
        
        public string HandwashFacilities { get; set; }
    }

    public class vmPage4_Metadata
    {
        [Required(ErrorMessage = "Please provide an answer to this question regarding hand washing facilities.")]
        public string HandwashFacilities { get; set; }
    }
}