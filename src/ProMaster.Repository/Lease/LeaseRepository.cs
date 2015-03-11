using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Lease;
using ProMaster.Core.Lease;
using ProMaster.Core.ViewModels;
using ProMaster.Core.Lease.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.Repository.Lease
{
    public class LeaseRepository : ILeaseRepository
    {
        ProMasterLeaseDataEntities entities = new ProMasterLeaseDataEntities();

        public IEnumerable<LeaseTerm> GetLeaseTerm()
        {
            //throw new NotImplementedException();
            return entities.LeaseTerms;
        }


        public IEnumerable<Core.Lease.Tenant> GetLeaseTenant()
        {
            //throw new NotImplementedException();
            return entities.Tenants.Where(a=>a.IsActive == false); // && a.leaseId == 0); For newly added tenants, IsActive = false, and leaseId = 0
        }

        public IEnumerable<Core.Lease.Tenant> GetNewLeaseTenant()
        {
            //throw new NotImplementedException();
            return entities.Tenants.Where(a => a.IsActive == false);
        }


        public IEnumerable<EditLeaseViewModel> LeaseForEdit(int id)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         join term in entities.LeaseTerms on lease.LeaseTermId equals term.LeaseTermId

                         //join tnt in entities.Tenants on lease.LeaseId equals tnt.LeaseId
                         where lease.LeaseId == id
                         select new EditLeaseViewModel
                         {
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseDesc = lease.LeaseDesc,
                             LeaseStartDate = lease.LeaseStartDate,
                             LeaseEndDate = lease.LeaseEndDate,

                             LeaseUpdateDate = lease.UpdateDate,
                             UpdateDate = lease.UpdateDate,

                             RentAmount = lease.RentAmount,
                             RentDueOn = lease.RentDueOn,
                             RentFrequency = lease.RentFrequency,
                             LeaseTerm = term.LeaaseTerm,
                             LeaseTermId = lease.LeaseTermId,
                             DamageDepositAmount = lease.DamageDepositAmount,
                             PetDepositAmount = lease.PetDepositAmount,
                             Notes = lease.Notes,
                             PropertyId = lease.PropertyId,
                             IsActive = lease.IsActive


                             //TenantId = tnt.TenantId,
                             //FirstName = tnt.FirstName,
                             //LastName = tnt.LastName,
                             //ContactEmail = tnt.ContactEmail,
                             //ContactTelephone1 = tnt.ContactTelephone1,
                             //AvartaImgUrl = tnt.UserAvartaImgUrl
                         };

            return result;
        }

        public IEnumerable<Core.Lease.Lease> GetLeaseForEdit(int id)        {
            
            return entities.Leases.Where(l => l.LeaseId == id);
        }

        public IEnumerable<Core.Lease.Property> GetLeaseProperty()
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(s => s.RentalStatusId == 2);
        }


        public IEnumerable<Core.Lease.Property> GetAllProperty()
        {
            //throw new NotImplementedException();
            return entities.Properties;
        }

        public IEnumerable<Core.Lease.Tenant> GetTenantbyId(int id)
        {
            //throw new NotImplementedException();
            return entities.Tenants.Where(t => t.TenantId == id);
        }

        public RentDepositTransfer GetTransferByPayment(int id)
        {
            //throw new NotImplementedException();
            return entities.RentDepositTransfers.Where(p => p.RentalPaymentId == id).FirstOrDefault();
        }


        public IEnumerable<ManageDisplayViewModel> GetLeaseInfo(int pmId)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         where lease.IsActive//property.PropertyManagerId == pmId && 
                         select new ManageDisplayViewModel
                         { 
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseSignDate = lease.LeaseSignDate,
                             RentAmount = lease.RentAmount,
                             LeaseStartDate = lease.LeaseStartDate,
                             SignDate = lease.LeaseSignDate,
                             CreateDate = lease.CreationDate,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,

                             TenantId = tenant.TenantId,
                             TenantFirstName = tenant.FirstName,
                             TenantLastName = tenant.LastName,
                             
                             

                             LeaseAddedDate = lease.CreationDate,
                             LeaseUpdateDate = lease.UpdateDate
                             
                         };

            return result.OrderByDescending(d=>d.CreateDate);
        }


        public IEnumerable<ManageDisplayViewModel> GetLeaseList(int pmId)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         where property.PropertyManagerId == pmId
                         select new ManageDisplayViewModel 
                         {
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseSignDate = lease.LeaseSignDate,
                             RentAmount = lease.RentAmount,
                             StartDate = lease.LeaseStartDate,
                             SignDate = lease.LeaseSignDate,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,

                             TenantId = tenant.TenantId,
                             TenantFirstName = tenant.FirstName,
                             TenantLastName = tenant.LastName,



                             LeaseAddedDate = lease.CreationDate,
                             LeaseUpdateDate = lease.UpdateDate
                         };

            return result;
        }


        public IEnumerable<ManageDisplayViewModel> GetCandidateLeaseList(int pmId)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         //join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         where property.PropertyManagerId == pmId && lease.IsActive == false
                         select new ManageDisplayViewModel
                         {
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseSignDate = lease.LeaseSignDate,
                             RentAmount = lease.RentAmount,
                             StartDate = lease.LeaseStartDate,
                             SignDate = lease.LeaseSignDate,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,

                             //TenantId = tenant.TenantId,
                             //TenantFirstName = tenant.FirstName,
                             //TenantLastName = tenant.LastName,



                             LeaseAddedDate = lease.CreationDate,
                             LeaseUpdateDate = lease.UpdateDate
                         };

            return result;
        }


        public IEnumerable<DisplayLeaseViewModel> LeaseDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         join term in entities.LeaseTerms on lease.LeaseTermId equals term.LeaseTermId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         //join tnt in entities.Tenants on lease.LeaseId equals tnt.LeaseId
                         where lease.LeaseId == id
                         select new DisplayLeaseViewModel 
                         { 
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseDesc = lease.LeaseDesc,
                             LeaseStartDate = lease.LeaseStartDate,
                             LeaseEndDate = lease.LeaseEndDate,
                             LeaseSignDate = lease.LeaseSignDate,
                             LeaseUpdateDate = lease.UpdateDate,
                             LeaseAddedDate = lease.CreationDate,
                             RentAmount = lease.RentAmount,
                             RentDueOn = lease.RentDueOn,
                             RentFrequency = lease.RentFrequency,
                             LeaseTerm = term.LeaaseTerm,
                             LeaseTermId = lease.LeaseTermId,
                             DamageDepositAmount = lease.DamageDepositAmount,
                             PetDepositAmount = lease.PetDepositAmount,
                             Notes = lease.Notes,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyBuildYear = property.PropertyBuildYear,

                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressCountry = address.PropertyCountry,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode
                             
                             //TenantId = tnt.TenantId,
                             //FirstName = tnt.FirstName,
                             //LastName = tnt.LastName,
                             //ContactEmail = tnt.ContactEmail,
                             //ContactTelephone1 = tnt.ContactTelephone1,
                             //AvartaImgUrl = tnt.UserAvartaImgUrl
                         };

            return result;
        }



        //public IEnumerable<DisplayLeaseViewModel> LeaseDetailsWithoutTenant(int id) //to be deleted.
        //{
        //    throw new NotImplementedException();
            
        //}

        public IEnumerable<DisplayLeaseViewModel> GetTenantList(int id)
        {
            throw new NotImplementedException();

        }


        public IEnumerable<RentPayment> GetLateRentPayment(int id)
        {
            //throw new NotImplementedException();
            return entities.RentPayments.Where(t => t.TenantId == id && t.RentIsPaidOntime == false);
        }

        public IEnumerable<Core.Lease.Tenant> GetTenantByLeaseId(int id)
        {
            //throw new NotImplementedException();
            return entities.Tenants.Where(l => l.LeaseId == id);
        }

        public IEnumerable<Core.Lease.Property> GetPropertyById(int id)
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(p => p.PropertyId == id);
        }

        public IEnumerable<Core.Lease.Document> DocumenetByLeaseId(int id, int pid)
        {
            //throw new NotImplementedException();
            return entities.Documents.Where(d=>d.DocumentPrincipalId == id && d.DocumentPrincipalTypeId ==pid);
        }
        
        public Core.Lease.Lease GetLeaseById(int id)
        {
            //throw new NotImplementedException();
            return entities.Leases.Where(l => l.LeaseId == id).FirstOrDefault();

        }

        public IEnumerable<Core.Lease.Lease> GetLeaseByPropertyId(int id)
        {
            //throw new NotImplementedException();
            return entities.Leases.Where(l => l.PropertyId == id && l.IsActive);
        }


        public IEnumerable<DisplayLeaseViewModel> GetPaymentHistoryByTenant(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in entities.RentPayments
                         join method in entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         where payment.TenantId == id
                         select new DisplayLeaseViewModel
                         { 
                             RentPaymentId = payment.RentPaymentId,
                             TenantId = payment.TenantId,
                             RentReciptMonth = payment.RentPaymentMonth,
                             RentAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentMoth = payment.RentPaymentMonth,
                             PaymentYear = payment.RentPaymentYear,
                             Notes = payment.Notes,
                             RentOnTime = payment.RentIsPaidOntime
                         };

            return result;
        }

        public RentPayment GetPaymentyById(int id)
        {
            //throw new NotImplementedException();
            return entities.RentPayments.Where(p => p.RentPaymentId == id).FirstOrDefault();
        }


        public RentPaymentViewModel GetRentPaymentById(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in entities.RentPayments
                         join tenant in entities.Tenants on payment.TenantId equals tenant.TenantId
                         join method in entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         //join transfer in entities.RentDepositTransfers on payment.RentPaymentId equals transfer.RentalPaymentId
                         //join bank   in entities.Banks on transfer.BankId equals bank.BankId
                         where payment.RentPaymentId == id
                         select new RentPaymentViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,
                             PaymenAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentYear = payment.RentPaymentYear,
                             PaymentMonth = payment.RentPaymentMonth,
                             RentPaymentMethod = method.RentPaymentMethod1,
                             IsRentTransferred = payment.IsDepositForOwner,
                             //TransferDate = transfer.RentTransferDate,
                             //Bank = bank.BankName,

                             TenantId = tenant.TenantId,
                             FirstName = tenant.FirstName,
                             LastName = tenant.LastName,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1

                             

                         };

            return result.FirstOrDefault();
        }


        public RentPaymentViewModel GetRentPaymentTransferrdToBankById(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in entities.RentPayments
                         join tenant in entities.Tenants on payment.TenantId equals tenant.TenantId
                         join method in entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         join transfer in entities.RentDepositTransfers on payment.RentPaymentId equals transfer.RentalPaymentId
                         join bank in entities.Banks on transfer.BankId equals bank.BankId
                         where payment.RentPaymentId == id
                         select new RentPaymentViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,
                             PaymenAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentYear = payment.RentPaymentYear,
                             PaymentMonth = payment.RentPaymentMonth,
                             RentPaymentMethod = method.RentPaymentMethod1,
                             IsRentTransferred = payment.IsDepositForOwner,
                             TransferDate = transfer.RentTransferDate,
                             Bank = bank.BankName,

                             TenantId = tenant.TenantId,
                             FirstName = tenant.FirstName,
                             LastName = tenant.LastName,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1



                         };

            return result.FirstOrDefault();
        }
        //public RentDepositTransfer GetRentTransferByPaymentId(int id)
        //{
        //    //throw new NotImplementedException();
        //    //var result = from transfer in entities.RentDepositTransfers
        //    //             select new RentPaymentViewModel
        //    //             {

        //    //             };
        //    //return result.FirstOrDefault();
        //    return entities.RentDepositTransfers.Where(p => p.RentalPaymentId == id).FirstOrDefault();
        //}


        public IEnumerable<SiteSearchViewModel> ShowAllLeaseResults(string searchString)
        {
            //throw new NotImplementedException();
            var result = from lease in entities.Leases
                         where lease.LeaseTitle.ToUpper().Contains(searchString.ToUpper()) || lease.LeaseDesc.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel
                         {
                             LeaseId = lease.LeaseId,
                             LeaseAddDate = lease.CreationDate,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseDesc = lease.LeaseDesc,                             
                             StartDate = lease.LeaseStartDate,
                             EndDate = lease.LeaseEndDate
                         };

            return result;
        }


        public void AddLease(Core.Lease.Lease lease)
        {
            //throw new NotImplementedException();
            entities.Leases.AddObject(lease);
        }
        
        public void AddTenant(Core.Lease.Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public void AddPaymentHistory(RentPayment payment)
        {
            //throw new NotImplementedException();
            entities.RentPayments.AddObject(payment);
        }

        public void AddTransfer(RentDepositTransfer transfer)
        {
            //throw new NotImplementedException();
            entities.RentDepositTransfers.AddObject(transfer);
        }


        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }



























    }
}
