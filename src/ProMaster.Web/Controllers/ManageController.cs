using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Security;
using System.IO;
using RazorPDF;
using PagedList;

using ProMaster.Core.ViewModels;
using ProMaster.DAL.Property;
using ProMaster.DAL.Landlord;
using ProMaster.Core.PropertyOwner.ViewModels;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.Property;
using ProMaster.Core.Contract.ViewModels;
using ProMaster.DAL.Contract;
using ProMaster.Core.Tenant;
using ProMaster.Core.Tenant.ViewModels;
using ProMaster.DAL.Tenant;
using ProMaster.Core.Lease;
using ProMaster.Core.Lease.ViewModels;
using ProMaster.DAL.Lease;
using ProMaster.Infrastructure.Utilities;
using ProMaster.Core.Vendor.ViewModels;
using ProMaster.DAL.Vendor;
using ProMaster.Core.Vendor;
using ProMaster.Core.Contract;
using ProMaster.Core.StrataCouncil;
using ProMaster.Core.StrataCouncil.ViewModels;
using ProMaster.DAL.StrataCouncil;
using ProMaster.DAL.Document;
using ProMaster.Web.Filters;
using ProMaster.Infrastructure.Logging;

namespace ProMaster.Web.Controllers
{
    [Authorize] //(Roles = "PropertyManager")]
    [CustomHandleError]
    public class ManageController : Controller
    {

        #region DI Configuration

        IPropertyRepository _propertyRepository;
        IPropertyOwnerRepository _propertyOwnerRepository;
        IManagementContractRepository _contractRepository;
        ITenantRepository  _tenantRepository;
        ILeaseRepository _leaseRepository;
        IVendorRepository _vendorRepository;
        IStrataCouncilRepository _strataRepository;
        IDocumentRepository _documentRepository;

        public ManageController(IPropertyRepository propertyRepository, IPropertyOwnerRepository propertyOwnerRepository, 
            IManagementContractRepository contractRepository, ITenantRepository tenantRepository, ILeaseRepository leaseRepository,
            IVendorRepository vendorRepository, IStrataCouncilRepository strataRepository, IDocumentRepository documentRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "manage";

            _propertyRepository = propertyRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
            _contractRepository = contractRepository;
            _tenantRepository = tenantRepository;
            _leaseRepository = leaseRepository;
            _vendorRepository = vendorRepository;
            _strataRepository = strataRepository;
            _documentRepository = documentRepository;
        }

        #endregion

        // 
        // Logger
        readonly Log4NetLogger _logger = new Log4NetLogger();

        
        //Manage Home
        public ActionResult Index() //Dashboard style
        {
            ViewBag.CurrentPage = "manage";
            ManageDisplayViewModel management = new ManageDisplayViewModel();



            string[] userRoles = Roles.GetRolesForUser(User.Identity.Name);

            //if (User.IsInRole("PropertyManager"))
            if ( userRoles.Contains("PropertyManager") )
            {

                int pmId = _propertyRepository.GetPmId(User.Identity.Name);

                //IEnumerable<ShowLandlordViewModel> Landlords = _propertyOwnerRepository.ListAllLandlord(User.Identity.Name);

                IEnumerable<ManageDisplayViewModel> myLandlords = _propertyOwnerRepository.ListMyLandlords(User.Identity.Name);

                IEnumerable<ManageDisplayViewModel> myContracts = _contractRepository.GetMyContract(pmId);

                IEnumerable<ManageDisplayViewModel> myLease = _leaseRepository.GetLeaseInfo(pmId);

                IEnumerable<ManageDisplayViewModel> myApplication = _propertyRepository.GetAllApplicationsByPmId(pmId);

                IEnumerable<ManageDisplayViewModel> myVendor = _vendorRepository.GetAllVendors();

                IEnumerable<ManageDisplayViewModel> myStrataCouncil = _strataRepository.GetAllStrataCouncils();

                IEnumerable<ManageDisplayViewModel> myTenant = _tenantRepository.GetTenantList();


                //IEnumerable<ManageDisplayViewModel> PropertyForRent = _propertyRepository.ListPropertyForRent(); 

                management.LandlordList = myLandlords.Take(10);
                management.MyContractList = myContracts.Take(10);
                management.LeaseList = myLease.Take(10);
                management.ApplicatioinList = myApplication;
                management.VendorList = myVendor.Take(10);
                management.StrataCouncilList = myStrataCouncil.Take(10);
                management.Tenants = myTenant.Take(10);


                management.MyProperty = _propertyRepository.GetMyProperty(pmId).Take(10);
                //Management.PropertiesForRent = _propertyRepository.ListPropertyForRent();

                //IEnumerable<PropertyList> MyListing = _propertyRepository.GetListById(Management.MyProperty.FirstOrDefault().PropertyId);


            }
            else
            {
                if (userRoles.Contains("SuperAdmin"))
                {
                    return RedirectToAction("Index", "Admin"); //the destination page is to be determined
                }
                return RedirectToAction("Index", "ClientPortal");
            }

            return View("Index", management);

        }

        public ActionResult GetStarted()
        {


            return View();
        }

        #region Json Data for client side Ajax paging

        [HttpPost]
        public JsonResult GetTotalPropertyRecords()
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            int NumberOfProperties = _propertyRepository.GetMyProperty(pmId).Count();

            return Json(NumberOfProperties);
        }


        [HttpPost]
        public JsonResult GetPagedPropertyData(int pageIndex, int pageSize)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            var data = _propertyRepository.GetMyProperty(pmId).Skip(pageIndex * pageSize).Take(pageSize);
                        
