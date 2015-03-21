using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.UserProfile;
using ProMaster.Infrastructure.UserProfile;
using ProMaster.Infrastructure.UserProfile.ViewModels;
using ProMaster.Core.ViewModels;

namespace ProMaster.Repository.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository
    {
        ProMasterUserProfileDataEntities entities = new ProMasterUserProfileDataEntities();

        //public Profile ListProfileByUserName(string UserName)
        //{
        //    throw new NotImplementedException();
            
        //}
        MyProfileViewModel IUserProfileRepository.ListProfileByUserName(string userName)
        {
            //throw new NotImplementedException();
            var result = from profile in entities.Profiles
                         join roles in entities.webpages_Roles on profile.RoleId equals roles.RoleId
                         where profile.UserName == userName
                         select new MyProfileViewModel 
                         { 
                             UserName = profile.UserName,
                             FirstName = profile.FirstName,
                             LastName = profile.LastName,
                             ContactEmail = profile.ContactEmail,
                             ContactTelephone1 = profile.ContactTelephone1,
                             ContactTelephone2 = profile.ContactTelephone2,
                             AvartaImgUrl = profile.UserAvartaImgUrl,
                             CreationDate = profile.CreationDate,
                             UpdateDate = profile.UpdateDate

                         };
            return result.FirstOrDefault();
                
        }

        public IEnumerable<webpages_Roles> ListRoles()
        {
            //throw new NotImplementedException();
            //return entities.webpages_Roles;

            var roles = from role in entities.webpages_Roles
                        where role.RoleId != 1 && role.RoleId != 2
                        //orderby  role.RoleId ascending
                        select role;
            return roles;
        }

        public IEnumerable<webpages_Roles> GetAdminRole()
        {
            //throw new NotImplementedException();

            var roles = from role in entities.webpages_Roles
                        where role.RoleId == 5
                        //orderby  role.RoleId ascending
                        select role;
            return roles;
        }

        //public bool GetUserStatus(string userName)
        //{
        //    //throw new NotImplementedException();
        //    User firstOrDefault;
        //    firstOrDefault = entities.Users.FirstOrDefault(u => u.UserName == userName);
        //    return firstOrDefault != null; // && firstOrDefault.Disabled;
        //}


        public webpages_Membership GetMember(int userId)
        {
            //throw new NotImplementedException();
            return entities.webpages_Membership.Where(u => u.UserId == userId).FirstOrDefault();
        }


        public User GetUserByName(string userName)
        {
            //throw new NotImplementedException();
            return entities.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public int GetRoleIdByUserName(string userName)
        {
            //throw new NotImplementedException();
            var firstOrDefault = entities.Profiles.FirstOrDefault(u => u.UserName == userName);
            if (firstOrDefault != null)
                return firstOrDefault.RoleId;
            return 0;
        }

        public MyProfileViewModel ListLandlordProfileByUserName(string userName)
        {
            //throw new NotImplementedException();
            var result = from landlord in entities.PropertyOwners
                         where landlord.UserName == userName
                         select new MyProfileViewModel
                         {
                             UserName = landlord.UserName,
                             FirstName = landlord.FirstName,
                             LastName = landlord.LastName,
                             ContactEmail = landlord.ContactEmail,
                             ContactTelephone1 = landlord.ContactTelephone1,
                             ContactTelephone2 = landlord.ContactTelephone2,
                             CreationDate = landlord.CreationDate,
                             UpdateDate = landlord.UpdateDate,
                             AvartaImgUrl = landlord.UserAvartaImgUrl
                         };
            return result.FirstOrDefault();

        }

        public MyProfileViewModel ListTenantProfileByUserName(string userName)
        {
            //throw new NotImplementedException();
            var result = from tenant in entities.Tenants
                         where tenant.UserName == userName
                         select new MyProfileViewModel 
                         { 
                             UserName = tenant.UserName,
                             FirstName = tenant.FirstName,
                             LastName = tenant .LastName,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1,
                             ContactTelephone2 = tenant.ContactTelephone2,
                             CreationDate = tenant.CreateDate,
                             UpdateDate = tenant.UpdateDate,
                             AvartaImgUrl = tenant.UserAvartaImgUrl
                         };
            return result.FirstOrDefault();
        }

        public MyProfileViewModel ListPropertyManagerProfileByUserName(string userName)
        {
            //throw new NotImplementedException();
            var result = from manager in entities.PropertyManagers
                         where manager.UserName == userName
                         select new MyProfileViewModel
                         {
                             UserName = manager.UserName,
                             FirstName = manager.FirstName,
                             LastName = manager.LastName,
                             ContactEmail = manager.ContactEmail,
                             ContactTelephone1 = manager.ContactTelephone1,
                             ContactTelephone2 = manager.ContactTelephone2,
                             CreationDate = manager.CreationDate,
                             UpdateDate = manager.UpdateDate,
                             AvartaImgUrl = manager.UserAvartaImgUrl
                         };
            return result.FirstOrDefault();
        }

        public MyProfileViewModel ListSuperAdminProfileByUserName(string userName)
        {
            //throw new NotImplementedException();
            var result = from admin in entities.Profiles
                         where admin.UserName == userName
                         select new MyProfileViewModel
                         {
                             UserName = admin.UserName,
                             FirstName = admin.FirstName,
                             LastName = admin.LastName,
                             ContactEmail = admin.ContactEmail,
                             ContactTelephone1 = admin.ContactTelephone1,
                             ContactTelephone2 = admin.ContactTelephone2,
                             CreationDate = admin.CreationDate,
                             UpdateDate = admin.UpdateDate,
                             AvartaImgUrl = admin.UserAvartaImgUrl
                         };
            return result.FirstOrDefault();
        }


        public PropertyManager GetPropertyManagerById(int id)
        {
            //throw new NotImplementedException();
            return entities.PropertyManagers.Where(p => p.PropertyManagerId == id).FirstOrDefault();
        }

        
        PropertyManager IUserProfileRepository.GetPropertyManagerEmailByUserName(string userName)
        {
            //throw new NotImplementedException();
            return entities.PropertyManagers.Where(u => u.UserName == userName).FirstOrDefault();
        }


        public Infrastructure.UserProfile.Tenant GetTenantForEdit(string userName)
        {
            //throw new NotImplementedException();
            if (entities != null) return entities.Tenants.FirstOrDefault(u => u.UserName == userName);
            return null;
        }

        public Profile GetAdminForEdit(string userName)
        {
            //throw new NotImplementedException();
            return entities.Profiles.FirstOrDefault(u => u.UserName == userName);
        }

        public PropertyOwner GetLandlordForEdit(string userName)
        {
            //throw new NotImplementedException();
            return entities.PropertyOwners.FirstOrDefault(u => u.UserName == userName);
        }

        public PropertyManager GetPropertyManagerForEdit(string userName)
        {
            //throw new NotImplementedException();
            return entities.PropertyManagers.FirstOrDefault(u => u.UserName == userName);
        }

        public AdminViewModel GetManagerById(int id)
        {
            //throw new NotImplementedException();
            var result = from manager in entities.PropertyManagers
                         //join property in entities.Properties
                         where manager.PropertyManagerId == id
                         select new AdminViewModel 
                         {
                             PropertyManagerId = manager.PropertyManagerId,
                             PMFirstName = manager.FirstName,
                             PMLastName = manager.LastName,
                             PMEmail = manager.ContactEmail,
                             PMTelephone1 = manager.ContactTelephone1,
                             PMTelephone2 = manager.ContactTelephone2,
                             PMAvatarImgUrl = manager.UserAvartaImgUrl
                         };

            return result.FirstOrDefault();
        }

        public IEnumerable<AdminViewModel> PropertyByPm(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where property.PropertyManagerId == id
                         select new AdminViewModel { 
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyType = type.PropertyType1,
                             PropertyId = property.PropertyId,
                             CreationDate = property.CreatedDate

                         };

            return result;
        }

        public IEnumerable<PropertyManager> ListedAllPropertyManagers()
        {
            //throw new NotImplementedException();
            return entities.PropertyManagers;
        }


        public IEnumerable<PropertyManager> ListedActivePropertyManagers()
        {
            //throw new NotImplementedException();
            return entities.PropertyManagers.Where(a => a.IsActive);
        }

        public void DeleteManager(PropertyManager manager)
        {
            //throw new NotImplementedException();
            entities.PropertyManagers.Remove(manager);
        }


        public void AddProfile(Profile profile)
        {
            //throw new NotImplementedException();
            entities.Profiles.Add(profile);
        }

        public void AddPropertyManager(PropertyManager manager)
        {
            //throw new NotImplementedException();
            entities.PropertyManagers.Add(manager);
        }

        public void SaveProfile()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }

        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }




























    }

    /* Moved to DAL
     * //
     
    public static class ExtensionClass
    {
        public static IEnumerable<SelectListItem> ToRoleList(this IEnumerable<webpages_Roles> Roles, int id)
        { 
            return 
                Roles.OrderBy(role => role.RoleName)
                .Select(role => new SelectListItem
                {
                    Selected = (role.RoleId == id),
                    Text = role.RoleName,
                    Value = role.RoleId.ToString()                
                });
                
        }
    }
     
     * 
     * */
}
