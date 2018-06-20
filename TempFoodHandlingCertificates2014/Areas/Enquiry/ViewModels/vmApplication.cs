using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace TempFoodHandlingCertificates2014.Areas.Enquiry.ViewModels
{
    public class vmApplication
    {

        [Display(Name = "Application Number")]
        public int ApplicationId { get; set; }


        [Display(Name ="Submission date")]
        public DateTime SubmittedDate { get; set; }

        public string DisplaySubmittedDate
        {
            get
            {
                return this.SubmittedDate.ToString("dd MMM yyyy");
                //return String.Format("d-MMM-yy", this.SubmittedDate);
            }
        }

        [Display(Name ="Event name")]
        public string EventName { get; set; }

        [Display(Name ="Event date")]
        public DateTime EventStartDate { get; set; }

        public string DisplayEventStartDate
        {
            get
            {
                return this.EventStartDate.ToString("dd MMM yyyy");
            }
        }

        [Display(Name ="Business name of stall")]
        public string StallBusinessName { get; set; }

        [Display(Name ="Applicant name")]
        public string ApplicantName { get; set; }

        [Display(Name ="Type of food")]
        public string TypeOfFood { get; set; }
        
    }
}
