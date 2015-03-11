using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;

using ProMaster.DAL.ClientPortal;
using ProMaster.Core.ClientPortal.ViewModels;
using ProMaster.Core.Contract;
using ProMaster.Core.Lease;
using ProMaster.Core.Tenant;
using ProMaster.Core.PropertyOwner;
using ProMaster.Core.Reporting;
using PropertyOwner = ProMaster.Core.PropertyOwner.PropertyOwner;


namespace ProMaster.Repository.ClientPortal
{
    public class ClientPortalRepository : IClientPortalRepository
    {
        //Database context
        //
        readonly ProMasterManagementContractDataEntities _contracts = new ProMasterManagementContractDataEntities();
        readonly ProMasterTenantDataEntities _tenants = new ProMasterTenantDataEntities();
        //ProMasterPropertyDataEntities prooperties = new ProMasterPropertyDataEntities();
        readonly ProMasterLeaseDataEntities _leases = new ProMasterLeaseDataEntities();
        readonly ProMasterPropertyOwnerDataEntities _owners = new ProMasterPropertyOwnerDataEntities();
        readonly ProMasterReportingDataModelEntities1 _reports = new ProMasterReportingDataModelEntities1();

        public int GetTenantId(string userName)
        {
            //throw new NotImplementedException();
            var firstOrDefault = _tenants.Tenants.FirstOrDefault(u => u.UserName == userName);
            return firstOrDefault != null ? firstOrDefault.TenantId : 0;
        }

        public int GetLandlordId(string userName)
        {
            //throw new NotImplementedException();
            PropertyOwner firstOrDefault = _owners.PropertyOwners.FirstOrDefault(u => u.UserName == userName);
            return firstOrDefault != null ? firstOrDefault.PropertyOwnerId : 0;
        }

        public DisplayClientPortalViewModel GetLandlordInfo(int id)
        {
            //throw new NotImplementedException();
            var result = from landlord in _owners.PropertyOwners
                         where landlord.PropertyOwnerId == id
                         select new DisplayClientPortalViewModel
                         {
                             ClientFirstName = landlord.FirstName,
                             ClientLastName = landlord.LastName,
                             ClientContactTel1 = landlord.ContactTelephone1,
                             ClientContactTel2 = landlord.ContactTelephone2,
                             ClientContactEmail = landlord.ContactEmail,
                             ClientAvatarImgUrl = landlord.UserAvartaImgUrl
                         };

            return result.FirstOrDefault();

        }

        public DisplayClientPortalViewModel GetTenantInfo(int id)
        {
            //throw new NotImplementedException();
            var result = from tenant in _tenants.Tenants
                         where tenant.TenantId == id
                         select new DisplayClientPortalViewModel
                         {
                             ClientFirstName = tenant.FirstName,
                             ClientLastName = tenant.LastName,
                             ClientContactTel1 = tenant.ContactTelephone1,
                             ClientContactTel2 = tenant.ContactTelephone2,
                             ClientContactEmail = tenant.ContactEmail,
                             ClientAvatarImgUrl = tenant.UserAvartaImgUrl
                         };

            return result.FirstOrDefault();
        }

        public DisplayClientPortalViewModel GetLeaseByTenant(int id)
        {
            //throw new NotImplementedException();
            var result = from lease in _leases.Leases                        
                         join tenant in _leases.Tenants on lease.LeaseId equals tenant.LeaseId
                         join property in _leases.Properties on lease.PropertyId equals property.PropertyId
                         join address in _leases.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join term in _leases.LeaseTerms on lease.LeaseTermId equals term.LeaseTermId
                         where tenant.TenantId == id
                         select new DisplayClientPortalViewModel
                         {
                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseDesc = lease.LeaseDesc,
                             LeaseSignDate = lease.LeaseSignDate,
                             LeaseStartDate = lease.LeaseStartDate,
                             LeaseEndDate = lease.LeaseEndDate,
                             LeaseTerm = term.LeaaseTerm,
                             RentAmount = lease.RentAmount,
                             RentDueOn = lease.RentDueOn,
                             Addendum = lease.Addendum,
                             IsActive = lease.IsActive,
                             DamageDepositAmount = lease.DamageDepositAmount,
                             PetDepositAmount = lease.PetDepositAmount,
                             RentFrequency = lease.RentFrequency,
                             

                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyBuildYear = property.PropertyBuildYear,

                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry
                             

                         };

            return result.FirstOrDefault();
        }

