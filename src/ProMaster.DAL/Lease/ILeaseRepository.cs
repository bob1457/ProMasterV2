using System.Collections.Generic;
using ProMaster.Core.Lease;
using ProMaster.Core.ViewModels;
using ProMaster.Core.Lease.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.Lease
{
    public interface ILeaseRepository
    {
        IEnumerable<LeaseTerm> GetLeaseTerm();
        IEnumerable<Core.Lease.Tenant> GetLeaseTenant();
        IEnumerable<Core.Lease.Tenant> GetNewLeaseTenant(); //for adding tenants to the existing lease, but excluding existing tenants already in the lease
        IEnumerable<Core.Lease.Property> GetLeaseProperty();
        IEnumerable<Core.Lease.Property> GetAllProperty();
        IEnumerable<Core.Lease.Tenant> GetTenantbyId(int id);
        IEnumerable<ManageDisplayViewModel> GetLeaseInfo(int pmId);
        IEnumerable<ManageDisplayViewModel> GetLeaseList(int pmId);
        IEnumerable<ManageDisplayViewModel> GetCandidateLeaseList(int pmId);

        IEnumerable<DisplayLeaseViewModel> LeaseDetails(int id);
        //IEnumerable<DisplayLeaseViewModel> LeaseDetailsWithoutTenant(int id);
        IEnumerable<DisplayLeaseViewModel> GetTenantList(int id);

        IEnumerable<Core.Lease.Lease> GetLeaseForEdit(int id);
        Core.Lease.Lease GetLeaseById(int id);
        IEnumerable<EditLeaseViewModel> LeaseForEdit(int id);

        IEnumerable<SiteSearchViewModel> ShowAllLeaseResults(string searchString);

        RentDepositTransfer GetTransferByPayment(int id); //rent payment id
        //--start of text code

        IEnumerable<Core.Lease.Lease> GetLeaseByPropertyId(int id);


        //---end of test code

        IEnumerable<Core.Lease.Tenant> GetTenantByLeaseId(int id);
        IEnumerable<Core.Lease.Property> GetPropertyById(int id); //id is property id in the lease entity

        IEnumerable<Core.Lease.Document> DocumenetByLeaseId(int id, int pid);

        IEnumerable<DisplayLeaseViewModel> GetPaymentHistoryByTenant(int id);

        IEnumerable<RentPayment> GetLateRentPayment(int id); //id is tenant id

        RentPayment GetPaymentyById(int id);
        RentPaymentViewModel GetRentPaymentById(int id);
        RentPaymentViewModel GetRentPaymentTransferrdToBankById(int id);
        //RentDepositTransfer GetRentTransferByPaymentId(int id);

        void AddLease(Core.Lease.Lease lease);
        void AddTenant(Core.Lease.Tenant tenant);
        void AddPaymentHistory(RentPayment payment);
        void AddTransfer(RentDepositTransfer transfer);
        

        void Save();


    }
}
