
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TempFoodHandlingCertificates2014.Areas.Admin.ViewModels;
using TempFoodHandlingCertificates2014.Areas.Enquiry.ViewModels;
using TempFoodHandlingCertificates2014.Models;

namespace TempFoodHandlingCertificates2014.Areas.Enquiry.Controllers
{
    [Authorize(Users ="Enquiry@hobartcity.com.au,health@hobartcity.com.au")]
    public class EnquireController : Controller
    {
        private TempFoodStallEDMContainer db = new TempFoodStallEDMContainer();

        [HttpGet]
        public ActionResult Default()
        {
            vmSearchApplication vmSearchApplication = new vmSearchApplication();

            //Search date from start of previous month
            var now = DateTime.Now.Date;
            DateTime dtStartSearchDateFrom = new DateTime(now.Year, now.Month, 1).AddMonths(-1).Date;

            vmSearchApplication.ApplicationSubmittedSearchDateFrom = dtStartSearchDateFrom;

            vmSearchApplication.ApplicationList = FilterApplicationList(vmSearchApplication);
            return View(vmSearchApplication);
            
        }

        [HttpPost]
        public ActionResult Default(vmSearchApplication vmSearchApplication)
        {
            vmSearchApplication.ApplicationList = FilterApplicationList(vmSearchApplication);
            return View(vmSearchApplication);
        }



