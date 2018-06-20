using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.Areas.Enquiry.ViewModels
{
    public class vmSearchApplication
    {
        [Display(Name ="Name search (applies to applicant and stall name)")]
        public string NameSearch { get; set; }
        [Display(Name ="Event name")]
        public string EventNameSearch { get; set; }

        [Display(Name ="Type of food")]
        public string FoodTypeSearch { get; set; }

        [Display(Name ="Search submission date from")]
        public DateTime? ApplicationSubmittedSearchDateFrom { get; set; }
        [Display(Name ="Search submission date to")]
        public DateTime? ApplicationSubmittedSearchDateTo { get; set; }

        [Display(Name ="Search on stall date from")]
        public DateTime? StallSearchDateFrom { get; set; }
        [Display(Name ="Search on stall date to")]
        public DateTime? StallSearchDateTo { get; set; }

       
        public List<vmApplication> ApplicationList { get; set; }
    }
}
