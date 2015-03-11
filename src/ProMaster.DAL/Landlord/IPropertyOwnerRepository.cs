using System.Collections.Generic;
using System.Linq;
using ProMaster.Core.ViewModels;
using ProMaster.Core.PropertyOwner;
using ProMaster.Core.PropertyOwner.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;
using ProMaster.Core.Property.ViewModels;

namespace ProMaster.DAL.Landlord
{
    public interface IPropertyOwnerRepository
    {
        IEnumerable<ShowLandlordViewModel> ListAllLandlord(string pmName);
        IEnumerable<ManageDisplayViewModel> ListMyLandlords(string pmName);

        IEnumerable<CreatePropertyViewModel> GetMyLandlords(string pmName);

        IQueryable< ManageDisplayViewModel> GetLandlordDetails(int id); //id = landlord id
        IEnumerable<SiteSearchViewModel> ShowAllLandlordResult(string searchString);
        ShowLandlordViewModel GetLandlordOnlyById(int id);
        ShowLandlordViewModel GetLandlordById(int id);
        PropertyOwner GetOwnerById(int id);

        PropertyOwner GetOwenrEmailByUserName(string userName);
        IEnumerable<Core.PropertyOwner.Lease> GetLeaseByPropertyId(int id);
        IEnumerable<Core.PropertyOwner.Tenant> GetTenantByLeaseId(int id); //no need to have it
        IEnumerable<Core.PropertyOwner.Property> GetPropertytByOwnerId(int id);
        IEnumerable<Core.PropertyOwner.Document> GetDocumentByOwnerId(int id);
        IEnumerable<ManagementContract> GetContractByPropertyId(int id);
        IEnumerable<PropertyOwner> GetOwnerListByPm(string pm);

        void AddPropertyOwner(PropertyOwner owner);
        void Save();
    }
}
