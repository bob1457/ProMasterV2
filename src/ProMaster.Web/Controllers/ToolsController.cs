using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;

using PagedList;
using ProMaster.DAL.Property;
using ProMaster.DAL.Landlord;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.Property;
using ProMaster.DAL.Contract;
using ProMaster.Core.Tenant;
using ProMaster.Core.Tenant.ViewModels;
using ProMaster.DAL.Tenant;
using ProMaster.DAL.Lease;
using ProMaster.Infrastructure.Utilities;
using ProMaster.DAL.Vendor;
using ProMaster.DAL.StrataCouncil;
using ProMaster.DAL.Document;
using ProMaster.DAL.Logging;
using ProMaster.Infrastructure.Logging.ViewModels;

namespace ProMaster.Web.Controllers
{
    [Authorize]//(Roles = "PropertyManager")]
    public class ToolsController : Controller
    {
        //
        // GET: /Tools/

        #region DI Configuration

        IPropertyRepository _propertyRepository;
        IPropertyOwnerRepository _propertyOwnerRepository;
        IManagementContractRepository _contractRepository;
        ITenantRepository  _tenantRepository;
        ILeaseRepository _leaseRepository;
        IVendorRepository _vendorRepository;
        IStrataCouncilRepository _strataRepository;
        IDocumentRepository _documentRepository;
        ILogReportingRepository _logReportingReposioty;

        public ToolsController(IPropertyRepository propertyRepository, IPropertyOwnerRepository propertyOwnerRepository, 
            IManagementContractRepository contractRepository, ITenantRepository tenantRepository, ILeaseRepository leaseRepository,
            IVendorRepository vendorRepository, IStrataCouncilRepository strataRepository, IDocumentRepository documentRepository,
            ILogReportingRepository logReportingReposioty)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "tools";

            _propertyRepository = propertyRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
            _contractRepository = contractRepository;
            _tenantRepository = tenantRepository;
            _leaseRepository = leaseRepository;
            _vendorRepository = vendorRepository;
            _strataRepository = strataRepository;
            _documentRepository = documentRepository;
            _logReportingReposioty = logReportingReposioty;
        }

        #endregion

        //static int count = 0;

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "tools";

            if(Roles.GetRolesForUser(User.Identity.Name).Contains("PropertyManager"))
            {
                return View();
            }
            if(Roles.GetRolesForUser(User.Identity.Name).Contains("SuperAdmin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "ClientPortal");
            }
        }


        #region Calendar/Scheduler

        public ActionResult Calendar()//(int id)
        {
            // ProMaster.Core.Property.Calendar model = new Core.Property.Calendar();

            //ProMaster.Core.Property.Calendar calEvent = _propertyRepository.GetCalendarEventById(id);


            //return PartialView("_CalendarEvent", model);
            return View();
        }

        public JsonResult GetCalendarEvents(double start, double end)
        {
            //start = 1392647400;
            //end = 1393056000;

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            int id = _propertyRepository.GetPmId(User.Identity.Name);

            var events = _propertyRepository.GetEventListByPmId(id);//, fromDate, toDate);

            //foreach (var item in events)
            //{ 

            //}
            var EventList = from e in events
                            select new
                            {
                                id = e.CalendarId,
                                title = e.CalendarTitle,
                                start = e.EventStart,
                                end = e.EventEnd,
                                details = e.CalendarDesc,
                                allDay = e.IsAllDay
                            };
            var rows = EventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCalendarEvent(string eventTitle, string eventDetails, string start, string end, bool allday)
        {
            ProMaster.Core.Property.Calendar calEvent = new ProMaster.Core.Property.Calendar();

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);


            calEvent.CalendarTitle = eventTitle;
            calEvent.CalendarDesc = eventDetails;
            calEvent.EventStart = start;
            calEvent.EventEnd = end;
            calEvent.IsAllDay = allday;

            calEvent.EventImgUrl = "default";
            calEvent.PmId = pmId;

            calEvent.CreationDate = DateTime.Now;
            calEvent.UpdateDate = DateTime.Now;

            _propertyRepository.AddCalendarEvent(calEvent);
            _propertyRepository.Save();

            return Json("Added!");
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return origin.AddSeconds(timestamp);
        }

