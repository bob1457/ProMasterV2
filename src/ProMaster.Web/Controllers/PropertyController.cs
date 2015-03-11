using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using PagedList;
using ProMaster.Core.Property;
using ProMaster.Core.Property.ViewModels;
using ProMaster.DAL.Property;
using ProMaster.DAL.Tenant;
using ProMaster.Core.Tenant;
using ProMaster.Infrastructure.Email;
using Document = ProMaster.Core.Property.Document;

namespace ProMaster.Web.Controllers
{
    public class PropertyController : Controller
    {
        #region DI Configuration

        IPropertyRepository _propertyRepository;
        ITenantRepository _tenantRepository;


        public PropertyController(IPropertyRepository propertyRepository, ITenantRepository tenantRepository)  //depending on interface instead of implementation
        {
            _propertyRepository = propertyRepository;
            _tenantRepository = tenantRepository;
        }

        #endregion
        //
        // GET: /Property/

        public ActionResult Index(int? page)  //Display full list of rental listing
        {
            
            const int pageSize = 2; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);


            //ListingViewModel model = new ListingViewModel();

            IEnumerable<ListingViewModel> property = _propertyRepository.ShowListedProperty().OrderByDescending(d => d.ListedDate);

            _propertyRepository.GetPicturesForProperties();

            //model.PropertyPictures = pictures;

            //model.PropertyList = property;


            var properties = property.ToPagedList(pageNumber, pageSize);
                        

            //if (model.PropertyPictures.Count() != 0)
            //{
            //    model.PropertyListPictureId = pictures.FirstOrDefault().PropertyListPictureId;
            //    model.PropertyPictureTitle = pictures.FirstOrDefault().PropertyPictureTitle;
            //    model.ListedPropertyPictureCaption = pictures.FirstOrDefault().ListedPropertyPictureCaption;
            //    model.ListPictureImgUrl = pictures.FirstOrDefault().ListPictureImgUrl;
            //    model.ListPictureImgUrlMed = pictures.FirstOrDefault().ListPictureImgUrlMed;
            //    model.ListPictureImgUrlThumb = pictures.FirstOrDefault().ListPictureImgUrlThumb;
            //    model.PictureCreateDate = pictures.FirstOrDefault().PictureCreateDate;
            //    model.PictureUpdateDate = pictures.FirstOrDefault().PictureUpdateDate;
            //}


            //****** Temporarily disabled ****************
            //if (Request.IsAjaxRequest())
            //    return PartialView("_ListedProperty", properties);
            //****** Temporarily disabled ****************


            return View(properties);
            //return View("Index", model);
        }

        //
        // GET: /Property/Details/5       


        public ActionResult PropertyDetails(int id)
        {
            //DisplayPropertyViewModel propertyDetails = 
            DisplayPropertyViewModel model = new DisplayPropertyViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);

            IEnumerable<DisplayPropertyViewModel> imagesByProperty = _propertyRepository.GetPhotoForProperty(id);

            var displayPropertyViewModels = property as DisplayPropertyViewModel[] ?? property.ToArray();
            IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(displayPropertyViewModels.FirstOrDefault().PropertyAddressId);
            IEnumerable<PropertyFacility> facility = _propertyRepository.GetFacilityById(displayPropertyViewModels.FirstOrDefault().PropertyFacilityId);
            IEnumerable<PropertyFeature> feature = _propertyRepository.GetFeatureById(displayPropertyViewModels.FirstOrDefault().PropertyFeatureId);
            //IEnumerable<Core.Property.PropertyOwner> owner = _propertyRepository.GetLandlordById(property.FirstOrDefault().LandlordId);
            IEnumerable<Core.Property.Document> document = _propertyRepository.DocumenetByPropertyid(id, 1);

            var propertyAddresses = address as PropertyAddress[] ?? address.ToArray();
            model.Address = propertyAddresses;
            var propertyFeatures = feature as PropertyFeature[] ?? feature.ToArray();
            model.Feature = propertyFeatures;
            var propertyFacilities = facility as PropertyFacility[] ?? facility.ToArray();
            model.Facility = propertyFacilities;
            var documentInfo = document as Document[] ?? document.ToArray();
            model.DocumentInfo = documentInfo;
            model.PropertyPictures = imagesByProperty;

