using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TempFoodHandlingCertificates2014.Areas.Admin.ViewModels;
using TempFoodHandlingCertificates2014.Models;


namespace TempFoodHandlingCertificates2014.Areas.Admin.Controllers
{
    
    [Authorize(Users = "health@hobartcity.com.au")]

    public class CertificateController : Controller
    {
       
        private TempFoodStallEDMContainer db = new TempFoodStallEDMContainer();
     
        // GET: Admin/Certificate
        [Authorize()]
        public ActionResult Index()
        {
            //Default
            int maxRowsToShow = new ViewModels.vmFilterCertificates().RecordCount;


            ViewBag.FilterCertificates = new ViewModels.vmFilterCertificates();

            return View(db.Applications
                .OrderByDescending(e => e.AppID)
                .Where(e => ((e.ProcessApplication != null && !e.ProcessApplication.IsDeleted) || e.ProcessApplication == null))
                .Take(maxRowsToShow)
                .ToList());
               
        }
        [HttpPost]

        public ActionResult Index(ViewModels.vmFilterCertificates vmFilterCertificates)
        {

            //Start of region SetFilterParameters
            #region SetFilterParameters 
            if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Any date submitted"))
            {
                vmFilterCertificates.SubmittedDateStartSearch = null;
                vmFilterCertificates.SubmittedDateEndSearch = null;

            }

            if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Submitted this week"))
            {
                vmFilterCertificates.SubmittedDateStartSearch = vmFilterCertificates.DefaultSubmittedDateStartSearch;
                vmFilterCertificates.SubmittedDateEndSearch = vmFilterCertificates.DefaultSubmittedDateEndSearch;

                vmFilterCertificates.EventDateStartSearch = null;
                vmFilterCertificates.EventDateEndSearch = null;
            }


            if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Any event date"))
            {
                vmFilterCertificates.EventDateStartSearch = null;
                vmFilterCertificates.EventDateEndSearch = null;
            }

            //if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Events next week"))
            //if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Events next 7 days"))
            if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Events this week"))
            {
                vmFilterCertificates.EventDateStartSearch = vmFilterCertificates.DefaultEventDateStartSearch;
                vmFilterCertificates.EventDateEndSearch = vmFilterCertificates.DefaultEventDateEndSearch;

                vmFilterCertificates.SubmittedDateStartSearch = null;
                vmFilterCertificates.SubmittedDateEndSearch = null;
            }

            if (Request.Form["submit"] != null && Request.Form["submit"].Equals("Events next week"))
            {
                vmFilterCertificates.EventDateStartSearch = vmFilterCertificates.DefaultEventDateStartSearch.AddDays(7);
                vmFilterCertificates.EventDateEndSearch = vmFilterCertificates.DefaultEventDateEndSearch.AddDays(7);

                vmFilterCertificates.SubmittedDateStartSearch = null;
                vmFilterCertificates.SubmittedDateEndSearch = null;
            }
            #endregion
            //End of region SetFilterParameters

            ModelState.Clear();

            ViewBag.FilterCertificates = vmFilterCertificates;
           

            
        

                       

        //These submits are displayed as hyperlinks
        if ((Request.Form["application_action"] != null) && ((Request.Form["application_action"].ToLower().Contains("payment")) || (Request.Form["application_action"].ToLower().Contains("cert"))  
           || (Request.Form["application_action"].ToLower().Contains("reminder"))
           || (Request.Form["application_action"].ToLower().Contains("inspect"))
           || (Request.Form["application_action"].ToLower().Contains("approvenotification"))  //Notification approval added Oct 2016
            ))
        {

            string id = Request.Form["application_action"];
            string strAppId = String.Empty;
            int appId = 0;

            for (int i = 0; i < id.Length; i++)
            {
                if (Char.IsDigit(id[i]))
                {
                    strAppId = String.Concat(strAppId, id[i].ToString());
                }
            }

            if (Int32.TryParse(strAppId, out appId))
            {

                //See if there is an existing payment record
                Models.ProcessApplication processApplication =
                    db.ProcessApplications.Where(e => e.Application.AppID == appId).FirstOrDefault();

                if (processApplication == null)
                {
                    processApplication = new Models.ProcessApplication()
                    {
                        Application = db.Applications.Where(e => e.AppID == appId).FirstOrDefault(),
                       
                        // InspectionRequired = "false"  //TODO Make this nullable perhaps or not and maybe a boolean
                    };
                    //Assign guid
                   processApplication.GUID = db.Applications.Where(e => e.AppID == appId).FirstOrDefault().GUID;
                        
                    db.ProcessApplications.Add(processApplication);
                    db.SaveChanges();

                }

                if (id.ToLower().Contains("payment"))
                {
                    //Add or remove 
                    if (id.ToLower().Contains("record"))
                    {
                        processApplication.PaymentRecieved = true;
                        //Use the description field as string to accomodate additional
                        //'not required' description.
                        processApplication.PaymentReceivedDescription = "Yes";
                        db.SaveChanges();
                    }

                    if (id.ToLower().Contains("remove"))
                    {
                        processApplication.PaymentRecieved = false;
                        //Use the description field as string to accomodate additional
                        //'not required' description.
                        processApplication.PaymentReceivedDescription = "No";
                        db.SaveChanges();
                    }
                    //New method added 28 May 2015
                    if (id.ToLower().Contains("not required"))
                    {
                        processApplication.PaymentRecieved = false;  //Assume if payment not required, then not received.
                        processApplication.PaymentReceivedDescription = "Not required.";
                        db.SaveChanges();
                    }
                }
                else if (id.ToLower().Contains("cert"))
                {
                    if (id.ToLower().Contains("record"))
                    {
                        processApplication.CertificateIssuedDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    else if (id.ToLower().Contains("remove"))
                    {
                        processApplication.CertificateIssuedDate = null;
                        db.SaveChanges();
                    }
                }
                else if (id.ToLower().Contains("reminder"))
                {
                    if (id.ToLower().Contains("issue"))
                    {
                            processApplication.ReminderSent = DateTime.Now.Date;
                        db.SaveChanges();

                        //Put information in viewbag so email can be sent.
                        ViewBag.SendEmail = true;
                            //put model in the viewbag so that the email partial view can use this.

                            var applicationId = processApplication.Application.AppID.Value;

                            ViewBag.ApplicationId = applicationId;


                    }
                    else if (id.ToLower().Contains("revoke"))
                    {
                        processApplication.ReminderSent = null;
                        db.SaveChanges();
                    }
                    
                }
                /*This is changed from boolean field requires inspection
                to string field consisting of
                Green Team, Red Team, Not Required.
                Display one of the following:
                1.  Green Team
                2.  Red Team
                3.  Not Required
                4.  Default Display = Undecided and G, R, N as hyperlinks
                5. Items 1, 2 and 3 are hyperlinks that return to default.
                */
                //else if (id.ToLower().Contains("inspect"))
                //{
                //    if (id.ToLower().Contains("will"))
                //    {
                //        processApplication.InspectionRequired = true;
                //        db.SaveChanges();
                //    }
                //    else if (id.ToLower().Contains("wont"))
                //    {
                //        processApplication.InspectionRequired = false;
                //        db.SaveChanges();
                //    }
                //}
                else if (id.ToLower().Contains("inspect"))
                {
                    /*
                     * This can contain one of the following values:
                     * Green, Red, NotRequired
                     */


                    if (id.ToLower().Contains("notrequired"))
                    {
                        processApplication.InspectionRequired = "Not Required";
                    }
                    else if (id.ToLower().Contains("green"))
                    {
                        processApplication.InspectionRequired = "Green Team";
                    }
                    else if (id.ToLower().Contains("red"))
                    {
                        processApplication.InspectionRequired = "Red Team";
                    }
                    else if (id.ToLower().Contains("undecided"))
                    {
                        processApplication.InspectionRequired = String.Empty;
                    }
                    
                    db.SaveChanges();
                }
                else if (id.ToLower().Contains("approve"))
                    {

                        //Put information in viewbag so email can be sent.
                        ViewBag.SendEmailApproveNotification = true;

                        var applicationId = processApplication.Application.AppID.Value;

                        ViewBag.ApplicationId = applicationId;

                        processApplication.NotificationApproved = true;
                        processApplication.NotificationSentDate = DateTime.Now;
                        db.SaveChanges();
                    }
            }

        }

        return View(db.Applications
        .Where(e => e.SubmittedDate != null)
        .OrderByDescending(e => e.AppID)
        .ToList()
        .Where(e => Admin.Helpers.ApplicationFilter.FilterCertificates(e, vmFilterCertificates))
        .Take(vmFilterCertificates.RecordCount)
        .ToList());
          
           
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult IssueCertificate(int id)
        {

            //Update the certificate issued date


            //Determine if we can find the process application record in the database.
            Models.ProcessApplication processApplication = 
                    db.ProcessApplications.Where(e => e.Application.AppID == id).FirstOrDefault();


            //Todo - this can't be right!!

            if (processApplication == null)
            {
                processApplication = new Models.ProcessApplication();
             //   processApplication.GUID =  Guid.NewGuid().ToString();
                processApplication.Application = db.Applications.Where(e => e.AppID == id).FirstOrDefault();
                db.ProcessApplications.Add(processApplication);
            }

            processApplication.CertificateIssuedDate = DateTime.Now;
            processApplication.GUID = processApplication.Application.GUID;

            db.SaveChanges();
            //end of update certificate issued date


            Admin.ViewModels.vmIssueCertificate vmIssueCertificate =
                        db.Applications.Where(e => e.AppID == id)
                        .Select(e => new Admin.ViewModels.vmIssueCertificate()
                        {
                            AppId = e.AppID.Value,
                            Id = e.Id,
                            ApplicantName = e.ApplicantDetails.ApplicantName,
                            EventName = e.EventDetails.EventName,
                            EventLocation = e.EventDetails.EventLocation,
                            //EventStartDate = e.EventDetails.EventStartDate.HasValue ? e.EventDetails.EventStartDate : null,
                            EventStartDate = e.EventDetails.EventStartDate,
                            OtherEventDates = e.EventDetails.OtherEventDates,
                            StallBusinessName = e.StallDetails.StallBusinessName,
                            EmailAddress = e.ApplicantDetails.ApplicantEmail,
                            GUID = e.GUID
                        }).FirstOrDefault();

            ViewBag.ShowEmailLink = true;
            return View(vmIssueCertificate);
        }

        // GET: Admin/Certificate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Admin/Certificate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Certificate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AppID,GUID,SubmittedDate,EventDetails,StallDetails,ApplicantDetails,StallType,FoodDetails,HowFoodsKeptCold,HowFoodsKeptHot,HandwashFacilities")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Admin/Certificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Application application;

            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {
                application = db.Applications.Where(e => e.AppID == id).FirstOrDefault();

                if (application == null)
                {
                    return HttpNotFound();
                }

                

                //Create a new record and save this to the databse.
                if (application.ProcessApplication == null)
                {
                    Models.ProcessApplication processApplication = new Models.ProcessApplication()
                    {
                        Application = application,
                        GUID = application.GUID
                    };
                    application.ProcessApplication = processApplication;
                    db.SaveChanges();
                }
                dbContextTransaction.Commit();
            }
            Admin.ViewModels.vmProcessApplication vmProcessApplication
                     = new Admin.ViewModels.vmProcessApplication();

            vmProcessApplication.AppId = id.Value;
            vmProcessApplication.Id = application.Id;
            
            vmProcessApplication.ProcessApplication = application.ProcessApplication;
            vmProcessApplication.Application = application;
           
            return View(vmProcessApplication);
        }

        // POST: Admin/Certificate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(vmProcessApplication vmProcessApplication)
        {
            if (ModelState.IsValid)
            {

                var AppId = vmProcessApplication.AppId;


                Models.ProcessApplication existingProcessApplication =
                    db.ProcessApplications.Where(e => e.Application.AppID == vmProcessApplication.AppId).FirstOrDefault();



                db.Entry(existingProcessApplication).CurrentValues.SetValues(vmProcessApplication.ProcessApplication);

                
                db.Entry(existingProcessApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmProcessApplication);
        }

        // GET: Admin/Certificate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Admin/Certificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Admin/Certificate/EditApplication/5
        public ActionResult EditApplication(int id)
        {
            Application application = db.Applications.Where(e => e.Id == id).FirstOrDefault();
            if (application == null)
            {
                return HttpNotFound("Unable to retrieve this application.");
            }

            return View(application);

        }

        public ActionResult UpdateEventDetails (int id)
        {
            //Note - the id above is the app Id
            TempFoodHandlingCertificates2014.Models.EventDetails eventDetails =
                db.Applications.Where(e => e.AppID == id).FirstOrDefault().EventDetails;
            
            if (eventDetails == null)
            {
                return HttpNotFound("Unable to retrieve this application");
            }
            eventDetails.Id = id;
            return View(eventDetails);

        }

        [HttpPost]
        public ActionResult UpdateEventDetails(Models.EventDetails eventDetails)
        {
            if (ModelState.IsValid)
            {
                Models.Application application = db.Applications
                                                    .Where(e => e.AppID == eventDetails.Id)
                                                    .FirstOrDefault();
               
                application.EventDetails = eventDetails;

                db.SaveChanges();
                return RedirectToAction("Edit", new { id = eventDetails.Id});

            }

            return View(eventDetails);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