        public JsonResult DeleteCalendarEvent(int id)
        {
            ProMaster.Core.Property.Calendar calendarEvent = _propertyRepository.GetCalendarEventById(id);

            _propertyRepository.DeleteCalendarEvent(calendarEvent);
            _propertyRepository.Save();

            return Json("The event is successfully deleted!");
        }

        public JsonResult UpdateCalendarEvent(int id, string title, string details, string start, string end, bool allday)
        {
            ProMaster.Core.Property.Calendar calendarEvent = _propertyRepository.GetCalendarEventById(id);

            calendarEvent.CalendarDesc = details;
            calendarEvent.CalendarTitle = title;
            calendarEvent.EventStart = start;
            calendarEvent.EventEnd = end;
            calendarEvent.IsAllDay = allday;
            calendarEvent.UpdateDate = DateTime.Now;

            _propertyRepository.Save();

            return Json("The event is successfully updated!");

        }

        //public ActionResult GetRecentEvents(int id)
        //{

        //    return View();
        //}

        #endregion

        #region Marketing

        public ActionResult Marketing()
        {
            
            PropertyMarketingViewModel model = new PropertyMarketingViewModel();

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<PropertyMarketingViewModel> propertyListed = _propertyRepository.ListedProperty(2, pmId);
            IEnumerable<PropertyMarketingViewModel> pendingProperty = _propertyRepository.PreListedProperty(4,  pmId);
            IEnumerable<PropertyMarketingViewModel> rentedProperty = _propertyRepository.RentedProperty(1,  pmId);

            model.PendingProperty = pendingProperty;
            model.PropertiesForRent = propertyListed;
            model.RentedPropertyList = rentedProperty;

            return View("Marketing", model);
            

            /*return RedirectToAction("Construction", "Home");*/
           
        }

        //public ActionResult StatusUpdate(int id)
        public ActionResult PropertyUpdate(int id)
        {
            //DisplayPropertyViewModel property = _propertyRepository.GetPropertyById(id);
            ListingViewModel model = new ListingViewModel();

            ListingViewModel property = _propertyRepository.GetListedPropertyById(id);

            IEnumerable<ListingViewModel> pictures = _propertyRepository.GetPicturesForProperty(id);

            var listingViewModels = pictures as ListingViewModel[] ?? pictures.ToArray();
            model.PropertyPictures = listingViewModels;
            

            //var status = _propertyRepository.GetPropertyStatus();
            //property.PropertyStatus = status.ToPropertyStatusList(-1);

            if(listingViewModels.Count() != 0)
            {
                model.PropertyListPictureId = listingViewModels.FirstOrDefault().PropertyListPictureId;
                model.ListedPropertyPictureCaption = listingViewModels.FirstOrDefault().ListedPropertyPictureCaption;
                model.ListPictureImgUrl = listingViewModels.FirstOrDefault().ListPictureImgUrl;
                model.PictureCreateDate = listingViewModels.FirstOrDefault().PictureCreateDate;
                model.ListPictureImgUrlThumb = listingViewModels.FirstOrDefault().ListPictureImgUrlThumb;
                model.ListPictureImgUrlMed = listingViewModels.FirstOrDefault().ListPictureImgUrlMed;
                model.PictureCreateDate = listingViewModels.FirstOrDefault().PictureCreateDate;
                model.PictureUpdateDate = listingViewModels.FirstOrDefault().PictureUpdateDate;
            }

            model.PropertyId = property.PropertyId;
            model.PorpertyListingTitle = property.PorpertyListingTitle;
            model.PropertyListDesc = property.PropertyListDesc;
            model.PropertyStatus = property.PropertyStatus;
            model.PropertyType = property.PropertyType;
            model.RecordCreateDate = property.ListedDate;
            model.RecordUpdateDate = property.RecordUpdateDate;
            model.PropertyName = property.PropertyName;
            model.ListedRentAmount = property.ListedRentAmount;
            model.PropertyBuildYear = property.PropertyBuildYear;
            model.IsListedExternally = property.IsListedExternally;
            model.PropertyPictureTitle = property.PropertyPictureTitle;
            model.PropertyImageUrl = property.PropertyImageUrl;
            model.IsListedExternally = property.IsListedExternally;
            model.Notes = property.Notes;

            //return View("StatusUpdate", model);
            return View("PropertyUpdate", model);
        }

