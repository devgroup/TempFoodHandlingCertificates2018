using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace TempFoodHandlingCertificates2014.Helpers
{
    public static class EmailHelper
    {
        
        public static void NotifyHealthOfficers(int AppID)
        {
            
            MailMessage mailMessage = new MailMessage();
            ObtainRecipients(mailMessage);
            string webLink = ObtainWebLink(AppID);

            mailMessage.Subject = "A new temporary food application has been lodged.";
            mailMessage.Body = "This email is to advise you that a new temporary food handling application has been lodged.  You can click on the link below to process this application.";

            mailMessage.Body = webLink;

            SmtpClient smtpClient = new SmtpClient();

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch
            {
                //Error intentionally not handled.
            }

        }

    
        public static string ObtainWebLink(int AppId)
        {

            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            
            return 
                   url.Action(
                               "Edit",
                               "Certificate",
                               new { Area = "Admin", id = AppId }
                               , HttpContext.Current.Request.Url.Scheme
                               );
        }
        public static void ObtainRecipients(MailMessage mailMessage)
        {
            string recipients = ConfigurationManager.AppSettings["EmailRecipients"];

            if (String.IsNullOrEmpty(recipients))
            {
                recipients = "wasst@hobartcity.com.au;goat@tas.farm.au";
            }

            List<string> recipientList = recipients.Split(';').ToList<string>();
            foreach (string recipient in recipientList)
            {
                mailMessage.To.Add(recipient);
            }
            
            
            mailMessage.From = new MailAddress("No-reply_TempFoodHandling@hobartcity.com.au");

           
        }
    }
}