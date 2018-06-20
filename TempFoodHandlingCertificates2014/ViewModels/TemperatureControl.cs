using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.ViewModels
{
    [MetadataType(typeof(TemperatureControlMetadata))]
    public class TemperatureControl
    {
        public string HowFoodsKeptCold { get; set; }
        public string HowFoodsKeptHot { get; set; }

        //public List<string> HowFoodsKeptColdList
        //{
        //    get
        //    {
        //        //TODO Return this list from XML document
        //        return new List<string>()
        //        {
        //            "Eski with ice",
        //            "Refrigerated unit",
        //            "Chilled display unit",
        //            "Documented 2 hr 4 hr rule",
        //            "I am selling only non potentially hazardous foods.  ie biscuits / jam / bread / cake not containing cream"
        //        };
        //    }
        //}
        
    }

    public class TemperatureControlMetadata
    {
        [Display(Name="Cold foods must be kept at 5°C.  How will foods be kept cold?")]
        [Required(ErrorMessage="You must answer this question on how you intend to manage cold foods." )]
        public string HowFoodsKeptCold { get; set; }
     
        [Required(ErrorMessage="You must answer this question on how you intend to manage hot foods.")]
        [Display(Name = "Hot foods must be kept above 60°C and monitored with a thermometer.  How will you do this?")]
        public string HowFoodsKeptHot { get; set; }
    }
}