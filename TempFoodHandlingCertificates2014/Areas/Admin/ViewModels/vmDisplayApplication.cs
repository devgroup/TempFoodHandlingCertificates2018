using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

    namespace TempFoodHandlingCertificates2014.Areas.Admin.ViewModels
{
    public class vmDisplayApplication
    {
        public int Id { get; set; }

        [Display(Name ="Application number")]
        public int ApplicationNumber { get; set; }

        [Display(Name ="Event name")]
        public string EventName { get; set; }
        [Display(Name ="Event date")]
        public DateTime EventDate { get; set; }

        public String DisplayEventDate
        {
            get
            {
                return this.EventDate.ToString("dd MMM yyyy");
            }
        }

        [Display(Name ="Other event dates")]
        public string OtherEventDates { get; set; }


        [Display(Name ="Business name of stall")]
        public string BusinessStallName { get; set; }

        [Display(Name ="Applicant name")]
        public string ApplicantName { get; set; }

        [Display(Name="Type of food")]
        public string TypeOfFood { get; set; }

        //The following properties found on ProcessApplication Table
        [Display(Name ="Amount due")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
       // [UIHint("Currency")]
        public decimal? AmountDue { get; set; }

        [Display(Name ="Receipt number")]
        public string ReceiptNumber { get; set; }


        [Display(Name ="Is payment received?")]
        public bool IsPaymentReceived { get; set; }
        

        public string DisplayIsPaymentReceived
        {
            get
            {
                if (this.IsPaymentReceived)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }
        public string CreatedOrFound { get; set; }
    }
}