        public IEnumerable<DisplayClientPortalViewModel> GetRentPaymentHistory(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in _leases.RentPayments
                         join method in _leases.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         where payment.TenantId == id
                         select new DisplayClientPortalViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,                             
                             RentReciptMonth = payment.RentPaymentMonth,
                             RentAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentMoth = payment.RentPaymentMonth,
                             PaymentYear = payment.RentPaymentYear,
                             NumberOfLatePayment = payment.NumberOfLatePayment,
                             Notes = payment.Notes,
                             RentOnTime = payment.RentIsPaidOntime
                         };

            return result;

        }


        public IEnumerable<DisplayClientPortalViewModel> GetFeeRevenueHistory(int id)
        {
            //throw new NotImplementedException();
            var result = from fee in _contracts.ManagementFees
                         join type in _contracts.ManagementFeeTypes on fee.ManagementFeeTypeId equals type.ManagementFeeTypeId
                         join contract in _contracts.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         where fee.ManagementContractId == id
                         select new DisplayClientPortalViewModel
                         {
                             ContractId = contract.ManagementContractId,
                             FeeNotes = fee.Notes,
                             FeeMonth = fee.ManagementFeeMonth,
                             FeeYear = fee.ManagementFeeYear,
                             FeePaidAmount = fee.ManagementFeeAmount,
                             FeePaidDate = fee.ReceivedDate,
                             FeeType = type.ManagementFeeType1
                         };

            return result;
        }


        public IEnumerable<DisplayClientPortalViewModel> GetRentRevenueHistory(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in _leases.RentPayments
                         join method in _leases.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         join transfer in _leases.RentDepositTransfers on payment.RentPaymentId equals transfer.RentalPaymentId
                         //join bank in leases.Banks on transfer.BankId equals bank.BankId
                         join tenant in _leases.Tenants on payment.TenantId equals tenant.TenantId
                         join lease in _leases.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in _leases.Properties on lease.PropertyId equals property.PropertyId
                         where property.PropertyId == id && payment.IsDepositForOwner
                         select new DisplayClientPortalViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,                             
                             RentReciptMonth = payment.RentPaymentMonth,
                             RentAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentMoth = payment.RentPaymentMonth,
                             PaymentYear = payment.RentPaymentYear,
                             NumberOfLatePayment = payment.NumberOfLatePayment,
                             Notes = payment.Notes,
                             IsDepositForOwner = payment.IsDepositForOwner,
                             RentOnTime = payment.RentIsPaidOntime
                         };