        [HttpPost]
        public ActionResult PropertyUpdate(int id, PropertyMarketingViewModel model)
        {
            Core.Property.Property property = _propertyRepository.PropertyById(id);
            Core.Property.PropertyList Listing = _propertyRepository.GetListById(id).FirstOrDefault();

            /* Add property pictures */

            property.PropertyListDesc = Request.Form["PropertyListDesc"];
            Listing.ListingTitle = Request.Form["PorpertyListingTitle"];
            //property.RentalStatusId = model.RentalStatusId;
            Listing.ListedRentAmount = Decimal.Parse( Request.Form["ListedRentAmount"]);
            Listing.Notes = Request.Form["Notes"];
            Listing.UpdateDate = DateTime.Now;
            

            _propertyRepository.Save();

            return RedirectToAction("Marketing", "Tools");
        }

        public ActionResult GetPicturesByProperty(int id)
        {
            return PartialView("_PropertyListPictures");
        }
       
        [HttpPost]
        public JsonResult DeletePropertyPicture(int id)
        {
            PropertyListingPicture picture = _propertyRepository.GetPictureById(id);

            _propertyRepository.DeletePicture(picture);
            _propertyRepository.Save();

            
            #region Delete physical files in the directory

            string filePath = Server.MapPath(picture.ImgUrl);
            string filePath2 = Server.MapPath(picture.ImgUrlMed);
            string filePath3 = Server.MapPath(picture.ImgUrlThumb);

            FileProcessor.DeleteFile(filePath);
            FileProcessor.DeleteFile(filePath2);
            FileProcessor.DeleteFile(filePath3);

            #endregion

            return Json("The picture has been deleted!");
        }
        //**********************************************************************************
        //
        // need more work here to publish the property to lising with markeing information
        // with either ajax or form to update on the sever side
        //
        //**********************************************************************************

        public ActionResult ListingPublish(int id)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            ListingViewModel model = new ListingViewModel();

            ListingViewModel property = _propertyRepository.GetPreListedProperty(4, pmId);


            IEnumerable<ListingViewModel> pictures = _propertyRepository.GetPicturesForProperty(id);

            var listingViewModels = pictures as ListingViewModel[] ?? pictures.ToArray();
            model.PropertyPictures = listingViewModels;


            //var status = _propertyRepository.GetPropertyStatus();
            //property.PropertyStatus = status.ToPropertyStatusList(-1);

            if (model.PropertyPictures.Count() != 0)
            {
                model.PropertyListPictureId = listingViewModels.FirstOrDefault().PropertyListPictureId;
                model.ListedPropertyPictureCaption = listingViewModels.FirstOrDefault().ListedPropertyPictureCaption;
                model.ListPictureImgUrl = listingViewModels.FirstOrDefault().ListPictureImgUrl;
                model.PictureCreateDate = listingViewModels.FirstOrDefault().PictureCreateDate;
                model.ListPictureImgUrlThumb = listingViewModels.FirstOrDefault().ListPictureImgUrlThumb;
                model.ListPictureImgUrlMed = listingViewModels.FirstOrDefault().ListPictureImgUrlMed;
                model.PictureCreateDate = listingViewModels.FirstOrDefault().PictureCreateDate;
                model.PictureUpdateDate = listingViewModels.FirstOrDefault().PictureUpdateDate;
                model.PropertyListPictureId = listingViewModels.FirstOrDefault().PropertyListPictureId;
            }

            model.PropertyId = property.PropertyId;
            model.PorpertyListingTitle = property.PorpertyListingTitle;
            model.PropertyListDesc = property.PropertyListDesc;
            model.PropertyStatus = property.PropertyStatus;
            model.PropertyType = property.PropertyType;
            model.RecordCreateDate = property.RecordCreateDate;
            model.RecordUpdateDate = property.RecordUpdateDate;
            model.PropertyName = property.PropertyName;
            model.ListedRentAmount = property.ListedRentAmount;
            model.PropertyBuildYear = property.PropertyBuildYear;
            model.IsListedExternally = property.IsListedExternally;
            model.PropertyPictureTitle = property.PropertyPictureTitle;
            model.PropertyImageUrl = property.PropertyImageUrl;
            model.IsListedExternally = property.IsListedExternally;            