            return Json(data);
        }
        #endregion


        #region Manage Properties


        public ActionResult AddProperty()
        {
            CreatePropertyViewModel model = new CreatePropertyViewModel();

            var brmList = new List<SelectListItem>
            {
                new SelectListItem {Text = "One", Value="1"},
                new SelectListItem {Text = "Two", Value="2"},
                new SelectListItem {Text = "Three", Value="3"},
                new SelectListItem {Text = "Four", Value="4"},
                new SelectListItem {Text = "Five", Value="5"},
                new SelectListItem {Text = "Six or more", Value="6"},

            };

            var btmList = new List<SelectListItem>
            {
                new SelectListItem {Text = "One", Value="1"},
                new SelectListItem {Text = "Two", Value="2"},
                new SelectListItem {Text = "Three", Value="3"},
                new SelectListItem {Text = "Four", Value="4"},
                new SelectListItem {Text = "Five", Value="5"},
                new SelectListItem {Text = "Six or more", Value="6"},

            };
            
            var floorList = new List<SelectListItem>
            {
                new SelectListItem {Text = "One", Value= "1" },
                new SelectListItem {Text = "Two", Value="2"},
                new SelectListItem {Text = "Three", Value="3"}               

            };

            var parkingList = new List<SelectListItem>
            {
                new SelectListItem {Text = "One", Value="1"},
                new SelectListItem {Text = "Two", Value="2"},
                new SelectListItem {Text = "Three", Value="3"}              

            };

            var provinceList = new List<SelectListItem>
            {
                //new SelectListItem {Text = "Alberta", Value="1"},
                new SelectListItem {Text = "British Columbia", Value="1"},                

            };

            var countryList = new List<SelectListItem>
            {                
                new SelectListItem {Text = "Canada", Value="1"},                

            };


            ViewBag.DropdownData1 = btmList;
            ViewBag.DropdownData2 = brmList;
            ViewBag.DropdownData3 = floorList;
            ViewBag.DropdownData4 = parkingList;

            ViewBag.DropdownData5 = provinceList;

            ViewBag.DropdownData6 = countryList;

            var propertyTypes = _propertyRepository.GetPropertyTypes();
            //var PropertyStatus = _propertyRepository.GetPropertyStatus(); The rental status is always "pre-listing, id=4" when new property is added
            var PropertyOwners = _propertyRepository.GetLandlordsByPm(User.Identity.Name);

            model.PropertyType = propertyTypes.ToPropertyTypeList(-1);

            //model.PropertyStatus = PropertyStatus.ToPropertyStatusList(-1);
            
            model.LandlordList = PropertyOwners.ToLandlordList(-1);

            return View("AddProperty", model);
        }

        public ActionResult AllProperties() //For site admin view
        {
            return View();
        }

        //public ActionResult AllPropertiesByPm()
        public ActionResult AllPropertiesByPm(string currentFiler, int? page)
        {
            var role = Roles.GetRolesForUser(User.Identity.Name);

            if(role.Contains("PropertyManager"))
            {
                int pmId = _propertyRepository.GetPmId(User.Identity.Name);

                //IEnumerable<ManageDisplayViewModel> properties = _propertyRepository.GetPropertyListByPmId(pmId);

                var properties = _propertyRepository.GetPropertyListByPmId(pmId).OrderBy(d => d.CreateDate);

                const int pageSize = 10; //for testing purpose, to be adjustetd
                int pageIndex = (page ?? 1) - 1;
                int pageNumber = (page ?? 1);

                //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
                var paginatedPropertyList = properties.ToPagedList(pageNumber, pageSize);

                return View(paginatedPropertyList);
                //return RedirectToAction("Index", "Manage");
            }
            else
            {
                if(role.Contains("SuperAdmin"))
                {
                    return RedirectToAction("AllProperties","Admin");
                }
                else
                {
                    return RedirectToAction("Index", "ClientPortal");
                }
            }
            
        }
        
        [HttpPost]
        public ActionResult AddProperty(CreatePropertyViewModel model, FormCollection form)
        {
            ProMaster.Core.Property.Property property = new ProMaster.Core.Property.Property();
            Core.Property.PropertyAddress Address = new Core.Property.PropertyAddress();
            PropertyFacility Facility = new PropertyFacility();
            PropertyFeature Feature = new PropertyFeature();


            if (ModelState.IsValid)
            {

                //Property
                //
                property.PropertyName = Request.Form["PropertyName"];
                property.PropertyDesc = Request.Form["PropertyDesc"];
                property.PropertyImgUrl = "/Content/Documents/Property/porperty_default.png";
                property.PropertyVideoUrl = "default";
                property.PropertyBuildYear = Int32.Parse(Request.Form["PropertyBuildYear"]);                
                property.PropertyManagerId = _propertyRepository.GetPmId(User.Identity.Name);
                property.IsActive = model.IsActive;
                property.RentalStatusId = 4; // model.RentalStatusId; The rental status is always "pre-listing, id=4" when new property is added
                property.PropertyOwnerId = model.LandlordId;
                property.PropertyTypeId = model.PropertyTypeId;
                property.CreatedDate = DateTime.Now;
                property.UpdateDate = DateTime.Now;
                property.PropertyMgmntlStatusId = 0;

                //Add property Address
                //
                Address.PropertySuiteNumber = Request.Form["PropertyAddressSuiteNumber"];
                Address.PropertyNumber = Request.Form["PropertyAddressStreetNumber"];
                Address.PropertyStreet = Request.Form["PropertyAddressStreetName"];
                Address.PropertyCity = Request.Form["PropertyAddressCity"];
                Address.PropertyStateProvince = Request.Form["PropertyAddressProvState"];
                Address.PropertyZipPostCode = Request.Form["PropertyAddressPostZipCode"];
                Address.PropertyCountry = Request.Form["PropertyAddressCountry"];

                //Save address
                //
                _propertyRepository.AddPropertyAddress(Address);
                
                _propertyRepository.Save();

                property.PropertyAddress = Address;                   

                property.PropertyAddressId = Address.PropertyAddressId;


                //Add property facility
                //
                Facility.AirConditioner = model.AireConditioner;
                Facility.Refrigerator = model.Refrigerator;
                Facility.BlindsCurtain = model.BasementAvailable;
                Facility.CommonFacility = model.CommonFacility;
                Facility.Dishwasher = model.Dishwasher;
                Facility.FireAlarmSystem = model.FireAlarm;
                Facility.Furniture = model.Furniture;
                Facility.Laundry = model.Laundry;
                Facility.TVInternet = model.TVInternet;
                Facility.SecuritySystem = model.Security;
                Facility.Stove = model.Stove;
                Facility.Others = Request.Form["Others"];
                Facility.Notes = Request.Form["Notes1"];

                //Save facility
                //
                _propertyRepository.AddPropertyFacility(Facility);
                //_propertyRepository.Save();

                //Add property feature
                //
                Feature.NumberOfBathrooms = Int32.Parse(Request.Form["NumberOfBedrooms"]); //Convert.ToInt32(Request.Form["NumberOfBathrooms"]);
                Feature.NumberOfBedrooms = Int32.Parse(Request.Form["NumberOfBedrooms"]);
                Feature.NumberOfLayers = Int32.Parse(Request.Form["NumberOfFloors"]);
                Feature.NumberOfParking = Int32.Parse(Request.Form["NumberOfParkings"]);
                Feature.TotalLivingArea = Int32.Parse(Request.Form["TotalLivingArea"]);                
                Feature.BasementAvailable = model.BasementAvailable;
                Feature.IsShared = model.IsShared;
                Feature.Notes = Request.Form["Notes2"];

                //Save features
                //
                _propertyRepository.AddPropertyFeature(Feature);
                _propertyRepository.Save();

                property.PropertyFacility = Facility;
                property.PropertyFacilityId = Facility.PropertyFacilityId;    

                property.PropertyFeature = Feature;
                property.PropertyFeatureId = Feature.PropertyFeatureId;

                //Add property listing information
                //


                //PropertyList propertyList = new PropertyList();

                //propertyList.IsActive = true;
                //propertyList.ListedRentAmount = "";


                
                //Save property
                //
                //_propertyRepository.AddProperty(property);
                
                //_propertyRepository.Save();

                _logger.Info("Prooperty " + property.PropertyName + " has been added.");

                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return View("AddProperty", model);
            }
        }

        public ActionResult PropertyDetails(int id)
        {
            DisplayPropertyViewModel model = new DisplayPropertyViewModel();
                
            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.GetPropertyDetails(id);

            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);
            IEnumerable<PropertyFacility> facility = _propertyRepository.GetFacilityById(property.FirstOrDefault().PropertyFacilityId);
            IEnumerable<PropertyFeature> feature = _propertyRepository.GetFeatureById(property.FirstOrDefault().PropertyFeatureId);
            IEnumerable<Core.Property.PropertyOwner> owner = _propertyRepository.GetLandlordById(property.FirstOrDefault().LandlordId);
            IEnumerable<PropertyList> list = _propertyRepository.GetListById(id);
            IEnumerable<Core.Property.Document> document = _propertyRepository.DocumenetByPropertyid(id, 1);
            //IEnumerable<Core.Property.Document> document = _propertyRepository.GetDocumenetByPropertyid(id);

            //IEnumerable<Core.Vendor.WorkOrder> workOrder = _vendorRepository.GetWorkOrderHistoryByPropertyId(id);
            IEnumerable<DisplayPropertyViewModel> workOrder = _vendorRepository.GetWorkOrdersByPropertyId(id);
            IEnumerable<DisplayPropertyViewModel> otherCost = _vendorRepository.GetOtherCostByPropertyId(id);
            IEnumerable<DisplayPropertyViewModel> events = _propertyRepository.EventListByProperty(id);

            //Core.Property.Lease lease = _propertyRepository.LeaseById(id);

            //--Start of test code              //--End of test code

            IEnumerable<Core.Lease.Lease> leaseList = _leaseRepository.GetLeaseByPropertyId(property.FirstOrDefault().PropertyId);

            if (leaseList.Count() != 0)
            {
                IEnumerable<Core.Tenant.Tenant> tenantList = _tenantRepository.GetTenantsByLeaseId(leaseList.FirstOrDefault().LeaseId);
                model.LeaseInfo = leaseList; //test code
                model.TenantInfo = tenantList; //test code
            }
            else
            {
                model.LeaseInfo = null;
                model.TenantInfo = null;
            }
            

            var docTypeList = _documentRepository.GetDocType();
            model.DocumentTypeList = docTypeList.ToDocTypeList(-1);

            var principalTypeList = _documentRepository.GetPrincipaltype();
            model.DocumentTyPrincipalpeList = principalTypeList.ToPrincipalTypeList(-1);


            model.Address = address;
            model.Feature = feature;
            model.Facility = facility;
            model.ListInfo = list;
            model.DocumentInfo = document;
            model.WorkOrderList = workOrder;
            model.OtherCostList = otherCost;
            model.Landlords = owner;
            model.EventList = events;
            

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;
            model.PropertyDesc = property.FirstOrDefault().PropertyDesc;
            model.PropertyType = property.FirstOrDefault().PropertyType;
            model.PropertyBuildYear = property.FirstOrDefault().PropertyBuildYear;
            model.IsActive = property.FirstOrDefault().IsActive;
            model.CreationDate = property.FirstOrDefault().CreationDate;
            model.UpdateDate = property.FirstOrDefault().UpdateDate;
            model.Status = property.FirstOrDefault().Status;
            model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
            model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;

            model.NumberOfBathrooms = feature.FirstOrDefault().NumberOfBathrooms;
            model.NumberOfBedrooms = feature.FirstOrDefault().NumberOfBedrooms;
            model.NumberOfFloors = feature.FirstOrDefault().NumberOfLayers;
            model.NumberOfParkings = feature.FirstOrDefault().NumberOfParking;
            model.IsShared = feature.FirstOrDefault().IsShared;
            model.TotalLivingArea = feature.FirstOrDefault().TotalLivingArea;

            model.Stove = facility.FirstOrDefault().Stove;
            model.Refrigerator = facility.FirstOrDefault().Refrigerator;
            model.Furniture = facility.FirstOrDefault().Furniture;
            model.AireConditioner = facility.FirstOrDefault().AirConditioner;
            model.TVInternet = facility.FirstOrDefault().TVInternet;
            model.Dishwasher = facility.FirstOrDefault().Dishwasher;
            model.Laundry = facility.FirstOrDefault().Laundry;
            model.Blinds = facility.FirstOrDefault().BlindsCurtain;
            model.CommonFacility = facility.FirstOrDefault().CommonFacility;
            model.Others = facility.FirstOrDefault().Others;
            model.Notes2 = facility.FirstOrDefault().Notes;
            model.Security = facility.FirstOrDefault().SecuritySystem;
            model.FireAlarm = facility.FirstOrDefault().FireAlarmSystem;
            model.LandlordId = owner.FirstOrDefault().PropertyOwnerId;
            model.LandlordLastName = owner.FirstOrDefault().LastName;
            model.LandlordFirstName = owner.FirstOrDefault().FirstName;
            model.LandlordContactEmail = owner.FirstOrDefault().ContactEmail;
            model.LandlordContactTelephone = owner.FirstOrDefault().ContactTelephone1;

            if(model.Status == "Listed")
            {
                model.MonthlyRent = list.FirstOrDefault().ListedRentAmount;
                model.ListingDate = list.FirstOrDefault().ListedDate;
            }
           


            return View("PropertyDetails", model);
        }

        public ActionResult EditProperty(int id)
        {
            DisplayPropertyViewModel property = _propertyRepository.GetPropertyById(id);

            var pType = _propertyRepository.GetPropertyTypes();
            property.PropertyTypeList = pType.ToPropertyTypeList(-1);

            var status = _propertyRepository.GetPropertyStatus();
            property.PropertyStatus = status.ToPropertyStatusList(-1);

            return View("EditProperty", property);
        }

        [HttpPost]
        public ActionResult EditProperty(int id, DisplayPropertyViewModel model)
        {
            ProMaster.Core.Property.Property property = _propertyRepository.PropertyById(id);
            Core.Property.PropertyAddress Address = _propertyRepository.AddressById(property.PropertyAddressId);
            PropertyFacility Facility = _propertyRepository.FacilityById(property.PropertyFacilityId);
            PropertyFeature Feature = _propertyRepository.FeaturesById(property.PropertyFeatureId);
            //PropertyType type = _propertyRepository



            #region Update/Update property image

            var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

            if (iconImage != null)
            {
                var origImageFileName = Path.GetFileName(iconImage.FileName);

                var fileExtension = FileProcessor.GetFileExtension(origImageFileName);

                //var iconImageFileName = Path.GetFileNameWithoutExtension(origImageFileName);

                //var newImageFileName = iconImageFileName + "_album_cover" + fileExtension;
                var newImageFileName = "Property_" + property.PropertyId + fileExtension;

                iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Property/", newImageFileName));

                property.PropertyImgUrl = Path.Combine("~/Content/Documents/Property/", newImageFileName);
 
            }

            #endregion



            property.PropertyName = Request.Form["PropertyName"];
            property.PropertyDesc = Request.Form["PropertyDesc"];
            //property.PropertyImgUrl = "~/Content/Documents/Prooperty/porperty_default.png";
            //property.PropertyVideoUrl = "default";
            property.PropertyBuildYear = Convert.ToInt32(Request.Form["PropertyBuildYear"]);
            property.PropertyManagerId = _propertyRepository.GetPmId(User.Identity.Name);
            property.IsActive = model.IsActive;
            property.RentalStatusId = model.RentalStatusId;
            //property.PropertyOwnerId =  model.LandlordId;
            property.PropertyTypeId = model.PropertyTypeId;  
          
            property.UpdateDate = DateTime.Now;

            //_propertyRepository.Save();

            Address.PropertySuiteNumber = Request.Form["PropertyAddressSuiteNumber"];
            Address.PropertyNumber = Request.Form["PropertyAddressStreetNumber"];
            Address.PropertyStreet = Request.Form["PropertyAddressStreetName"];
            Address.PropertyCity = Request.Form["PropertyAddressCity"];
            Address.PropertyStateProvince = Request.Form["PropertyAddressProvState"];
            Address.PropertyZipPostCode = Request.Form["PropertyAddressPostZipCode"];
            Address.PropertyCountry = Request.Form["PropertyAddressCountry"];

            Facility.AirConditioner = model.AireConditioner;
            Facility.Refrigerator = model.Refrigerator;
            Facility.BlindsCurtain = model.BasementAvailable;
            Facility.CommonFacility = model.CommonFacility;
            Facility.Dishwasher = model.Dishwasher;
            Facility.FireAlarmSystem = model.FireAlarm;
            Facility.Furniture = model.Furniture;
            Facility.Laundry = model.Laundry;
            Facility.TVInternet = model.TVInternet;
            Facility.SecuritySystem = model.Security;
            Facility.Stove = model.Stove;
            Facility.Others = Request.Form["Others"];

            Feature.NumberOfBathrooms = Int32.Parse(Request.Form["NumberOfBathrooms"]); //Convert.ToInt32(Request.Form["NumberOfBathrooms"]);
            Feature.NumberOfBedrooms = Int32.Parse(Request.Form["NumberOfBedrooms"]);
            Feature.NumberOfLayers = Int32.Parse(Request.Form["NumberOfFloors"]);
            Feature.NumberOfParking = Int32.Parse(Request.Form["NumberOfParkings"]);
            Feature.TotalLivingArea = Int32.Parse(Request.Form["TotalLivingArea"]);
            Feature.BasementAvailable = model.BasementAvailable;
            Feature.IsShared = model.IsShared;
            Feature.Notes = Request.Form["Notes2"];

            _propertyRepository.Save();

            return RedirectToAction("PropertyDetails/" + property.PropertyId);
        }

        public ActionResult UpdateImg(int id, DisplayPropertyViewModel model)
        {

            ProMaster.Core.Property.Property property = _propertyRepository.PropertyById(id);

            var iconImage = WebImage.GetImageFromRequest(); //Get uploaded file   

            if (iconImage != null)
            {
                var origImageFileName = Path.GetFileName(iconImage.FileName);

                var fileExtension = FileProcessor.GetFileExtension(origImageFileName);

                //var iconImageFileName = Path.GetFileNameWithoutExtension(origImageFileName);

                //var newImageFileName = iconImageFileName + "_album_cover" + fileExtension;
                var newImageFileName = "Property_" + property.PropertyId + fileExtension;

                iconImage.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Property/", newImageFileName));

                property.PropertyImgUrl = Path.Combine("/Content/Documents/Property/", newImageFileName);

                property.UpdateDate = DateTime.Now;

                _propertyRepository.Save();

            }

            return RedirectToAction("PropertyDetails/" + property.PropertyId);  
        }


        //public JsonResult DeleteProperty(int id) //use ajax to deactivate property (set isActivre to false - soft delete)
        public ActionResult DeleteProperty(int id)
        {
            ProMaster.Core.Property.Property property = _propertyRepository.PropertyById(id);

            property.IsActive = false;

            _propertyRepository.Save();

            //return Json("The property has been deactivated and marked for complete deletion!");
            return Content(Boolean.TrueString);
        }

        public void ProcessImage()
        { 

        }

        //[HttpPost]
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

        //**************************************************************
        //******************Management event****************************
        //**************************************************************
        //

        [HttpPost]
        public JsonResult AddEvent(int id, string eventName, int eventTypeId, DateTime eventDate, string eventDetails, decimal Mileage, decimal TimeSpent) //id is PropertyId
        {            
            ManagementEvent MgmntEvent = new ManagementEvent();
            try
            {
                MgmntEvent.EventName = eventName;
                MgmntEvent.EventTypeId = eventTypeId;
                MgmntEvent.EventDetails = eventDetails;
                MgmntEvent.EventDate = eventDate;
                MgmntEvent.CreationDate = DateTime.Now;
                MgmntEvent.PropertyId = id;

                MgmntEvent.MileageIncurred = Mileage;
                MgmntEvent.TimeSpent = TimeSpent;

                _propertyRepository.AddEvent(MgmntEvent);
                _propertyRepository.Save();

                return Json("Event Added!");
            }
            catch 
            { 
                return Json("Error occured!");
            }
        }

        public ActionResult GetEventsByProperty(int id)
        {
            //throw new NotImplementedException();
            DisplayPropertyViewModel model = new DisplayPropertyViewModel();

            IEnumerable<DisplayPropertyViewModel> eventList = _propertyRepository.EventListByProperty(id);

            model.EventList = eventList;

            return PartialView("_EventList", model);
        }

        public ActionResult EventDetails(int id)
        {
            DisplayPropertyViewModel model = new DisplayPropertyViewModel();

            DisplayPropertyViewModel ManagementEvent = _propertyRepository.GetEventByid(id);

            model.EventDate = ManagementEvent.EventDate;
            model.EventId = ManagementEvent.EventId;
            model.EventTitle = ManagementEvent.EventTitle;
            model.EventDetails = ManagementEvent.EventDetails;
            model.TimeSpent = ManagementEvent.TimeSpent;
            model.MileageIncurred = ManagementEvent.MileageIncurred;
            model.EventType = ManagementEvent.EventType;

            model.PropertyName = ManagementEvent.PropertyName;
            model.RentStatus = ManagementEvent.RentStatus;
            model.PropertyDesc = ManagementEvent.PropertyDesc;


            return View("EventDetails", model);
        }
       
        public ActionResult GetListedPropertyList()
        {
            CreateLeaseViewModel lease = new CreateLeaseViewModel();

            var Property = _leaseRepository.GetLeaseProperty();

            lease.PropertyList = Property.ToPropertyList(-1);
            //IEnumerable<Core.Lease.Property> properties = _leaseRepository.GetAllProperty();

            return PartialView("_propertyList", lease);
        }

        public ActionResult GetAllPropertyList()
        {
            CreateLeaseViewModel lease = new CreateLeaseViewModel();

            var Property = _leaseRepository.GetAllProperty();

            lease.PropertyList = Property.ToPropertyList(-1);
            //IEnumerable<Core.Lease.Property> properties = _leaseRepository.GetAllProperty();

            return PartialView("_propertyList", lease);
        }

        #endregion
        

        #region Manage Property Owners

        public ActionResult PropertyOwnerDetails(int id)
        {
            
            //ShowLandlordViewModel Landlord = 
            ShowLandlordViewModel model = new ShowLandlordViewModel();

            model.PropertyList = _propertyOwnerRepository.GetPropertytByOwnerId(id);

            if (model.PropertyList.Count() != 0)
            {
                ShowLandlordViewModel landlord = _propertyOwnerRepository.GetLandlordById(id);
                
                model.DocumentInfo = _propertyOwnerRepository.GetDocumentByOwnerId(id);
                model.ContractList = _propertyOwnerRepository.GetContractByPropertyId(id);

                model.LandlordFirstName = landlord.LandlordFirstName;
                model.LandlordLastName = landlord.LandlordLastName;
                model.LandlordContactEmail = landlord.LandlordContactEmail;
                model.LandlordContactTelephone1 = landlord.LandlordContactTelephone1;
                model.LandlordContactTelephone2 = landlord.ContactTelephone2;
                model.LandlordAvartaImgUrl = landlord.LandlordAvartaImgUrl;
                model.LandlordCreationDate = landlord.LandlordCreationDate;
                model.LandlordUpdateDate = landlord.LandlordUpdateDate;
                model.LandlordId = landlord.LandlordId;
                model.OnLineAccessEnabled = landlord.OnLineAccessEnabled;
                model.UserName = landlord.UserName;
            }
            else 
            {
                ShowLandlordViewModel landlord = _propertyOwnerRepository.GetLandlordOnlyById(id);

                model.DocumentInfo = _propertyOwnerRepository.GetDocumentByOwnerId(id);
                model.ContractList = _propertyOwnerRepository.GetContractByPropertyId(id);
                model.LandlordFirstName = landlord.LandlordFirstName;
                model.LandlordLastName = landlord.LandlordLastName;
                model.LandlordContactEmail = landlord.LandlordContactEmail;
                model.LandlordContactTelephone1 = landlord.LandlordContactTelephone1;
                model.LandlordContactTelephone2 = landlord.ContactTelephone2;
                model.LandlordAvartaImgUrl = landlord.LandlordAvartaImgUrl;
                model.LandlordCreationDate = landlord.LandlordCreationDate;
                model.LandlordUpdateDate = landlord.LandlordUpdateDate;
                model.LandlordId = landlord.LandlordId;
                model.OnLineAccessEnabled = landlord.OnLineAccessEnabled;
                model.UserName = landlord.UserName;
            }

            // Maybe there is no need to have the if...else here?

            return View("PropertyOwnerDetails", model);
        }

        public ActionResult AllPropertyOwners()
        {
            return View();
        }
       
        public ActionResult AddPropertyOwner()
        {
            CreateLandlordViewModel Landlord = new CreateLandlordViewModel();

            //string returnUrl = "";

            //if (HttpContext.Request.UrlReferrer != null)
            //{
            //    returnUrl = HttpContext.Request.UrlReferrer.PathAndQuery;
            //}

            //TempData["url"] = returnUrl;

            return View("AddPropertyOwner", Landlord);
        }
        
        [HttpPost]
        public ActionResult AddPropertyOwner(CreateLandlordViewModel model, FormCollection form)
        {
            Core.PropertyOwner.PropertyOwner Owner = new Core.PropertyOwner.PropertyOwner();

            //string Url0 = TempData["url"] as string;

            //string Url = Url0.Remove(1, 7);

            //string Url = "";

            //if (TempData["url"] != null)
            //{
            //    string Url0 = TempData["url"] as string;

            //    Url = Url0.Remove(1, 7);

            //}
            //else
            //{
            //    Url = "";
            //}

            Owner.FirstName = Request.Form["FistName"];
            Owner.LastName = Request.Form["LastName"];
            Owner.ContactEmail = Request.Form["ContactEmail"];
            Owner.ContactTelephone1 = Request.Form["ContactEmail"];
            Owner.ContactTelephone2 = Request.Form["ContactEmai2"];
            Owner.IsActive = true;
            Owner.UserAvartaImgUrl = "~/Content/Documents/Landlord/user_default.png";
            Owner.UserName = "tba";
            Owner.RoleId = 4;
            Owner.CreationDate = DateTime.Now;
            Owner.UpdateDate = DateTime.Now;
            Owner.OnlineAccessEnbaled = false; // model.OnLineAccessEnabled; This feature is currently disabled
            Owner.AddedBy = User.Identity.Name;
            Owner.AddProperty = model.AddProperty;

            _propertyOwnerRepository.AddPropertyOwner(Owner);
            _propertyOwnerRepository.Save();

            //if (Url.Length > 0)
            //{
            //    return RedirectToAction(Url, "Manage");
            //}
            //else
            //{
            return RedirectToAction("AllLandlordsByPm");
            //}
            
        }

        [HttpPost]
        public JsonResult AddOwner(string firstName, string lastName, string eMail, string telePhone, string telePhone2 )
        {
            Core.PropertyOwner.PropertyOwner owner = new Core.PropertyOwner.PropertyOwner();

            owner.FirstName = firstName;
            owner.LastName = lastName;
            owner.ContactEmail = eMail;
            owner.ContactTelephone1 = telePhone;
            owner.ContactTelephone2 = telePhone2;
            owner.UserName = "tba";
            owner.OnlineAccessEnbaled = false;
            owner.UserAvartaImgUrl = "~/Content/Documents/Landlord/user_default.png";
            owner.IsActive = true;
            owner.RoleId = 4;
            owner.AddedBy = User.Identity.Name;
            owner.CreationDate = DateTime.Now;
            owner.UpdateDate = DateTime.Now;

            _propertyOwnerRepository.AddPropertyOwner(owner);
            _propertyOwnerRepository.Save();

            return Json("Property owner added!");
        }


        public ActionResult ListAllLandlords()
        {
            IEnumerable<ShowLandlordViewModel> Landlords = _propertyOwnerRepository.ListAllLandlord(User.Identity.Name);

            return View(Landlords);
        }

        
        public ActionResult GetPropertyOwners()
        {
            CreatePropertyViewModel model = new CreatePropertyViewModel();

            var owners = _propertyRepository.GetLandlordsByPm(User.Identity.Name);

            model.LandlordList = owners.ToLandlordList(-1);

           return PartialView("_ownerList", model);
 
        
        }


        public ActionResult AllLandlordsByPm(string currentFiler, int? page)
        {
            IEnumerable<ManageDisplayViewModel> allLandlords = _propertyOwnerRepository.ListMyLandlords(User.Identity.Name).Distinct().OrderByDescending(d=>d.CreateDate);

            const int pageSize = 10; //for testing purpose, to be adjustetd

            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
            var paginatedOwnerList = allLandlords.ToPagedList(pageNumber, pageSize);

            return View(paginatedOwnerList);

        }

        public ActionResult EditLandlord(int id)
        {
            Core.Property.PropertyOwner owner = _propertyRepository.GetLandlordById(id).FirstOrDefault();

            return View(owner);
        }

        [HttpPost]
        public ActionResult EditLandlord(int id, FormCollection collection)
        {
            Core.Property.PropertyOwner owner = _propertyRepository.GetLandlordById(id).FirstOrDefault();

            owner.FirstName = Request.Form["FirstName"];
            owner.LastName = Request.Form["LastName"];
            owner.ContactEmail = Request.Form["ContactEmail"];
            owner.ContactTelephone1 = Request.Form["ContactTelephone1"];
            owner.ContactTelephone2 = Request.Form["ContactTelephone2"];
            owner.UpdateDate = DateTime.Now;

            _propertyRepository.Save();


            return RedirectToAction("PropertyOwnerDetails/" + id); ;
        }


        public ActionResult DeleteOwner(int id)
        {           
            Core.Property.PropertyOwner owner = _propertyRepository.GetLandlordById(id).FirstOrDefault();
            owner.IsActive = false;

            _tenantRepository.Save();

            return Content(Boolean.TrueString);

        }
        
        #endregion

        #region Management Contract

        public ActionResult AddManagementContract()
        {
            var contract = new CreateContractViewModel();

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            var feeFrequencyList = _contractRepository.GetFeeFrequency();
            var propertyList = _contractRepository.GetMyProperty(pmId);
            

            contract.FeeFrequency = feeFrequencyList.ToFeeFrequencyList(-1);
            contract.Property = propertyList.ToPropertyList(-1);

            return View("AddManagementContract", contract);
        }

        [HttpPost]
        public ActionResult AddManagementContract(CreateContractViewModel model, FormCollection collection)
        {
            Core.Contract.ManagementContract contract = new Core.Contract.ManagementContract();

            Core.Contract.Property property = _contractRepository.GetManagedProperty(model.PropertyId);

            if (ModelState.IsValid)
            {
                contract.ManagementContractTitile = Request.Form["ContractTitle"];
                contract.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                contract.EndDate = DateTime.Parse(Request.Form["EndDate"]);
                contract.ListingFeeScale = Request.Form["ListingFeeScale"];
                contract.ManagementFeeScale = Request.Form["FeeScale"];
                contract.PropertyId = model.PropertyId;
                contract.ManagementFeeFrequencyId = model.FeeFrequencyId;
                contract.ContractSignDate = DateTime.Parse(Request.Form["SignDate"]);
                contract.ManagementContractDocUrl = "default";
                contract.CreateDate = System.DateTime.Now;
                contract.UpdateDate = System.DateTime.Now;

                property.PropertyMgmntlStatusId = 1;
                contract.IsActive = true;

                _contractRepository.AddManagementContract(contract);
                _contractRepository.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult ContractDetails(int id)
        {
            DisplayContractViewModel model = new DisplayContractViewModel();

            IEnumerable<DisplayContractViewModel> contract = _contractRepository.GeteContractDetails(id);

            IEnumerable<Core.Contract.Document> document = _contractRepository.DocumenetByContractId(id, 2);

            var displayContractViewModels = contract as DisplayContractViewModel[] ?? contract.ToArray();
            IEnumerable<Core.Contract.Property> property = _contractRepository.GetPropertyByOwner(displayContractViewModels.FirstOrDefault().LandlordId);

            IEnumerable<DisplayContractViewModel> fees = _contractRepository.GetFeeHistoryByContract(id);


            var docTypeList = _documentRepository.GetDocType();
            model.DocumentTypeList = docTypeList.ToDocTypeList(-1);

            var principalTypeList = _documentRepository.GetPrincipaltype();
            model.DocumentTyPrincipalList = principalTypeList.ToPrincipalTypeList(-1);

            model.DocumentInfo = document;
            model.FeeHistoryList = fees;

            model.ContractId = displayContractViewModels.FirstOrDefault().ContractId;
            model.ContractTitle = displayContractViewModels.FirstOrDefault().ContractTitle;
            model.StartDate = displayContractViewModels.FirstOrDefault().StartDate;
            model.EndDate = displayContractViewModels.FirstOrDefault().EndDate;
            model.ManagementFeeFrequency = displayContractViewModels.FirstOrDefault().ManagementFeeFrequency;
            model.FeeScale = displayContractViewModels.FirstOrDefault().FeeScale;
            model.ListingFeeScale = displayContractViewModels.FirstOrDefault().ListingFeeScale;
            model.CreateDate = displayContractViewModels.FirstOrDefault().CreateDate;
            model.UpdateDate = displayContractViewModels.FirstOrDefault().UpdateDate;
            model.SignDate = displayContractViewModels.FirstOrDefault().SignDate;

            model.LandlordId = displayContractViewModels.FirstOrDefault().LandlordId;
            model.LandlordFirstName = displayContractViewModels.FirstOrDefault().LandlordFirstName;
            model.LandlordLastName = displayContractViewModels.FirstOrDefault().LandlordLastName;
            model.LandlordEmail = displayContractViewModels.FirstOrDefault().LandlordEmail;
            model.LandlordTelepone = displayContractViewModels.FirstOrDefault().LandlordTelepone;

            model.PropertyId = displayContractViewModels.FirstOrDefault().PropertyId;
            model.PropertyName = displayContractViewModels.FirstOrDefault().PropertyName;

            model.FeeMonth = displayContractViewModels.FirstOrDefault().FeeMonth;
            model.FeeYear = displayContractViewModels.FirstOrDefault().FeeYear;
            model.FeeType = displayContractViewModels.FirstOrDefault().FeeType;
            model.FeePaidDate = displayContractViewModels.FirstOrDefault().FeePaidDate;
            model.FeePaidAmount = displayContractViewModels.FirstOrDefault().FeePaidAmount;


            return View("ContractDetails", model);
        }

        public ActionResult EditContract(int id)
        {
            CreateContractViewModel contract = _contractRepository.GetContractById(id);

            var FeeFrequencyList = _contractRepository.GetFeeFrequency();
            contract.FeeFrequency = FeeFrequencyList.ToFeeFrequencyList(-1);

            return View("EditContract", contract);
        }

        [HttpPost]
        public ActionResult EditContract(int id, CreateContractViewModel model)
        {
            //CreateContractViewModel model = new CreateContractViewModel();

            Core.Contract.ManagementContract contract = _contractRepository.GetContract(id);

            contract.ManagementContractTitile = Request.Form["ContractTitle"];
            contract.StartDate = DateTime.Parse(Request.Form["StartDate"]);
            contract.EndDate = DateTime.Parse(Request.Form["EndDate"]);
            //contract.ContractSignDate = DateTime.Parse(Request.Form["SignDate"]);
            contract.ManagementFeeFrequencyId = model.FeeFrequencyId;
            contract.ListingFeeScale = Request.Form["ListingFeeScale"];
            contract.ManagementFeeScale = Request.Form["FeeScale"];
            contract.UpdateDate = DateTime.Now;

            _contractRepository.Save();

            return RedirectToAction("ContractDetails/" + id);
        }

        public ActionResult AllContractByPm(string currentFiler, int? page)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<ManageDisplayViewModel> contractList = _contractRepository.GetMyContract(pmId).OrderBy(d => d.CreateDate);

            const int pageSize = 2; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
            var paginatedContractList = contractList.ToPagedList(pageNumber, pageSize);

            return View(paginatedContractList);
        }

        public ActionResult DeleteContract(int id)
        {
            Core.Contract.ManagementContract contract = _contractRepository.GetContract(id);

            contract.IsActive = false;

            _contractRepository.Save();

            return Content(Boolean.TrueString);
        }


        public JsonResult AddFeePaymentByContract(int id, DateTime paydate, string paymonth,
            string payyear, decimal payamount, int paytypeid, string notes)
        {
            ManagementFee fee = new ManagementFee();

            fee.ManagementContractId = id;
            
            fee.ReceivedDate = paydate;
            fee.ManagementDueDate = DateTime.Now;
            fee.ManagementFeeMonth = paymonth;
            fee.ManagementFeeYear = payyear;
            fee.ManagementFeeAmount = payamount;
            fee.Notes = notes;
            fee.IsReceived = true;
            fee.ManagementFeeTypeId = paytypeid;

            _contractRepository.AddManagementFee(fee);
            _contractRepository.Save();

            return Json("Added successfully!"); 
        }

        public ActionResult GetFeePaymentByContract(int id)
        {
            DisplayContractViewModel model = new DisplayContractViewModel();

            IEnumerable<DisplayContractViewModel> history = _contractRepository.GetFeeHistoryByContract(id);

            model.FeeHistoryList = history;

            return PartialView("_FeePaymentLog", model); 

        }

        public ActionResult GenerateFeeRecipt(int id)
        {

            DisplayFeePaymentViewModel payment = _contractRepository.GetFeePaymentById(id);
            
            return new PdfResult(payment, "GenerateFeeRecipt");
        }

        #endregion

        #region Manage Tenants

        public ActionResult AddTenant()
        {
            CreateTenantViewModel tenant = new CreateTenantViewModel();

            string returnUrl = "";

            if (HttpContext.Request.UrlReferrer != null)
            {
                returnUrl = HttpContext.Request.UrlReferrer.PathAndQuery;
            }

            TempData["url"] = returnUrl;

            return View("AddTenant", tenant);
        }

        //public ActionResult AllTenants() //For site admin only
        //{
        //    return View();
        //}

        public JsonResult AddNewTenant(string firstName, string lastName, string eMail, string telePhone, string telePhone2)
        {
            ProMaster.Core.Tenant.Tenant tenant = new ProMaster.Core.Tenant.Tenant();

            tenant.FirstName = firstName;
            tenant.LastName = lastName;
            tenant.ContactEmail = eMail;
            tenant.ContactTelephone1 = telePhone;
            tenant.ContactTelephone2 = telePhone2;
            tenant.OnlineAccessEnbaled = false;
            tenant.RoleId = 3;
            tenant.IsActive = false;
            tenant.ManagedBy = User.Identity.Name;
            tenant.UserAvartaImgUrl = "~/Content/Documents/Tenant/user_default.png";
            tenant.UserName = "tba";
            tenant.LeaseId = 0;
            tenant.CreateDate = DateTime.Now;
            tenant.UpdateDate = DateTime.Now;

            

            _tenantRepository.AddTenant(tenant);
            _tenantRepository.Save();

            return Json("New tenant has been added!");
        }

        [HttpPost]
        public JsonResult AddTenantToLease(int id, int leaseId) //id is tenantId
        {
            //Core.Lease.Lease lease = _leaseRepository.GetLeaseById(id);

            Core.Lease.Tenant tenant = _leaseRepository.GetTenantbyId(id).FirstOrDefault();

            //int leaseId = id;

            Core.Lease.Lease lease = _leaseRepository.GetLeaseById(leaseId);

            tenant.LeaseId = leaseId;
            tenant.IsActive = true;
            tenant.UpdateDate = DateTime.Now;

            lease.IsActive = true;
            lease.UpdateDate = DateTime.Now;

            _leaseRepository.Save();

            return Json("The tenant has been added.");
        }

        [HttpPost]
        public JsonResult AddNewTenantToLease(int id, string firstName, string lastName, string eMail, string telePhone, string telePhone2) //id is leaseId
        {
            Core.Tenant.Tenant tenant = new Core.Tenant.Tenant();

            tenant.FirstName = firstName;
            tenant.LastName = lastName;
            tenant.ContactEmail = eMail;
            tenant.ContactTelephone1 = telePhone;
            tenant.ContactTelephone2 = telePhone2;
            tenant.IsActive = true;
            tenant.UserName = "tba";
            tenant.UserAvartaImgUrl = "~/Content/Documents/Tenant/user_default.png";
            tenant.RoleId = 3;
            tenant.OnlineAccessEnbaled = false;
            tenant.ManagedBy = User.Identity.Name;
            tenant.LeaseId = id;
            tenant.CreateDate = DateTime.Now;
            tenant.UpdateDate = DateTime.Now;

            _tenantRepository.AddTenant(tenant);
            _tenantRepository.Save();

            return Json("The tenant Has been added");
        }

        //public JsonResult AddTenantToLease(int id, int lid) //id is leaseId
        //{
        //    ProMaster.Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);           
           
        //    tenant.LeaseId = id;
            
        //    _tenantRepository.Save();

        //    return Json("The tenant Has been added");
        //}


        public ActionResult AllTenantsByPm(string currentFiler, int? page)
        {
            IEnumerable<ManageDisplayViewModel> TenantsByPm = _tenantRepository.GetActiveTenantList().OrderBy(d => d.CreateDate);

            const int pageSize = 10; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
            var paginatedTenantList = TenantsByPm.ToPagedList(pageNumber, pageSize);

            return View(paginatedTenantList);
        }

        public ActionResult GetTenantList()
        {
            CreateLeaseViewModel model = new CreateLeaseViewModel();

            var Tenant = _leaseRepository.GetLeaseTenant();

            model.TenantList = Tenant.ToTenantList(-1);


            return PartialView("_tenList", model);
        }

        public ActionResult AllTenants()
        {

            return PartialView("_AllTenantList");
        }

        [HttpPost]
        public ActionResult AddTenant(CreateTenantViewModel model, FormCollection collection)
        {
            ProMaster.Core.Tenant.Tenant tenant = new ProMaster.Core.Tenant.Tenant();

            //string Url0 = TempData["url"] as string;

            //string Url = Url0.Remove(1, 7);

            tenant.FirstName = Request.Form["FistName"];
            tenant.LastName = Request.Form["LastName"];
            tenant.ContactEmail = Request.Form["ContactEmail"];
            tenant.ContactTelephone1 = Request.Form["ContactTelephone1"];
            tenant.ContactTelephone2 = Request.Form["ContactTelephone2"];
            tenant.UserName = "tba";
            tenant.OnlineAccessEnbaled = false;// model.OnLineAccessEnabled;
            tenant.UserAvartaImgUrl = "~/Content/Documents/Tenant/user_default.png";
            tenant.RoleId = 3;
            tenant.LeaseId = 0;
            tenant.IsActive = false;
            tenant.ManagedBy = User.Identity.Name;
            tenant.CreateDate = DateTime.Now;
            tenant.UpdateDate = DateTime.Now;

            _tenantRepository.AddTenant(tenant);
            _tenantRepository.Save();

            //if (Url.Length > 0)
            //{
            //    return RedirectToAction("Url", "Manage");
            //}
            //else
            //{
            return RedirectToAction("AllTenantsByPm");
            //}
           
        }

        public ActionResult TenantDetails(int id)
        {
            DisplayTenantViewModel model = new DisplayTenantViewModel();

            Core.Tenant.Tenant tenantById = _tenantRepository.GetTenantById(id);

            IEnumerable<DisplayTenantViewModel> tenant;

            if(tenantById.LeaseId != 0)
            {
                tenant = _tenantRepository.GetActiveTenantDetailsById(id);

                model.LeaseInfo = _tenantRepository.LeaseById(id); // lease id from tenant entity
                model.PropertyInfo = _tenantRepository.PropertyById(id);
                model.DocumentInfo = _tenantRepository.DocumentByTenantId(id, 3);
                model.PaymentInfo = _tenantRepository.GetRentPaymentLogByTenant(id);

                var docTypeList = _documentRepository.GetDocType();
                model.DocumentTypeList = docTypeList.ToDocTypeList(-1);

                var principalTypeList = _documentRepository.GetPrincipaltype();
                model.DocumentTyPrincipalpeList = principalTypeList.ToPrincipalTypeList(-1);

                int LatePayment = _tenantRepository.GetNumberOfLatePayment(id);

                model.TenantId = tenant.FirstOrDefault().TenantId;
                model.FistName = tenant.FirstOrDefault().FistName;
                model.LastName = tenant.FirstOrDefault().LastName;
                model.ContactEmail = tenant.FirstOrDefault().ContactEmail;
                model.ContactTelephone1 = tenant.FirstOrDefault().ContactTelephone1;
                model.ContactTelephone2 = tenant.FirstOrDefault().ContactTelephone2;
                model.CreationDate = tenant.FirstOrDefault().CreationDate;
                model.UpdateDate = tenant.FirstOrDefault().UpdateDate;
                model.AvartaImgUrl = tenant.FirstOrDefault().AvartaImgUrl;
                model.OnLineAccessEnabled = tenant.FirstOrDefault().OnLineAccessEnabled;
                model.UserName = tenant.FirstOrDefault().UserName;

                model.LeaseId = tenant.FirstOrDefault().LeaseId;
                model.LeaseTitle = tenant.FirstOrDefault().LeaseTitle;
                model.LeaseDesc = tenant.FirstOrDefault().LeaseDesc;
                model.LeaseStartDate = tenant.FirstOrDefault().LeaseStartDate;
                model.LeaseEndDate = tenant.FirstOrDefault().LeaseEndDate;
                model.LeaseSignDate = tenant.FirstOrDefault().LeaseSignDate;
                model.LeaseTerm = tenant.FirstOrDefault().LeaseTerm;
                model.RentAmount = tenant.FirstOrDefault().RentAmount;
                model.LeaseAddedDate = tenant.FirstOrDefault().LeaseAddedDate;
                model.DamageDepositAmount = tenant.FirstOrDefault().DamageDepositAmount;
                model.PetDepositAmount = tenant.FirstOrDefault().PetDepositAmount;
                model.RentFrequency = tenant.FirstOrDefault().RentFrequency;
                model.IsActive = tenant.FirstOrDefault().IsActive;

                //model.NumberOfLatePayment = LatePayment; //tenant.FirstOrDefault().NumberOfLatePayment;

                ViewBag.late = LatePayment;
                TempData["late"] = LatePayment;

                model.PropertyId = tenant.FirstOrDefault().PropertyId;
                model.PropertyName = tenant.FirstOrDefault().PropertyName;
                model.PropertyImageUrl = tenant.FirstOrDefault().PropertyImageUrl;
                model.PropertyDesc = tenant.FirstOrDefault().PropertyDesc;
                model.PropertyBuildYear = tenant.FirstOrDefault().PropertyBuildYear;

            }
            else
            {
                tenant = _tenantRepository.GetTenantDetailsById(id);

                model.DocumentInfo = _tenantRepository.DocumentByTenantId(id, 3);

                var docTypeList = _documentRepository.GetDocType();
                model.DocumentTypeList = docTypeList.ToDocTypeList(-1);

                var principalTypeList = _documentRepository.GetPrincipaltype();
                model.DocumentTyPrincipalpeList = principalTypeList.ToPrincipalTypeList(-1);

                model.TenantId = tenant.FirstOrDefault().TenantId;
                model.FistName = tenant.FirstOrDefault().FistName;
                model.LastName = tenant.FirstOrDefault().LastName;
                model.ContactEmail = tenant.FirstOrDefault().ContactEmail;
                model.ContactTelephone1 = tenant.FirstOrDefault().ContactTelephone1;
                model.ContactTelephone2 = tenant.FirstOrDefault().ContactTelephone2;
                model.CreationDate = tenant.FirstOrDefault().CreationDate;
                model.UpdateDate = tenant.FirstOrDefault().UpdateDate;
                model.AvartaImgUrl = tenant.FirstOrDefault().AvartaImgUrl;
                model.OnLineAccessEnabled = tenant.FirstOrDefault().OnLineAccessEnabled;
                model.UserName = tenant.FirstOrDefault().UserName;

            }

            

            

            return View("TenantDetails", model);
        }

        
        public ActionResult GetTenantInfo()
        {
            ManageDisplayViewModel Management = new ManageDisplayViewModel();

            IEnumerable<ManageDisplayViewModel> tenants = _tenantRepository.GetTenantList();

            Management.Tenants = tenants;

            return PartialView("_TenantInfo", Management);
        }

        public ActionResult EditTenant(int id)
        {
            ProMaster.Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);
            //CreateTenantViewModel tenant = _tenantRepository.GetTenantDetailsById(id);

            return View(tenant);
        }

        [HttpPost]
        public ActionResult EditTenant(int id, CreateTenantViewModel model)
        {
            ProMaster.Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

            tenant.FirstName = Request.Form["FirstName"];
            tenant.LastName = Request.Form["LastName"];
            tenant.ContactEmail = Request.Form["ContactEmail"];
            tenant.ContactTelephone1 = Request.Form["ContactTelephone1"];
            tenant.ContactTelephone2 = Request.Form["ContactTelephone2"];
            tenant.OnlineAccessEnbaled = model.OnLineAccessEnabled;
            tenant.UpdateDate = DateTime.Now;
            tenant.IsActive = model.IsActive;

            _tenantRepository.Save();

            if (model.OnLineAccessEnabled == true)
            {
                TempData["id"] = id;
                TempData["pid"] = 3;

                return RedirectToAction("Register", "Account"); 
            }
            else 
            { 
                return RedirectToAction("TenantDetails/" + id); 
            }
        }


        public ActionResult GetActiveTenants()
        {
            ManageDisplayViewModel model = new ManageDisplayViewModel();

            IEnumerable<ManageDisplayViewModel> tenants = _tenantRepository.GetActiveTenantList();

           model.Tenants = tenants;

           return PartialView("_TenantDisplayList", model);
        }

        public ActionResult GetCandidateTenants()
        {
            ManageDisplayViewModel model = new ManageDisplayViewModel();

            IEnumerable<ManageDisplayViewModel> tenants = _tenantRepository.GetTenantCandidates();

            model.Tenants = tenants;

            return PartialView("_TenantDisplayList", model);
        }


        public ActionResult DeleteTenant(int id)
        { 
            ProMaster.Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

            //tenant.IsActive = false;

            tenant.LeaseId = 0;

            _tenantRepository.Save();

            return Content(Boolean.TrueString);

        }

        public ActionResult TeancyApplication()
        {
            int pId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<ManageDisplayViewModel> application = _propertyRepository.GetAllApplicationsByPmId(pId);

            return View("TeancyApplication", application);
        }


        public ActionResult ApplicationDetails(int id)
        {
            DisplayApplicationViewModel application = _propertyRepository.TenancyApplicationByDetails(id);


            return View("ApplicationDetails", application);
        }

        

        public ActionResult ApproveApplicant(int id, int pid)//, CreateTenantViewModel model)
        {
            Core.Property.TenancyApplication application = _propertyRepository.GetApplicationById(id);

            Core.Property.Property property = _propertyRepository.PropertyById(pid);

            application.IsApproved = true;
            property.RentalStatusId = 1;
            _propertyRepository.Save(); //Update approval status

            //Transfer information to Tenant table - create a new tenant
            //
            Core.Tenant.Tenant tenant = new Core.Tenant.Tenant();

            tenant.FirstName = application.ApplicantFirstName;
            tenant.LastName = application.ApplicantLastName;
            tenant.ContactEmail = application.ApplicantContactEmail;
            tenant.ContactTelephone1 = application.ApplicantContactTel;
            tenant.ContactTelephone2 = "";
            tenant.UserName = "tba";
            tenant.OnlineAccessEnbaled = false;
            tenant.UserAvartaImgUrl = "default";
            tenant.RoleId = 3;
            tenant.LeaseId = 0;
            tenant.IsActive = false;
            //application.IsApproved = true;
            tenant.ManagedBy = User.Identity.Name;
            tenant.CreateDate = DateTime.Now;
            tenant.UpdateDate = DateTime.Now;

            _tenantRepository.AddTenant(tenant);
            _tenantRepository.Save();

            return RedirectToAction("Index");
        }

        #endregion

        #region Manage Property Leases

        public ActionResult AddLease()
        {
            CreateLeaseViewModel lease = new CreateLeaseViewModel();

            var rentFrequencyList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Monthly", Value="Monthly"},
                new SelectListItem {Text = "Yearly", Value="Yearly"},
                new SelectListItem {Text = "Other", Value="Other"},               

            };

            var rentDueDate = new List<SelectListItem>
            {
                new SelectListItem {Text = "1st Day of the Month", Value="1"},
                new SelectListItem {Text = "15th Day of the Month", Value="15"},
                new SelectListItem {Text = "Other", Value="0"},               

            };

            ViewBag.DropdownData = rentFrequencyList;
            ViewBag.DropdownData2 = rentDueDate;

            var LeaseTerm = _leaseRepository.GetLeaseTerm();
            var Tenant = _leaseRepository.GetLeaseTenant();
            var Property = _leaseRepository.GetLeaseProperty();

            lease.LeaseTermList = LeaseTerm.ToLeaseTermList(-1);
            lease.TenantList = Tenant.ToTenantList(-1);
            lease.PropertyList = Property.ToPropertyList(-1);

            return View("AddLease", lease);
        }

        [HttpPost]
        public ActionResult AddLease(CreateLeaseViewModel model, FormCollection form)
        {
            Core.Lease.Lease lease = new Core.Lease.Lease();
            //Core.Lease.Tenant tenant = new Core.Lease.Tenant();
            Core.Lease.Tenant tenant = new Core.Lease.Tenant();

            Core.Property.Property property = _propertyRepository.PropertyById(model.PropertyId);

            //if (ModelState.IsValid) 
            //{
                lease.LeaseTitle = Request.Form["LeaseTitle"];
                lease.LeaseDesc = Request.Form["LeaseDesc"];
                lease.LeaseStartDate = DateTime.Parse(Request.Form["LeaseStartDate"]);
                lease.LeaseEndDate = DateTime.Parse(Request.Form["LeaseEndDate"]);
                lease.PropertyId = model.PropertyId;
                lease.LeaseTermId = model.LeaseTermId;
                lease.RentAmount = decimal.Parse(Request.Form["RentAmount"]);
                lease.DamageDepositAmount = decimal.Parse(Request.Form["DamageDepositAmount"]);
                lease.PetDepositAmount = decimal.Parse(Request.Form["PetDepositAmount"]);
                lease.RentDueOn = Request.Form["RentDueOn"];
                lease.RentFrequency = Request.Form["RentFrequency"];
                lease.Addendum = model.Addendum;
                lease.LeaseSignDate = DateTime.Parse(Request.Form["LeaseSignDate"]);
                lease.Notes = Request.Form["Notes"];
                lease.IsActive = false;
                lease.CreationDate = DateTime.Now;
                lease.UpdateDate = DateTime.Now;

                property.RentalStatusId = 1;                

                _leaseRepository.AddLease(lease);
                //_leaseRepository.Save();

                //_propertyRepository.Save();

                //Get newly inserted lease
                //
                model.LeaseInfo = lease;

                model.LeaseId = lease.LeaseId;

            /**/



                if (model.AddTenant == true)
                {
                    //Add a new tenant
                    //
                    if (model.TenantId != 0)
                    {
                        Core.Lease.Tenant newTenant = _leaseRepository.GetTenantbyId(model.TenantId).FirstOrDefault();

                        if(newTenant != null)
                        {
                            newTenant.LeaseId = lease.LeaseId; //assign the new tenant with the new lease id

                            newTenant.IsActive = true;

                            lease.IsActive = true;

                            //_leaseRepository.Save();
                        }
                       
                    }

                   


                    //tenant.FirstName = Request.Form["FistName"];
                    //tenant.LastName = Request.Form["LastName"];
                    //tenant.ContactEmail = Request.Form["ContactEmail"];
                    //tenant.ContactTelephone1 = Request.Form["ContactTelephone1"];
                    //tenant.ContactTelephone2 = Request.Form["ContactTelephone2"];
                    //tenant.OnlineAccessEnbaled = false;
                    //tenant.UserAvartaImgUrl = "~/Content/Documents/Tenant/user_default.png";
                    //tenant.CreateDate = DateTime.Now;
                    //tenant.UpdateDate = DateTime.Now;
                    //tenant.RoleId = 3;

                    

                    //newTenant.IsActive = true;
                    //newTenant.ManagedBy = User.Identity.Name;

                    //_leaseRepository.AddTenant(tenant);


                    //_leaseRepository.Save(); 
                }
                //else
                //{
                //    #region //****************
                    
                //    tenant.FirstName = "f";
                //    tenant.LastName = "l";
                //    tenant.ContactEmail = "e";
                //    tenant.ContactTelephone1 = "t";
                //    tenant.ContactTelephone2 = "t";
                //    tenant.OnlineAccessEnbaled = false;
                //    tenant.UserAvartaImgUrl = "default";
                //    tenant.CreateDate = DateTime.Now;
                //    tenant.UpdateDate = DateTime.Now;
                //    tenant.RoleId = 3;

                //    tenant.LeaseId = 1; 

                //    tenant.IsActive = true;
                //    tenant.ManagedBy = User.Identity.Name;
                    
                //    #endregion  //************************************

                //    IEnumerable<Core.Lease.Tenant> ExstingTenant = _leaseRepository.GetTenantbyId(model.TenantId);
                
                //    //model.leaseInfo = lease;
                //    ExstingTenant.FirstOrDefault().LeaseId = lease.LeaseId;  //assign the newly created leaseId to tenant table
                //    ExstingTenant.FirstOrDefault().IsActive = true;
                //    ExstingTenant.FirstOrDefault().UpdateDate = DateTime.Now;

                //    _leaseRepository.Save();
                //}
            
                //tenant.TenantId = model.TenantId;


                return RedirectToAction("Index");

            //}

            //return View("AddLease", model);
        }

        public ActionResult AllLeaseByPm(string currentFiler, int? page)
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<ManageDisplayViewModel> leaseList = _leaseRepository.GetLeaseList(pmId).OrderByDescending(d => d.CreateDate);

            const int pageSize = 2; //for testing purpose, to be adjustetd
            int pageIndex = (page ?? 1) - 1;
            int pageNumber = (page ?? 1);

            //var paginatedPropertyList = new PaginatedList<>(properties, pageIndex, pageSize);
            var paginatedLeaseList = leaseList.ToPagedList(pageNumber, pageSize);

            return View(paginatedLeaseList);
        }

        public ActionResult LeaseDetails(int id)
        {
            Core.Lease.ViewModels.DisplayLeaseViewModel model = new Core.Lease.ViewModels.DisplayLeaseViewModel();

            //find out if there is any tenant for this lease
            //


            try
            {
                IEnumerable<DisplayLeaseViewModel> lease = _leaseRepository.LeaseDetails(id);
                IEnumerable<Core.Lease.Document> document = _leaseRepository.DocumenetByLeaseId(id, 4);
                IEnumerable< Core.Lease.RentPayment> payment = _leaseRepository.GetLateRentPayment(lease.FirstOrDefault().TenantId);
                IEnumerable<Core.Tenant.Tenant> tenant = _tenantRepository.GetTenantsByLeaseId(lease.FirstOrDefault().LeaseId);

                //IEnumerable<Core.Tenant.Tenant> newTenantList = _tenantRepository.GetNewTenantsList();

                //model.NewTenantList = newTenantList;


                //Get unsigned (newly added) tenant list
                //




                model.TenantInfo = _leaseRepository.GetTenantByLeaseId(id);
                model.PropertyInfo = _leaseRepository.GetPropertyById(lease.FirstOrDefault().LeaseId);
                model.DocumentInfo = document;
                model.PaymentInfo = payment;
                model.TenantList = tenant;

                var docTypeList = _documentRepository.GetDocType();
                model.DocumentTypeList = docTypeList.ToDocTypeList(-1);

                var principalTypeList = _documentRepository.GetPrincipaltype();
                model.DocumentTyPrincipalpeList = principalTypeList.ToPrincipalTypeList(-1);


                model.LeaseId = lease.FirstOrDefault().LeaseId;
                model.LeaseTitle = lease.FirstOrDefault().LeaseTitle;
                model.LeaseDesc = lease.FirstOrDefault().LeaseDesc;
                model.LeaseTerm = lease.FirstOrDefault().LeaseTerm;
                model.LeaseStartDate = lease.FirstOrDefault().LeaseStartDate;
                model.LeaseEndDate = lease.FirstOrDefault().LeaseEndDate;
                model.LeaseSignDate = lease.FirstOrDefault().LeaseSignDate;
                model.RentAmount = lease.FirstOrDefault().RentAmount;
                model.RentDueOn = lease.FirstOrDefault().RentDueOn;
                model.RentFrequency = lease.FirstOrDefault().RentFrequency;
                model.Notes = lease.FirstOrDefault().Notes;
                model.LeaseAddedDate = lease.FirstOrDefault().LeaseAddedDate;
                model.LeaseUpdateDate = lease.FirstOrDefault().LeaseUpdateDate;
                model.DamageDepositAmount = lease.FirstOrDefault().DamageDepositAmount;
                model.PetDepositAmount = lease.FirstOrDefault().PetDepositAmount;

                model.TenantId = lease.FirstOrDefault().TenantId;
                model.FirstName = lease.FirstOrDefault().FirstName;
                model.LastName = lease.FirstOrDefault().LastName;
                model.ContactEmail = lease.FirstOrDefault().ContactEmail;
                model.ContactTelephone1 = lease.FirstOrDefault().ContactTelephone1;
                model.AvartaImgUrl = lease.FirstOrDefault().AvartaImgUrl;

                model.PropertyId = lease.FirstOrDefault().PropertyId;
                model.PropertyName = lease.FirstOrDefault().PropertyName;
                model.LeaseDesc = lease.FirstOrDefault().LeaseDesc;
                model.PropertyDesc = lease.FirstOrDefault().PropertyDesc;
                model.PropertyImageUrl = lease.FirstOrDefault().PropertyImageUrl;
                model.PropertyBuildYear = lease.FirstOrDefault().PropertyBuildYear;

                model.PropertyAddressPostZipCode = lease.FirstOrDefault().PropertyAddressPostZipCode;
                model.PropertyAddressProvState = lease.FirstOrDefault().PropertyAddressProvState;
                model.PropertyAddressCity = lease.FirstOrDefault().PropertyAddressCity;
                model.PropertyAddressStreetNumber = lease.FirstOrDefault().PropertyAddressStreetNumber;
                model.PropertyAddressStreetName = lease.FirstOrDefault().PropertyAddressStreetName;
                model.PropertyAddressCountry = lease.FirstOrDefault().PropertyAddressCountry;



                model.NumberOfLatePayment = payment.Count();

                return View("LeaseDetails", model);
            }
            catch(Exception ex) 
            {
                return View("Error: " + ex.Message);
            }

            
        }

        public JsonResult GetNewTenants()
        {
            ProMasterLeaseDataEntities db = new ProMasterLeaseDataEntities();

            var result = from tenants in db.Tenants.Where(a => a.IsActive == false)
                         select new
                         {
                             id = tenants.TenantId,
                             name = tenants.LastName + ", " + tenants.FirstName
                         };
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewLeases()
        {
            ProMasterLeaseDataEntities db = new ProMasterLeaseDataEntities();

            var result = from leases in db.Leases.Where(a => a.IsActive == false)
                         select new
                         {
                             id = leases.LeaseId,
                             name = leases.LeaseTitle
                         };

            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetActiveLeases()
        {
            ManageDisplayViewModel model = new ManageDisplayViewModel();

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<ManageDisplayViewModel> leases = _leaseRepository.GetLeaseList(pmId);

            model.LeaseList = leases;

            return PartialView("_LeaseDisplayList", model);
        }

        public ActionResult GetCandidateLeases()
        {
            ManageDisplayViewModel model = new ManageDisplayViewModel();

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            IEnumerable<ManageDisplayViewModel> leases = _leaseRepository.GetCandidateLeaseList(pmId);

            model.LeaseList = leases;

            return PartialView("_LeaseDisplayList", model);
        }

        public ActionResult UpdatedTenantList(int id)
        {
            DisplayLeaseViewModel model = new DisplayLeaseViewModel();

            //IEnumerable<DisplayLeaseViewModel> TenantList = _leaseRepository.GetTenantList(id);


            IEnumerable<Core.Tenant.Tenant> tenant = _tenantRepository.GetTenantsByLeaseId(id);


            model.TenantList = tenant;

            return PartialView("_TenantList", model);
 
        }

        public ActionResult EditLease(int id)
        {
            EditLeaseViewModel lease = _leaseRepository.LeaseForEdit(id).FirstOrDefault();

            var rentFrequencyList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Monthly", Value="Monthly"},
                new SelectListItem {Text = "Yearly", Value="Yearly"},
                new SelectListItem {Text = "Other", Value="Other"},               

            };

            ViewBag.DropdownData = rentFrequencyList;

            var LeaseTerm = _leaseRepository.GetLeaseTerm();
            var Tenant = _leaseRepository.GetNewLeaseTenant(); //to add new tenant to the existing lease
            //var Property = _leaseRepository.GetLeaseProperty();

            lease.LeaseTermList = LeaseTerm.ToLeaseTermList(-1);
            lease.TenantList = Tenant.ToTenantList(-1);
            //lease.PropertyList = Property.ToPropertyList(-1);

            return View("EditLease", lease);
        }

        [HttpPost]
        public ActionResult EditLease(int id, EditLeaseViewModel model)
        {
            Core.Lease.Lease lease = _leaseRepository.GetLeaseForEdit(id).FirstOrDefault();

            //if (ModelState.IsValid) 
            //{
                lease.LeaseTitle = Request.Form["LeaseTitle"];
                lease.LeaseDesc = Request.Form["LeaseDesc"];
                lease.LeaseStartDate = DateTime.Parse(Request.Form["LeaseStartDate"]);
                lease.LeaseEndDate = DateTime.Parse(Request.Form["LeaseEndDate"]);                
                lease.LeaseTermId = model.LeaseTermId;
                lease.PropertyId = lease.PropertyId;

                
                lease.RentAmount =  decimal.Parse(Request.Form["RentAmount"]);
                lease.DamageDepositAmount = decimal.Parse(Request.Form["DamageDepositAmount"]);
                lease.PetDepositAmount = decimal.Parse(Request.Form["PetDepositAmount"]);
                lease.RentDueOn = Request.Form["RentDueOn"];
                lease.RentFrequency = Request.Form["RentFrequency"];
                lease.Addendum = model.Addendum;
                lease.IsActive = model.IsActive;
                
                lease.Notes = Request.Form["Notes"];                
                lease.UpdateDate = DateTime.Now;
                                
                _leaseRepository.Save();
            //}
                return RedirectToAction("LeaseDetails/" + id); 
        }

        public ActionResult DeleteLease(int id)
        {
            Core.Lease.Lease lease = _leaseRepository.GetLeaseForEdit(id).FirstOrDefault();
            lease.IsActive = false;

            _leaseRepository.Save();

            return Content(Boolean.TrueString);
        }

        public ActionResult LeaseInfoByTenant(int id) //id is tenant id
        {
            int leaseId = _tenantRepository.GetLeaseIdByTenant(id);

            ManageDisplayViewModel LeaseInfo = _leaseRepository.GetLeaseInfo(leaseId).FirstOrDefault();

            return View(LeaseInfo);
        }

        [HttpPost]
        public JsonResult AddRentPayment(int id, DateTime paydate, string paymonth, string payyear, decimal payamount, int paymethodid, bool collected,
            bool deposit, string notes, int bankId, decimal transferAmt, DateTime tranferDate, bool direct)
        {
            Core.Lease. RentPayment payment = new Core.Lease.RentPayment();
            
            Core.Lease.Tenant tenant = _leaseRepository.GetTenantbyId(id).FirstOrDefault();

            int leaseId = tenant.LeaseId;

            Core.Lease.Lease lease = _leaseRepository.GetLeaseById(leaseId); 

            if (ModelState.IsValid){
                try
                {
                    payment.CreationDate = DateTime.Now;

                    payment.TenantId = id;
                    payment.RentPaymentDate = paydate;
                    payment.RentPaidAmount = payamount;
                    payment.RentPaymentMethodId = paymethodid;
                    payment.IsCollectedInPerson = collected;
                    payment.IsDepositForOwner = deposit;
                    payment.RentPaymentMonth = paymonth;
                    payment.RentPaymentYear = payyear;

                    DateTime dueDate;

                    if (payment.RentPaymentDate.ToString().Contains("15"))
                    {
                        dueDate = DateTime.Parse( paymonth + "/15/" + payyear);
                    }
                    else
                    {
                        dueDate = DateTime.Parse( paymonth + "/1/" + payyear);
                    }
                    
                    

                    int paymentLateStatus = DateTime.Compare(dueDate , payment.RentPaymentDate);

                    if (paymentLateStatus < 0)
                    {
                        payment.RentIsPaidOntime = false;
                        payment.NumberOfLatePayment += 1;
                    }
                    else
                    { 
                        payment.RentIsPaidOntime = true;
                    }

                    payment.Notes = notes;

                    _leaseRepository.AddPaymentHistory(payment);
                    _leaseRepository.Save();

                    if (payment.IsDepositForOwner == true)
                    {
                        if(direct != true) //if not direct transfer to owner with cash/cheque
                        {
                            RentDepositTransfer transfer = new RentDepositTransfer();
                        
                            transfer.BankId = bankId;
                            transfer.RentTransferAmount =  transferAmt;
                            transfer.RentTransferDate = tranferDate;

                            transfer.RentPayment = payment;
                            transfer.RentalPaymentId = payment.RentPaymentId;

                            _leaseRepository.AddTransfer(transfer);
                            _leaseRepository.Save();
                        }
                        else
                        {
                            //do nothing. indicate bank deposit/transfer is false or direct transfer to owner in view(UI)
                            RentDepositTransfer transfer = new RentDepositTransfer();

                            transfer.BankId = 0; //no bank, i.e. direct transfer
                            transfer.RentTransferAmount = transferAmt;
                            transfer.RentTransferDate = tranferDate;

                            transfer.RentPayment = payment;
                            transfer.RentalPaymentId = payment.RentPaymentId;

                            _leaseRepository.AddTransfer(transfer);
                            _leaseRepository.Save();
                        }
                        
                        
                    }

                    return Json("Rent payment added successfully!"); 
                }
                catch (Exception e)
                {
                return Json("Error: " + e.Message + "occured!");
                }

                
            }
            else
            {
                return Json("Try it again");
            }
            
        }

        [HttpPost]
        public JsonResult GetDocumentList(int id, int pid)
        {

            return Json("Ok");
        }

        public ActionResult GetPaymentByTenant(int id)
        {
            DisplayTenantViewModel model = new DisplayTenantViewModel();


            IEnumerable<DisplayTenantViewModel> history = _tenantRepository.GetRentPaymentLogByTenant(id);

            model.PaymentInfo = history;

            return PartialView("_RentPaymentLog", model); 
        }

        public ActionResult RentPaymentDetails(int id)
        {
            RentPaymentViewModel model = new RentPaymentViewModel();

            RentPaymentViewModel rent = _leaseRepository.GetRentPaymentById(id);

            if(rent.IsRentTransferred)
            {
                RentDepositTransfer transfer = _leaseRepository.GetTransferByPayment(rent.RentPaymentId);

                RentPaymentViewModel rentToBank = _leaseRepository.GetRentPaymentTransferrdToBankById(id);

                if(transfer.BankId != 0)
                {
                    

                    model.RentPaymentId = rentToBank.RentPaymentId;
                    model.PaymentMonth = rentToBank.PaymentMonth;
                    model.PaymentYear = rentToBank.PaymentYear;
                    model.PaymenAmount = rentToBank.PaymenAmount;
                    model.PaymentDate = rentToBank.PaymentDate;
                    model.RentPaymentMethod = rentToBank.RentPaymentMethod;



                    //Tenant info
                    //
                    model.TenantId = rentToBank.TenantId;
                    model.FirstName = rentToBank.FirstName;
                    model.LastName = rentToBank.LastName;
                    model.ContactEmail = rentToBank.ContactEmail;
                    model.ContactTelephone1 = rent.ContactTelephone1;

                    model.IsRentTransferred = rentToBank.IsRentTransferred;
                    model.TranferAmount = rentToBank.TranferAmount;
                    model.TransferDate = rentToBank.TransferDate;
                    model.Bank = rentToBank.Bank;

                    model.TransferMethod = "Bank Account Deposit/Transfer";
                }
                else
                {
                    model.RentPaymentId = rent.RentPaymentId;
                    model.PaymentMonth = rent.PaymentMonth;
                    model.PaymentYear = rent.PaymentYear;
                    model.PaymenAmount = rent.PaymenAmount;
                    model.PaymentDate = rent.PaymentDate;
                    model.RentPaymentMethod = rent.RentPaymentMethod;
                    model.IsRentTransferred = rent.IsRentTransferred;
                    model.TransferDate = transfer.RentTransferDate;

                    //Tenant info
                    //
                    model.TenantId = rent.TenantId;
                    model.FirstName = rent.FirstName;
                    model.LastName = rent.LastName;
                    model.ContactEmail = rent.ContactEmail;
                    model.ContactTelephone1 = rent.ContactTelephone1;
                    //model.TransferDate = rentToBank.TransferDate;

                    model.TransferMethod = "Direct Transfer to Owner";
                }

                return View("RentPaymentDetails", model);

            }

            model.RentPaymentId = rent.RentPaymentId;
            model.PaymentMonth = rent.PaymentMonth;
            model.PaymentYear = rent.PaymentYear;
            model.PaymenAmount = rent.PaymenAmount;
            model.PaymentDate = rent.PaymentDate;
            model.RentPaymentMethod = rent.RentPaymentMethod;



            //Tenant info
            //
            model.TenantId = rent.TenantId;
            model.FirstName = rent.FirstName;
            model.LastName = rent.LastName;
            model.ContactEmail = rent.ContactEmail;
            model.ContactTelephone1 = rent.ContactTelephone1;

            //model.IsRentTransferred = rent.IsRentTransferred;
            //model.TranferAmount = rent.TranferAmount;
            //model.TransferDate = rent.TransferDate;
            //model.Bank = rent.Bank;

            //if(model.IsRentTransferred)
            //{
            //    Core.Lease.RentDepositTransfer transferToBank = _leaseRepository.GetTransferByPayment(id);
            //    if(transferToBank.BankId != 0)
            //    {
            //        model.TransferMethod = "Bank Account Deposit/Transfer";
            //    }
            //    else
            //    {
            //        model.TransferMethod = "Direct Transfer to Owner";
            //    }
            //}


            return View("RentPaymentDetails", model);
        }

        [HttpPost]
        public JsonResult TransferRent(int id, decimal amount, int bankId)
        {
            RentDepositTransfer transfer = new RentDepositTransfer();
            Core.Lease.RentPayment payment = _leaseRepository.GetPaymentyById(id);

            transfer.BankId = bankId;
            transfer.RentTransferAmount = amount;
            transfer.RentTransferDate = DateTime.Now;
            transfer.RentalPaymentId = id;

            payment.IsDepositForOwner = true;

            _leaseRepository.AddTransfer(transfer);
            _leaseRepository.Save();

            return Json("Tansferred successfully!");
        }

        public ActionResult GetTransferStatus(int id)
        {
            RentPaymentViewModel payment = _leaseRepository.GetRentPaymentById(id);

            return PartialView("_TransferStatus", payment);
        }
        //public ActionResult EditRentPayment(int id)
        //{

        //}

        public ActionResult FeePaymentDetails(int id)
        {
            DisplayFeePaymentViewModel model = new DisplayFeePaymentViewModel();

            DisplayFeePaymentViewModel fee = _contractRepository.GetFeePaymentById(id);

            model.FeePaidAmount = fee.FeePaidAmount;
            model.FeePaidDate = fee.FeePaidDate;
            model.FeeType = fee.FeeType;
            model.FeeYear = fee.FeeYear;
            model.FeeMonth = fee.FeeMonth;
            model.LandlordId = fee.LandlordId;
            model.LandlordFisrtName = fee.LandlordFisrtName;
            model.LandlordLastName = fee.LandlordLastName;
            model.LandlordContactEmail = fee.LandlordContactEmail;


            return View("FeePaymentDetails", model);
        }

        public ActionResult GenerateRentRecipt(int id)
        {
            //RentPayment payment = _leaseRepository.GetPaymentyById(id);

            RentPaymentViewModel payment = _leaseRepository.GetRentPaymentById(id);

            //var reciptPDF = new PdfResult(payment, "GenerateRentRecipt");

            //return reciptPDF; //RazorPDF.PdfResult(GenerateRentRecipt, "LeaseDetails");
            //return View(payment);
            return new PdfResult(payment, "GenerateRentRecipt");
        }


        #endregion

        #region Manage Vendor

        public ActionResult AddVendor()
        {
            //throw new NotImplementedException();
            CreateVendorViewModel model = new CreateVendorViewModel();

            var Specialties = _vendorRepository.GetSpecialtyList();

            model.VendorSpecialtyList = Specialties.ToVendorSpecialtyList(-1);

            //string returnUrl = "";

            //if (HttpContext.Request.UrlReferrer != null)
            //{
            //    returnUrl = HttpContext.Request.UrlReferrer.PathAndQuery;            

            //    TempData["url"] = returnUrl;
            //}

            return View("AddVendor", model);
        }

        [HttpPost]
        public ActionResult AddVendor(CreateVendorViewModel model, FormCollection collection)
        {
            //throw new NotImplementedException();
            Core.Vendor.Vendor vendor = new Core.Vendor.Vendor();

            //string Url = "";

            //if (TempData["url"] != null)
            //{
            //    string Url0 = TempData["url"] as string;

            //    Url = Url0.Remove(1, 7);

            //}
            //else
            //{
            //    Url = "";
            //}

            try
            {
                vendor.VendorBusinessName = Request.Form["VendorBusinessName"];
                vendor.FirstName = Request.Form["FistName"];
                vendor.LastName = Request.Form["LastName"];
                vendor.VendorDesc = Request.Form["VendorDesc"];
                vendor.VendorContactEmail = Request.Form["ContactEmail"];
                vendor.VendorContactTelephone1 = Request.Form["ContactTelephone1"];
                vendor.VendorContactTelephone2 = Request.Form["ContactTelephone2"];
                vendor.VendorSpecialtyId = model.VendorSpecialtyId;
                vendor.RoleId = 5;
                vendor.IsActive = true;
                vendor.UserAvartaImgUrl = "default";
                vendor.OnlineAccessEnbaled = false;
                vendor.UserName = "tbd";

                vendor.CreationDate = DateTime.Now;
                vendor.UpdateDate = DateTime.Now;

                _vendorRepository.AddVendor(vendor);
                _vendorRepository.Save();

                return RedirectToAction("Index", "Manage");
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return View();
            }

            //if (Url.Length > 0)
            //{
            //    return RedirectToAction("Url", "Manage");
            //}
            //else
            //{ 
            //    return RedirectToAction("Index");
            //}
        }

        public JsonResult GetVendorSpecialtyList()
        {
            PromMasterVendorDataEntities db = new PromMasterVendorDataEntities();

            var result = from specialty in db.VendorSpecialties
                         select new
                         {
                             id = specialty.VendorSpecialtyId,
                             name = specialty.VendorSpecialtyName
                         };
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult AddVendorToList(string businessName, string description, int specialtyId, string firstName, string lastName,
            string eMail, string telePhone, string telePhone2)
        {
            Core.Vendor.Vendor vendor = new Core.Vendor.Vendor();

            vendor.VendorBusinessName = businessName;
            vendor.FirstName = firstName;
            vendor.LastName = lastName;
            vendor.VendorDesc = description;
            vendor.VendorContactEmail = eMail;
            vendor.VendorContactTelephone1 = telePhone;
            vendor.VendorContactTelephone2 = telePhone2;
            vendor.VendorSpecialtyId = specialtyId;
            vendor.RoleId = 5;
            vendor.IsActive = true;
            vendor.UserAvartaImgUrl = "default";
            vendor.OnlineAccessEnbaled = false;
            vendor.UserName = "tbd";

            vendor.CreationDate = DateTime.Now;
            vendor.UpdateDate = DateTime.Now;

            _vendorRepository.AddVendor(vendor);
            _vendorRepository.Save();

            //return PartialView("_venList");
            return Json("Vendor Added!");
        }

        public ActionResult GetVendorList()
        {
            CreateWorkOrderViewModel model = new CreateWorkOrderViewModel();

            var vendor = _vendorRepository.GetVendorList();
            model.VendorList = vendor.ToVendorList(-1);

            return PartialView("_venList", model);
        }


        public ActionResult VendorDetails(int id)
        {
            //throw new NotImplementedException();
            DisplayVendorViewModel model = new DisplayVendorViewModel();

            IEnumerable<DisplayVendorViewModel> vendor = _vendorRepository.GetVendorDetails(id);

            model.VendorId = vendor.FirstOrDefault().VendorId;
            model.VendorBusinessName = vendor.FirstOrDefault().VendorBusinessName;
            model.FistName = vendor.FirstOrDefault().FistName;
            model.LastName = vendor.FirstOrDefault().LastName;
            model.ContactEmail = vendor.FirstOrDefault().ContactEmail;
            model.ContactTelephone1 = vendor.FirstOrDefault().ContactTelephone1;
            model.ContactTelephone2 = vendor.FirstOrDefault().ContactTelephone2;
            model.VendorSepcialtyName = vendor.FirstOrDefault().VendorSepcialtyName;
            model.CreationDate = vendor.FirstOrDefault().CreationDate;
            model.UpdateDate = vendor.FirstOrDefault().UpdateDate;
            model.IsActive = vendor.FirstOrDefault().IsActive;
            model.VendorSpecialtyId = vendor.FirstOrDefault().VendorSpecialtyId;

            return View("VendorDetails", model);
        }

        public ActionResult EditVendor(int id)
        {
            //throw new NotImplementedException();
            DisplayVendorViewModel vendor = _vendorRepository.GetVendorDetails(id).FirstOrDefault();

            var Specialties = _vendorRepository.GetSpecialtyList();
            vendor.VendorSpecialtyList = Specialties.ToVendorSpecialtyList(-1);

            return View("EditVendor", vendor);

        }

        [HttpPost]
        public ActionResult EditVendor(int id, CreateVendorViewModel model )
        {
            //throw new NotImplementedException();
            Core.Vendor.Vendor vendor = _vendorRepository.VendorForEdit(id);

            try
            {
                vendor.VendorBusinessName = Request.Form["VendorBusinessName"];
                vendor.FirstName = Request.Form["FistName"];
                vendor.LastName = Request.Form["LastName"];
                vendor.VendorDesc = Request.Form["VendorDesc"];
                vendor.VendorContactEmail = Request.Form["ContactEmail"];
                vendor.VendorContactTelephone1 = Request.Form["ContactTelephone1"];
                vendor.VendorContactTelephone2 = Request.Form["ContactTelephone2"];
                vendor.VendorSpecialtyId = model.VendorSpecialtyId;
                
                vendor.UpdateDate = DateTime.Now;
                
                _vendorRepository.Save();
            }
            catch
            {
                return View();
            }
            return RedirectToAction("VendorDetails/" + id);
        }


        public ActionResult DeleteVendor(int id)
        {
            Core.Vendor.Vendor vendor = _vendorRepository.VendorForEdit(id);

            vendor.IsActive = false;

            _vendorRepository.Save();

            return Content(Boolean.TrueString);

        }

        #endregion

        #region Manage Work Order

        public ActionResult AddWorkOrder(int id) //id is PropertyId
        {
            //throw new NotImplementedException();
            CreateWorkOrderViewModel model = new CreateWorkOrderViewModel();

            var Categories = _vendorRepository.GetAllWorkOrderCategories();
            var Status = _vendorRepository.GetWorkOrderStatusList();
            var Vendors = _vendorRepository.GetVendorList();
            var Types = _vendorRepository.GetWorkOrderType();

            var payment = new List<SelectListItem>
            {
                new SelectListItem {Text = "Cash", Value="Cash"},
                new SelectListItem {Text = "Cheque", Value="v"},
                new SelectListItem {Text = "Money Order ", Value="Money Order"},
                new SelectListItem {Text = "Bank Draft", Value="Bank Draft"},
                new SelectListItem {Text = "Electronic Transfer", Value="Electronic Transfer"},
                new SelectListItem {Text = "Others", Value="Others"},

            };

            ViewBag.DropdownData = payment;

            model.CategoryList = Categories.ToWorkOrderCategoryList(-1);
            model.StatusList = Status.ToWorkOrderStatusList(-1);
            model.VendorList = Vendors.ToVendorList(-1);
            model.TypeList = Types.ToWorkOrderTypeList(-1);

            return View("AddWorkOrder", model);

        }

        [HttpPost]
        public ActionResult AddWorkOrder(int id, CreateWorkOrderViewModel model)
        {
            Core.Vendor.WorkOrder workOrder = new Core.Vendor.WorkOrder();

            if (ModelState.IsValid)
            {

                try
                {
                    workOrder.WorkOrderName = Request.Form["WorkOrderName"];
                    workOrder.WorkOrderDetails = Request.Form["WorkOrderDetails"];
                    workOrder.WorkOrderCategoryId = model.WorkOrderCategoryId;
                    workOrder.PropertyId = id;
                    workOrder.VendorId = model.VendorId;
                    workOrder.WorkOrderTypeId = model.WorkOrderTypeId;
                    workOrder.WorkOrderStatusId = model.WorkOrderStatusId;
                    workOrder.WorkOrderCategoryId = model.WorkOrderCategoryId;
                    workOrder.InvoiceDocUrl = "default";
                    workOrder.IsAuthorized = model.IsAuthorized;                   
                    workOrder.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                    workOrder.EndDate = DateTime.Parse(Request.Form["EndDate"]);

                    workOrder.IsPaid = model.IsPaid;

                    if (model.IsPaid == false)
                    {
                        workOrder.PaymentAmount = 0;
                        workOrder.PaymentMethod = "Cash";
                        workOrder.InvoiceAmount = 0;
                        workOrder.InvoiceDate = DateTime.Now;
                        workOrder.PaymentDate = DateTime.Now;

                    }
                    else
                    {
                        workOrder.PaymentMethod = Request.Form["PaymentMethod"];
                        workOrder.InvoiceDate = DateTime.Parse(Request.Form["InvoiceDate"]);
                        workOrder.InvoiceAmount = decimal.Parse(Request.Form["InvoiceAmount"]);
                        workOrder.PaymentDate = DateTime.Parse(Request.Form["PaidDate"]);
                        workOrder.InvoiceDate = DateTime.Parse(Request.Form["InvoiceDate"]);
                        workOrder.PaymentAmount = Decimal.Parse(Request.Form["PaymentAmount"]);
                    }

                    workOrder.RecordCreationDate = DateTime.Now;
                    workOrder.RecordUpdateDate = DateTime.Now;

                    _vendorRepository.AddWorkOrder(workOrder);
                    _vendorRepository.Save();


                }
                catch
                {
                    return View("AddWorkOrder", model);
                }
            }
            else
            {
                return View("AddWorkOrder", model);
            }

            
            return RedirectToAction("PropertyDetails/" + id);
 
        }
        
        public ActionResult AddOtherCost(int id)
        {
            OtherCostViewModel model = new OtherCostViewModel();

            var Category = _vendorRepository.GetAllCostCategories();
            model.CostCategoryList = Category.ToCostCategoryList(-1);
            
            return View("AddOtherCost", model);
        }

        [HttpPost]
        public ActionResult AddOtherCost(int id, OtherCostViewModel model)
        {

            //
            OtherCost cost = new OtherCost();

            cost.PropertyId = id;
            cost.CostName = Request.Form["OtherCostName"];
            cost.CostDesc = Request.Form["OtherCostName"];
            cost.CostAmount = decimal.Parse(Request.Form["CostAmount"]);
            cost.IsPaid = model.IsPaid;
            cost.CostCategoryId = model.CostCategoryId;
            
            cost.CostAddedDate = DateTime.Now;

            _vendorRepository.AddOtherCost(cost);
            _vendorRepository.Save();


            return RedirectToAction("PropertyDetails/" + id);
        }

        public ActionResult OtherCostDetails(int id, OtherCostViewModel model)
        {
            OtherCostViewModel cost = _vendorRepository.CostDetails(id);

            model.PropertyName = cost.PropertyName;
            model.OtherCostName = cost.OtherCostName;
            model.CostDetails = cost.CostDetails;
            model.CostAddDate = cost.CostAddDate;
            model.CostCategory = cost.CostCategory;
            model.IsPaid = cost.IsPaid;
            model.CostAmount = cost.CostAmount;
            model.OtherCostId = cost.OtherCostId;

            return View("OtherCostDetils", model);
        }

        public ActionResult EditOtherCost(int id)
        {
            OtherCostViewModel cost = _vendorRepository.CostDetails(id);

            var Category = _vendorRepository.GetAllCostCategories();
            cost.CostCategoryList = Category.ToCostCategoryList(-1);
            
            return View("EditOtherCost", cost);
        }

        [HttpPost]
        public ActionResult EditOtherCost(int id, OtherCostViewModel model)
        {
            OtherCost cost = _vendorRepository.CostForEdit(id);

            
            
            cost.CostName = Request.Form["OtherCostName"];
            cost.CostDesc = Request.Form["CostDetails"];
            cost.IsPaid = model.IsPaid;
            cost.CostCategoryId = model.CostCategoryId;
            cost.CostAmount = decimal.Parse(Request.Form["CostAmount"]);
            
            _vendorRepository.Save();

            return RedirectToAction("OtherCostDetils/" + id);
        }

        public ActionResult WorkOrderDetails(int id)
        {
            //throw new NotImplementedException();
            DisplayWorkOrderViewModel model = new DisplayWorkOrderViewModel();

            DisplayWorkOrderViewModel order = _vendorRepository.GetWorkOrderDetails(id);

            model.WorkOrderId = order.WorkOrderId;
            model.WorkOrderName = order.WorkOrderName;
            model.WorkOrderDetails = order.WorkOrderDetails;
            model.WorkOrderStatus = order.WorkOrderStatus;
            model.StartDate = order.StartDate;
            model.EndDate = order.EndDate;
            model.IsAuthorized = order.IsAuthorized;
            model.IsPaid = order.IsPaid;
            model.InvoiceAmount = order.InvoiceAmount;
            model.InvoiceDate = order.InvoiceDate;
            model.PaidDate = order.PaidDate;
            model.PaymentMethod = order.PaymentMethod;
            model.WorkOrderCategory = order.WorkOrderCategory;
            model.WorkOrderTypeName = order.WorkOrderTypeName;
            model.RecordCreationDate = order.RecordCreationDate;
            model.RecordUpdateDate = order.RecordUpdateDate;
            model.VendorId = order.VendorId;
            model.VendorName = order.VendorName;

            model.PropertyName = order.PropertyName;
            model.PropertyAddressStreet = order.PropertyAddressStreet;
            model.PropertyAddressNumber = order.PropertyAddressNumber;
            model.PropertyAddressSuoteNumber = order.PropertyAddressSuoteNumber;
            model.PropertyAddressCity = order.PropertyAddressCity;
            model.PropertyAddressProvState = order.PropertyAddressProvState;
            model.PropertyAddressPostZipCodet = order.PropertyAddressPostZipCodet;


            model.PaymentAmount = order.PaymentAmount;
            model.WorkOrderTypeName = order.WorkOrderTypeName;


            return View("WorkOrderDetails", model);

        }

        public ActionResult EditWorkOrder(int id)
        {
             
            
            CreateWorkOrderViewModel order = _vendorRepository.GetWorkOrderForEdit(id);

            var Categories = _vendorRepository.GetAllWorkOrderCategories();
            var Status = _vendorRepository.GetWorkOrderStatusList();
            var Vendors = _vendorRepository.GetVendorList();
            var Types = _vendorRepository.GetWorkOrderType();

            var payment = new List<SelectListItem>
            {
                new SelectListItem {Text = "Cash", Value="Cash"},
                new SelectListItem {Text = "Cheque", Value="v"},
                new SelectListItem {Text = "Money Order ", Value="Money Order"},
                new SelectListItem {Text = "Bank Draft", Value="Bank Draft"},
                new SelectListItem {Text = "Electronic Transfer", Value="Electronic Transfer"},
                new SelectListItem {Text = "Others", Value="Others"},

            };

            ViewBag.DropdownData = payment;

            order.CategoryList = Categories.ToWorkOrderCategoryList(-1);
            order.StatusList = Status.ToWorkOrderStatusList(-1);
            order.VendorList = Vendors.ToVendorList(-1);
            order.TypeList = Types.ToWorkOrderTypeList(-1);

            return View("EditWorkOrder", order);
        }

        [HttpPost]
        public ActionResult EditWorkOrder(int id, CreateWorkOrderViewModel model)
        {
            Core.Vendor.WorkOrder workOrder = _vendorRepository.WorkOrderForEdit(id);

            try
            {
                workOrder.WorkOrderName = Request.Form["WorkOrderName"];
                workOrder.WorkOrderDetails = Request.Form["WorkOrderDetails"];
                workOrder.WorkOrderCategoryId = model.WorkOrderCategoryId;
                workOrder.PropertyId = id;
                workOrder.VendorId = model.VendorId;
                workOrder.WorkOrderTypeId = model.WorkOrderTypeId;
                workOrder.WorkOrderStatusId = model.WorkOrderStatusId;
                //workOrder.WorkOrderCategoryId = model.WorkOrderCategoryId;
                workOrder.InvoiceDocUrl = "default";
                
                workOrder.IsAuthorized = model.IsAuthorized;
                
                workOrder.IsPaid = model.IsPaid; 
                
                workOrder.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                workOrder.EndDate = DateTime.Parse(Request.Form["EndDate"]);

                if (workOrder.IsPaid == false)
                {
                    workOrder.InvoiceAmount = 0;
                    workOrder.InvoiceDate = DateTime.Now;
                    workOrder.PaymentDate = DateTime.Now; //.Parse(Request.Form["PaidDate"]);
                    workOrder.PaymentMethod = "Cash";
                    workOrder.PaymentAmount = 0;
                }
                else
                {
                    workOrder.InvoiceAmount = decimal.Parse(Request.Form["InvoiceAmount"]);
                    workOrder.InvoiceDate = DateTime.Parse(Request.Form["InvoiceDate"]);
                    workOrder.PaymentDate = DateTime.Parse(Request.Form["PaidDate"]);
                    workOrder.PaymentMethod = Request.Form["PaymentMethod"];
                    workOrder.PaymentAmount = decimal.Parse(Request.Form["PaymentAmount"]);
                }

                
                workOrder.RecordUpdateDate = DateTime.Now;
                
                _vendorRepository.Save();
            }
            catch
            {
                return RedirectToAction("EditWorkOrder/" + id, model);
            }

            return RedirectToAction("WorkOrderDetails/" + id);
        }

        #endregion

        #region Strata Council Management

        public ActionResult AddStrataCouncil()
        {
            CreateStrataCouncilViewModel model = new CreateStrataCouncilViewModel();

            return View("AddStrataCouncil", model); 
        }

        [HttpPost]
        public ActionResult AddStrataCouncil(FormCollection collection)
        {
            StrataCouncil council = new StrataCouncil();

            council.StrataCouncilTitle = Request.Form["StrataCouncilName"];
            council.StrataCouncilMailingAddress = Request.Form["StrataCounvilMailingAddress"];
            council.StrataManagerFirstName = Request.Form["StrataManageFirstName"];
            council.StrataManagerLastName = Request.Form["StrataManagerLastName"];
            council.StrataManagerEmail = Request.Form["StrataManagerContactEmail"];
            council.StrataManagerTelephone = Request.Form["StrataManagerContactTel"];
            council.Notes = Request.Form["Notes"];
            council.IsActive = true;

            council.CreateDate = DateTime.Now;
            council.UpdateDate = DateTime.Now;

            _strataRepository.AddStrataCouncil(council);
            _strataRepository.Save();

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult StrataCouncilDetails(int id, DisplayStrataCouncilViewModel model)
        {

             //= new DisplayStrataCouncilViewModel();

            DisplayStrataCouncilViewModel council = _strataRepository.GetStrataCouncilDetails(id);

            IEnumerable<DisplayStrataCouncilViewModel> properties = _strataRepository.GetPropertyByCouncil(council.StrataCouncilId);


            //var Property = _strataRepository.GetCondoPropertyList();
            //council.CondoList = Property.ToCondoList(-1);

            if (properties.Count() != 0)
            {
                model.PropertyList = properties;
            }

            model.StrataCouncilId = council.StrataCouncilId;
            model.StrataCouncilName = council.StrataCouncilName;
            model.StrataCounvilMailingAddress = council.StrataCounvilMailingAddress;
            model.StrataManageFirstName = council.StrataManageFirstName;
            model.StrataManagerLastName = council.StrataManagerLastName;
            model.StrataManagerContactTel = council.StrataManagerContactTel;
            model.StrataManagerContactEmail = council.StrataManagerContactEmail;



            return View("StrataCouncilDetails", model);
        }

        public ActionResult EditStrataCouncil(int id)
        {
            StrataCouncil council = _strataRepository.GetCouncilById(id);

            return View(council);
        }

        [HttpPost]
        public ActionResult EditStrataCouncil(int id, FormCollection form)
        {
            StrataCouncil council = _strataRepository.GetCouncilById(id);
            
            council.StrataCouncilTitle = Request.Form["StrataCouncilTitle"];
            council.StrataManagerFirstName = Request.Form["StrataManagerFirstName"];
            council.StrataManagerLastName = Request.Form["StrataManagerLastName"];
            council.StrataManagerEmail = Request.Form["StrataManagerEmail"];
            council.StrataCouncilMailingAddress = Request.Form["StrataCouncilMailingAddress"];
            council.StrataManagerTelephone = Request.Form["StrataManagerTelephone"];

            _strataRepository.Save();

            return RedirectToAction("StrataCouncilDetails/" + id);
        }

        public ActionResult DeleteStrataCouncil(int id)
        {
            StrataCouncil council = _strataRepository.GetCouncilById(id);
            council.IsActive = false;

            _strataRepository.Save();

            return Content(Boolean.TrueString);
 
        }

        public JsonResult GetCondoList()
        {
            //var Property = _strataRepository.GetCondoPropertyList();
            ProMasterStrataCouncilDataEntities db = new ProMasterStrataCouncilDataEntities();

            //return Json(Property, JsonRequestBehavior.AllowGet);

            //return this.Json(new
            //{
            //    result = (from property in db.Properties select new {id = property.PropertyId, name = property.PropertyName})
            //}, JsonRequestBehavior.AllowGet
            //);

            var result = from properties in db.Properties.Where(t => t.PropertyTypeId == 2 || t.PropertyTypeId == 3).Where(l=>l.StrataCouncilId == null)
                         select new
                         {
                             id = properties.PropertyId,
                             name = properties.PropertyName
                         };
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetManagedCondoList(int id, DisplayStrataCouncilViewModel model)
        {
            DisplayStrataCouncilViewModel council = _strataRepository.GetStrataCouncilDetails(id);

            IEnumerable<DisplayStrataCouncilViewModel> properties = _strataRepository.GetPropertyByCouncil(council.StrataCouncilId);

            if (properties.Count() != 0)
            {
                model.PropertyList = properties;
            }

            return PartialView("_CondoList", model);
        }

        [HttpPost]
        public JsonResult AddCondoToStrata(int id, int sid) //is is the propertyId
        {
            Core.StrataCouncil.Property property = _strataRepository.GetPropertyById(id);

            property.StrataCouncilId = sid;

            _strataRepository.Save();

            return Json("The condo has been added successfully");
        }

        #endregion

        #region Tenant Screening

        public ActionResult TenantScreen(TenantScreenViewModel model, int id)
        {
            TenantScreenViewModel application = _tenantRepository.GetApplicationById(id);

            //@Html.DropDownListFor(model=>model.ScreenCheckStatusId, Model.ScreenCheckStatusList)


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

            

            //var status = new List<SelectListItem>
            //{
                
            //    new SelectListItem {Text = "Pending", Value="PD"},
            //    new SelectListItem {Text = "Pass", Value="PS"},
            //    new SelectListItem {Text = "Fail ", Value="FL"},                      

            //};

            //ViewBag.DropdownData = status;

            //model.IncomeCheckStatus = application.IncomeCheckStatus;
            //model.PreviousEmployerReferenceCheckStatus = application.PreviousEmployerReferenceCheckStatus;
            //model.CurrentEmployerReferenceCheckStatus = application.CurrentEmployerReferenceCheckStatus;
            //model.CurrentLandLordReferenceCheckStatus = application.CurrentLandLordReferenceCheckStatus;
            //model.PreviousLandlordReferenceCheckStatus = application.PreviousLandlordReferenceCheckStatus;
            //model.CreditReportCheckStatus = application.CreditReportCheckStatus;
            //model.FinalScreeningStatus = application.FinalScreeningStatus;

            //model.ScreenCheckStatusList2 = _tenantRepository.GetCheckStatusByApplicantId(id);
            //model.ScreenCheckTypeList = _tenantRepository.GetCheckTypeBypplicantId(id);

            model.Screening = _tenantRepository.ScreeningById(id);
            model.DocCheckList = _tenantRepository.DocCheckListByAppId(id);

            return View("TenantScreen", model);
        }

        [HttpPost]
        public ActionResult TenantScreen(TenantScreenViewModel model, int id, FormCollection collection)
        {
            ScreenProcess process = _tenantRepository.GetScreeenProcessByAppId(id); //Application Id
            DocumentCheckList list = _tenantRepository.GetDocumentCheckListByAppId(id);

            process.CreditReportCheckStatus = model.CreditReportCheckStatus;//Request.Form["CreditReportCheckStatus"];
            process.IsScreenProcessCompleted = model.IsScreenProcessCompleted;
            process.PreviousEmploerCheckStatus = Request.Form["PreviousEmployerReferenceCheckStatus"];
            process.PrevioustLandlordCheckStatus = Request.Form["PreviousLandlordReferenceCheckStatus"];
            process.CurrentEmploerCheckStatus = model.CurrentEmployerReferenceCheckStatus; // Request.Form["PreviousLandlordReferenceCheckStatus"];
            process.CurrentLandlordCheckStatus = model.CurrentLandLordReferenceCheckStatus;//Request.Form["CurrentLandLordReferenceCheckStatus"];
            process.ScreenProcessCompletionDate = DateTime.Now;
            process.ScreenFinalStatus = Request.Form["FinalScreeningStatus"];
            process.RentToIncomeRatioCheckStatus = Request.Form["IncomeCheckStatus"];

            list.IsCreditReportProvided = model.IsCreditReportAvailable;
            list.IsEmployerReferenceProvided = model.EmployerReferenceDocAvailable;
            list.IsIdentificationProvided = model.IdentificationDocAvailable;
            list.IsIncomeStatementProvided = model.IncomeDocAvailable;
            list.IsLandLordReferenceProvided = model.LandlordReferenceDocAvailable;
            list.DocumentCheckListCompletionDate = DateTime.Now;

            _tenantRepository.Save();

            return RedirectToAction("TenantScreen/" + id);
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id, int sId, int tId, string notes)
        {

            TenantScreening screen = _tenantRepository.GetScreenStatus(id, tId);

            screen.ScreeningCheckStatusId = sId;
            screen.ScreenCheckUpdateDate = DateTime.Now;
            screen.ScreenNotes = notes;

            _tenantRepository.Save();

            return Json("The Screen Status has been updated successfully!");
        }

        public ActionResult GetScreenStatusByAppId(int id, TenantScreenViewModel model)
        {
            //TenantScreenViewModel model = new TenantScreenViewModel();

            IEnumerable<TenantScreenViewModel> status = _tenantRepository.ScreeningById(id);

            model.Screening = status;

            return PartialView("_ScreenStatus", model);
        }

        [HttpPost]
        public JsonResult GetNotes(int id, int tId)
        {
            string notes = _tenantRepository.GetScreenNotes(id, tId);
            return Json(notes);
        }
        
        #endregion

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

        #endregion

        #region Messaging

        public ActionResult Message()
        {
            MessagingVieModel model = new MessagingVieModel();

            IEnumerable<MessagingVieModel> msgs = _propertyRepository.GetListMessageByRecipient(User.Identity.Name);

            return View("Message", msgs);
        }

        public JsonResult SendMessage(string to, string subject, string body)
        {
            Message msg = new Message();

            
            string[] result_array = to.Split(',');

            string fn = result_array[1];
            string ln = result_array[0];

            fn = fn.Trim();
            ln = ln.Trim();

            string username = _propertyRepository.GetUserName(ln, fn);            

            msg.ToUserName = username;
            msg.SentByUserName = User.Identity.Name;
            msg.MessageSubject = subject;
            msg.MessageBody = body;
            msg.SentDate = DateTime.Now;
            msg.MessageReadStatus = false;
            msg.MessageTypeId = 1;
            msg.PropertyId = 1;
            msg.ToAllLandlords = false;
            msg.ToAllTenants = false;

            _propertyRepository.AddMessage(msg);
            _propertyRepository.Save();

            return Json("Message sent successfully!");
        }


        public ActionResult GetMessageList(int id)
        {

            return PartialView("");
        }


        public JsonResult GetOnlineUserName(string term)
        {
            ProMasterPropertyDataEntities db = new ProMasterPropertyDataEntities();

            var tenants = from tenant in db.Tenants.Where(u => u.FirstName.StartsWith(term))
                          select tenant.LastName + ", " + tenant.FirstName;

            tenants = tenants.Distinct();

            var landlords = from landlord in db.PropertyOwners.Where(u => u.FirstName.StartsWith(term))
                            select landlord.LastName + ", " + landlord.FirstName;

            landlords = landlords.Distinct();


            var tenantList = tenants.ToList();
            var landlordList = landlords.ToList();

            var finalList = new List<string>();



            finalList.AddRange(tenantList);

            finalList.AddRange(landlordList);


            return Json(finalList, JsonRequestBehavior.AllowGet);

        }

        


        #endregion

        /*

        #region Site Search

        [HttpPost]
        public ActionResult SiteSearch(string searchText)
        {
            SiteSearchViewModel searchModel = new SiteSearchViewModel();

            IEnumerable<SiteSearchViewModel> properties = _propertyRepository.ShowPropertySearchResults(searchText);
            IEnumerable<SiteSearchViewModel> tenants = _tenantRepository.ShowAllTenantSearchResult(searchText);

            IEnumerable<SiteSearchViewModel> leases = _leaseRepository.ShowAllLeaseResults(searchText);

            IEnumerable<SiteSearchViewModel> contracts = _contractRepository.ShowAllContractResult(searchText);

            IEnumerable<SiteSearchViewModel> vendors = _tenantRepository.ShowAllTenantSearchResult(searchText);
            IEnumerable<SiteSearchViewModel> councils = _tenantRepository.ShowAllTenantSearchResult(searchText);
            IEnumerable<SiteSearchViewModel> landlords = _propertyOwnerRepository.ShowAllLandlordResult(searchText);

            searchModel.ProopertyResults = properties;
            searchModel.TenantResults = tenants;
            searchModel.ContractResults = contracts;
            searchModel.LeaseResults = leases;
            searchModel.LandlordResults = landlords;
            searchModel.VendorResults = vendors;
            searchModel.CouncilResults = councils;


            return View("SiteSearch", searchModel);
        }

        #endregion
        */



        //***********************************************************
        //
        //***********************************************************

        //
        // GET: /Manage/Details/5

        
    }
}