            return result;
                         
        }


        public IEnumerable<DisplayClientPortalViewModel> GetRentPaymentHistoryByPeriod(int id, DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from payment in _leases.RentPayments
                         join method in _leases.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         where (payment.TenantId == id) && ((EntityFunctions.TruncateTime(payment.RentPaymentDate)) >= startDate) && ((EntityFunctions.TruncateTime(payment.RentPaymentDate)) <= endDate) && payment.IsDepositForOwner
                         select new DisplayClientPortalViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,
                             RentReciptMonth = payment.RentPaymentMonth,
                             RentAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentMoth = payment.RentPaymentMonth,
                             PaymentYear = payment.RentPaymentYear,
                             NumberOfLatePayment = payment.NumberOfLatePayment,
                             Notes = payment.Notes,
                             RentOnTime = payment.RentIsPaidOntime
                         };

            return result;
        }

        public IEnumerable<DisplayClientPortalViewModel> GetRentRevenueHistoryByperiod(int id, DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from rent in _leases.RentPayments
                         join method in _leases.RentPaymentMethods on rent.RentPaymentMethodId equals method.RentPaymentMethodId
                         join transfer in _leases.RentDepositTransfers on rent.RentPaymentId equals transfer.RentalPaymentId
                         //join bank in leases.Banks on transfer.BankId equals bank.BankId
                         join tenant in _leases.Tenants on rent.TenantId equals tenant.TenantId
                         join lease in _leases.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in _leases.Properties on lease.PropertyId equals property.PropertyId
                         where (property.PropertyId == id) && (EntityFunctions.TruncateTime((rent.RentPaymentDate)) >= startDate) && ((EntityFunctions.TruncateTime(rent.RentPaymentDate)) <= endDate) 
                         select new DisplayClientPortalViewModel
                         {
                             RentPaymentId = rent.RentPaymentId,

                             RentAmount = rent.RentPaidAmount,
                             PaymentDate = rent.RentPaymentDate,
                             PaymentMoth = rent.RentPaymentMonth,
                             PaymentYear = rent.RentPaymentYear,
                             IsDepositForOwner = rent.IsDepositForOwner,
                             RentPaymentMethod = method.RentPaymentMethod1,

                             Notes = rent.Notes,
                             RentOnTime = rent.RentIsPaidOntime
                         };

            return result;
        }

        public IEnumerable<DisplayClientPortalViewModel> GetFeeRevenueHistoryByPeriod(int id, DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from fee in _contracts.ManagementFees
                         join type in _contracts.ManagementFeeTypes on fee.ManagementFeeTypeId equals type.ManagementFeeTypeId
                         join contract in _contracts.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         where (fee.ManagementContractId == id) && ((EntityFunctions.TruncateTime(fee.ReceivedDate)) >= startDate) && ((EntityFunctions.TruncateTime(fee.ReceivedDate)) <= endDate)
                         select new DisplayClientPortalViewModel
                         {
                             ContractId = contract.ManagementContractId,
                             FeeNotes = fee.Notes,
                             FeeMonth = fee.ManagementFeeMonth,
                             FeeYear = fee.ManagementFeeYear,
                             FeePaidAmount = fee.ManagementFeeAmount,
                             FeePaidDate = fee.ReceivedDate,
                             FeeType = type.ManagementFeeType1
                         };

            return result;
        }

        public DisplayClientPortalViewModel GetContractByLandlord(int id)
        {
            throw new NotImplementedException();
            //var result = from contract in contracts
            //             join 
        }

        public IEnumerable<DisplayClientPortalViewModel> GetWorkOrdersByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from order in _reports.WorkOrders
                         //join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in _reports.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in _reports.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join status in _reports.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         where (order.PropertyId == id)
                         select new DisplayClientPortalViewModel
                         {
                             //PropertyName = property.PropertyName,
                             //PropertyId = property.PropertyId,
                             WorkOrderId = order.WorkOrderId,
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             WorkOrderTypeName = type.WorkOrderTypeName,
                             WorkOrderCategory = category.CategoryName,
                             PaidDate = order.PaymentDate,
                             PaymentMethod = order.PaymentMethod,
                             InvoiceAmount = order.InvoiceAmount,
                             InvoiceDate = order.InvoiceDate,
                             IsPaid = order.IsPaid,
                             WorkOrderStatus = status.WorkOrderStatus,
                             StartDate = order.StartDate,
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }

        public IEnumerable<DisplayClientPortalViewModel> GetWorkOrdersByPropertyByPeriod(int id, DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from order in _reports.WorkOrders
                         //join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in _reports.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in _reports.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join status in _reports.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         where (order.PropertyId == id) && ((EntityFunctions.TruncateTime(order.StartDate)) >= startDate) && ((EntityFunctions.TruncateTime(order.StartDate)) <= endDate)
                         select new DisplayClientPortalViewModel
                         {
                             //PropertyName = property.PropertyName, 
                             //PropertyId = property.PropertyId,
                             WorkOrderId = order.WorkOrderId,
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             WorkOrderTypeName = type.WorkOrderTypeName,
                             WorkOrderCategory = category.CategoryName,
                             PaidDate = order.PaymentDate,
                             PaymentMethod = order.PaymentMethod,
                             InvoiceAmount = order.InvoiceAmount,
                             InvoiceDate = order.InvoiceDate,
                             IsPaid = order.IsPaid,
                             WorkOrderStatus = status.WorkOrderStatus,
                             StartDate = order.StartDate,
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }
















        
    }
}