            return View("ListingPublish", model);
           
            
        }


        [HttpPost]
        public ActionResult ListingPublish(int id, ListingViewModel model)
        {
            PropertyList list = new PropertyList();
            Core.Property.Property property = _propertyRepository.PropertyById(id);

            list.PropertyId = id;
            list.ListingTitle = Request.Form["PorpertyListingTitle"];
            list.ListedRentAmount = decimal.Parse(Request.Form["ListedRentAmount"]);
            property.PropertyListDesc = Request.Form["PropertyListDesc"]; 
            list.IsListedExternal = model.IsListedExternally;
            list.CreationDate = DateTime.Now;
            list.ListedDate = DateTime.Now;
            list.UpdateDate = DateTime.Now;
            list.Notes = Request.Form["Notes"];
            list.IsActive = true;

            property.RentalStatusId = 2;

            _propertyRepository.AddPropertyListing(list);
            _propertyRepository.Save();

             return RedirectToAction("Marketing", "Tools");
        }

        
        public JsonResult ActivateListing(int id, DateTime date, decimal rent) 
        //public ActionResult ActivateListing(int id, DateTime date, decimal rent)
        {
            //throw new NotImplementedException();
            Core.Property.Property property = _propertyRepository.PropertyById(id);

            property.RentalStatusId = 2;

            //Insert into talbe PropertyList with ListDate and Monthly Rental Amount
            //
            PropertyList propertyList = new PropertyList();



            propertyList.PropertyId = id;
            propertyList.ListedDate = date;
            propertyList.ListedRentAmount = rent;

            _propertyRepository.AddPropertyListing(propertyList);

            _propertyRepository.Save();


            return Json("Listed Successfully!");
        }

        public JsonResult Unlist(int id)
        {
            var property = _propertyRepository.PropertyById(id);

            property.RentalStatusId = 4;

            _propertyRepository.Save();

            return Json("The listing has been successfully removed!");
        }
                
        public ActionResult AddPropertyPicture(int id,  PropertyListingPicture picture, HttpPostedFileBase file)
        {
            //string sourdeUrl = Request.UrlReferrer.ToString();

            //string rand = RandomNumber.GetRandomNumber(10);

            if (file != null)
            { 
                if (file.ContentLength > 0 && file.ContentLength < 10 * 1024 * 1024)
                {
                    #region Upload file

                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Documents/Property"), System.IO.Path.GetFileName(file.FileName));

                    string picPath = Path.Combine("~/Content/Documents/Property/", file.FileName);

                    string extension = FileProcessor.GetFileExtension(file.FileName);

                    string purePath = Server.MapPath("~/Content/Documents/Property/");

                    file.SaveAs(path);
                
                    #endregion


                    picture.ImgUrl = picPath;

                    //count = count++;


                    var numberOfImagesForProperty = _propertyRepository.GetPicturesForProperty(id).Count();

                    var count = numberOfImagesForProperty + 1;

                    #region processing image file - re-sizing to get thumbnails and medium size pictures

                    ImageProcessor.SaveResizedImage(purePath, file.FileName, "P_m_" + id + "_" + count + extension, 100);

                    ImageProcessor.SaveResizedImage(purePath, file.FileName, "P_t_" + id + "_" + count + extension, 45);



                    string thumbPath = "~/Content/Documents/Property/" + "P_t_" + id + "_" + count + extension;
                    string medPath = "~/Content/Documents/Property/" + "P_M_" + id + "_" + count + extension;

                    picture.ImgUrlMed = medPath;
                    picture.ImgUrlThumb = thumbPath;

                    #endregion

                    picture.PropertyPictureTitle = Request.Form["PropertyPictureTitle"];
                    picture.Caption = Request.Form["ListedPropertyPictureCaption"];
                    picture.PropertyId = id;
                    picture.CreationDate = DateTime.Now;
                    picture.UpdateDate = DateTime.Now;

                    _propertyRepository.AddPropertyPicture(picture);
                    _propertyRepository.Save();

                    //if (sourdeUrl.Contains("ListingPublish"))
                    //{
                    //    return RedirectToAction("ListingPublish", "Tools", new { id = id });
                    //}
                    //else
                    //{
                    //    return RedirectToAction("PropertyUpdate", "Tools", new { id = id });
                    //}

                    
                }
            }
            else
            {
                    ModelState.AddModelError("Error", "Please select the image file to upload.");
                    
            }

            //if (sourdeUrl.Contains("ListingPublish"))
            //{
            //    return RedirectToAction("ListingPublish", "Tools", new { id = id });
            //}
            //else
            //    return RedirectToAction("PropertyUpdate", "Tools", new { id = id });
            //}

            return RedirectToAction("PropertyUpdate", "Tools", new { id = id }); 

            
        }
        
        public ActionResult GetPictureList(int id) //property id
        {
            ListingViewModel model = new ListingViewModel();

            IEnumerable<ListingViewModel> pictures = _propertyRepository.GetPicturesForProperty(id);

            model.PropertyPictures = pictures;

            return PartialView("_PropertyListPictures");
        }

         #endregion
       

        #region Messaging

        public ActionResult Messaging()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion

        #region Application Screening

        public ActionResult Applications()
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatActiveApplications();

            IEnumerable<TenantScreenViewModel> CompltedApplications = _tenantRepository.GatCompletedApplications();

            var PropertyList = _tenantRepository.GetListedPropertyByPmId(pmId);
            model.ListedProperty = PropertyList.ToListedPropertyList(-1);

            var AllPropertyList = _tenantRepository.GetAllPropertyByPmId(pmId);
            model.AllProperty = AllPropertyList.ToAllPropertyList(-1);

            model.ApplicationList = ListApplications;
            model.CompletedApplications = CompltedApplications;

            return View("Applications", model);

            //return RedirectToAction("Construction", "Home");
           
        }

        public ActionResult ApplicationDetails(int id)
        {
            TenantScreenViewModel model = new TenantScreenViewModel();

            TenantScreenViewModel application = _tenantRepository.GetApplicationById(id);

            model.ApplicationId = application.ApplicationId;
            model.ApplicantFirstName = application.ApplicantFirstName;
            model.ApplicantLastName = application.ApplicantLastName;
            model.ApplicantContactEmail = application.ApplicantContactEmail;
            model.ApplicantContactTel = application.ApplicantContactTel;
            model.ApplicantCurrentAddress = application.ApplicantCurrentAddress;
            model.ApplicantPreviousAddress = application.ApplicantPreviousAddress;
            model.ApplicantCurrentLandlorContact = application.ApplicantCurrentLandlorContact;
            model.ApplicantPreviousLandlorContact = application.ApplicantPreviousLandlorContact;
            model.ApplicantCurrentEmploerContact = application.ApplicantCurrentEmploerContact;
            model.ApplicantPreviousEmploerContact = application.ApplicantPreviousEmploerContact;
            model.NumberOfOccupantse = application.NumberOfOccupantse;
            model.NumberOfChildren = application.NumberOfChildren;
            model.ApplicantEmploymentIncome = application.ApplicantEmploymentIncome;
            model.ApplicantOtherIncome = application.ApplicantOtherIncome;
            model.ApplicantCreditScore = application.ApplicantCreditScore;
            model.IsApplicationActive = application.IsApplicationActive;
            model.PropertyBuildYear = application.PropertyBuildYear;
            model.IsAuthorizedToCheckReference = application.IsAuthorizedToCheckReference;
            //model.ReferenceDocAvailable = application.ReferenceDocAvailable;
            model.IsAllDocumentsReady = application.IsAllDocumentsReady;
            model.ApplicaitonDate = application.ApplicaitonDate;
            model.IsScreenProcessCompleted = application.IsScreenProcessCompleted;
            model.ApplicantSIN = application.ApplicantSIN;
            model.PropertyDesc = application.PropertyDesc;
            model.PropertyName = application.PropertyName;
            model.IsApplicationApproved = application.IsApplicationApproved;
            model.IsApplicationActive = application.IsApplicationActive;

            model.ScreeningNotes = application.ScreeningNotes;

            return View("ApplicationDetails", model);
        }

        //public ActionResult ApplicationList()
        //{

        //    return View();
        //}


        public ActionResult GetActiveApplicationList()
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatActiveApplications();

            //IEnumerable<TenantScreenViewModel> CompltedApplications = _tenantRepository.GatCompletedApplications();

            var PropertyList = _tenantRepository.GetListedPropertyByPmId(pmId);
            model.ListedProperty = PropertyList.ToListedPropertyList(-1);

            model.ApplicationList = ListApplications;
            //model.CompletedApplications = CompltedApplications;

            return PartialView("_ApplicationList", model);
        }

        public ActionResult GetCompletedApplicationList()
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatCompletedApplications();

            //IEnumerable<TenantScreenViewModel> CompltedApplications = _tenantRepository.GatCompletedApplications();

            var PropertyList = _tenantRepository.GetListedPropertyByPmId(pmId);
            model.ListedProperty = PropertyList.ToListedPropertyList(-1);

            model.CompletedApplications = ListApplications;
            //model.CompletedApplications = CompltedApplications;

            return PartialView("_CompletedApplicationList", model);
        }


        public ActionResult GetActiveApplicationListByProperty(int id)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatActiveApplications(id);

            //IEnumerable<TenantScreenViewModel> CompltedApplications = _tenantRepository.GatCompletedApplications();

            var PropertyList = _tenantRepository.GetListedPropertyByPmId(pmId);
            model.ListedProperty = PropertyList.ToListedPropertyList(-1);

            model.ApplicationList = ListApplications;
            //model.CompletedApplications = CompltedApplications;

            return PartialView("_ApplicationList", model);
        }


        public ActionResult GetCompletedApplicationListByProperty(int id)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            TenantScreenViewModel model = new TenantScreenViewModel();

            //IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatActiveApplications(id);

            IEnumerable<TenantScreenViewModel> DoneApplications = _tenantRepository.GatCompletedApplications(id);

            var PropertyList = _tenantRepository.GetListedPropertyByPmId(pmId);
            model.ListedProperty = PropertyList.ToListedPropertyList(-1);

            //model.ApplicationList = ListApplications;
            model.CompletedApplications = DoneApplications;

            return PartialView("_CompletedApplicationList", model);
        }


        public ActionResult ProcessApp(int id, TenantScreenViewModel model)
        {
            TenantScreenViewModel application = _tenantRepository.GetApplicationById(id);
            
            //@Html.DropDownListFor(model=>model.ScreenCheckStatusId, Model.ScreenCheckStatusList)

            //ProMaster.Core.Tenant.TenancyApplication apps = _tenantRepository.ApplicationById(id);
            //Load the application and screen configuration
            //
            //model.app.IsApproved = apps.IsApproved;
            //model.app.IsActive = apps.IsActive;

            TenantScreening screen = _tenantRepository.GetScreeningApp(id);

            DocumentCheckList doclist = _tenantRepository.GetDocumentCheckListByAppId(id);

            //screen.TeancyApplicationId.ToString().Count() 

            if (screen  == null)
            {
                //Continue with screening process
                // 
                //add the app to the screening table and proceed with screening
                //
                for (int i = 1; i <= 6; i++)
                {
                    TenantScreening screenConfig = new TenantScreening();

                    screenConfig.TeancyApplicationId = id;
                    screenConfig.ScreeningCheckTypeId = i;
                    screenConfig.ScreeningCheckStatusId = 1;
                    screenConfig.ScreenCheckUpdateDate = DateTime.Now;
                    
                    _tenantRepository.AddTenantScreen(screenConfig);

                     _tenantRepository.Save();
                }

            }

            if (doclist == null)
            {
                DocumentCheckList list = new DocumentCheckList();

                list.IsCreditReportProvided = false;
                list.IsEmployerReferenceProvided = false;
                list.IsIdentificationProvided = false;
                list.IsIncomeStatementProvided = false;
                list.IsLandLordReferenceProvided = false;
                list.TenancyApplicanionId = id;
                list.DocumentCheckListCompletionDate = DateTime.Now; //it is actually status updated date

                _tenantRepository.AddDocCheckList(list);

                _tenantRepository.Save();

            }


            var ScreenStatusList = _tenantRepository.GetScreenStatus();
            model.ScreenCheckStatusList = ScreenStatusList.ToStatusList(-1);

            model.ApplicationId = application.ApplicationId;
            model.ApplicantFirstName = application.ApplicantFirstName;
            model.ApplicantLastName = application.ApplicantLastName;
            model.ApplicantContactEmail = application.ApplicantContactEmail;
            model.ApplicantContactTel = application.ApplicantContactTel;
            model.ApplicantCurrentAddress = application.ApplicantCurrentAddress;
            model.ApplicantPreviousAddress = application.ApplicantPreviousAddress;
            model.ApplicantCurrentLandlorContact = application.ApplicantCurrentLandlorContact;
            model.ApplicantPreviousLandlorContact = application.ApplicantPreviousLandlorContact;
            model.ApplicantCurrentEmploerContact = application.ApplicantCurrentEmploerContact;
            model.ApplicantPreviousEmploerContact = application.ApplicantPreviousEmploerContact;
            model.NumberOfOccupantse = application.NumberOfOccupantse;
            model.NumberOfChildren = application.NumberOfChildren;
            model.ApplicantEmploymentIncome = application.ApplicantEmploymentIncome;
            model.ApplicantOtherIncome = application.ApplicantOtherIncome;
            model.ApplicantCreditScore = application.ApplicantCreditScore;
            model.IsApplicationActive = application.IsApplicationActive;
            model.IsAuthorizedToCheckReference = application.IsAuthorizedToCheckReference;
            //model.ReferenceDocAvailable = application.ReferenceDocAvailable;
            model.IsAllDocumentsReady = application.IsAllDocumentsReady;
            model.ApplicaitonDate = application.ApplicaitonDate;
            model.IsScreenProcessCompleted = application.IsScreenProcessCompleted;
            model.ApplicantSIN = application.ApplicantSIN;
            //model.ScreenCheckStatusId = application.ScreenCheckStatusId;
            model.PropertyBuildYear = application.PropertyBuildYear;
            model.PropertyDesc = application.PropertyDesc;
            model.PropertyName = application.PropertyName;

            model.Screening = _tenantRepository.ScreeningById(id);
            model.DocCheckList = _tenantRepository.DocCheckListByAppId(id);


            return View("ProcessApp", model);

        }

        public ActionResult GetScreenStatusByAppId(int id, TenantScreenViewModel model)
        {
            //TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> status = _tenantRepository.ScreeningById(id);

            model.Screening = status;

            return PartialView("_ScreenStatus", model);
        }

        //public ActionResult AllApplicatioinList()
        //{

        //    return View();
        //}

        public ActionResult Screening()
        {
            //TenantScreenViewModel model = new TenantScreenViewModel();

            //IEnumerable<TenantScreenViewModel> ListApplications = _tenantRepository.GatAllApplications();

            //return View("Screening", model);

            return RedirectToAction("Construction", "Home");
        }

        public ActionResult AllApplicationList(string currentFiler, int? page)
        {
            TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> applications = _tenantRepository.GatActiveApplications().OrderBy(d=>d.ApplicaitonDate);

            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
            var paginatedApplicationList = applications.ToPagedList(pageNumber, pageSize);

            return View(paginatedApplicationList);

        }

        [HttpPost]
        public JsonResult SaveStatus(int id, bool identity, bool income, bool land, bool emp, bool credit)  //TenantScreenViewModel model) // only document checklist needs to be saved
        {
            //TenantScreenViewModel application = _tenantRepository.GetApplicationById(id);

            DocumentCheckList list = _tenantRepository.GetDocumentCheckListByAppId(id);

            list.IsIdentificationProvided = identity;
            list.IsIncomeStatementProvided = income;
            list.IsLandLordReferenceProvided = land;
            list.IsEmployerReferenceProvided = emp;
            list.IsCreditReportProvided = credit;
            

            _tenantRepository.Save();


            return Json("The status has been updated successfully!");
        }


        [HttpPost]
        public JsonResult Approve(int id, string notes)
        {
            ProMaster.Core.Tenant.TenancyApplication application = _tenantRepository.ApplicationById(id);

            application.IsActive = false;
            application.IsApproved = true;
            application.ScreeningNotes = notes;

            //_tenantRepository.Save();

            //create an entery in Tenant table with leaseId = 0 for addding to liease next step -- the status is called "TENANT CANDIDATE"
            //
            ProMaster.Core.Tenant.Tenant tenant = new Core.Tenant.Tenant();

            tenant.FirstName = application.ApplicantFirstName;
            tenant.LastName = application.ApplicantLastName;
            tenant.ContactEmail = application.ApplicantContactEmail;
            tenant.ContactTelephone1 = application.ApplicantContactTel;
            tenant.LeaseId = 0;
            tenant.RoleId = 3;
            tenant.OnlineAccessEnbaled = false;
            tenant.IsActive = false;
            tenant.UserName = "tba";
            tenant.UserAvartaImgUrl = "~/Content/Documents/Tenant/user_default.png";
            tenant.ManagedBy = User.Identity.Name;
            tenant.CreateDate = DateTime.Now;
            tenant.UpdateDate = DateTime.Now;

            _tenantRepository.AddTenant(tenant);

            _tenantRepository.Save();



            return Json("The application has been approved!");
        }

        [HttpPost]
        public JsonResult Decline(int id, string notes)
        {
            ProMaster.Core.Tenant.TenancyApplication application = _tenantRepository.ApplicationById(id);

            application.IsActive = false;
            application.IsApproved = false;
            application.ScreeningNotes = notes;

            _tenantRepository.Save();

            return Json("The status has been declined successfully!");
        }



        #endregion

        #region Document Tools

        public ActionResult DocTool()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion


        #region Services Notification

        public ActionResult ServiceNote()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion

        #region Legal and Dispute

        public ActionResult Legal()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion


        #region Resources

        public ActionResult Resources()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion

        #region Customer Relations

        public ActionResult Relations()
        {


            return RedirectToAction("Construction", "Home");
        }

        #endregion

        #region Logs and Stats

        public ActionResult Logs(string level, string period, int? page)
        {
            //LoggingDashBoardViewModel model = new LoggingDashBoardViewModel();

            //IEnumerable<LoggingDashBoardViewModel> Totallogs = _logReportingReposioty.GetAllLogs();
            string uName = User.Identity.Name;

            if (!Roles.GetRolesForUser(uName).Contains("SuperAdmin")) return RedirectToAction("AccessDenied", "Error");

            const int pageSize = 20; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<LoggingDashBoardViewModel> logs;
            //int PageCount = Totallogs.Count() / pageSize;
            //ViewBag.PageCount = Totallogs.Count() / pageSize;
            if (level == null && period == null)
            {
                logs = _logReportingReposioty.GetAllLogs().OrderByDescending(d => d.LogDateTime);
            }
            else
            {
                if(level != null && period == null)
                {
                    logs = _logReportingReposioty.GetLogsByLevel(level).OrderByDescending(d => d.LogDateTime);
                }
                else
                {
                    if(level == null && period != null)
                    {
                        logs = _logReportingReposioty.GetLogsByPeriod(period).OrderByDescending(d => d.LogDateTime);
                    }
                    else
                    {
                        logs = _logReportingReposioty.GetLogsByLevelAndPeriod(level, period).OrderByDescending(d => d.LogDateTime);//.Take(10); //.Skip(pageSize * (page - 1));
                    }
                }
                 
            }
            

            //ViewBag.TotalLogs = logs.Count();

            var Logs = logs.ToPagedList(pageNumber, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_Logs", Logs);

            return View(Logs);
        }

        public ActionResult LogDetails(int id)
        {
            string uName = User.Identity.Name;
            if (!Roles.GetRolesForUser(uName).Contains("SuperAdmin")) return RedirectToAction("AccessDenied", "Error");

            var LogDetails = _logReportingReposioty.GetLogDetails(id);

            return View(LogDetails);
        }

        public ActionResult GetLogsByLevel(string level, int? page)
        {
            const int pageSize = 20; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<LoggingDashBoardViewModel> LogsByLevel = _logReportingReposioty.GetLogsByLevel(level).OrderByDescending(d => d.LogDateTime);

            var Logs = LogsByLevel.ToPagedList(pageNumber, pageSize);

            return PartialView("_Logs", Logs);
        }

        public ActionResult GetLogsByPeriod(string period, int? page)
        {
            const int pageSize = 20; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<LoggingDashBoardViewModel> LogsByPeriod = _logReportingReposioty.GetLogsByPeriod(period).OrderByDescending(d => d.LogDateTime);

            //int count = LogsByPeriod.Count();

            var Logs = LogsByPeriod.ToPagedList(pageNumber, pageSize);

            return PartialView("_Logs", Logs);
        }

        public ActionResult GetAllLogs(int? page)
        {
            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            IEnumerable<LoggingDashBoardViewModel> LogsByLevel = _logReportingReposioty.GetAllLogs().OrderByDescending(d => d.LogDateTime);

            var Logs = LogsByLevel.ToPagedList(pageNumber, pageSize);

            return PartialView("_Logs", Logs);
        }

        #endregion

    }
}
