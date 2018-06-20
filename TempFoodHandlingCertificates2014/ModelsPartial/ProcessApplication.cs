using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TempFoodHandlingCertificates2014.Models
{
    [MetadataType(typeof (ProcessApplicationMetadata))]
    public partial class ProcessApplication
    {
    }

    public class ProcessApplicationMetadata
    {
        public int Id { get; set; }
        [Display(Name="Is payment received?")]
        public bool PaymentRecieved { get; set; }
        public Nullable<System.DateTime> PaymentLastCheckedDate { get; set; }

        [Display(Name="Has reminder been sent?")]
        public Nullable<DateTime> ReminderSent { get; set; }

        [Display(Name="Notes")]
        public string ReminderNotes { get; set; }

        [Display(Name="Certificate can be issued")]
        public Nullable<bool> CanIssueCertificate { get; set; }

        [Display(Name="Date certificate issued")]
        public Nullable<System.DateTime> CertificateIssuedDate { get; set; }
        
        [Display(Name="Amount due")]
        public Nullable<decimal> AmountDue { get; set; }
       
        [Display(Name="Inspection Comments")]
        public string Comments { get; set; }

        [Display(Name="Inspection required")]
        public string InspectionRequired { get; set; }
        
        [Display(Name="Receipt number")]
        public string ReceiptNumber { get; set; }

        [Display(Name ="Archive")]
        public bool IsDeleted { get; set; }
        public virtual Application Application { get; set; }
    }
}