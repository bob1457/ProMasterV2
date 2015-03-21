using System.Collections.Generic;
using ProMaster.Infrastructure.UserProfile;
using ProMaster.Infrastructure.UserProfile.ViewModels;
using ProMaster.Core.ViewModels;

namespace ProMaster.DAL.UserProfile
{
    public interface IUserProfileRepository
    {
        MyProfileViewModel ListProfileByUserName(string userName);

        MyProfileViewModel ListTenantProfileByUserName(string userName);
        MyProfileViewModel ListLandlordProfileByUserName(string userName);
        MyProfileViewModel ListPropertyManagerProfileByUserName(string userName);
        MyProfileViewModel ListSuperAdminProfileByUserName(string userName);

        Infrastructure.UserProfile.Tenant GetTenantForEdit(string userName);
        Profile GetAdminForEdit(string userName);
        PropertyOwner GetLandlordForEdit(string userName);
        PropertyManager GetPropertyManagerForEdit(string userName);
        PropertyManager GetPropertyManagerById(int id);
        IEnumerable<PropertyManager> ListedAllPropertyManagers();
        IEnumerable<PropertyManager> ListedActivePropertyManagers();
        AdminViewModel GetManagerById(int id);
        IEnumerable<AdminViewModel> PropertyByPm(int id);

        PropertyManager GetPropertyManagerEmailByUserName(string userName);

        //bool GetUserStatus(string userName);
        User GetUserByName(string userName);


        webpages_Membership GetMember(int userId);


        
        
        IEnumerable<webpages_Roles> ListRoles();

        IEnumerable<webpages_Roles> GetAdminRole();

        int GetRoleIdByUserName(string userName);

        void AddProfile(Profile profile);

        void AddPropertyManager(PropertyManager manager);

        void DeleteManager(PropertyManager manager);
        
        void Save();
    }   
}
