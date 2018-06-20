using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.ViewModels
{
    [MetadataType(typeof(vmPage2Metadata))]
    public class vmPage2
    {
        public int Id { get; set; }


        public Models.FoodDetails FoodDetails { get; set; }
        
        public string StallType { get; set; }
        
    }

    public class vmPage2Metadata
    {
         public Models.FoodDetails FoodDetails { get; set; }
        [Required(ErrorMessage="You must select the type of stall")]
        public string StallType { get; set; }
    }
}