        private List<vmApplication> FilterApplicationList(vmSearchApplication vmSearchApplication)
        {

            Func<string, List<string>> splitString
                = x =>
                    {
                        List<string> result = new List<string>();
                        if (!String.IsNullOrWhiteSpace(x))
                        {
                            result = x.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            //result.ForEach(e => e.Trim());
                        }

                        return result;
                    };

            //We are searching for these needles.
            List<string> nameSearchList = splitString(vmSearchApplication.NameSearch);

            Predicate<vmApplication> filter =
                app
                =>
                {

                    bool nameSearch = String.IsNullOrWhiteSpace(vmSearchApplication.NameSearch)
                                        || nameSearchList.All(e => String.Concat(app.ApplicantName, app.StallBusinessName).ToLower().Contains(e));


                    bool eventNameSearch = String.IsNullOrWhiteSpace(vmSearchApplication.EventNameSearch)
                                        || splitString(vmSearchApplication.EventNameSearch).All(e => app.EventName.ToLower().Contains(e));


                    bool foodType = String.IsNullOrWhiteSpace(vmSearchApplication.FoodTypeSearch)
                                        || ((String.IsNullOrWhiteSpace(app.TypeOfFood) && (vmSearchApplication.FoodTypeSearch == null))) //Can't search on food type if it is null
                                        || ((app.TypeOfFood != null) && (splitString(vmSearchApplication.FoodTypeSearch)).All(e => app.TypeOfFood.ToLower().Contains(e)));

                    DateTime? searchSubmissionDateStart = vmSearchApplication.ApplicationSubmittedSearchDateFrom;


                    bool searchSubmissionDate =(
                                                //No start date and no end date
                                                
                                                (!vmSearchApplication.ApplicationSubmittedSearchDateFrom.HasValue
                                                && (!vmSearchApplication.ApplicationSubmittedSearchDateTo.HasValue)
                                                )



                                                //Start date but no end date
                                                ||
                                                (
                                                vmSearchApplication.ApplicationSubmittedSearchDateFrom.HasValue
                                                  && (!vmSearchApplication.ApplicationSubmittedSearchDateTo.HasValue)
                                                  && (app.SubmittedDate >= vmSearchApplication.ApplicationSubmittedSearchDateFrom.Value)
                                                  )
                                                //No start date but has end date
                                                ||
                                                (
                                                    (!vmSearchApplication.ApplicationSubmittedSearchDateFrom.HasValue)
                                                    && (vmSearchApplication.ApplicationSubmittedSearchDateTo.HasValue)
                                                    && app.SubmittedDate <= vmSearchApplication.ApplicationSubmittedSearchDateTo.Value

                                                )
                                                //Start ddate and end date
                                                ||
                                                (
                                                    (vmSearchApplication.ApplicationSubmittedSearchDateFrom.HasValue)
                                                    && (vmSearchApplication.ApplicationSubmittedSearchDateTo.HasValue)
                                                    
                                                    && (app.SubmittedDate >= vmSearchApplication.ApplicationSubmittedSearchDateFrom.Value)
                                                    && (app.SubmittedDate <= vmSearchApplication.ApplicationSubmittedSearchDateTo.Value)
                                                )
                                                );


                    bool searchStallDate = (
                                               //No start date and no end date

                                               (!vmSearchApplication.StallSearchDateFrom.HasValue
                                               && (!vmSearchApplication.StallSearchDateTo.HasValue)
                                               )



                                               //Start date but no end date
                                               ||
                                               (
                                               vmSearchApplication.StallSearchDateFrom.HasValue
                                                 && (!vmSearchApplication.StallSearchDateTo.HasValue)
                                                 && (app.EventStartDate >= vmSearchApplication.StallSearchDateFrom.Value)
                                                 )
                                               //No start date but has end date
                                               ||
                                               (
                                                   (!vmSearchApplication.StallSearchDateFrom.HasValue)
                                                   && (vmSearchApplication.StallSearchDateTo.HasValue)
                                                   && app.EventStartDate <= vmSearchApplication.StallSearchDateTo.Value

                                               )
                                               //Start ddate and end date
                                               ||
                                               (
                                                   (vmSearchApplication.StallSearchDateFrom.HasValue)
                                                   && (vmSearchApplication.StallSearchDateTo.HasValue)

                                                   && (app.EventStartDate >= vmSearchApplication.StallSearchDateFrom.Value)
                                                   && (app.EventStartDate <= vmSearchApplication.StallSearchDateTo.Value)
                                               )
                                               );


                    return nameSearch && eventNameSearch && foodType && searchSubmissionDate && searchStallDate;


                };
            


            return db.Applications.Where(e => e.AppID != null).Select(e => new vmApplication()
            {
                ApplicantName = e.ApplicantDetails.ApplicantName,
                ApplicationId = e.AppID.Value,
                EventName = e.EventDetails.EventName,
                EventStartDate = e.EventDetails.EventStartDate.HasValue ? e.EventDetails.EventStartDate.Value : e.SubmittedDate.Value,
                //EventStartDate = e.EventDetails.EventStartDate,
                StallBusinessName = e.StallDetails.StallBusinessName,
                SubmittedDate = e.SubmittedDate.Value,
                TypeOfFood = e.FoodDetails.FoodDetailsTypeOfFood


            }).ToList()
            .Where(e => filter(e))
            .ToList();

        }

       [HttpGet]
        public ActionResult EditDetails(int id)
        {

            var app = db.Applications.Where(e => e.AppID == id).FirstOrDefault();
            if (app == null)
            {
                return HttpNotFound();
            }

            vmDisplayApplication vmDisplayApplication = RetrieveProperties_vmDisplayApplication(app);

            
           
            return View(vmDisplayApplication);
        }


        private vmDisplayApplication RetrieveProperties_vmDisplayApplication(int applicationId)
        {
            Models.Application app = 
                db.Applications.Where(e => e.AppID == applicationId)
                .FirstOrDefault();

            if (app != null)
            {
                return RetrieveProperties_vmDisplayApplication(app);
            }
            else
            {
                return null;
            }
        }

