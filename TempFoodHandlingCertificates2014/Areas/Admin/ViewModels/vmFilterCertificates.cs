using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TempFoodHandlingCertificates2014.Helpers;


namespace TempFoodHandlingCertificates2014.Areas.Admin.ViewModels
{
    [MetadataType(typeof(vmFilterCertificatesMetadata))]
    public class vmFilterCertificates
    {

        public vmFilterCertificates()
        {
            this.RecordCount = 50;  //Default

            this.ShowArchived = false;

            ////Start of current week for DateSubmitted dates.
            //this.SubmittedDateStartSearch = DefaultSubmittedDateStartSearch;
            //this.SubmittedDateEndSearch = DefaultSubmittedDateEndSearch;
            //this.EventDateStartSearch = DefaultEventDateStartSearch;
            //this.EventDateEndSearch = DefaultEventDateEndSearch;

        }
        public int RecordCount { get; set; }
        public string PaymentAndCertificateFilter { get; set; }
        /*
         * One of the following:
         * No payment received.
         * Payment received but certificate not issued
         * Certificate issued.
         * No further processing required.  (eg not expecting future payment)
         */

        public List<string> PaymentAndCertificateFilterList
        {
            get
            {
                return new List<string>()
                {
                    "No restriction",
                    "No payment received",
                    "Certificate not issued, payment received",
                    "Certificate issued",
                    "Inspection Required - Green Team",
                    "Inspection Required - Red Team",
                    "Inspection Required - Any Team"
                   
                };
            }
        }

      
        public DateTime DefaultSubmittedDateStartSearch
        {
            get
            {
                return DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            }
        }

        public DateTime DefaultSubmittedDateEndSearch
        {
            get
            {
                return DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(6);
            }
        }
        public DateTime DefaultEventDateStartSearch
        {
            get
            {
                //return DateTime.Now.Date;
                return DateTime.Now.StartOfWeek(DayOfWeek.Monday);//.AddDays(7);
            }
        }

         public DateTime DefaultEventDateEndSearch
        {
            get
            {
                //return DateTime.Now.Date.AddDays(7);
                return DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7); //.AddDays(13);
            }
        }

        public int Id { get; set; }
        public string ApplicantNamePartialSearch { get; set; }


       
        [Display(Name = "Stall name")]
        public string StallBusinessNamePartialSearch { get; set; }

        public string EventNamePartialSearch { get; set; }

        public Nullable<DateTime> SubmittedDateStartSearch { get; set; }

        public string SubmittedDateStartSearchStringFormat
        {
            get
            {
                if (this.SubmittedDateStartSearch.HasValue)
                {
                    return this.SubmittedDateStartSearch.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    this.SubmittedDateStartSearch = DateTime.Parse(value);
                }
                catch(System.ArgumentNullException)
                {
                    this.SubmittedDateStartSearch = null;
                }
            }
        }
        public Nullable<DateTime> SubmittedDateEndSearch { get; set; }
        public string SubmittedDateEndSearchStringFormat
        {
            get
            {
                if (this.SubmittedDateEndSearch.HasValue)
                {
                    return this.SubmittedDateEndSearch.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    this.SubmittedDateEndSearch = DateTime.Parse(value);
                }
                catch(System.ArgumentNullException)
                {
                    this.SubmittedDateEndSearch = null;
                }
            }
        }

        public Nullable<DateTime> EventDateStartSearch { get; set; }
        public string EventDateStartSearchStringFormat
        {
            get
            {            
                if (this.EventDateStartSearch.HasValue)
                {
                    return this.EventDateStartSearch.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    this.EventDateStartSearch = DateTime.Parse(value);
                }
                catch (System.ArgumentNullException)
                {
                    this.EventDateStartSearch = null;
                }
            }
        }
        public Nullable<DateTime> EventDateEndSearch { get; set; }
        public string EventDateEndSearchStringFormat
        {
            get
            {
                if (this.EventDateEndSearch.HasValue)
                {
                    return this.EventDateEndSearch.Value.ToString("dd-MMM-yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    this.EventDateEndSearch = DateTime.Parse(value);
                }
                catch(System.ArgumentNullException)
                {
                    this.EventDateEndSearch = null;
                }
            }
        }

       

        public string DateSearchType { get; set; }  //DateSubmitted or Event Date
       
        public bool ShowArchived { get; set; }
    }

    public class vmFilterCertificatesMetadata
    {
        [Display(Name="Payment and certificate")]
        public string PaymentAndCertificateFilter { get; set; }

        [Display(Name="Name of applicant")]
        public string ApplicantNamePartialSearch { get; set; }

        [Display(Name="Name of event")]
        public string EventNamePartialSearch { get; set; }

        [Display(Name="Date submitted start")]
        public Nullable<DateTime> SubmittedDateStartSearch { get; set; }

        [Display(Name = "Date submitted end")]
        public Nullable<DateTime> SubmittedDateEndSearch { get; set; }
        
        [Display(Name="Event date start")]
        public Nullable<DateTime> EventDateStartSearch { get; set; }

        [Display(Name="Event date end")]
        public Nullable<DateTime> EventDateEndSearch { get; set; }

        [Display(Name="Maximum number of rows in results")]
        public int RecordCount { get; set; }

        [Display(Name = "Show archived only")]
        public bool ShowArchived { get; set; }
    }
   
}