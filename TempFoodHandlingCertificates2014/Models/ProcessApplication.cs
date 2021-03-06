//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TempFoodHandlingCertificates2014.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessApplication
    {
        public ProcessApplication()
        {
            this.PaymentRecieved = false;
            this.CanIssueCertificate = true;
            this.IsDeleted = false;
        }
    
        public int Id { get; set; }
        public string GUID { get; set; }
        public bool PaymentRecieved { get; set; }
        public string PaymentReceivedDescription { get; set; }
        public Nullable<System.DateTime> PaymentLastCheckedDate { get; set; }
        public bool CanIssueCertificate { get; set; }
        public Nullable<System.DateTime> CertificateIssuedDate { get; set; }
        public Nullable<decimal> AmountDue { get; set; }
        public string Comments { get; set; }
        public string InspectionRequired { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReminderNotes { get; set; }
        public Nullable<System.DateTime> ReminderSent { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> NotificationSentDate { get; set; }
        public Nullable<bool> NotificationApproved { get; set; }
    
        public virtual Application Application { get; set; }
    }
}
