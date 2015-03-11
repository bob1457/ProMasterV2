using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Landlord;
using ProMaster.Core.PropertyOwner;
using ProMaster.Core.PropertyOwner.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;
using ProMaster.Core.Property.ViewModels;

namespace ProMaster.Repository.Landlord
{
    public class PropertyOwnerRepository : IPropertyOwnerRepository
    {
        ProMasterPropertyOwnerDataEntities entities = new ProMasterPropertyOwnerDataEntities();

        public IEnumerable<ShowLandlordViewModel> ListAllLandlord(string pmName)
        {
            //throw new NotImplementedException();
            var results = from landlord in entities.PropertyOwners
                          where landlord.AddedBy == pmName
                          select new ShowLandlordViewModel
                          {
                              LandlordId = landlord.PropertyOwnerId,
                              LandlordFirstName = landlord.FirstName,
                              LandlordLastName = landlord.LastName,
                              LandlordContactEmail = landlord.ContactEmail,
                              LandlordContactTelephone1 = landlord.ContactTelephone1,
                              LandlordContactTelephone2 = landlord.ContactTelephone2,
                              LandlordCreationDate = landlord.CreationDate,
                              LandlordUpdateDate = landlord.UpdateDate
                          };                         

            return results;
        }


        public IEnumerable<ManageDisplayViewModel> ListMyLandlords(string pmName)
        {
            //throw new NotImplementedException();
            var result = from owners in entities.PropertyOwners
                         //join property in entities.Properties on owners.PropertyOwnerId equals property.PropertyOwnerId
                         where owners.AddedBy == pmName && owners.IsActive
                         select new ManageDisplayViewModel 
                         { 
                             LandlordId = owners.PropertyOwnerId,
                             FistName = owners.FirstName,
                             LastName = owners.LastName,
                             ContactEmail = owners.ContactEmail,
                             ContactTelephone1 = owners.ContactTelephone1,
                             ContactTelephone2 = owners.ContactTelephone2,
                             LandlordCreationDate = owners.CreationDate,
                             //PropertyName = property.PropertyName,
                             AvartaImgUrl = owners.UserAvartaImgUrl,
                             CreateDate = owners.CreationDate,
                             LandlordUpdateDate = owners.UpdateDate
                         };

            return result.OrderByDescending(d=>d.CreateDate);
        }

        
        public IEnumerable<PropertyOwner> GetOwnerListByPm(string pm)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.Where(l => l.AddedBy == pm);
        }


        public IEnumerable<CreatePropertyViewModel> GetMyLandlords(string pmName)
        {
            throw new NotImplementedException();
            //var result = from owners in entities.PropertyOwners
            //             //join property in entities.Properties on owners.PropertyOwnerId equals property.PropertyOwnerId
            //             where owners.AddedBy == pmName
            //             select new CreatePropertyViewModel
            //             {
            //                 LandlordId = owners.PropertyOwnerId,
            //                  = owners.FirstName,
            //                 LastName = owners.LastName
                             
            //             };

            //return result;
        }


        public IQueryable< ManageDisplayViewModel> GetLandlordDetails(int id)//(string pmName) Here id is landlord's id
        {
            //throw new NotImplementedException();
            var result = from landlord in entities.PropertyOwners
                         where landlord.PropertyOwnerId == id
                         select new ManageDisplayViewModel { 
                             FistName = landlord.FirstName,
                             LastName = landlord.LastName,
                             ContactEmail = landlord.ContactEmail,
                             ContactTelephone1 = landlord.ContactTelephone1,
                             ContactTelephone2 = landlord.ContactTelephone2,
                             AvartaImgUrl = landlord.UserAvartaImgUrl,
                             OnLineAccessEnabled = landlord.OnlineAccessEnbaled,
                             LandlordCreationDate = landlord.CreationDate,
                             LandlordUpdateDate = landlord.UpdateDate
                         };
            return result;
        }

