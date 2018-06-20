using System.Web.Mvc;

namespace TempFoodHandlingCertificates2014.Areas.Enquiry
{
    public class EnquiryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Enquiry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Enquiry_default",
                "Enquiry/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}