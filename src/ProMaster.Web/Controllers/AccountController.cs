using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Helpers;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using ProMaster.Infrastructure.UserProfile;
using WebMatrix.WebData;

using ProMaster.Web.Filters;
using ProMaster.Web.Models;


using ProMaster.DAL.UserProfile;
using ProMaster.Infrastructure.UserProfile.ViewModels;
using ProMaster.Infrastructure.Utilities;
using ProMaster.Infrastructure.Email;
using ProMaster.DAL.Tenant;
//using ProMaster.DAL.UserProfile;
using ProMaster.DAL.Landlord;
using ProMaster.Infrastructure.Logging;

//using log4net;

namespace ProMaster.Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {        
        #region DI Configuration

        readonly IUserProfileRepository _userProfileRepository;
        readonly ITenantRepository _tenantRepository;
        readonly IPropertyOwnerRepository _propertyOwnerRepository;

        public AccountController(IUserProfileRepository profileRepository, ITenantRepository tenantRepository, IPropertyOwnerRepository propertyOwnerRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "home";

            _userProfileRepository = profileRepository;
            _tenantRepository = tenantRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
        }

        #endregion

        Log4NetLogger logger = new Log4NetLogger();

        #region Authentication
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {            
            //log4net.ILog logger = LogManager.GetLogger(typeof(AccountController));
            //Log4NetLogger logger = new Log4NetLogger();

            try
            {
                if(model.UserName != null )
                {

                    //bool status = userProfileRepository.GetUserStatus(model.UserName);

                    //if (status == false)
                    //{
                        if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
                        {
                            //return RedirectToLocal(returnUrl);

                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToLocal(returnUrl);
                            }
                            //  Redirect based on user role if there is no return URL

                            string[] roles = Roles.GetRolesForUser(model.UserName);

                            if (roles.Contains("SuperAdmin"))
                            {
                                logger.Info("The super admin " + model.UserName + " logged in.");
                                return RedirectToAction("Index", "Admin");                                    
                            }
                            if (roles.Contains("PropertyManager"))
                            {
                                logger.Info("The property manager " + model.UserName + " logged in.");
                                return RedirectToAction("Index", "Manage");
                            }
                            logger.Info("The client " + model.UserName + " logged in.");
                            return RedirectToAction("Index", "ClientPortal");
                        }
                    //}

                    // If we got this far, something failed, redisplay form
                    ModelState.AddModelError("", "The user name or password provided is incorrect!");

                    logger.Warn("TThe user name or password provided is incorrect!");
                }
                else
                {
                    ModelState.AddModelError("", "The user name is required!");

                    logger.Warn("The user name is required!");
                }

               
                //Find out if the user is active or not 

            
                return View(model);
            }
            catch(Exception ex)
            {
                logger.Error(ex);

                return View("Error");
            }
            
            
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        //[AllowAnonymous]
        //[Authorize(Roles = "SuperAdmin")]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]        
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // *************** This register action is only for superadmin to create a property manager ****************************
                //
                    try
                    {
                        //if (User.Identity.IsAuthenticated && User.IsInRole("SuperAdmin"))
                        //{
                            WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                            Roles.AddUserToRole(model.UserName, "PropertyManager");


                            //Send notification email to the property manager
                            //
                            SMTPService smtp = new SMTPService();

                            string mgrEmail = _userProfileRepository.GetPropertyManagerForEdit(model.UserName).ContactEmail;
                            string emailBody = "Your account has been created as follows: username: " + model.UserName + ", password: " + model.Password + ".";

                            smtp.SendMail("admin@promaster.com", mgrEmail, "Your Account Information", emailBody, false);

                            //if (roles.Contains("SuperAdmin"))
                            //{
                            logger.Info("The project manager " + model.UserName + "has been setup.");

                            return RedirectToAction("Index", "Admin");
                        //}


                        //WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                        //WebSecurity.Login(model.UserName, model.Password);

                        //Update user entitiy, incluidng all types of users with different roles, with the newly created UserName
                        //
                        //string UserName = model.UserName;

                        //int id = Convert.ToInt64( TempData["PID"]); //0;//It will be retrieved for this specific user account(table to be updated)

                        
                        ////UpdateUserAccount(id, UserName); //update the account with UserName field

                        ////Get RoleId for the given user account
                        ////
                        //int roleId = GetRoleId(model.UserName);

                        ////Add criteria for assigning role to the new user: findout the roleId!
                        ////
                        //switch (roleId)
                        //{
                        //    case 3:
                        //        Roles.AddUserToRole(model.UserName, "Tenant");
                        //            break;
                        //    case 4:
                        //        Roles.AddUserToRole(model.UserName, "Landlord");
                        //            break;
                        //    case 5:
                        //    Roles.AddUserToRole(model.UserName, "Vendor");
                        //            break;
                        //    default:
                        //            break;
                        //}

                        // string[] roles = Roles.GetRolesForUser(User.Identity.Name);

                        //Add property manager role to newly created pm account
                        //

                        //Roles.AddUserToRole(model.UserName, "PropertyManager");
                        //Roles.AddUserToRole(model.UserName, "SuperAdmin");

                        //Send notification email to the property manager
                        //***************************************************************

                        //SMTPService smtp = new SMTPService();

                        //string mgrEmail = _userProfileRepository.GetPropertyManagerForEdit(model.UserName).ContactEmail;
                        //string emailBody = "Your account has been created as follows: username: " + model.UserName + ", password: " + model.Password + ".";

                        //smtp.SendMail("admin@promaster.com", mgrEmail, "Your Account Information", emailBody, false);

                        ////if (roles.Contains("SuperAdmin"))
                        ////{
                        //logger.Info("The project manager " + model.UserName + "has been setup.");
                        //***************************************************************

                        //return RedirectToAction("Index", "Admin");
                            //return RedirectToAction("ManagePMs", "Admin");
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Manage");
                        //}

                    }
                    catch (MembershipCreateUserException ex)
                    {
                        ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));

                        string msg = LogUtility.BuildExceptionMessage(ex);

                        logger.Error(msg, ex);
                    }
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            

        }

        [HttpPost]
        [Authorize]
        public JsonResult CreateAccount(string userName, string passWord, int id, int pId)
        {
            try 
            {
                WebSecurity.CreateUserAndAccount(userName, passWord);

                UpdateUserAccount(id, pId, userName, passWord);

                logger.Info("The client " + userName + "has been setup.");

                return Json("Account created successfully!");
            }
            catch(Exception ex)
            {              
                logger.Error("Error occured!", ex);
                
                return Json("Error occured!");
            }
            
        }

        public void SendEmail(string userEmail, string userName, string passWord)
        {
            //Send notification email to the property manager
            //
            try
            {
                SMTPService smtp = new SMTPService();
           
                string emailBody = "Your account has been created as follows: username: " + userName + ", password: " + passWord + ".";

                smtp.SendMail("admin@promaster.com", userEmail, "Your Account Information", emailBody, false);
            }
            catch(Exception ex)
            {
                logger.Error("Error occured!", ex);
            }
            
        }

        //This routin needs to be fixed
        //
        string GetUserRole(string userName)
        {
            var userRole = Roles.GetRolesForUser(userName); // userProfileRepository.GetRoleIdByUserName(UserName);

            return userRole.ToString();
        }

        public ActionResult EnableOnlineAccess(int id, int rId)
        {
            return View(); 
        }


        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")] //only for creating property manager by superadmin
        //[Authorize(Roles = "PropertyManager")]
        public ActionResult EnableOnlineAccess(RegisterModel model, int id, int rId)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                //For all user online access setup, add a swith statement to find which role id is, then get different user, e.g. tenant, owner, etc. for updating
                //
                switch (rId)
                {
                    case 2:
                        var manager = _userProfileRepository.GetPropertyManagerById(id);

                        manager.UserName = model.UserName;
                        manager.UserAvartaImgUrl = "~/Content/Documents/Others/" + manager.UserName + ".png";
                        _userProfileRepository.Save();

                        Roles.AddUserToRole(model.UserName, "PropertyManager");

                        UpdateAvatar(manager.UserName);

                        SendEmail( manager.ContactEmail, manager.UserName, model.Password);

                        break;
                    case 3:
                        Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

                        tenant.UserName = model.UserName;
                        tenant.OnlineAccessEnbaled = true;
                        tenant.UserAvartaImgUrl = "~/Content/Documents/Others/" + tenant.UserName + ".png";
                        tenant.OnlineAccessEnbaled = true;

                        _userProfileRepository.Save();

                        Roles.AddUserToRole(model.UserName, "Tenant");

                        SendEmail(tenant.ContactEmail, tenant.UserName, model.Password);

                        break;
                    case 4:
                        Core.PropertyOwner.PropertyOwner owner = _propertyOwnerRepository.GetOwnerById(id);

                        owner.UserName = model.UserName;
                        owner.OnlineAccessEnbaled = true;
                        owner.UserAvartaImgUrl = "~/Content/Documents/Others/" + owner.UserName + ".png";
                        owner.OnlineAccessEnbaled = true;

                        _userProfileRepository.Save();

                        Roles.AddUserToRole(model.UserName, "Landlord");

                        SendEmail(owner.ContactEmail, owner.UserName, model.Password);

                        break;
                }
                

                _userProfileRepository.Save();

 
            }

            return RedirectToAction("ManagePMs", "Admin");
 
        }

        
        [HttpPost]
        [Authorize(Roles = "PropertyManager")] //only for creating user account for tenants and landlords by property manager
        public JsonResult EnableClientOnlineAccess(int id, int rId, string userName, string passWd)
        {
            WebSecurity.CreateUserAndAccount(userName, passWd);

            switch(rId)
            {
                case 3:
                    Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

                        tenant.UserName = userName;
                        tenant.OnlineAccessEnbaled = true;

                        UpdateAvatar(userName);

                        tenant.UserAvartaImgUrl = "~/Content/Documents/Others/" + tenant.UserName + ".png";

                        _tenantRepository.Save();

                        _userProfileRepository.Save();

                        Roles.AddUserToRole(userName, "Tenant");

                        SendEmail(tenant.ContactEmail, tenant.UserName, passWd);
                    break;
                case 4:
                    Core.PropertyOwner.PropertyOwner owner = _propertyOwnerRepository.GetOwnerById(id);

                        owner.UserName = userName;
                        owner.OnlineAccessEnbaled = true;

                        UpdateAvatar(userName);

                        owner.UserAvartaImgUrl = "~/Content/Documents/Others/" + owner.UserName + ".png";

                        _propertyOwnerRepository.Save();

                        _userProfileRepository.Save();

                        Roles.AddUserToRole(userName, "Landlord");

                        SendEmail(owner.ContactEmail, owner.UserName, passWd);
                    break;
            }

            return Json("Online access enabled!");
        }

        public JsonResult DisableClientOnlineAccess(int id, string userName, int rId)
        {
            switch (rId)
            {
                case 3:
                    Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

                    tenant.UserName = "tba";
                    tenant.OnlineAccessEnbaled = false;
                    tenant.UserAvartaImgUrl = "~/Content/Documents/Others/avatar_default.png";

                    _tenantRepository.Save();

                    Roles.RemoveUserFromRole(userName, "tenant");

                    var membership = (SimpleMembershipProvider)Membership.Provider;
                    membership.DeleteAccount(userName);
                    Membership.DeleteUser(userName, true);


                    _userProfileRepository.Save();

                    //Roles.AddUserToRole(UserName, "Tenant");
                    
                    break;
                case 4:
                    Core.PropertyOwner.PropertyOwner owner = _propertyOwnerRepository.GetOwnerById(id);

                    owner.UserName = "tba";
                    owner.OnlineAccessEnbaled = false;
                    owner.UserAvartaImgUrl = "~/Content/Documents/Others/avatar_default.png";

                    _propertyOwnerRepository.Save();

                    Roles.RemoveUserFromRole(userName, "Landlord");

                    var membership2 = (SimpleMembershipProvider)Membership.Provider;
                    membership2.DeleteAccount(userName);
                    Membership.DeleteUser(userName, true);



                    _userProfileRepository.Save();

                    //Roles.AddUserToRole(UserName, "Landlord");
                    
                    break;
            }

            return Json("Online access disabled!");
        }
        private void UpdateAvatar(string userName)
        {
            //Setup default user avatar
            //
            string newfileName = userName + ".png";

            string fromPath = Server.MapPath("~/Content/Documents/Others/avatar_default.png");//"C:\\Project\\ProMaster\\ProMaster.Web\\Content\\Documents\\Others\\avatar_default.png";
            string toPath = Server.MapPath("~/Content/Documents/Others/") + newfileName; // "C:\\Project\\ProMaster\\ProMaster.Web\\Content\\Documents\\Others\\";

            System.IO.File.Copy(fromPath,
                toPath, true);
            
        }


        //Update user account info
        //
        void UpdateUserAccount(int id, int pId, string userName, string passWd)
        {
            if (passWd == null) throw new ArgumentNullException("passWd");
            var tenant = _tenantRepository.GetTenantById(id);

            //Update user table for added usernmae
            //
            tenant.UserName = userName;
            tenant.OnlineAccessEnbaled = true;
            
            //Setup default user avatar
            //
            string newfileName = userName + ".png";

            string fromPath = Server.MapPath("~/Content/Documents/Others/");
            string toPath = Server.MapPath("~/Content/Documents/Others/");

            //System.IO.File.Copy("C:\\Users\\Bob\\Documents\\Visual Studio 2012\\Projects\\ProMaster\\ProMaster.Web\\Content\\Documents\\Others\\avatar_default.png", 
            //    "C:\\Users\\Bob\\Documents\\Visual Studio 2012\\Projects\\ProMaster\\ProMaster.Web\\Content\\Documents\\Others\\" + newfileName, true);

            System.IO.File.Copy(fromPath + "avatar_default.png", toPath + newfileName, true);


            tenant.UserAvartaImgUrl = "~/Content/Documents/Others/" + newfileName;

            _tenantRepository.Save();

            //Assigne role to the username
            //
            string role;

            switch (pId)
            {
                case 3:
                    role = "Tenant";
                    break;
                case 4:
                    role = "Landlord";
                    break;
                default:
                    role = "";
                    break;
            }

            Roles.AddUserToRole(userName, role);
 
            //sending email to user
            //
            
            string body = "Your online access is activated!" + " " + tenant.UserName + "/" + passWd;
            SMTPService email = new SMTPService();

            email.SendMail("admin@promaster.com", tenant.ContactEmail, "Online Access Enabled",  body, false);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        } //Manage password (change password)


        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(string oldpass, string newpass)//Infrastructure.Security.ChangePasswordModel model)
        //public ActionResult ChangePassword(Infrastructure.Security.ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                var changePasswordSucceeded = false;
                try
                {
                    var currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    if (currentUser != null) changePasswordSucceeded = currentUser.ChangePassword(oldpass, newpass);
                }
                catch (Exception ex)
                {
                    changePasswordSucceeded = false;
                    logger.Error("Error occured!", ex);
                }

                if (changePasswordSucceeded)
                {
                    //return RedirectToAction("ChangePasswordSuccess");
                    logger.Info("Password has been changed for " + User.Identity.Name +".");
                    return Json("The password has been successfully changed!");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                logger.Warn("The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
            return Json("Error occured, please try it again!");
        }

        [AllowAnonymous]
        public JsonResult ResetPassword(string userName, string userEmail)
        {
            try
            {
                Core.Tenant.Tenant tenant = _tenantRepository.GetEmailByTenantUserName(userName);

                if(tenant == null)
                {
                    Core.PropertyOwner.PropertyOwner owner = _propertyOwnerRepository.GetOwenrEmailByUserName(userName);

                    if(owner == null)
                    {
                        var mgr = _userProfileRepository.GetPropertyManagerEmailByUserName(userName);

                        if(mgr != null)
                        {
                            if(mgr.ContactEmail == userEmail)
                            {
                                //send reset url to user's email (property manager)
                                //var token = WebSecurity.GeneratePasswordResetToken(mgr.UserName);
                                //var tmpPassword = Membership.GeneratePassword(6, 0);
                                //WebSecurity.ResetPassword(token, tmpPassword);
                                ResetAndSendPassword(mgr.UserName, mgr.ContactEmail);

                                logger.Info("Password has been reset for " + mgr.UserName);
                            }
                        }
                    }
                    else
                    {
                        if(owner.ContactEmail == userEmail)
                        {
                            //send reset url to user's email (property owner)
                            ResetAndSendPassword(owner.UserName, owner.ContactEmail);

                            logger.Info("Password has been reset for " + owner.UserName);
                        }
                    }
                }
                else
                {
                    if(tenant.ContactEmail == userEmail)
                    {
                        //send reset url to user's email (tenant)
                        ResetAndSendPassword(tenant.UserName, tenant.ContactEmail);

                        logger.Info("Password has been reset for " + tenant.UserName);
                    }
                }

            
                return Json("Password reset information has been sent!");
            }
            catch(Exception ex)
            {
                logger.Error(ex);

                return Json("Error occured!");
            }
           
        }

        private void ResetAndSendPassword(string userName, string userEmail)
        {
            //Reset password
            //
            var token = WebSecurity.GeneratePasswordResetToken(userName);
            var tmpPassword = Membership.GeneratePassword(6, 0);
            WebSecurity.ResetPassword(token, tmpPassword);

            //Send email
            //
            var smtp = new SMTPService();
            string mailBody = "Your new password is " + "\r\n" + tmpPassword;
            smtp.SendMail(userEmail, "noreply@promaster.com", "Password Rest", mailBody, false);
        }
        #endregion

        #region User Profile

        /*
        [Authorize(Roles = "Property Manager")]
        public ActionResult CreateProfile()
        {
            CreateProfileViewModel ProfileModel = new CreateProfileViewModel();

            var Roles = userProfileRepository.ListRoles();

            ProfileModel.Roles = Roles.ToRoleList(-1).OrderBy(i=>i.Text);

            return View("CreateProfile", ProfileModel);
        }

        [Authorize(Roles = "Property Manager")]
        [HttpPost]
        public ActionResult CreateProfile(FormCollection form, CreateProfileViewModel model)
        {
            Profile profile = new Profile();            

            profile.FirstName = Request.Form["FistName"];
            profile.LastName = Request.Form["LastName"];
            profile.ContactEmail = Request.Form["ContactEmail"];
            profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
            profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
            //profile.RoleId = model.RoleId;
            profile.UserAvartaImgUrl = "Default";
            profile.CreationDate = DateTime.Now;
            profile.UpdateDate = DateTime.Now;

            userProfileRepository.AddProfile(profile);
            //userProfileRepository.SaveProfile();

            return View();
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult CreatePmProfile()
        {
            CreateProfileViewModel ProfileModel = new CreateProfileViewModel();

            var Roles = userProfileRepository.ListRoles();

            ProfileModel.Roles = Roles.ToRoleList(-1).OrderBy(i => i.Text);

            return View("CreateProfile", ProfileModel);
        }
*/

        [Authorize]
        public ActionResult MyProfile()
        {
            string userName = User.Identity.Name; //Get login username

            //Get RoleId for the logged-in user account
            //
            //string userRole = GetUserRole(UserName);

            MyProfileViewModel profile = new MyProfileViewModel();

            if (User.IsInRole("SuperAdmin"))
            {
                profile = _userProfileRepository.ListSuperAdminProfileByUserName(userName);
            }

            if (User.IsInRole("PropertyManager"))
            {
                profile = _userProfileRepository.ListPropertyManagerProfileByUserName(userName);
            }

            if (User.IsInRole("Tenant"))
            {
                profile = _userProfileRepository.ListTenantProfileByUserName(userName);
            }

            if (User.IsInRole("Landlord"))
            {
                profile = _userProfileRepository.ListLandlordProfileByUserName(userName);
            }

            if (profile != null)
            {
                return View("MyProfile", profile);
            }
            //switch (userRole)
            //{
            //    case "SuperAdmin":
            //        profile = userProfileRepository.ListSuperAdminProfileByUserName(UserName);
            //        break;
            //    case "PropertyManager":
            //        profile = userProfileRepository.ListPropertyManagerProfileByUserName(UserName);
            //        break;
            //    case "Tenant":
            //        profile = userProfileRepository.ListTenantProfileByUserName(UserName);
            //        break;
            //    case "Landlord":
            //        profile = userProfileRepository.ListLandlordProfileByUserName(UserName);
            //        break;
            //    default:
            //        break;
            //}

            return RedirectToAction("CreateProfile", "Account");
        }


        public ActionResult EditProfile()
        {
            string userName = User.Identity.Name;



            if (User.IsInRole("SuperAdmin"))
            {
                MyProfileViewModel profile = _userProfileRepository.ListSuperAdminProfileByUserName(userName);
                return View ("EditProfile", profile);            
            }

            if (User.IsInRole("PropertyManager"))
            {
                MyProfileViewModel profile = _userProfileRepository.ListPropertyManagerProfileByUserName(userName);
                return View ("EditProfile", profile);        
            }

            if (User.IsInRole("Tenant"))
            {
                MyProfileViewModel profile = _userProfileRepository.ListTenantProfileByUserName(userName);
                return View("EditProfile", profile);
            }

            if (User.IsInRole("Landlord"))
            {
                MyProfileViewModel profile = _userProfileRepository.ListLandlordProfileByUserName(userName);
                return View("EditProfile", profile);
            }

            return RedirectToAction("MyProfile");
            
        }

        [HttpPost]
        public ActionResult EditProfile(MyProfileViewModel model)
        {
            string userName = User.Identity.Name;

            var user = _userProfileRepository.GetUserByName(userName);

            if (User.IsInRole("SuperAdmin"))
            {
                var profile = _userProfileRepository.GetAdminForEdit(userName);

                #region Update avatar image

                var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

                if (iconImage != null)
                {
                    var origImageFileName = Path.GetFileName(iconImage.FileName);

                    var fileExtension = FileProcessor.GetFileExtension(origImageFileName);

                    //var iconImageFileName = Path.GetFileNameWithoutExtension(origImageFileName);

                    //var newImageFileName = iconImageFileName + "_album_cover" + fileExtension;
                    var newImageFileName = User.Identity.Name + "_"  + fileExtension;

                    iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newImageFileName));

                    Image img = Image.FromFile(newImageFileName);

                    if (origImageFileName != null) img.Save(origImageFileName, ImageFormat.Png);

                    profile.UserAvartaImgUrl = Path.Combine("~/Content/Documents/Others/", newImageFileName);

                }
                else
                {
                    
                    profile.FirstName = Request.Form["FirstName"];
                    profile.LastName = Request.Form["LastName"];
                    profile.ContactEmail = Request.Form["ContactEmail"];
                    profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
                    profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
                    profile.UpdateDate = DateTime.Now;

                    user.FirstName = Request.Form["FirstName"];
                    user.LastName = Request.Form["LastName"];
                    user.Email = Request.Form["Email"];
                    
                }
                #endregion

                
            } 

            if (User.IsInRole("PropertyManager"))
            {
                var profile = _userProfileRepository.GetPropertyManagerForEdit(userName);

                #region Update avatar image

                var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

                if (iconImage != null)
                {
                    var origImageFileName = Path.GetFileName(iconImage.FileName);

                    var fileExtension = FileProcessor.GetFileExtension(origImageFileName);


                    var newImageFileName = User.Identity.Name + "_" + fileExtension;

                    iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newImageFileName));

                    profile.UserAvartaImgUrl = Path.Combine("~/Content/Documents/Others/", newImageFileName);

                }
                else
                {
                    profile.FirstName = Request.Form["FirstName"];
                    profile.LastName = Request.Form["LastName"];
                    profile.ContactEmail = Request.Form["ContactEmail"];
                    profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
                    profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
                    profile.UpdateDate = DateTime.Now;

                }
                #endregion

                
            }

            if (User.IsInRole("Tenant"))
            {
                var profile = _userProfileRepository.GetTenantForEdit(userName);

                #region Update avatar image

                var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

                if (iconImage != null)
                {
                    var origImageFileName = Path.GetFileName(iconImage.FileName);

                    var fileExtension = FileProcessor.GetFileExtension(origImageFileName);

                    //var iconImageFileName = Path.GetFileNameWithoutExtension(origImageFileName);

                    //var newImageFileName = iconImageFileName + "_album_cover" + fileExtension;
                    var newImageFileName = User.Identity.Name + "_" + fileExtension;

                    iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newImageFileName));

                    profile.UserAvartaImgUrl = Path.Combine("~/Content/Documents/Others/", newImageFileName);

                }
                else
                {
                    profile.FirstName = Request.Form["FirstName"];
                    profile.LastName = Request.Form["LastName"];
                    profile.ContactEmail = Request.Form["ContactEmail"];
                    profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
                    profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
                    profile.UpdateDate = DateTime.Now;
                }
                #endregion

                
            }

            if (User.IsInRole("Landlord"))
            {
               var profile = _userProfileRepository.GetLandlordForEdit(userName);

               #region Update avatar image

               var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

               if (iconImage != null)
               {
                   var origImageFileName = Path.GetFileName(iconImage.FileName);

                   var fileExtension = FileProcessor.GetFileExtension(origImageFileName);

                   //var iconImageFileName = Path.GetFileNameWithoutExtension(origImageFileName);

                   //var newImageFileName = iconImageFileName + "_album_cover" + fileExtension;
                   var newImageFileName = User.Identity.Name + fileExtension;

                   iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newImageFileName));

                   profile.UserAvartaImgUrl = Path.Combine("~/Content/Documents/Others/", newImageFileName);

               }
               else
               {
                   
                   profile.FirstName = Request.Form["FirstName"];
                   profile.LastName = Request.Form["LastName"];
                   profile.ContactEmail = Request.Form["ContactEmail"];
                   profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
                   profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
                   profile.UpdateDate = DateTime.Now;
               }
               #endregion

               
            }

            //Update memebership user table
            //
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];
            user.Email = Request.Form["ContactEmail"];
            

            _userProfileRepository.Save();

            return RedirectToAction("MyProfile");
        }

        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProfile(FormCollection form)
        {
            //throw new NotImplementedException();
            string userName = User.Identity.Name;

            var profile = new Profile();

            var user = _userProfileRepository.GetUserByName(userName);

            profile.UserName = userName;
            profile.FirstName = Request.Form["FirstName"];
            profile.LastName = Request.Form["LastName"];
            profile.ContactEmail = Request.Form["ContactEmail"];
            profile.ContactTelephone1 = Request.Form["ContactTelephone1"];
            profile.ContactTelephone2 = Request.Form["ContactTelephone2"];
            profile.UserAvartaImgUrl = "/Content/Documents/Others/admin.png";
            profile.RoleId = 1;
            profile.OnlineAccessEnbaled = true;
            profile.UserPrincipalId = 0;

            profile.CreationDate = DateTime.Now;
            profile.UpdateDate = DateTime.Now;

            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Email = profile.ContactEmail;
            user.AvatarImgUrl = profile.UserAvartaImgUrl;

            _userProfileRepository.AddProfile(profile);

            _userProfileRepository.Save();

            return RedirectToAction("MyProfile", "Account");

        }

        #endregion

        #region External Authentications
        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            // User is new, ask for their desired membership name
            string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider;
            string providerUserId;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        
    }
}