        public ShowLandlordViewModel GetLandlordById(int id)
        {
            //throw new NotImplementedException();
            var result = from landlord in entities.PropertyOwners
                         join property in entities.Properties on landlord.PropertyOwnerId equals property.PropertyOwnerId                        
                         where (landlord.PropertyOwnerId == id)
                         select new ShowLandlordViewModel 
                         {
                             LandlordId = landlord.PropertyOwnerId,
                             UserName = landlord.UserName,
                             LandlordFirstName = landlord.FirstName,
                             LandlordLastName = landlord.LastName,
                             LandlordContactEmail = landlord.ContactEmail,
                             LandlordContactTelephone1 = landlord.ContactTelephone1,
                             LandlordContactTelephone2 = landlord.ContactTelephone2,
                             LandlordAvartaImgUrl = landlord.UserAvartaImgUrl,
                             LandlordCreationDate = landlord.CreationDate,
                             LandlordUpdateDate = landlord.UpdateDate,
                             OnLineAccessEnabled = landlord.OnlineAccessEnbaled
                         
                         };

            return result.FirstOrDefault();
        }

        public ShowLandlordViewModel GetLandlordOnlyById(int id)
        {
            //throw new NotImplementedException();
            var result = from landlord in entities.PropertyOwners
                         //join property in entities.Properties on landlord.PropertyOwnerId equals property.PropertyOwnerId
                         where (landlord.PropertyOwnerId == id)
                         select new ShowLandlordViewModel
                         {
                             LandlordId = landlord.PropertyOwnerId,
                             UserName = landlord.UserName,
                             LandlordFirstName = landlord.FirstName,
                             LandlordLastName = landlord.LastName,
                             LandlordContactEmail = landlord.ContactEmail,
                             LandlordContactTelephone1 = landlord.ContactTelephone1,
                             LandlordContactTelephone2 = landlord.ContactTelephone2,
                             LandlordAvartaImgUrl = landlord.UserAvartaImgUrl,
                             LandlordCreationDate = landlord.CreationDate,
                             LandlordUpdateDate = landlord.UpdateDate

                         };

            return result.FirstOrDefault();
        }

        public PropertyOwner GetOwnerById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.Where(o => o.PropertyOwnerId == id).FirstOrDefault();
        }

        public PropertyOwner GetOwenrEmailByUserName(string userName)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.Where(u => u.UserName == userName).FirstOrDefault();
        }

        IEnumerable<ManagementContract> IPropertyOwnerRepository.GetContractByPropertyId(int id)
        {
            //throw new NotImplementedException()
            return entities.ManagementContracts.Where(c=>c.PropertyId == id);
        }


        public IEnumerable<Core.PropertyOwner.Property> GetPropertytByOwnerId(int id)
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(o=>o.PropertyOwnerId == id);
        }



        public IEnumerable<Core.PropertyOwner.Lease> GetLeaseByPropertyId(int id)
        {
            //throw new NotImplementedException();
            return entities.Leases.Where(p => p.PropertyId == id);

        }


        public IEnumerable<Core.PropertyOwner.Document> GetDocumentByOwnerId(int id)
        {
            //throw new NotImplementedException();
            return entities.Documents.Where(p => p.DocumentPrincipalId == id);
        }


        public IEnumerable<Core.PropertyOwner.Tenant> GetTenantByLeaseId(int id)
        {
            //throw new NotImplementedException();
            return entities.Tenants.Where(l => l.LeaseId == id);
        }

        public IEnumerable<SiteSearchViewModel> ShowAllLandlordResult(string searchString) 
        {
            //throw new NotImplementedException();
            var result = from landlord in entities.PropertyOwners
                         where landlord.FirstName.ToUpper().Contains(searchString.ToUpper()) || landlord.LastName.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel
                         {
                             LandlordId = landlord.PropertyOwnerId,
                             LandlordFirstName = landlord.FirstName,
                             LandlordLastName = landlord.LastName,
                             LandlordContactEmail = landlord.ContactEmail,
                             LandlordContactTelephone = landlord.ContactTelephone1
                             
                         };

            return result;
        }

        public void AddPropertyOwner(PropertyOwner owner)
        {
            //throw new NotImplementedException();
            entities.PropertyOwners.AddObject(owner);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }










    }
}