        private vmDisplayApplication RetrieveProperties_vmDisplayApplication(Application app)
        {
            var processApp = db.ProcessApplications.Where(e => e.Application.AppID == app.AppID).FirstOrDefault();

            string createOrFound = "Found"; //default


            if (processApp == null)
            {
                processApp = new ProcessApplication()
                {
                    Application = app,
                    GUID = app.GUID,
                    PaymentRecieved = false

                };
                createOrFound = "Create";
                db.ProcessApplications.Add(processApp);
                db.SaveChanges();
            }



            vmDisplayApplication vmDisplayApplication = new vmDisplayApplication()
            {
                //ApplicationNumber = app.AppID,
                AmountDue = processApp.AmountDue,
                ApplicantName = app.ApplicantDetails.ApplicantName,
                ApplicationNumber = app.AppID.Value,
                BusinessStallName = app.StallDetails.StallBusinessName,
                CreatedOrFound = createOrFound,
               // EventDate = app.EventDetails.EventStartDate.HasValue ? app.EventDetails.EventStartDate.Value : DateTime.Now.Date,
                //EventDate = app.EventDetails.EventStartDate,
                EventName = app.EventDetails.EventName,
                IsPaymentReceived = processApp.PaymentRecieved,
                OtherEventDates = app.EventDetails.OtherEventDates,
                ReceiptNumber = processApp.ReceiptNumber,
                TypeOfFood = app.FoodDetails.FoodDetailsTypeOfFood,
                Id = app.Id
                
            };

            //Now handle event date.
            if (app.EventDetails.EventStartDate.HasValue)
            {
                vmDisplayApplication.EventDate = app.EventDetails.EventStartDate.Value;
            }
            else
            {
                if (app.SubmittedDate.HasValue)
                {
                    vmDisplayApplication.EventDate = app.SubmittedDate.Value;
                }
                else
                {
                    vmDisplayApplication.EventDate = DateTime.Now.Date;
                }
            }
            return vmDisplayApplication;
        }

        [HttpPost]
        public ActionResult EditDetails(vmDisplayApplication vmDisplayApplication)
        {
            if (ModelState.IsValid)
            {

                var processApp = 
                    db.ProcessApplications
                        .Where(e => e.Application.AppID == vmDisplayApplication.ApplicationNumber)
                        .FirstOrDefault();

                if (processApp != null)
                {

                    processApp.AmountDue = vmDisplayApplication.AmountDue;
                    processApp.ReceiptNumber = vmDisplayApplication.ReceiptNumber;
                    processApp.PaymentRecieved = vmDisplayApplication.IsPaymentReceived;


                    db.SaveChanges();

                    //Now decide if we are going to redirect page (or not!)
                    if (Request.Form["SaveAndReturn"] != null)
                    {
                        return RedirectToAction(actionName: "Default", controllerName: "Enquire", routeValues: new { area = "Enquiry" });
                    }
                   
                }
            }
            //Refresh properties on view model.
            vmDisplayApplication = RetrieveProperties_vmDisplayApplication(vmDisplayApplication.ApplicationNumber);
            if (vmDisplayApplication != null)
            {
                return View(vmDisplayApplication);
            }
            else
            {
                return HttpNotFound(String.Format("Application {0} not found", vmDisplayApplication.ApplicationNumber));
            }
        }
        // GET: Enquiry/Enquire
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.ProcessApplication);
            return View(applications.ToList());
        }

        // GET: Enquiry/Enquire/Details/5
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

        // GET: Enquiry/Enquire/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.ProcessApplications, "Id", "GUID");
            return View();
        }

        // POST: Enquiry/Enquire/Create
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

            ViewBag.Id = new SelectList(db.ProcessApplications, "Id", "GUID", application.Id);
            return View(application);
        }

        // GET: Enquiry/Enquire/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Id = new SelectList(db.ProcessApplications, "Id", "GUID", application.Id);
            return View(application);
        }

        // POST: Enquiry/Enquire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AppID,GUID,SubmittedDate,EventDetails,StallDetails,ApplicantDetails,StallType,FoodDetails,HowFoodsKeptCold,HowFoodsKeptHot,HandwashFacilities")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.ProcessApplications, "Id", "GUID", application.Id);
            return View(application);
        }

        // GET: Enquiry/Enquire/Delete/5
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

        // POST: Enquiry/Enquire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
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
