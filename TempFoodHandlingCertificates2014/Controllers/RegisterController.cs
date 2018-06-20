using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TempFoodHandlingCertificates2014.Models;

namespace TempFoodHandlingCertificates2014.Controllers
{
   
    public class RegisterController : Controller
    {
       
        private TempFoodStallEDMContainer db = new TempFoodStallEDMContainer();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        // GET: Page1
        public ActionResult Page1(string GUID)
        {
            ViewModels.vmPage1 vmPage1 = new ViewModels.vmPage1();
            if (!String.IsNullOrWhiteSpace(GUID))
            {
                vmPage1 = db.Applications
                                .Where(e => e.GUID == GUID)
                                .Select(e => new ViewModels.vmPage1()
                                {
                                    EventDetails = e.EventDetails,
                                    StallDetails = e.StallDetails,
                                    ApplicantDetails = e.ApplicantDetails,

                                })
                                .FirstOrDefault();

               // vmPage1.EventDetails.EventStartDateString = vmPage1.EventDetails.EventStartDate.ToShortDateString();
                               
            }
            return View(vmPage1);
        }

        [HttpPost]
        public ActionResult Page1(ViewModels.vmPage1 vmPage1, string GUID)
        {
           //TODO - Put GUID in controller route value.
          
            string buttonValue = Request.Form["submit"];
            Models.Application application;

            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(GUID))
                {
                    application = new Models.Application()
                    {
                        GUID = Guid.NewGuid().ToString(),
                        EventDetails = vmPage1.EventDetails,
                        StallDetails = vmPage1.StallDetails,
                        ApplicantDetails =vmPage1.ApplicantDetails
                    };

                    //DateTime eventStartDate = new DateTime();

                    //if (DateTime.TryParse(application.EventDetails.EventStartDateString,
                    //    out eventStartDate))
                    //{
                    //    application.EventDetails.EventStartDate = eventStartDate;
                    //}
                    //else
                    //{
                    //    throw new Exception(String.Format("Could not convert {0} to date format", application.EventDetails.EventStartDateString));

                    //}



                    db.Applications.Add(application);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        return View("_DbEntityValidationException", ex);
                    }
                    catch
                    {
                        //Do nothing
                    }
                }
                else
                {
                    //Existing application
                    application = db.Applications
                                        .Where(e => e.GUID == GUID)
                                        .FirstOrDefault();
                    //Set the relevant properties
                    application.EventDetails = vmPage1.EventDetails;
                    application.StallDetails = vmPage1.StallDetails;
                    application.ApplicantDetails = vmPage1.ApplicantDetails;
                    db.Entry(application).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        return View("_DbEntityValidationException", ex);
                    }
                    catch 
                    {
                        //Do nothing
                    }
                }
                
                return RedirectToAction("Page2", "Register", new { GUID = application.GUID });


            } //ModelState.IsValid
            //else
            //{
            //    foreach (var x in ModelState.Values.Where(e => e.Errors != null))
            //    {
            //        foreach (var y in x.Errors)
                    
            //        {
            //            var z = y.ErrorMessage;
            //        }
            //    }
            //}

            return View(vmPage1);
        }

        //GET:  Page2
        public ActionResult Page2(string GUID)
        {

            ViewModels.vmPage2 vmPage2 =
                db.Applications.Where(e => e.GUID == GUID)
                .Select(e => new ViewModels.vmPage2()
                {
                    Id = e.Id,
                    FoodDetails = e.FoodDetails,
                    StallType = e.StallType
                }).FirstOrDefault();

            return View(vmPage2);
           
        }

        [HttpPost]
        public ActionResult Page2(ViewModels.vmPage2 vmPage2, string GUID)
        {
            List<string> errMsgList = new List<string>();

            string buttonValue = Request.Form["submit"];

            //INPUTS AND OUTPUTS
            //1. ModelState Valid and No Redirect       - Save and no redirect
            //2. ModelState Valid and Redirect.         - Save and redirect
            //3. ModelState Invalid and No Redirect     -No save and re-display
            //4. ModelState Invalid and Redirect        -No save are redirect

            if (ModelState.IsValid)
            {
                Application application = db.Applications
                                                            .Where(e => e.GUID == GUID)
                                                          .FirstOrDefault();
                //Set the relevant properties
                application.StallType = vmPage2.StallType;
                application.FoodDetails = vmPage2.FoodDetails;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    if (!buttonValue.ToLower().Contains("prev"))
                    {
                        return View("_DbEntityValidationException", ex);
                    }
                   
                }
                catch 
                {
                    //Do nothing
                }


                if (buttonValue.ToLower().Contains("prev"))
                {
                    return RedirectToAction("Page1", new { GUID = GUID });
                }
                return RedirectToAction("Page3", new { GUID = GUID });
            }
            else
            {
                //Model state is not valid

                if (buttonValue.ToLower().Contains("prev"))
                {
                    return RedirectToAction("Page1", new { GUID = GUID });
                }
                return View(vmPage2);
            }
       
        }

        //GET: Page3
        public ActionResult Page3(string GUID)
        {

            Models.Application application = new Models.Application();
            if (!String.IsNullOrWhiteSpace(GUID))
            {
                application = db.Applications
                                .Where(e => e.GUID == GUID)
                                .FirstOrDefault();
            }
            //Attach TemperatureControl class
            ViewModels.vmPage3 vmPage3 = db.Applications
                                            .Where(e => e.GUID == GUID)
                                            .Select(e => new ViewModels.vmPage3()
                                            {
                                                TemperatureControl = new ViewModels.TemperatureControl()
                                                {
                                                    HowFoodsKeptCold = e.HowFoodsKeptCold,
                                                    HowFoodsKeptHot = e.HowFoodsKeptHot
                                                }
                                            }).FirstOrDefault();

           

            return View(vmPage3);
        }

        [HttpPost]
        public ActionResult Page3(ViewModels.vmPage3 vmPage3, string GUID)
        {

            string buttonValue = Request.Form["submit"];
            if (ModelState.IsValid)
            {
                
                Application application = db.Applications
                                                            .Where(e => e.GUID == GUID)
                                                          .FirstOrDefault();
                //Set the relevant properties
                application.HowFoodsKeptCold = vmPage3.TemperatureControl.HowFoodsKeptCold;
                application.HowFoodsKeptHot = vmPage3.TemperatureControl.HowFoodsKeptHot;
               
                db.SaveChanges();
                if (buttonValue.ToLower().Contains("prev"))
                {
                    return RedirectToAction("Page2", new { GUID = GUID });
                }
                return RedirectToAction("Page4", new { GUID = GUID });
            }
            else
            {

                if (buttonValue.ToLower().Contains("prev"))
                {
                    return RedirectToAction("Page2", new { GUID = GUID });
                }
                return View(vmPage3);
            }
        }

        //GET: Page4
        public ActionResult Page4(string GUID)
        {

            ViewModels.vmPage4 vmPage4 =
                db.Applications
                    .Where(e => e.GUID == GUID)
                    .Select(e => new ViewModels.vmPage4()
                    {
                        HandwashFacilities = e.HandwashFacilities
                    })
                    .FirstOrDefault();

           return View(vmPage4);
           
        }

        [HttpPost]
        public ActionResult Page4(ViewModels.vmPage4 vmPage4, string GUID)
        {
            string buttonValue = Request.Form["submit"];

            if (ModelState.IsValid)
            {
                Models.Application application = db.Applications
                                                    .Where(e => e.GUID == GUID)
                                                    .FirstOrDefault();

                application.HandwashFacilities = application.HandwashFacilities;
               
                db.SaveChanges();
                if (buttonValue.ToLower().Contains("prev"))
                {
                    return RedirectToAction("Page3", new { GUID = GUID });
                }
                return RedirectToAction("Page5", new { GUID = GUID });


            }
            if (buttonValue.ToLower().Contains("prev"))
            {
                return RedirectToAction("Page3", new { GUID = GUID });
            }
            return View(vmPage4);
        }

        //GET:  Page5
        public ActionResult Page5(string GUID)
        {
            ViewModels.vmPage5 vmPage5 = new ViewModels.vmPage5();

            return View(vmPage5);
        }

        [HttpPost]
        public ActionResult Page5(ViewModels.vmPage5 vmPage5 , string GUID)
        {
            
            string buttonValue = Request.Form["submit"];

            if (ModelState.IsValid)
            {
                Models.Application application = db.Applications
                                                           .Where(e => e.GUID == GUID)
                                                              .FirstOrDefault();
                application.SubmittedDate = DateTime.Now;
                //The AppId becomes the next submitted AppId
                TempData["SendEmail"] = true;
                if (application.AppID == null)
                {
                    application.AppID = db.Applications.Max(e => e.AppID) + 1;
                    TempData["SendEmail"] = true;
                }
                else
                {
                    TempData["SendEmail"] = false;
                }
                db.SaveChanges();

                return RedirectToAction("Confirm", new { id = application.AppID });
            }
            return View(vmPage5);

        }

        public ActionResult Confirm(int id)
        {
           

            if (TempData["SendEmail"] != null && TempData["SendEmail"] is bool && ((bool) TempData["SendEmail"]))
            {
                Helpers.EmailHelper.NotifyHealthOfficers(AppID: id);
            }

            var app = db.Applications.Where(e => e.AppID == id).FirstOrDefault().EventDetails;

            if (app != null && app.EventStartDate.HasValue && (app.EventStartDate.Value - DateTime.Now).TotalDays < 5 )
            {
                ViewBag.HighlightDateRestrictions = true;

            }
            else
            {
                ViewBag.HighlightDateRestrictions = false;
            }

            

            ViewBag.AppId = id;
            return View("Certificate");
            //return View("ApplicationReceived");
           
        }
    }
}