using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TempFoodHandlingCertificates2014.Areas.Admin.ViewModels
{
    public class vmIssueCertificate

    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string ApplicantName { get; set; }
        public string StallBusinessName { get; set; }
        public string EventName { get; set; }

     
        //public DateTime EventStartDate { get; set; }
        public Nullable<DateTime> EventStartDate { get; set; }

        public string DisplayEventStartDate
        {
            get
            {
                string result = string.Empty;

                if (this.EventStartDate.HasValue)
                {
                    result = EventStartDate.Value.ToShortDateString();
                }
                return result;
            }
        }
        public DateTime EventEndDate { get; set; }

        public string OtherEventDates { get; set; }

        public bool RequiresAllocationToHealthOfficer { get; set; }
        public string HealthOfficerAllocatedTo { get; set; }
        public string EmailAddress { get; set; }
        public string GUID { get; set; }


     //   public List<Tuple<int, string>> HealthOfficers { get; set; }

        public string EventLocation { get; set; }
        public bool IsPartOfLargerEvent { get; set; }

    }
}