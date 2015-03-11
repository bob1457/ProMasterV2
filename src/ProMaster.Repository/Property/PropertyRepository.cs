using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Property;
using ProMaster.Core.Property;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.Repository.Property
{
    public class PropertyRepository : IPropertyRepository
    {
        ProMasterPropertyDataEntities entities = new ProMasterPropertyDataEntities();

        public IEnumerable<PropertyType> GetPropertyTypes()
        {
            //throw new NotImplementedException();
            return entities.PropertyTypes;
        }

        public IEnumerable<RentalStatu> GetPropertyStatus()
        {
            //throw new NotImplementedException();
            return entities.RentalStatus;
        }

        public int GetPmId(string userName)
        {
            //throw new NotImplementedException();
            PropertyManager singleOrDefault;
            singleOrDefault = entities.PropertyManagers.SingleOrDefault(n => n.UserName == userName);
            return singleOrDefault != null ? singleOrDefault.PropertyManagerId : 0;
        }

        public IEnumerable<PropertyOwner> GetLandlordsByPm(string pmName)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.Where(p => p.AddedBy == pmName);
        }


        public IEnumerable<ManageDisplayViewModel> GetMyProperty(int id) //id is Property Manager's id
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         where property.PropertyManagerId == id && property.IsActive
                         select new ManageDisplayViewModel 
                         { 
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             pType = type.PropertyType1,
                             RentalStatus = status.Status,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             CreateDate = property.CreatedDate,
                             PropertyUpdateDate = property.UpdateDate
                         
                         };

            return result.OrderByDescending(d=>d.CreateDate);
        }



        public IEnumerable<PropertyMarketingViewModel> ListedProperty(int sId, int pmId)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where property.RentalStatusId == sId && property.PropertyManagerId == pmId
                         select new PropertyMarketingViewModel
                         {
                             PropertyId = property.PropertyId,
                             
                             //LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,                             
                             RentalStatusId = property.RentalStatusId,                             
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreateDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             //RentStatus = status.Status,
                             BuildYear = property.PropertyBuildYear,
                         };

            return result;
        }

        public IEnumerable<PropertyMarketingViewModel> PreListedProperty(int id, int pmId) //id is rental status id
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where property.RentalStatusId == id && property.PropertyManagerId == pmId
                         select new PropertyMarketingViewModel
                         {
                             PropertyId = property.PropertyId,

                             //LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             RentalStatusId = property.RentalStatusId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreateDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             //RentStatus = status.Status,
                             BuildYear = property.PropertyBuildYear,
                         };

            return result;
        }



        public ListingViewModel GetPreListedProperty(int id, int pmId)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where property.RentalStatusId == id && property.PropertyManagerId == pmId
                         select new ListingViewModel
                         {
                             PropertyId = property.PropertyId,

                             //LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyListDesc = property.PropertyListDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             RentalStatusId = property.RentalStatusId,                             
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             RecordCreateDate = property.CreatedDate,
                             RecordUpdateDate = property.UpdateDate,
                             //RentStatus = status.Status,
                              
                         };

            return result.FirstOrDefault();
        }


        public IEnumerable<PropertyMarketingViewModel> RentedProperty(int id, int pmId)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where property.RentalStatusId == id && property.PropertyManagerId == pmId
                         select new PropertyMarketingViewModel
                         {
                             PropertyId = property.PropertyId,

                             //LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             RentalStatusId = property.RentalStatusId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreateDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             //RentStatus = status.Status,
                             BuildYear = property.PropertyBuildYear,
                         };

            return result;
        }




        public IEnumerable<ManageDisplayViewModel> GetPropertyListByPmId(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         where property.PropertyManagerId == id
                         select new ManageDisplayViewModel
                         {
                             PropertyId = property.PropertyId,                            
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             pType = type.PropertyType1,                             
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreateDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentalStatus = status.Status,
                         };

            return result;
        }

        public IEnumerable<DisplayPropertyViewModel> PropertyDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from plist in entities.PropertyLists
                         join property in entities.Properties on plist.PropertyId equals property.PropertyId
                         //join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         //join features in entities.PropertyFeatures on property.PropertyFeatureId equals features.PropertyFeatureId
                         //join facilities in entities.PropertyFacilities on property.PropertyFacilityId equals facilities.PropertyFacilityId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         //join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         //join lease in entities.Leases on property.PropertyId equals lease.PropertyId
                         //join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         //join payment in entities.RentPayments on tenant.TenantId equals payment.TenantId
                         //join method in entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         
                         where property.PropertyId == id
                         select new DisplayPropertyViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyFacilityId = property.PropertyFacilityId,
                             PropertyFeatureId = property.PropertyFeatureId,
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             Status = status.Status,
                             PropertyAddressId = property.PropertyAddressId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentStatus = status.Status,
                             PropertyListDesc = property.PropertyListDesc,
                             PorpertyListingTitle = plist.ListingTitle,
                             ListedRentAmount = plist.ListedRentAmount
                             //PropertyAddressSuiteNumber = address.PropertySuiteNumber,                             
                             //PropertyAddressStreetNumber = address.PropertySuiteNumber,
                             //PropertyAddressStreetName = address.PropertyStreet,
                             //PropertyAddressCity = address.PropertyCity,
                             //PropertyAddressProvState = address.PropertyStateProvince,
                             //PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             //PropertyAddressCountry = address.PropertyCountry,

                             //NumberOfBathrooms = features.NumberOfBathrooms,
                             //NumberOfBedrooms = features.NumberOfBedrooms,
                             //TotalLivingArea = features.TotalLivingArea,
                             //NumberOfFloors = features.NumberOfLayers,
                             //NumberOfParkings = features.NumberOfParking,

                             //Stove = facilities.Stove,
                             //Refrigerator = facilities.Refrigerator,
                             //Dishwasher = facilities.Dishwasher,
                             //AireConditioner = facilities.AirConditioner,
                             //Blinds = facilities.BlindsCurtain,
                             //Laundry = facilities.Laundry,
                             //CommonFacility = facilities.CommonFacility,
                             //Security = facilities.SecuritySystem,
                             //FireAlarm = facilities.FireAlarmSystem,
                             //TVInternet = facilities.TVInternet,
                             //Furniture = facilities.Furniture,
                             //Others = facilities.Others,
                             //Notes2 = facilities.Notes,

                             //LandlordFirstName = landlord.FirstName,
                             //LandlordLastName = landlord.LastName,
                             //LandlordContactEmail = landlord.ContactEmail,
                             //LandlordContactTelephone = landlord.ContactTelephone1


                         };
            return result;
            //




        }

        public IEnumerable<DisplayPropertyViewModel> GetPropertyDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties 
                         //join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         //join features in entities.PropertyFeatures on property.PropertyFeatureId equals features.PropertyFeatureId
                         //join facilities in entities.PropertyFacilities on property.PropertyFacilityId equals facilities.PropertyFacilityId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         //join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         //join lease in entities.Leases on property.PropertyId equals lease.PropertyId
                         //join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         //join payment in entities.RentPayments on tenant.TenantId equals payment.TenantId
                         //join method in entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId

                         where property.PropertyId == id
                         select new DisplayPropertyViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyFacilityId = property.PropertyFacilityId,
                             PropertyFeatureId = property.PropertyFeatureId,
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             Status = status.Status,
                             PropertyAddressId = property.PropertyAddressId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentStatus = status.Status,
                             //PropertyListDesc = property.PropertyListDesc,
                             //PorpertyListingTitle = plist.ListingTitle,
                             //ListedRentAmount = plist.ListedRentAmount
                             //PropertyAddressSuiteNumber = address.PropertySuiteNumber,                             
                             //PropertyAddressStreetNumber = address.PropertySuiteNumber,
                             //PropertyAddressStreetName = address.PropertyStreet,
                             //PropertyAddressCity = address.PropertyCity,
                             //PropertyAddressProvState = address.PropertyStateProvince,
                             //PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             //PropertyAddressCountry = address.PropertyCountry,

                             //NumberOfBathrooms = features.NumberOfBathrooms,
                             //NumberOfBedrooms = features.NumberOfBedrooms,
                             //TotalLivingArea = features.TotalLivingArea,
                             //NumberOfFloors = features.NumberOfLayers,
                             //NumberOfParkings = features.NumberOfParking,

                             //Stove = facilities.Stove,
                             //Refrigerator = facilities.Refrigerator,
                             //Dishwasher = facilities.Dishwasher,
                             //AireConditioner = facilities.AirConditioner,
                             //Blinds = facilities.BlindsCurtain,
                             //Laundry = facilities.Laundry,
                             //CommonFacility = facilities.CommonFacility,
                             //Security = facilities.SecuritySystem,
                             //FireAlarm = facilities.FireAlarmSystem,
                             //TVInternet = facilities.TVInternet,
                             //Furniture = facilities.Furniture,
                             //Others = facilities.Others,
                             //Notes2 = facilities.Notes,

                             //LandlordFirstName = landlord.FirstName,
                             //LandlordLastName = landlord.LastName,
                             //LandlordContactEmail = landlord.ContactEmail,
                             //LandlordContactTelephone = landlord.ContactTelephone1


                         };

            return result;            

        }


        public IEnumerable<PropertyAddress> GetAddressById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyAddresses.Where(a => a.PropertyAddressId == id);
        }

        public IEnumerable<PropertyFeature> GetFeatureById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyFeatures.Where(f => f.PropertyFeatureId == id);
        }

        public IEnumerable<PropertyFacility> GetFacilityById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyFacilities.Where(f => f.PropertyFacilityId == id);
        }


        public IEnumerable<PropertyOwner> GetLandlordById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.Where(o => o.PropertyOwnerId == id);
        }

        public DisplayPropertyViewModel GetPropertyById(int id) //for editing
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join features in entities.PropertyFeatures on property.PropertyFeatureId equals features.PropertyFeatureId
                         join facilities in entities.PropertyFacilities on property.PropertyFacilityId equals facilities.PropertyFacilityId
                         join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         where property.PropertyId == id
                         select new DisplayPropertyViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyFacilityId = property.PropertyFacilityId,
                             PropertyFeatureId = property.PropertyFeatureId,
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyListDesc = property.PropertyListDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             Status = status.Status,
                             RentalStatusId = property.RentalStatusId,
                             PropertyAddressId = property.PropertyAddressId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentStatus = status.Status,

                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,


                             NumberOfBathrooms = features.NumberOfBathrooms,
                             NumberOfBedrooms = features.NumberOfBedrooms,
                             TotalLivingArea = features.TotalLivingArea,
                             NumberOfFloors = features.NumberOfLayers,
                             NumberOfParkings = features.NumberOfParking,

                             Stove = facilities.Stove,
                             Refrigerator = facilities.Refrigerator,
                             Dishwasher = facilities.Dishwasher,
                             AireConditioner = facilities.AirConditioner,
                             Blinds = facilities.BlindsCurtain,
                             Laundry = facilities.Laundry,
                             CommonFacility = facilities.CommonFacility,
                             Security = facilities.SecuritySystem,
                             FireAlarm = facilities.FireAlarmSystem,
                             TVInternet = facilities.TVInternet,
                             Furniture = facilities.Furniture,
                             Others = facilities.Others,
                             Notes2 = facilities.Notes,

                             LandlordFirstName = landlord.FirstName,
                             LandlordLastName = landlord.LastName,
                             LandlordContactEmail = landlord.ContactEmail,
                             LandlordContactTelephone = landlord.ContactTelephone1

                         };
            return result.FirstOrDefault();

        }


        public ListingViewModel GetListedPropertyById(int id)
        {
            //throw new NotImplementedException();
            var result = from listing in entities.PropertyLists
                         join property in entities.Properties on listing.PropertyId equals property.PropertyId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join facilities in entities.PropertyFacilities on property.PropertyFacilityId equals facilities.PropertyFacilityId
                         join features in entities.PropertyFeatures on property.PropertyFeatureId equals features.PropertyFeatureId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId                         
                         where property.PropertyId == id
                         select new ListingViewModel
                         {
                             PropertyId = property.PropertyId,                             
                             PropertyName = property.PropertyName,                            
                             PropertyListDesc = property.PropertyListDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             RentStatus = status.Status, 

                             PropertyAddressId = address.PropertyAddressId,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,

                             PropertyFeatureId = features.PropertyFeatureId,
                             NumberOfBathrooms = features.NumberOfBathrooms,
                             NumberOfBedrooms = features.NumberOfBedrooms,
                             TotalLivingArea = features.TotalLivingArea,
                             NumberOfFloors = features.NumberOfLayers,
                             NumberOfParkings = features.NumberOfParking,
                             
                             PropertyFacilityId = facilities.PropertyFacilityId,
                             Stove = facilities.Stove,
                             Refrigerator = facilities.Refrigerator,
                             Dishwasher = facilities.Dishwasher,
                             AireConditioner = facilities.AirConditioner,
                             Blinds = facilities.BlindsCurtain,
                             Laundry = facilities.Laundry,
                             CommonFacility = facilities.CommonFacility,
                             Security = facilities.SecuritySystem,
                             FireAlarm = facilities.FireAlarmSystem,
                             TVInternet = facilities.TVInternet,
                             Furniture = facilities.Furniture,
                             Others = facilities.Others,
                             Notes2 = facilities.Notes,
                             
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             RecordCreateDate = listing.CreationDate,
                             RecordUpdateDate = listing.UpdateDate,

                             PropertyListingId = listing.PropertyListId,
                             PorpertyListingTitle = listing.ListingTitle,
                             ListedRentAmount = listing.ListedRentAmount,
                             ListedDate = listing.ListedDate,
                             Notes = listing.Notes,
                             IsListedExternally = listing.IsListedExternal,
                             PictureCreateDate = listing.CreationDate,
                             PictureUpdateDate = listing.UpdateDate

                         };

            return result.FirstOrDefault();
        }



        public PropertyListingPicture GetPictureById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyListingPictures.Where(p => p.PropertyListingPictureId == id).FirstOrDefault();
        }


        public IEnumerable<PropertyList> GetListById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyLists.Where(l => l.PropertyId == id);
        }

        public Core.Property.Property PropertyById(int id)
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(p => p.PropertyId == id).FirstOrDefault();
        }

        public PropertyAddress AddressById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyAddresses.FirstOrDefault(a => a.PropertyAddressId == id);
        }

        public PropertyFeature FeaturesById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyFeatures.FirstOrDefault(f => f.PropertyFeatureId == id);
        }

        public PropertyFacility FacilityById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyFacilities.Where(f => f.PropertyFacilityId == id).FirstOrDefault();
        }



        public IEnumerable<SiteSearchViewModel> ShowPropertySearchResults(string searchString)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         where property.PropertyName.ToUpper().Contains(searchString.ToUpper()) || property.PropertyDesc.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyAddDate = property.CreatedDate
                         };

            return result;                        

        }

        //public Core.Property.Lease LeaseById(int id)
        //{
        //    //throw new NotImplementedException();
        //    return entities.Leases.Where(p => p.PropertyId == id).FirstOrDefault();
        //}

        public void AddApplicant(TenancyApplication applicant)
        {
            //throw new NotImplementedException();
            entities.TenancyApplications.AddObject(applicant);
        }


        public IEnumerable<ManageDisplayViewModel> GetAllApplicationsByPmId(int pmId)
        {
            //throw new NotImplementedException();
            var result = from application in entities.TenancyApplications
                         join property in entities.Properties on application.PropertyId equals property.PropertyId
                         where property.PropertyManagerId == pmId && application.IsApproved == false
                         select new ManageDisplayViewModel
                         {
                             ApplicaitonId = application.TenancyApplicationId,
                             FistName = application.ApplicantFirstName,
                             LastName = application.ApplicantLastName,
                             IsApproved = application.IsApproved,
                             ContactEmail = application.ApplicantContactEmail,
                             ContactTelephone1 = application.ApplicantContactTel,
                             PropertyName = property.PropertyName
                             




                         };

            return result;
        }



        public DisplayApplicationViewModel TenancyApplicationByDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from application in entities.TenancyApplications
                         join property in entities.Properties on application.PropertyId equals property.PropertyId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where application.TenancyApplicationId == id
                         select new DisplayApplicationViewModel 
                         { 
                             ApplicationId = application.TenancyApplicationId,
                             ApplicantFirstName = application.ApplicantFirstName,
                             ApplicantLastName = application.ApplicantLastName,
                             ApplicantContactEmail = application.ApplicantContactEmail,
                             ApplicatnContactTel = application.ApplicantContactTel,
                             CurrentAddress = application.CurrentAddress,
                             CurrentEmployerContact = application.CurrentEmployerContact,
                             CurrentLandlordContact = application.CurrentLandlordContact,
                             IdenticationAvailable = application.IsIdentificationDocAvailalbe,
                             AuthorizedRefCheck = application.IsAuthorizedForRefCheck,
                             CreditScore = application.CreditReportScore,
                             MonthlyIncome = application.CurrentMonthlyIncome,
                             ApplyDate = application.ApplicationSentDate,
                             IsApproved = application.IsApproved,
                             NumberOfChildren = application.NumberOfChildren,
                             NumberOfOccupants = application.NumberOfTenant,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyBuildYear = property.PropertyBuildYear,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             PropertyType = type.PropertyType1,
                             RentStatus = status.Status
                         };

            return result.FirstOrDefault();
        }

        public IEnumerable<DisplayPropertyViewModel> ShowAllProperty()
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join features in entities.PropertyFeatures on property.PropertyFeatureId equals features.PropertyFeatureId
                         join facilities in entities.PropertyFacilities on property.PropertyFacilityId equals facilities.PropertyFacilityId
                         join pList in entities.PropertyLists on property.PropertyId equals pList.PropertyId
                         //join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         where property.RentalStatusId == 2
                         select new DisplayPropertyViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyFacilityId = property.PropertyFacilityId,
                             PropertyFeatureId = property.PropertyFeatureId,
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             Status = status.Status,
                             RentalStatusId = property.RentalStatusId,
                             PropertyAddressId = property.PropertyAddressId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentStatus = status.Status,
                             BuildYear = property.PropertyBuildYear,

                             MonthlyRent = pList.ListedRentAmount,

                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,

                             NumberOfBathrooms = features.NumberOfBathrooms,
                             NumberOfBedrooms = features.NumberOfBedrooms,
                             TotalLivingArea = features.TotalLivingArea,
                             NumberOfFloors = features.NumberOfLayers,
                             NumberOfParkings = features.NumberOfParking,

                             Stove = facilities.Stove,
                             Refrigerator = facilities.Refrigerator,
                             Dishwasher = facilities.Dishwasher,
                             AireConditioner = facilities.AirConditioner,
                             Blinds = facilities.BlindsCurtain,
                             Laundry = facilities.Laundry,
                             CommonFacility = facilities.CommonFacility,
                             Security = facilities.SecuritySystem,
                             FireAlarm = facilities.FireAlarmSystem,
                             TVInternet = facilities.TVInternet,
                             Furniture = facilities.Furniture,
                             Others = facilities.Others,
                             Notes2 = facilities.Notes,

                             //LandlordFirstName = landlord.FirstName,
                             //LandlordLastName = landlord.LastName,
                             //LandlordContactEmail = landlord.ContactEmail,
                             //LandlordContactTelephone = landlord.ContactTelephone1

                         };

            return result;
        }



        public IEnumerable<ListingViewModel> ShowListedProperty()
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId                         
                         join pList in entities.PropertyLists on property.PropertyId equals pList.PropertyId
                         //join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         where property.RentalStatusId == 2
                         select new ListingViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyListingId = pList.PropertyListId,
                             
                             PorpertyListingTitle = pList.ListingTitle,
                             PropertyListDesc = property.PropertyListDesc,

                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             
                             RentalStatusId = property.RentalStatusId,
                             
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                            

                             ListedRentAmount = pList.ListedRentAmount,

                             

                         };

            return result;
        }

        public IEnumerable<ManageDisplayViewModel> ListPropertyForRent()
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         join pList in entities.PropertyLists on property.PropertyId equals pList.PropertyId
                         where property.RentalStatusId == 2
                         select new ManageDisplayViewModel
                         {
                             PropertyId = property.PropertyId,
                             //PropertyFacilityId = property.PropertyFacilityId,
                             //PropertyFeatureId = property.PropertyFeatureId,
                             LandlordId = property.PropertyOwnerId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             pType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             
                             RentalStatusId = property.RentalStatusId,
                             //PropertyAddressId = property.PropertyAddressId,
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,
                             RentalStatus = status.Status,
                             BuildYear = property.PropertyBuildYear,

                             MonthlyRent = pList.ListedRentAmount
                         };

            return result;


        }

        public PropertyMarketingViewModel GetPropertyForMarketing(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId                         
                         where property.PropertyId == id
                         select new PropertyMarketingViewModel 
                         {
                             PropertyId = property.PropertyId,
                             
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyTypeId = property.PropertyTypeId,
                             RentalStatus = status.Status,
                             RentalStatusId = property.RentalStatusId,                            
                             IsActive = property.IsActive,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreateDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,                             
                             BuildYear = property.PropertyBuildYear
                         };

            return result.FirstOrDefault();
        }


        public TenancyApplication GetApplicationById(int id)
        {
            //throw new NotImplementedException();
            return entities.TenancyApplications.Where(t => t.TenancyApplicationId == id).FirstOrDefault();
        }


        public IEnumerable<Core.Property.Document> DocumenetByPropertyid(int id, int pid)
        {
            //throw new NotImplementedException();
            return entities.Documents.Where(p => p.DocumentPrincipalId == id && p.DocumentPrincipalTypeId == pid);
        }


        public IEnumerable<Core.Property.Document> GetDocumenetByPropertyid(int id)
        {
            //throw new NotImplementedException();
            return entities.Documents.Where(p => p.DocumentPrincipalId == id);
        }


        //public IEnumerable<ManagementEvent> GetEventListById(int id)
        //{
        //    //throw new NotImplementedException();
        //    return entities.ManagementEvents.Where(p => p.PropertyId == id);
        //}


        public DisplayPropertyViewModel GetEventByid(int id)
        {
            //throw new NotImplementedException();
            var result = from mEvent in entities.ManagementEvents
                         join type in entities.EventTypes on mEvent.EventTypeId equals type.EventTypeId
                         join property in entities.Properties on mEvent.PropertyId equals property.PropertyId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         where mEvent.EventId == id
                         select new DisplayPropertyViewModel
                         {
                             EventId = mEvent.EventId,
                             EventDate = mEvent.EventDate,
                             EventType = type.EventType1,
                             EventTitle = mEvent.EventName,
                             EventDetails = mEvent.EventDetails,
                             TimeSpent = mEvent.TimeSpent,
                             MileageIncurred = mEvent.MileageIncurred,
                             CreationDate = mEvent.CreationDate

                         };

            return result.FirstOrDefault();
        }
        


        public IEnumerable<DisplayPropertyViewModel> EventListByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from events in entities.ManagementEvents
                         join type in entities.EventTypes on events.EventTypeId equals type.EventTypeId
                         where events.PropertyId == id
                         select new DisplayPropertyViewModel
                         {
                             EventId = events.EventId,
                             EventTitle = events.EventName,
                             EventDate = events.EventDate,
                             EventCreationDate = events.CreationDate,
                             EventDetails = events.EventDetails,
                             CreationDate = events.CreationDate,
                             EventType = type.EventType1,
                             MileageIncurred = events.MileageIncurred,
                             TimeSpent = events.TimeSpent

                         };
            return result;

        }

        public IEnumerable<Calendar> GetEventListByPmId(int id)
        {
            //throw new NotImplementedException();
            return entities.Calendars.Where(p => p.PmId == id);
        }



        public Calendar GetCalendarEventById(int id)
        {
            //throw new NotImplementedException();
            return entities.Calendars.Where(e => e.CalendarId == id).FirstOrDefault();
        }

        public IEnumerable<MessagingVieModel> GetListMessageByRecipient(string userName)
        {
            //throw new NotImplementedException();
            var result = from message in entities.Messages
                         join property in entities.Properties on message.PropertyId equals property.PropertyId
                         where message.ToUserName == userName
                         select new MessagingVieModel
                         {
                             Propertyid = message.PropertyId,
                             SentByUserName = message.SentByUserName,
                             MessageId = message.MessageId,
                             MessageSubject = message.MessageSubject,
                             MessageBody = message.MessageBody,
                             SentDate = message.SentDate,

                             PropertyName = property.PropertyName
                         };
            return result;
        }

        public IEnumerable<string> GetOnlineUserName()
        {
            throw new NotImplementedException();
        }


        public string GetUserName(string ln, string fn)
        {
            //throw new NotImplementedException();
            var firstOrDefault = entities.Tenants.Where(n => n.FirstName == fn && n.LastName == ln).FirstOrDefault();
            return firstOrDefault != null ? firstOrDefault.UserName : null;
        }


        public IEnumerable<ListingViewModel> GetPicturesForProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from picture in entities.PropertyListingPictures  
                         where picture.PropertyId == id
                         select new ListingViewModel
                         {
                             PropertyListPictureId = picture.PropertyListingPictureId,
                             ListedPropertyPictureCaption = picture.Caption,
                             ListPictureImgUrl = picture.ImgUrl,
                             ListPictureImgUrlThumb = picture.ImgUrlThumb,
                             ListPictureImgUrlMed = picture.ImgUrlMed,
                             PropertyId = picture.PropertyId,
                             PictureCreateDate = picture.CreationDate,
                             PictureUpdateDate = picture.UpdateDate
                         };

            return result;
        }

        public IEnumerable<DisplayPropertyViewModel> GetPhotoForProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from picture in entities.PropertyListingPictures
                         where picture.PropertyId == id
                         select new DisplayPropertyViewModel
                         {
                             PropertyListPictureId = picture.PropertyListingPictureId,                             
                             ListPictureImgUrl = picture.ImgUrl,
                             ListPictureImgUrlThumb = picture.ImgUrlThumb,
                             ListPictureImgUrlMed = picture.ImgUrlMed,
                             PropertyId = picture.PropertyId,                            
                         };

            return result;
        }


        IEnumerable<ListingViewModel> IPropertyRepository.GetPicturesForProperties()
        {
            //throw new NotImplementedException();
            var result = from picture in entities.PropertyListingPictures
                         select new ListingViewModel
                         {
                             PropertyListPictureId = picture.PropertyListingPictureId,
                             ListedPropertyPictureCaption = picture.Caption,
                             ListPictureImgUrl = picture.ImgUrl,
                             ListPictureImgUrlThumb = picture.ImgUrlThumb,
                             ListPictureImgUrlMed = picture.ImgUrlMed,
                             PropertyId = picture.PropertyId,
                             PictureCreateDate = picture.CreationDate,
                             PictureUpdateDate = picture.UpdateDate
                         };

            return result;
        }


        


        public void AddPropertyListPicture(PropertyListingPicture picture)
        {
            //throw new NotImplementedException();
            entities.PropertyListingPictures.AddObject(picture);
        }


        public void AddEvent(ManagementEvent eventlog)
        {
            //throw new NotImplementedException();
            entities.ManagementEvents.AddObject(eventlog);
        }

        public void AddPropertyAddress(PropertyAddress address)
        {
            //throw new NotImplementedException();
            entities.PropertyAddresses.AddObject(address);
        }

        public void AddPropertyFacility(PropertyFacility facility)
        {
            //throw new NotImplementedException();
            entities.PropertyFacilities.AddObject(facility);
        }

        public void AddPropertyFeature(PropertyFeature feature)
        {
            //throw new NotImplementedException();
            entities.PropertyFeatures.AddObject(feature);
        }

        public void AddProperty(Core.Property.Property property)
        {
            //throw new NotImplementedException();
            entities.Properties.AddObject(property);
        }


        public void AddPropertyPicture(PropertyListingPicture picture)
        {
            //throw new NotImplementedException();
            entities.PropertyListingPictures.AddObject(picture);
        }

        public void AddPropertyListing(PropertyList propertyList)
        {
            //throw new NotImplementedException();
            entities.PropertyLists.AddObject(propertyList);
        }
        
        public void AddCalendarEvent(Calendar calEvent)
        {
            //throw new NotImplementedException();
            entities.Calendars.AddObject(calEvent);
        }

        public void DeleteCalendarEvent(Calendar calendarEvent)
        {
            //throw new NotImplementedException();
            entities.Calendars.DeleteObject(calendarEvent);
        }

        public void AddMessage(Message message)
        {
            //throw new NotImplementedException();
            entities.Messages.AddObject(message);
        }

        public void DeleteMessage(Message message)
        {
            //throw new NotImplementedException();
            entities.Messages.DeleteObject(message);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }


        public void DeletePicture(PropertyListingPicture picture)
        {
            //throw new NotImplementedException();
            entities.PropertyListingPictures.DeleteObject(picture);
        }
















































    }
}
