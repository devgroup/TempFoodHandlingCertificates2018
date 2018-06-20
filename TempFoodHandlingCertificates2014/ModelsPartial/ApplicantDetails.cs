using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TempFoodHandlingCertificates2014.Models
{
    [MetadataType(typeof(ApplicantDetailsMetadata))]
    public partial class ApplicantDetails
    {
        public int Id { get; set; }
    }

    public class ApplicantDetailsMetadata
    {

        [Display(Name="Name of stall holder (Person)")]
        [Required(ErrorMessage="The name of the stall holder is compulsory.")]
        public string ApplicantName { get; set; }

        [Display(Name = "Phone number")]
        public string ApplicantPhoneNumber { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage="You must provide your email address.")]
        [EmailAddress(ErrorMessage="This email address is not in an accepted form.")]
        public string ApplicantEmail { get; set; }

        [Display(Name = "Postal address")]
        public string ApplicantPostalAddress { get; set; }


    }
}