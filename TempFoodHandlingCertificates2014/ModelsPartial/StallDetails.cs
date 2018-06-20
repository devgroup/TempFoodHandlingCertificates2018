using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TempFoodHandlingCertificates2014.Models
{
    [MetadataType(typeof(StallDetailsMetadata))]
    public partial class StallDetails
    {
        public int Id { get; set; }
    }

    public class StallDetailsMetadata
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name="Business name of stall")]
        [Required(ErrorMessage="The business name of the stall is compulsory")]
        public string StallBusinessName { get; set; }

        [Display(Name="Start time")]
        public string StallStartTime { get; set; }

        [Display(Name="Finish time")]
        public string StallFinishTime { get; set; }
    }
}