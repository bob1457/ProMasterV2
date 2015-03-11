using System.Collections.Generic;
using ProMaster.Core.Property;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.Property
{
    public interface IPropertyRepository
    {
        IEnumerable<PropertyType> GetPropertyTypes();
        IEnumerable<RentalStatu> GetPropertyStatus();
        IEnumerable<PropertyOwner> GetLandlordsByPm(string pmName);
        int GetPmId(string userName);
        IEnumerable<ManageDisplayViewModel> GetMyProperty(int id);

        IEnumerable<ManageDisplayViewModel> GetPropertyListByPmId(int id);

        IEnumerable<DisplayPropertyViewModel> GetPhotoForProperty(int id);
        IEnumerable<DisplayPropertyViewModel> ShowAllProperty();

        IEnumerable<ListingViewModel> GetPicturesForProperty(int id);

        IEnumerable<ListingViewModel> GetPicturesForProperties();

        PropertyListingPicture GetPictureById(int id);

        IEnumerable<PropertyMarketingViewModel> ListedProperty(int sId, int pmId); //id = status id
        IEnumerable<PropertyMarketingViewModel> PreListedProperty(int id, int pmId); //id = status id
        IEnumerable<PropertyMarketingViewModel> RentedProperty(int id, int pmId); //id = status id

        ListingViewModel GetPreListedProperty(int id, int pmId);

        IEnumerable<ManageDisplayViewModel> ListPropertyForRent();
        IEnumerable<DisplayPropertyViewModel> PropertyDetails(int id); //property id

        IEnumerable<DisplayPropertyViewModel> GetPropertyDetails(int id);

        IEnumerable<PropertyAddress> GetAddressById(int id); //address id
        IEnumerable<PropertyFeature> GetFeatureById(int id); //feature id
        IEnumerable<PropertyFacility> GetFacilityById(int id); //facility idp
        IEnumerable<PropertyOwner> GetLandlordById(int id); //property owner id
        IEnumerable<PropertyList> GetListById(int id);

        DisplayPropertyViewModel GetPropertyById(int id);
        ListingViewModel GetListedPropertyById(int id);

        IEnumerable<SiteSearchViewModel> ShowPropertySearchResults(string searchString);

        PropertyMarketingViewModel GetPropertyForMarketing(int id);

        IEnumerable<Core.Property.Document> DocumenetByPropertyid(int id, int pid);

        IEnumerable<Core.Property.Document> GetDocumenetByPropertyid(int id);

        Core.Property.Property PropertyById(int id);
        PropertyAddress AddressById(int id);
        PropertyFeature FeaturesById(int id);
        PropertyFacility FacilityById(int id);

        //Core.Property.Lease LeaseById(int id);
        

        IEnumerable<ManageDisplayViewModel> GetAllApplicationsByPmId(int pmId);
        DisplayApplicationViewModel TenancyApplicationByDetails(int id);
        TenancyApplication GetApplicationById(int id);

        //IEnumerable<ManagementEvent> GetEventListById(int id); //ia = PropertyId
        DisplayPropertyViewModel GetEventByid(int id); //for display event details

        IEnumerable<DisplayPropertyViewModel> EventListByProperty(int id);

        IEnumerable<Calendar> GetEventListByPmId(int id);//, DateTime start, DateTime end);
        Calendar GetCalendarEventById(int id);

        IEnumerable<MessagingVieModel> GetListMessageByRecipient(string userName);
        IEnumerable<string> GetOnlineUserName(); //for autocomplete
        string GetUserName(string ln, string fn); //for adding message

        void AddApplicant(TenancyApplication applicant);

        IEnumerable<ListingViewModel> ShowListedProperty();

        void AddPropertyAddress(PropertyAddress address);
        void AddPropertyFacility(PropertyFacility facility);
        void AddPropertyFeature(PropertyFeature feature);
        void AddProperty(Core.Property.Property property);
        void AddPropertyListing(PropertyList propertyList);
        void AddCalendarEvent(Calendar calEvent);
        void DeleteCalendarEvent(Calendar calendarEvent);

        void AddPropertyListPicture(PropertyListingPicture picture);

        void AddEvent(ManagementEvent eventlog);

        void AddPropertyPicture(PropertyListingPicture picture);

        void AddMessage(Message message);
        void DeleteMessage(Message message);
        void DeletePicture(PropertyListingPicture picture);
        

        void Save();
    }
}
