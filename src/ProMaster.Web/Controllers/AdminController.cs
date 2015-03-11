using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMatrix.WebData;

using ProMaster.Infrastructure.UserProfile;
using ProMaster.Infrastructure.UserProfile.ViewModels;
using ProMaster.DAL.UserProfile;
using ProMaster.Core.ViewModels;

//using log4net;
using ProMaster.Infrastructure.Logging;

namespace ProMaster.Web.Controllers
{
    

    [Authorize(Roles = "SuperAdmin")]
    //[HandleError]
    public class AdminController : Controller
    {
        #region DI Configuration

        readonly IUserProfileRepository _userProfileRepository;

        public AdminController(IUserProfileRepository profileRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "admin";
            _userProfileRepository = profileRepository;
        }

        #endregion
        //
        // GET: /Admin/

        readonly Log4NetLogger _logger = new Log4NetLogger();

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "admin";
            return View();
        }

        //
        // GET: /Admin/Details/5       

        //
        // GET: /Admin/Create

        public ActionResult CreatePropertyManager()
        {
            var profile = new CreateProfileViewModel();

            return View("CreatePropertyManager", profile);
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [Authorize]//(Roles = "SuperAdmin")]
        public ActionResult CreatePropertyManager(FormCollection collection)
        {
            var manager = new PropertyManager(); //use the table as the  model as this is the one to be populated.

            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid) return View();

                manager.FirstName = Request.Form["FirstName"];
                manager.LastName = Request.Form["LastName"];
                manager.ContactEmail = Request.Form["ContactEmail"];
                manager.ContactTelephone1 = Request.Form["ContactTelephone1"];
                manager.ContactTelephone2 = Request.Form["ContactTelephone2"];

                manager.UserName = "TBA";
                manager.UserAvartaImgUrl = "/Content/Documents/Others/pm_default.png";
                manager.RoleId = 2;
                manager.IsActive = true;

                manager.CreationDate = DateTime.Now;
                manager.UpdateDate = DateTime.Now;

                _userProfileRepository.AddPropertyManager(manager);
                _userProfileRepository.Save();

                int pId = manager.PropertyManagerId;

                TempData["PID"] = pId;

                //Enable online access - forward to user creation page (register: will be changed)
                //                    
                //return RedirectToAction("Register", "Account");
                return RedirectToAction("EnableOnlineAccess", "Account", new { id = pId, rId = 2} );
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                return View();
            }
        }


        public ActionResult EditPropertyManager(int id)
        {
            //AdminViewModel model = new AdminViewModel();
            PropertyManager manager = _userProfileRepository.GetPropertyManagerById(id);
            //return View("ËditPropertyManager", model);
            return View(manager);
        }


        [HttpPost]
        [Authorize]
        public ActionResult EditPropertyManager(int id, AdminViewModel model)
        {
            PropertyManager manager = _userProfileRepository.GetPropertyManagerById(id);

            _userProfileRepository.GetUserByName(manager.UserName);

            int userId;

            userId = WebSecurity.GetUserId(manager.UserName);

            webpages_Membership member = _userProfileRepository.GetMember(userId);

            if (ModelState.IsValid)
            {
                manager.FirstName = Request.Form["FirstName"];
                manager.LastName = Request.Form["LastName"];
                manager.ContactEmail = Request.Form["ContactEmail"];
                manager.ContactTelephone1 = Request.Form["ContactTelephone1"];
                manager.ContactTelephone2 = Request.Form["ContactTelephone2"];
                manager.IsActive = model.IsActive;
                

                //bool status = false;
                switch (manager.IsActive)
                {
                    case true:
                        member.IsConfirmed = true;
                        break;
                    default:
                        member.IsConfirmed = false;
                        break;
                }

                //userAccount.Disabled = status;

                _userProfileRepository.Save();
            }
            

            return RedirectToAction("ManagerDetails/" + id, "Admin");
        }


        public ActionResult ManagerDetails(int id)
        {
            var model = new AdminViewModel();

            AdminViewModel manager = _userProfileRepository.GetManagerById(id);

            var managedProperty = _userProfileRepository.PropertyByPm(id);

            model.ManagedPropertyList = managedProperty;

            model.PropertyManagerId = manager.PropertyManagerId;
            model.PMFirstName = manager.PMFirstName;
            model.PMLastName = manager.PMLastName;
            model.PMEmail = manager.PMEmail;
            model.PMTelephone1 = manager.PMTelephone1;
            model.PMTelephone2 = manager.PMTelephone2;
            model.PMAvatarImgUrl = manager.PMAvatarImgUrl;

            return View("ManagerDetails", model);
        }

        public ActionResult DeleteManager(int id)
        {
            var manager = _userProfileRepository.GetPropertyManagerById(id);

            //userProfileRepository.DeleteManager(manager);
            manager.IsActive = false;
            manager.UpdateDate = DateTime.Now;

            _userProfileRepository.Save();

            //return Json("The property manager has been successfully deleted");        
            return Content(Boolean.TrueString);
        }
       


        public ActionResult ManagePMs()
        {
            IEnumerable<PropertyManager> managers = _userProfileRepository.ListedActivePropertyManagers();

            return View(managers);
            //return RedirectToAction("Construction", "Home");
        }

        public ActionResult GetActiveManagers()
        {
            IEnumerable<PropertyManager> manager = _userProfileRepository.ListedActivePropertyManagers();

            return PartialView("_ManagerList", manager);
        }

        public ActionResult GetAllManagers()
        {
            IEnumerable<PropertyManager> manager = _userProfileRepository.ListedAllPropertyManagers();

            return PartialView("_ManagerList", manager);
            
        }

        public ActionResult BackofficeManager()
        {


            return View();
        }

        public ActionResult BusinessAnalytics()
        {


            return View();
        }

        public ActionResult SystemManager()
        {


            return View();
        }


        public ActionResult Relations()
        {


            return View();
        }

        
    }
}
