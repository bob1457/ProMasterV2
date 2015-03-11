using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

//Project references
//
//DI container, IoV implementation
//Interfaces between core and repository (database access and operation)
using ProMaster.Core.Property.ViewModels;
using ProMaster.DAL.Property;
using ProMaster.Infrastructure.Email;


namespace ProMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        #region DI Configuration

        IPropertyRepository _propertyRepository;

        public HomeController(IPropertyRepository propertyRepository)
        {
            ViewBag.CurrentPage = "home";

            _propertyRepository = propertyRepository;
        }

        #endregion

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "home";

            //ListingViewModel model = new ListingViewModel();
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.ShowAllProperty().Take(3);       //Actually it show property in listed status 

            IEnumerable<ListingViewModel> properties = _propertyRepository.ShowListedProperty().Take(3);

            
            //properties.FirstOrDefault(). = picutures.FirstOrDefault().ListPictureImgUrlThumb;
            //properties.FirstOrDefault().PictureCreateDate = picutures.FirstOrDefault().PictureCreateDate;

            //ModelState.Clear();
            //ModelState.AddModelError("Error", "Ex: This smaple error message");

            return View("Index", properties);
        }

        public JsonResult ContactUs(string name, string email, string tele, string category, string msg)
        {  

            string emailString = name + "\r\n" + "Email: " + email + "\r\n" + "Telephone: " + tele + "\r\n" + "Category: " + category + "\r\n" +
            "Message: " + msg; 

            SMTPService mail = new SMTPService();
            
            try
            {
                mail.SendMail("admin@promaster.com", "info@promaster.com", "Information Request", emailString, false);
            }
            catch (Exception)
            {
                throw;
            }
            

            return Json("Your request has been sent");
        }


        public ActionResult About()
        {
            ViewBag.CurrentPage = "about";

            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SiteSearch() //site wide quick search
        {
            return View();
        }

        public ActionResult Construction()
        {
            return View();
        }
    }
}
