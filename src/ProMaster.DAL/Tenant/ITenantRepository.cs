using System.Collections.Generic;
using ProMaster.Core.Tenant;
using ProMaster.Core.Tenant.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.Tenant
{
    public interface ITenantRepository
    {        
        IEnumerable<DisplayTenantViewModel> GetTenantDetailsById(int id);
        IEnumerable<DisplayTenantViewModel> GetActiveTenantDetailsById(int id);

        IEnumerable<Core.Tenant.Lease> LeaseById(int id);
        IEnumerable<Core.Tenant.Property> PropertyById(int id); //id=property id from lease table
        IEnumerable<Core.Tenant.Document> DocumentByTenantId(int id, int pid);
        Core.Tenant.Tenant GetTenantById(int id);
        Core.Tenant.Tenant GetEmailByTenantUserName(string userName);
        IEnumerable<ManageDisplayViewModel> GetTenantList();

        IEnumerable<ManageDisplayViewModel> GetActiveTenantList();
        IEnumerable<SiteSearchViewModel> ShowAllTenantSearchResult(string searchString);

        IEnumerable<Core.Tenant.Tenant> GetNewTenantsList();

        IEnumerable<ManageDisplayViewModel> GetTenantCandidates();

        IEnumerable<ManageDisplayViewModel> GetAllTenantByPm();

        IEnumerable<DisplayTenantViewModel> GetRentPaymentLogByTenant(int id);

        int GetNumberOfLatePayment(int id);
        IEnumerable<Core.Tenant.Tenant> GetTenantsByLeaseId(int id);

        IEnumerable<Core.Tenant.Property> GetListedPropertyByPmId(int id);

        IEnumerable<Core.Tenant.Property> GetAllPropertyByPmId(int id);

        IEnumerable<ScreeningCheckStatu> GetScreenStatus();        

        TenantScreenViewModel GetApplicationById(int id);

        TenancyApplication ApplicationById(int id);

        ScreenProcess GetScreeenProcessByAppId(int id);
        DocumentCheckList GetDocumentCheckListByAppId(int id);
        TenantScreening GetScreenStatus(int id, int tId);
        TenantScreening GetScreeningApp(int id);
        string GetScreenNotes(int id, int tId);

        int GetLeaseIdByTenant(int id); // id is tenant id

        IEnumerable<TenantScreenViewModel> GetCheckStatusByApplicantId(int id);

        IEnumerable<TenantScreenViewModel> GetCheckTypeBypplicantId(int id);

        IEnumerable<TenantScreenViewModel> ScreeningById(int id);

        IEnumerable<TenantScreenViewModel> GatActiveApplications();
        IEnumerable<TenantScreenViewModel> GatActiveApplications(int id);
        IEnumerable<TenantScreenViewModel> GatCompletedApplications();
        IEnumerable<TenantScreenViewModel> GatCompletedApplications(int id);
        TenantScreening ScreenProcessByAppId(int id, int pId);
        IEnumerable<TenantScreening> ScreenByAppId(int id);
        IEnumerable<TenantScreenViewModel> DocCheckListByAppId(int id);

        void AddTenant(Core.Tenant.Tenant tenant);        
        void AddTenantScreen(TenantScreening screen);
        void AddDocCheckList(DocumentCheckList list);

        void Save();
    }
}