            model.PropertyId = displayPropertyViewModels.FirstOrDefault().PropertyId;
            model.PropertyName = displayPropertyViewModels.FirstOrDefault().PropertyName;
            model.PropertyListDesc = displayPropertyViewModels.FirstOrDefault().PropertyListDesc;
            model.PorpertyListingTitle = displayPropertyViewModels.FirstOrDefault().PorpertyListingTitle;
            model.ListedRentAmount = displayPropertyViewModels.FirstOrDefault().ListedRentAmount;
            model.PropertyType = displayPropertyViewModels.FirstOrDefault().PropertyType;
            model.PropertyBuildYear = displayPropertyViewModels.FirstOrDefault().PropertyBuildYear;
            model.IsActive = displayPropertyViewModels.FirstOrDefault().IsActive;
            model.CreationDate = displayPropertyViewModels.FirstOrDefault().CreationDate;
            model.UpdateDate = displayPropertyViewModels.FirstOrDefault().UpdateDate;
            model.Status = displayPropertyViewModels.FirstOrDefault().Status;
            model.PropertyImageUrl = displayPropertyViewModels.FirstOrDefault().PropertyImageUrl;
            //model.RentAmount = property.FirstOrDefault().RentAmount;

            model.PropertyAddressStreetNumber = propertyAddresses.FirstOrDefault().PropertyNumber;
            model.PropertyAddressStreetName = propertyAddresses.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = propertyAddresses.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = propertyAddresses.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = propertyAddresses.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = propertyAddresses.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = propertyAddresses.FirstOrDefault().PropertyCountry;

            model.NumberOfBathrooms = propertyFeatures.FirstOrDefault().NumberOfBathrooms;
            model.NumberOfBedrooms = propertyFeatures.FirstOrDefault().NumberOfBedrooms;
            model.NumberOfFloors = propertyFeatures.FirstOrDefault().NumberOfLayers;
            model.NumberOfParkings = propertyFeatures.FirstOrDefault().NumberOfParking;
            model.IsShared = propertyFeatures.FirstOrDefault().IsShared;

            
            model.Stove = propertyFacilities.FirstOrDefault().Stove;
            model.Refrigerator = propertyFacilities.FirstOrDefault().Refrigerator;
            model.Furniture = propertyFacilities.FirstOrDefault().Furniture;
            model.AireConditioner = propertyFacilities.FirstOrDefault().AirConditioner;
            model.TVInternet = propertyFacilities.FirstOrDefault().TVInternet;
            model.Dishwasher = propertyFacilities.FirstOrDefault().Dishwasher;
            model.Laundry = propertyFacilities.FirstOrDefault().Laundry;
            model.Blinds = propertyFacilities.FirstOrDefault().BlindsCurtain;
            model.CommonFacility = propertyFacilities.FirstOrDefault().CommonFacility;
            model.Others = propertyFacilities.FirstOrDefault().Others;
            model.Notes2 = propertyFacilities.FirstOrDefault().Notes;
            model.Security = propertyFacilities.FirstOrDefault().SecuritySystem;
            model.FireAlarm = propertyFacilities.FirstOrDefault().FireAlarmSystem;

            if (model.DocumentInfo.Any())
            {
                model.DocumentId = documentInfo.FirstOrDefault().DocumentId;
                model.DocumentTitle = documentInfo.FirstOrDefault().DocumentName;
                model.DocumentUrl = documentInfo.FirstOrDefault().DocumentUrl;
            }
            

            return View("PropertyDetails", model);

