using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TempFoodHandlingCertificates2014.Models;
using TempFoodHandlingCertificates2014.Areas.Admin;
using TempFoodHandlingCertificates2014.Areas;
using TempFoodHandlingCertificates2014.Areas.Admin.ViewModels;

namespace TempFoodHandlingCertificates2014.Controllers
{
    public class ViewCertificateController : Controller
    {


        private TempFoodStallEDMContainer db = new TempFoodStallEDMContainer();
        // GET: ViewCertificate
        public ActionResult Index(string guid)
        {
            
             TempFoodHandlingCertificates2014.Areas.Admin.ViewModels.vmIssueCertificate vmIssueCertificate =
                        db.Applications.Where(e => e.GUID == guid)
                        .Select(e => new vmIssueCertificate()
                        {
                            AppId = e.AppID.Value,
                            Id = e.Id,
                            ApplicantName = e.ApplicantDetails.ApplicantName,
                            EventName = e.EventDetails.EventName,
                            EventLocation = e.EventDetails.EventLocation,
                            //EventStartDate = e.EventDetails.EventStartDate.Value,
                            EventStartDate = e.EventDetails.EventStartDate,
                            StallBusinessName = e.StallDetails.StallBusinessName,
                            EmailAddress = e.ApplicantDetails.ApplicantEmail,
                            GUID = e.GUID
                        }).FirstOrDefault();

            // return View("~/Areas/Admin/Views/Certificate/IssueCertificate", vmIssueCertificate);
             return View("IssueCertificate", vmIssueCertificate);
        }
    }
}