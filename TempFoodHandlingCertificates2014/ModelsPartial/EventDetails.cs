using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TempFoodHandlingCertificates2014.Helpers;
using System.Web.Mvc;

namespace TempFoodHandlingCertificates2014.Models
{
    [MetadataType(typeof(EventDetailsMetadata))]
    public partial class EventDetails
    {
        //Use this as the AppId
        public int Id { get; set; }
        //[Required(AllowEmptyStrings =false,ErrorMessage = "You are required to provide the event start date")]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        //public string EventStartDateString { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "You are required to provide the event start date")]
        public string EventStartDateDisplay
        {
            get
            {
                if (!this.EventStartDate.HasValue)
                {
                    this.EventStartDate = DateTime.Now.Date;
                }
                return this.EventStartDate.Value.ToString("dd-MMM-yyyy");
                
            }
            set
            {
                DateTime eventStart;
                if (DateTime.TryParse(value, out eventStart))
                {
                    this.EventStartDate = eventStart;
                }
                else
                {
                    //Very questionable code.
                    this.EventStartDate = DateTime.Now.Date;
                }
                //this.EventStartDate = DateTime.Parse(value);
            }
        }

    }

        public class EventDetailsMetadata
        {
            //[HiddenInput]
            //public int Id { get; set; }

            [System.ComponentModel.DataAnnotations.Display(Name = "Name of event")]
            [Required(ErrorMessage = "The name of the event must be provided.")]
            public string EventName { get; set; }

            [Display(Name = "Location of event")]
            [Required(ErrorMessage = "The address of the event must be provided.")]
            public string EventLocation { get; set; }

            [Display(Name = "Event description")]
            public string EventDescription { get; set; }

            // [Display(Name="Event start date")]
            // [Required(ErrorMessage="You mst nominate a date for the event from the calendar")]
            //// [FutureEventDate(ErrorMessage="The event must be at least 5 days away")]
            // [DataType(DataType.DateTime)]
            // [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:dd/MM/yyyy}")]
            //public Nullable<System.DateTime> EventStartDate { get; set; }

            [Display(Name = "Event start date")]
            [Required(ErrorMessage = "Event start date must be provided.")]
            public DateTime EventStartDate { get; set; }


            [Display(Name = "Other event dates")]
            public string OtherEventDates { get; set; }
        }
    
}