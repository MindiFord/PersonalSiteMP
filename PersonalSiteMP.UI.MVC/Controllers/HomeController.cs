using System.Web.Mvc;
using PersonalSiteMP.UI.MVC.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System;

namespace PersonalSiteMP.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message:<br/>" +
                    $"{cvm.Message}<br/>" +
                    $"<strong>from the email address:</strong> {cvm.Email}.";

                MailMessage mm = new MailMessage(
                // FROM address
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                // TO
                ConfigurationManager.AppSettings["EmailTo"].ToString(),
                // Subject
                null,
                // Email Body
                body);

                // Allow HTML in email (formatting br and strong above)
                mm.IsBodyHtml = true;
                // Sets email as high priority in my email account
                mm.Priority = MailPriority.High;
                // reply to person who filled out form (not my domain email)
                mm.ReplyToList.Add(cvm.Email);

                // Configure mail client - creds stored in web.config
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
                // Configure email credentials using web.config
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
                    ConfigurationManager.AppSettings["EmailPass"].ToString());

                try
                {
                    // Try to send email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    // Log error in ViewBag to be seen by admins
                    ViewBag.CustomerMessage =
                        $"We're sorry your request could not be completed at this time. Please try again later.<br/>" +
                        $"Error Message: <br/> {ex.StackTrace}";
                    return View(cvm);
                }

                return View("EmailConfirmation", cvm);

            }

            return View(cvm);
        }

    }
}