            //return View();
        }

        public ActionResult RentDetails(int id)
        {
            ListingViewModel model = new ListingViewModel();

            ListingViewModel property = _propertyRepository.GetListedPropertyById(id);

            //IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(property.PropertyAddressId);
            //IEnumerable<PropertyFacility> facility = _propertyRepository.GetFacilityById(property.PropertyFacilityId);
            //IEnumerable<PropertyFeature> feature = _propertyRepository.GetFeatureById(property.PropertyFeatureId);

            //model.Address = address;
            //model.Feature = feature;
            //model.Facility = facility;


            model.PropertyAddressStreetNumber = property.PropertyAddressStreetNumber;
            model.PropertyAddressStreetName = property.PropertyAddressStreetName;
            model.PropertyAddressSuiteNumber = property.PropertyAddressSuiteNumber;
            model.PropertyAddressCity = property.PropertyAddressCity;
            model.PropertyAddressProvState = property.PropertyAddressProvState;
            model.PropertyAddressPostZipCode = property.PropertyAddressPostZipCode;
            model.PropertyAddressCountry = property.PropertyAddressCountry;

            model.NumberOfBathrooms = property.NumberOfBathrooms;
            model.NumberOfBedrooms = property.NumberOfBedrooms;
            model.NumberOfFloors = property.NumberOfFloors;
            model.NumberOfParkings = property.NumberOfParkings;
            model.IsShared = property.IsShared;


            model.Stove = property.Stove;
            model.Refrigerator = property.Refrigerator;
            model.Furniture = property.Furniture;
            model.AireConditioner = property.AireConditioner;
            model.TVInternet = property.TVInternet;
            model.Dishwasher = property.Dishwasher;
            model.Laundry = property.Laundry;
            model.Blinds = property.Blinds;
            model.CommonFacility = property.CommonFacility;
            model.Others = property.Others;
            model.Notes2 = property.Notes2;
            model.Security = property.Security;
            model.FireAlarm = property.FireAlarm;


            return View("RentDetails", model);
        }


        public ActionResult RequestInfo()
        {

            return View();
        }

        public JsonResult GetImagesForProperty(int id)
        {
            ProMasterPropertyDataEntities db = new ProMasterPropertyDataEntities();

            var result = from images in db.PropertyListingPictures.Where(p => p.PropertyId == id)
                         select new
                         {
                             id = images.PropertyListingPictureId,
                             name = images.ImgUrlThumb
                         };

            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Apply(int id)
        //{
        //    CreateApplicationViewModel applicaton = new CreateApplicationViewModel();


        //    return View("Apply", applicaton);
        //}


        //public string GetRandomNumber(int lenght)
        //{
        //    string number = RandomNumber.GetRandomNumber(10);

        //    ViewBag.rn = number;

        //    return number;
        //}
        /*
        [HttpPost]
        public ActionResult Apply(int id, CreateApplicationViewModel model)
        {
            Core.Property.TenancyApplication application = new Core.Property.TenancyApplication();
            DocumentCheckList list = new DocumentCheckList();
            //ScreenProcess process = new ScreenProcess();
            TenantScreening screen = new TenantScreening();

            application.ApplicantFirstName = Request.Form["ApplicantFirstName"];
            application.ApplicantLastName = Request.Form["ApplicantLastName"];
            application.ApplicantContactEmail = Request.Form["ApplicantContactEmail"];
            application.ApplicantContactTel = Request.Form["ApplicatnContactTel"];
            application.CurrentAddress = Request.Form["CurrentAddress"];
            application.CurrentEmployerContact = Request.Form["CurrentEmployerContact"];
            application.PreviousEmployerContact = Request.Form["PreviousEmployerContact"];
            application.CurrentLandlordContact = Request.Form["CurrentLandlordContact"];
            application.PreviousLandlordContact = Request.Form["PreviousLandlordContact"];
            application.CurrentMonthlyIncome = decimal.Parse(Request.Form["MonthlyIncome"]);
            application.CreditReportScore = Request.Form["CreditScore"];
            application.NumberOfChildren = Int32.Parse(Request.Form["NumberOfChildren"]);
            application.NumberOfTenant = Int32.Parse(Request.Form["NumberOfOccupants"]);
            application.IsAuthorizedForRefCheck = model.AuthorizedRefCheck;
            application.PropertyId = id;
            application.IsIdentificationDocAvailalbe = model.IdenticationAvailable;

            application.IsApproved = false;
            application.ApplicationSentDate = DateTime.Now;

            _propertyRepository.AddApplicant(application);
            _propertyRepository.Save();
            
            

            //Create screen table entries for the application
            //
            for (int i = 1; i <= 6; i++)
            {
                screen.ScreeningCheckTypeId = i;
                screen.ScreeningCheckStatusId = 1;
                screen.TeancyApplicationId = application.TenancyApplicationId;
                screen.ScreenCheckUpdateDate = DateTime.Now;

                _tenantRepository.AddTenantScreen(screen);
                _tenantRepository.Save();
            }


            //_tenantRepository.AddTenantScreen(process);
            


            //Insert into doc checklist table
            //
            list.DocumentCheckListCompletionDate = DateTime.Now;
            list.IsCreditReportProvided = false;
            list.IsEmployerReferenceProvided = false;
            list.IsIdentificationProvided = false;
            list.IsIncomeStatementProvided = false;
            list.IsLandLordReferenceProvided = false;
            list.TenancyApplicanionId = application.TenancyApplicationId;

            _tenantRepository.AddDocCheckList(list);
            _tenantRepository.Save();

            //Send confirmation email to the applicant if email is provided
            //
            SMTPService mail = new SMTPService();

            mail.SendMail("admin@promaster.com", model.ApplicantContactEmail, 
                "Tenancy Application", "Your application has been received. Please note the following documents are required:", false );

            return RedirectToAction("Index");
        }
         * */

        [HttpPost]
        public JsonResult Apply(int id, string firstName, string lastName,  string sin, string eMail, string telePhone, string currentAddr, string currentEmp, string previousEmp,
            string currentLand, string previousLand, decimal monthlyIncom, string creditScore, int NumTenant, int NumChild) // CreateApplicationViewModel model)
        {
            var application = new Core.Property.TenancyApplication();
            var list = new DocumentCheckList();
            //ScreenProcess process = new ScreenProcess();
            var screen = new TenantScreening();

            application.ApplicantFirstName = firstName;
            application.ApplicantLastName = lastName;
            application.ApplicantSIN = sin;
            application.ApplicantContactEmail = eMail;
            application.ApplicantContactTel = telePhone;
            application.CurrentAddress = currentAddr;
            application.CurrentEmployerContact = currentEmp;
            application.PreviousEmployerContact = previousEmp;
            application.CurrentLandlordContact = currentLand;
            application.PreviousLandlordContact = previousLand;
            application.CurrentMonthlyIncome = monthlyIncom;
            application.CreditReportScore = creditScore;
            application.NumberOfChildren = NumChild;
            application.NumberOfTenant = NumTenant;

            application.PropertyId = id;
            application.IsActive = true;
            application.IsAuthorizedForRefCheck = true;
            application.IsApproved = false;
            application.ApplicationSentDate = DateTime.Now;

            _propertyRepository.AddApplicant(application);
            _propertyRepository.Save();

            /*******************************************************************************************************************************/
            /* this section will be implemented separately, on the appliction details page, add a button of adding to screen process or so... 
            /*******************************************************************************************************************************/

            /*
            //Create screen table entries for the application
            //
            for (int i = 1; i <= 6; i++)
            {
                screen.ScreeningCheckTypeId = i;
                screen.ScreeningCheckStatusId = 1;
                screen.TeancyApplicationId = application.TenancyApplicationId;
                screen.ScreenCheckUpdateDate = DateTime.Now;

                _tenantRepository.AddTenantScreen(screen);
                _tenantRepository.Save();
            }


            //_tenantRepository.AddTenantScreen(process);



            //Insert into doc checklist table
            //
            list.DocumentCheckListCompletionDate = DateTime.Now;
            list.IsCreditReportProvided = false;
            list.IsEmployerReferenceProvided = false;
            list.IsIdentificationProvided = false;
            list.IsIncomeStatementProvided = false;
            list.IsLandLordReferenceProvided = false;
            list.TenancyApplicanionId = application.TenancyApplicationId;

            _tenantRepository.AddDocCheckList(list);
            _tenantRepository.Save();

             */

            /********************************************************************************************************************/
            /********************************************************************************************************************/

            //Send confirmation email to the applicant if email is provided
            //
            SMTPService mail = new SMTPService();

            mail.SendMail("admin@promaster.com", eMail,
                "Tenancy Application", "Your application has been received. You will be contacted for the processing updates", false);

            return Json("Application Sent successfully!");
        }
    }
}
