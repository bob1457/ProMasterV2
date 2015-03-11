using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;

//using ProMaster.DAL.Accounting;
//using ProMaster.Core.Accounting;
//using ProMaster.Core.Accounting.ViewModels;
using ProMaster.Core.Reporting;
using ProMaster.Core.Reporting.ViewModels;
using ProMaster.DAL.Reporting;

namespace ProMaster.Repository.Reporting
{
    public class ReportingRepository: IReportingRepository
    {
        ProMasterReportingDataModelEntities1 entities = new ProMasterReportingDataModelEntities1();

        #region Accounting

        public IEnumerable<DisplayDashboardViewModel> MyProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         where property.PropertyManagerId == id
                         select new DisplayDashboardViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PType = type.PropertyType1,
                             RentalStatus = status.Status,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate
                         };
            return result;

        }


        public IEnumerable<DisplayDashboardViewModel> RentedProperty(int id, int statusId)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join status in entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         where property.PropertyManagerId == id && property.RentalStatusId == statusId
                         select new DisplayDashboardViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PType = type.PropertyType1,
                             RentalStatus = status.Status,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate
                         };
            return result;
        }


        public AccountingCostViewModel GetRepairCostByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join property in entities.Properties on order.PropertyId equals property.PropertyId
                         where property.PropertyId == id
                         select new AccountingCostViewModel
                         {
                             PropertyName = property.PropertyName,
                             PropertyId = property.PropertyId,
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             InvoiceAmount = order.InvoiceAmount,
                             PaymentAmount = order.PaymentAmount

                         };

            return result.FirstOrDefault();
        }



        public IEnumerable<AccountingCostViewModel> GetWorkOrdersByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         //join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         where (order.PropertyId == id)
                         select new AccountingCostViewModel
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
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }

        public IEnumerable<AccountingCostViewModel> GetWorkOrdersByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         //join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         where (order.PropertyId == id) && (EntityFunctions.TruncateTime(order.RecordCreationDate) >= sPeriod) && (EntityFunctions.TruncateTime(order.RecordCreationDate) <= ePeriod)
                         select new AccountingCostViewModel
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
                             CreationDate = order.RecordCreationDate,
                             IsPaid = order.IsPaid,
                             WorkOrderStatus = status.WorkOrderStatus,
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetWorkOrdersForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         where (EntityFunctions.TruncateTime(order.RecordCreationDate) >= sPeriod) && (EntityFunctions.TruncateTime(order.RecordCreationDate) <= ePeriod)
                         select new AccountingCostViewModel
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
                             CreationDate = order.RecordCreationDate,
                             IsPaid = order.IsPaid,
                             WorkOrderStatus = status.WorkOrderStatus,
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetOtherCostByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from cost in entities.OtherCosts
                         join property in entities.Properties on cost.PropertyId equals property.PropertyId
                         join category in entities.CostCategories on cost.CostCategoryId equals category.CostCategoryId
                         where cost.PropertyId == id && (EntityFunctions.TruncateTime(cost.CostAddedDate) >= sPeriod) && (EntityFunctions.TruncateTime(cost.CostAddedDate) <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             OtherCostId = cost.OtherCostId,
                             CostDetails = cost.CostDesc,
                             CostAmount = cost.CostAmount,
                             IsCostPaid = cost.IsPaid,
                             //IsPaid = cost.IsPaid,
                             InvoiceDocUrl = cost.CostInvoiceDocUrl,
                             CostAddDate = cost.CostAddedDate,
                             CostUpdateDate = cost.CostUpdateDate,
                             CostCategory = category.CostCategoryName,
                             OtherCostName = cost.CostName
                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetOtherCostForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from cost in entities.OtherCosts
                         join property in entities.Properties on cost.PropertyId equals property.PropertyId
                         join category in entities.CostCategories on cost.CostCategoryId equals category.CostCategoryId
                         where (EntityFunctions.TruncateTime(cost.CostAddedDate) >= sPeriod) && (EntityFunctions.TruncateTime(cost.CostAddedDate) <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             //PropertyId = property.PropertyId,
                             OtherCostId = cost.OtherCostId,
                             CostDetails = cost.CostDesc,
                             CostAmount = cost.CostAmount,
                             IsCostPaid = cost.IsPaid,
                             //IsPaid = cost.IsPaid,
                             InvoiceDocUrl = cost.CostInvoiceDocUrl,
                             CostAddDate = cost.CostAddedDate,
                             CostUpdateDate = cost.CostUpdateDate,
                             CostCategory = category.CostCategoryName,
                             OtherCostName = cost.CostName
                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetOtherCostByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from cost in entities.OtherCosts
                         join property in entities.Properties on cost.PropertyId equals property.PropertyId
                         join category in entities.CostCategories on cost.CostCategoryId equals category.CostCategoryId
                         where cost.PropertyId == id
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             OtherCostId = cost.OtherCostId,
                             CostDetails = cost.CostDesc,
                             CostAmount = cost.CostAmount,
                             IsCostPaid = cost.IsPaid,
                             InvoiceDocUrl = cost.CostInvoiceDocUrl,
                             CostAddDate = cost.CostAddedDate,
                             CostUpdateDate = cost.CostUpdateDate,
                             CostCategory = category.CostCategoryName,
                             OtherCostName = cost.CostName
                         };

            return result;

        }

        public IEnumerable<AccountingCostViewModel> GetAllRepairCost()
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         //where order.PropertyId == id
                         select new AccountingCostViewModel
                         {

                             PropertyName = property.PropertyName,
                             PropertyId = property.PropertyId,
                             //WorkOrderId = order.WorkOrderId,
                             //WorkOrderName = order.WorkOrderName,
                             //WorkOrderDetails = order.WorkOrderDetails,
                             //WorkOrderTypeName = type.WorkOrderTypeName,
                             //WorkOrderCategory = category.CategoryName,
                             //PaidDate = order.PaymentDate,
                             //PaymentMethod = order.PaymentMethod,
                             //InvoiceAmount = order.InvoiceAmount,
                             //InvoiceDate = order.InvoiceDate,
                             //IsPaid = order.IsPaid,
                             PaymentAmount = order.PaymentAmount

                         };

            return result;
        }

        public IEnumerable<AccountingCostViewModel> GetRentPaymentHistory(int id)
        {
            //throw new NotImplementedException();
            var result = from rent in entities.RentPayments
                         join tenant in entities.Tenants on rent.TenantId equals tenant.TenantId
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where property.PropertyId == id
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             RentPaymentId = rent.RentPaymentId,
                             RentPamentYear = rent.RentPaymentYear,
                             RentPaymentMonth = rent.RentPaymentMonth,
                             RentPaymentAmount = rent.RentPaidAmount

                         };

            return result;
        }



        public IEnumerable<AccountingCostViewModel> GetRentPaymentHistoryByPeriod(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from rent in entities.RentPayments
                         join tenant in entities.Tenants on rent.TenantId equals tenant.TenantId
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where property.PropertyId == id && (EntityFunctions.TruncateTime(rent.RentPaymentDate) >= sPeriod) && (EntityFunctions.TruncateTime(rent.RentPaymentDate) <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             RentPaymentId = rent.RentPaymentId,
                             RentPamentYear = rent.RentPaymentYear,
                             RentPaymentMonth = rent.RentPaymentMonth,
                             RentPaymentAmount = rent.RentPaidAmount

                         };

            return result;
        }



        public IEnumerable<AccountingCostViewModel> GetRentPaymentHistoryForAllPropertiesByPeriod(DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from rent in entities.RentPayments
                         join tenant in entities.Tenants on rent.TenantId equals tenant.TenantId
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where (EntityFunctions.TruncateTime(rent.RentPaymentDate) >= sPeriod) && (EntityFunctions.TruncateTime(rent.RentPaymentDate) <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             RentPaymentId = rent.RentPaymentId,
                             RentPamentYear = rent.RentPaymentYear,
                             RentPaymentMonth = rent.RentPaymentMonth,
                             RentPaymentAmount = rent.RentPaidAmount

                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetManagementFeeByProperty(int id)
        {
            //throw new NotImplementedException();
            var result = from fee in entities.ManagementFees
                         join contract in entities.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         where property.PropertyId == id
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             ManagementFeeId = fee.ManagementFeeId,
                             IsManagementFeeReceived = fee.IsReceived,
                             ManagementFeeAmount = fee.ManagementFeeAmount
                         };

            return result;
        }

        
        public IEnumerable<AccountingCostViewModel> GetManagementFeeByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from fee in entities.ManagementFees
                         join type in entities.ManagementFeeTypes on fee.ManagementFeeTypeId equals type.ManagementFeeTypeId
                         join contract in entities.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         where property.PropertyId == id && (fee.ReceivedDate >= sPeriod) && (fee.ReceivedDate <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             ManagementFeeId = fee.ManagementFeeId,
                             IsManagementFeeReceived = fee.IsReceived,
                             ManagementFeeType = type.ManagementFeeType1,
                             ManagementFeeAmount = fee.ManagementFeeAmount
                         };

            return result;
        }


        public IEnumerable<AccountingCostViewModel> GetManagementFeeForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from fee in entities.ManagementFees
                         join type in entities.ManagementFeeTypes on fee.ManagementFeeTypeId equals type.ManagementFeeTypeId
                         join contract in entities.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         where (fee.ReceivedDate >= sPeriod) && (fee.ReceivedDate <= ePeriod)
                         select new AccountingCostViewModel
                         {
                             PropertyId = property.PropertyId,
                             ManagementFeeId = fee.ManagementFeeId,
                             IsManagementFeeReceived = fee.IsReceived,
                             ManagementFeeType = type.ManagementFeeType1,
                             ManagementFeeAmount = fee.ManagementFeeAmount
                         };

            return result;
        }



        public IEnumerable<AccountingCostViewModel> TotalExpenses(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join costs in entities.OtherCosts on property.PropertyId equals costs.PropertyId
                         join orders in entities.WorkOrders on property.PropertyId equals orders.PropertyId
                         //join contract in entities.ManagementContracts on property.PropertyId equals contract.PropertyId
                         //join fee in entities.ManagementFees on contract.ManagementContractId equals fee.ManagementContractId
                         where property.PropertyId == id
                         select new AccountingCostViewModel
                         {
                             //totalExpenses = orders.PaymentAmount + costs.CostAmount
                             //totalFees = fee.ManagementFeeAmount,
                             totalRepairCosts = orders.PaymentAmount,
                             totalOtherCosts = costs.CostAmount

                         };

            return result;
        }

        public IEnumerable<ManagingAccountingViewModel> GetEventByProperty(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from events in entities.ManagementEvents
                         join property in entities.Properties on events.PropertyId equals property.PropertyId
                         where events.PropertyId == id && (events.EventDate >= sPeriod) && (events.EventDate <= ePeriod)
                         select new ManagingAccountingViewModel
                         {
                             PropertyId = events.PropertyId,
                             EventId = events.EventId,
                             EventTitle = events.EventName,
                             EventDate = events.EventDate,
                             VisitTimeSpent = events.TimeSpent,
                             MileageIncurred = events.MileageIncurred
                         };
            return result;
        }


        public IEnumerable<ManagingAccountingViewModel> GetFeeByProperty(int id, DateTime sPeriod, DateTime ePeriod)
        {
            //throw new NotImplementedException();
            var result = from fee in entities.ManagementFees
                         join contract in entities.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         where property.PropertyId == id && (fee.ReceivedDate >= sPeriod) && (fee.ReceivedDate <= ePeriod)
                         select new ManagingAccountingViewModel
                         {
                             PropertyId = property.PropertyId,
                             ManagementFeeId = fee.ManagementFeeId,
                             IsManagementFeeReceived = fee.IsReceived,
                             ManagementFeeReceivedDate = fee.ReceivedDate,
                             ManagementFeeAmount = fee.ManagementFeeAmount
                         };

            return result;

        }

        #endregion

        #region Reporting

        public IEnumerable<ReportingViewModel> GetAllPropertyList()
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         select new ReportingViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyType = type.PropertyType1,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             PropertyUpdateDate = property.UpdateDate
                         };

            return result;
        }

        public IEnumerable<ReportingViewModel> GetPropertyListWithPeriod(DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where ((EntityFunctions.TruncateTime(property.CreatedDate) >= startDate) && (EntityFunctions.TruncateTime(property.CreatedDate) <= endDate))
                         select new ReportingViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyType = type.PropertyType1,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             PropertyUpdateDate = property.UpdateDate
                         };

            return result;
        }

        public IEnumerable<ReportingViewModel> GetNewlyAddedPropertyList(DateTime fromDate)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         where ((EntityFunctions.TruncateTime(property.CreatedDate) >= fromDate))
                         select new ReportingViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyType = type.PropertyType1,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             PropertyUpdateDate = property.UpdateDate
                         };

            return result;
        }

        public IEnumerable<ReportingViewModel> GetAllTenantList()
        {
            //throw new NotImplementedException();
            var result = from tenant in entities.Tenants
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         select new ReportingViewModel
                         {
                             TenantId = tenant.TenantId
                         };

            return result;
        }

        public IEnumerable<ReportingViewModel> GetTenantListWithPeriod(DateTime startDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            var result = from tenant in entities.Tenants
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where ((EntityFunctions.TruncateTime(tenant.CreateDate) >= startDate) && (EntityFunctions.TruncateTime(tenant.CreateDate) <= endDate))
                         select new ReportingViewModel
                         {
                             TenantId = tenant.TenantId
                         };

            return result;
        }

        public IEnumerable<ReportingViewModel> GetNewlyAddedTenantList(DateTime fromDate)
        {
            //throw new NotImplementedException();
            var result = from tenant in entities.Tenants
                         join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where ((EntityFunctions.TruncateTime(tenant.CreateDate) >= fromDate))
                         select new ReportingViewModel
                         {
                             TenantId = tenant.TenantId
                         };

            return result;
        }

        public ReportingViewModel GetPropertyDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from property in entities.Properties
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         join type in entities.PropertyTypes on property.PropertyTypeId equals type.PropertyTypeId
                         join lease in entities.Leases on property.PropertyId equals lease.PropertyId
                         join tenant in entities.Tenants on lease.LeaseId equals tenant.LeaseId
                         where property.PropertyId == id
                         select new ReportingViewModel 
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyAddressStreetNumber = address.PropertyNumber,
                             PropertyAddressSuiteNumber = address.PropertySuiteNumber,
                             PropertyAddressStreetName = address.PropertyStreet,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCode = address.PropertyZipPostCode,
                             PropertyAddressCountry = address.PropertyCountry,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyType = type.PropertyType1,
                             PropertyImageUrl = property.PropertyImgUrl,
                             PropertyCreationDate = property.CreatedDate,
                             PropertyUpdateDate = property.UpdateDate
                         };

            return result.FirstOrDefault();
        }


        public IEnumerable<Core.Reporting.Property> AllReportingProperty()
        {
            //throw new NotImplementedException();
            return entities.Properties;
        }

        #endregion





        public IEnumerable<AccountingCostViewModel> TotalGrossRevenue(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountingCostViewModel> TotalNetIncome(int id)
        {
            throw new NotImplementedException();
        }













        

        

        

    }
}
