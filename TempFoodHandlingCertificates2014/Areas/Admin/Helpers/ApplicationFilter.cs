using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempFoodHandlingCertificates2014.Areas.Admin.ViewModels;
using TempFoodHandlingCertificates2014.Models;

namespace TempFoodHandlingCertificates2014.Areas.Admin.Helpers
{
    public class ApplicationFilter
    {
      //   Predicate<Models.Application> MyFilter = delegate(Models.Application application)
        public static bool FilterCertificates (Application application, vmFilterCertificates vmFilterCertificates)
        {
            bool result = true;
            

            /* Additional filter always required - submitted date is not Null */
            /* Fix bug on 28 May 2015 */

            bool cond0 = application.SubmittedDate.HasValue && application.AppID.HasValue;


            /*
             * condA = Filter selection on payment and certificate
             * condB = Filter selection based on applicant partial name
             * condC = Filter selection based on submission dates
             * condC = Filter selection based on event dates.
             * condD = Filter on applicant name
             * condE = Filter on stall business name
             * condF = Filter on event name
             * 
             * These conditions added.
             *   "Inspection Required - Green Team",
                    "Inspection Required - Red Team",
                    "Inspection Required - Any Team"
             */
             
            //Used for splitting search strings.
            char[] mySplitter = new char[2] { ' ', ',' };


            bool condA = true;  //Payment and certificate
            bool condB = true;  //Applicant name search
            bool condC = true;  //Submitted date
            bool condD = true;  //Event date
            bool condE = true;  //Business name
            bool condF = true;  //Event name
            bool condG = true;  //ShowOnlyArchived
           
            //Set filter on condition A - payment and certificate.
            string paymentAndCerticateFilter = vmFilterCertificates.PaymentAndCertificateFilter;

            switch (paymentAndCerticateFilter)
            {
                case "No payment received":
                    {
                        condA = application.ProcessApplication == null ||
                            //!application.ProcessApplication.PaymentRecieved.HasValue;
                            !application.ProcessApplication.PaymentRecieved; 
                    }
                    break;
                case "Certificate not issued, payment received":
                    {
                        
                        condA = ((application.ProcessApplication != null) && ((
                            application.ProcessApplication.PaymentRecieved)
                          //  application.ProcessApplication.PaymentRecieved.Value)
                           // && (application.ProcessApplication.CertificateIssuedDate == null)));
                            && (!application.ProcessApplication.CertificateIssuedDate.HasValue)));
                    }
                    break;
                case "Certificate issued":
                    {
                        condA = ((application.ProcessApplication != null) && (application.ProcessApplication.CertificateIssuedDate.HasValue));
                    }
                    break;
                case "Inspection Required - Green Team":
                    {
                        condA = ((application.ProcessApplication != null) &&
                            (application
                            .ProcessApplication
                            .InspectionRequired != null)  &&
                             (application
                            .ProcessApplication
                            .InspectionRequired
                            .ToLower()
                            .Contains("green")
                            ));
                    }
                    break;
                case "Inspection Required - Red Team":
                    condA = ((application.ProcessApplication != null) &&
                            (application
                            .ProcessApplication
                            .InspectionRequired != null) &&
                            (application
                            .ProcessApplication
                            .InspectionRequired
                            .ToLower()
                            .Contains("red")
                            ));
                    break;
                case "Inspection Required - Any Team":
                    condA = ((application.ProcessApplication != null) &&
                             (application
                            .ProcessApplication
                            .InspectionRequired != null) &&
                            (application
                            .ProcessApplication
                            .InspectionRequired
                            .ToLower()
                            .Contains("team")
                            ));
                    break;
                default:
                    break;
            }
           
           
            //Filter on applicant name search.
      
            condB = String.IsNullOrWhiteSpace(vmFilterCertificates.ApplicantNamePartialSearch)
                            ||
                            (!String.IsNullOrWhiteSpace(application.ApplicantDetails.ApplicantName.ToLower())
                //&& application.ApplicantDetails.ApplicantName.ToLower().Contains((vmFilterCertificates.ApplicantNamePartialSearch.ToLower())));
                            && vmFilterCertificates.ApplicantNamePartialSearch.Split(mySplitter).ToList()
                            .All(e => application.ApplicantDetails.ApplicantName.ToLower().Contains(e.ToLower())));



            //Filter on date submitted.
            if (vmFilterCertificates.SubmittedDateStartSearch.HasValue)
            {
                 if (vmFilterCertificates.SubmittedDateEndSearch.HasValue)
                 {
                     condC = application.SubmittedDate.HasValue 
                         && (application.SubmittedDate.Value.Date >= vmFilterCertificates.SubmittedDateStartSearch.Value.Date)
                         && (application.SubmittedDate.Value.Date <= vmFilterCertificates.SubmittedDateEndSearch.Value.Date);
                 }
                 else
                 {
                     condC = application.SubmittedDate.HasValue
                         && (application.SubmittedDate.Value.Date == vmFilterCertificates.SubmittedDateStartSearch.Value.Date);
                 }
            }

            //Filter on event date.
            if (vmFilterCertificates.EventDateStartSearch.HasValue)
            {
                if (vmFilterCertificates.EventDateEndSearch.HasValue)
                {
                    condD = application.EventDetails.EventStartDate.HasValue
                        && (application.EventDetails.EventStartDate.Value.Date >= vmFilterCertificates.EventDateStartSearch.Value.Date)
                        && (application.EventDetails.EventStartDate.Value.Date <= vmFilterCertificates.EventDateEndSearch.Value.Date);

                    //condD = 
                    //    (application.EventDetails.EventStartDate.Date >= vmFilterCertificates.EventDateStartSearch.Value.Date)
                    //    && (application.EventDetails.EventStartDate.Date <= vmFilterCertificates.EventDateEndSearch.Value.Date);
                }
                else
                {
                    condD = application.SubmittedDate.HasValue
                        && application.SubmittedDate.Value.Date == vmFilterCertificates.SubmittedDateStartSearch.Value.Date;
                }
            }
            
            //Filter on Stall business name
            condE = String.IsNullOrWhiteSpace(vmFilterCertificates.StallBusinessNamePartialSearch)
                            ||
                            (!String.IsNullOrWhiteSpace(application.StallDetails.StallBusinessName.ToLower())

                            && vmFilterCertificates.StallBusinessNamePartialSearch.Split(mySplitter).ToList()
                                    .All(e => application.StallDetails.StallBusinessName.ToLower().Contains(e.ToLower())));



            //D
            //Filter on event name
            condF = String.IsNullOrWhiteSpace(vmFilterCertificates.EventNamePartialSearch)
                            ||
                            (!String.IsNullOrWhiteSpace(application.EventDetails.EventName.ToLower())

                            && vmFilterCertificates.EventNamePartialSearch.Split(mySplitter).ToList()
                            .All(e => application.EventDetails.EventName.ToLower().Contains(e.ToLower())));

            /* 28 May - Introduce condition cond0 to fix bug */

            //29 Sept 2016 - Introduce Archive Flag for condition G

            condG = vmFilterCertificates.ShowArchived == ((application.ProcessApplication != null) && (application.ProcessApplication.IsDeleted));

            return result = cond0 && condA && condB && condC && condD && condE && condF && condG;



        }
    }
}