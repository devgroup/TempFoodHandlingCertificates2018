using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TempFoodHandlingCertificates2014.Helpers;

namespace TempFoodHandlingCertificates2014.ViewModels
{
    [MetadataType(typeof(vmPage5Metadata))]
    public class vmPage5
    {
        public bool ComplyHandwashFacilities { get; set; }
        public bool ComplyDHHSGuidelines { get; set; }
        public bool ComplyUnderstandPermit { get; set; }
    }
    


    public class vmPage5Metadata
    {
        [Display(Name = "My hand wash facilities comply with the requirement described above.")]
        [MustBeTrue(ErrorMessage="To receive a temporary food handling certificte your hand wash facilities must comply with the requirements described above.")]
        public bool ComplyHandwashFacilities { get; set; }

        [Display(Name = "I agree to comply with the requirements of the DHHS Guidelines for Temporary Food Stalls.")]
        [MustBeTrue(ErrorMessage = "To receive a temporary food handling certificate you must comply with the DHHS Guidelines for Temporary Food Stalls ")]
        public bool ComplyDHHSGuidelines { get; set; }

        [Display(Name = "I understand that I will only be permitted to sell the food listed on my application.")]
        [MustBeTrue(ErrorMessage = "To receive a temporary food handling certificate you must indicate that you understand that you will only be permitted to sell the food listed on your application. ")]
        public bool ComplyUnderstandPermit { get; set; }
    }
}