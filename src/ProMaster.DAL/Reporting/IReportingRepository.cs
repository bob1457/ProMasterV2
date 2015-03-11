using System;
using System.Collections.Generic;
using ProMaster.Core.Reporting.ViewModels;

namespace ProMaster.DAL.Reporting
{
    public interface IReportingRepository
    {
        #region Accounting

        IEnumerable<DisplayDashboardViewModel> MyProperty(int id);
        IEnumerable<DisplayDashboardViewModel> RentedProperty(int id, int statusId);

        AccountingCostViewModel GetRepairCostByProperty(int id); //id is property id


        IEnumerable<AccountingCostViewModel> GetAllRepairCost();


        IEnumerable<AccountingCostViewModel> GetWorkOrdersByProperty(int id); //id is property id
        IEnumerable<AccountingCostViewModel> GetWorkOrdersByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod); //id is property id
        IEnumerable<AccountingCostViewModel> GetOtherCostByProperty(int id);
        IEnumerable<AccountingCostViewModel> GetOtherCostByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod);
        IEnumerable<AccountingCostViewModel> GetManagementFeeByProperty(int id); //id is property id
        IEnumerable<AccountingCostViewModel> GetManagementFeeByPropertyByPeriod(int id, DateTime sPeriod, DateTime ePeriod);
        IEnumerable<AccountingCostViewModel> GetRentPaymentHistory(int id); //id is property id
        IEnumerable<AccountingCostViewModel> GetRentPaymentHistoryByPeriod(int id, DateTime sPeriod, DateTime ePeriod); //id is property id

        IEnumerable<AccountingCostViewModel> GetWorkOrdersForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod);
        IEnumerable<AccountingCostViewModel> GetOtherCostForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod);
        IEnumerable<AccountingCostViewModel> GetManagementFeeForAllPropertyByPeriod(DateTime sPeriod, DateTime ePeriod);
        IEnumerable<AccountingCostViewModel> GetRentPaymentHistoryForAllPropertiesByPeriod(DateTime sPeriod, DateTime ePeriod);

        IEnumerable<AccountingCostViewModel> TotalExpenses(int id); //id is property id
        IEnumerable<AccountingCostViewModel> TotalGrossRevenue(int id); //id is property id
        IEnumerable<AccountingCostViewModel> TotalNetIncome(int id); //id is property id

        IEnumerable<ManagingAccountingViewModel> GetEventByProperty(int id, DateTime sPeriod, DateTime ePeriod); //id is property id
        IEnumerable<ManagingAccountingViewModel> GetFeeByProperty(int id, DateTime sPeriod, DateTime ePeriod); //id is property id

        #endregion

        #region Reporting

        IEnumerable<ReportingViewModel> GetAllPropertyList();
        IEnumerable<ReportingViewModel> GetPropertyListWithPeriod(DateTime startDate, DateTime endDate);
        IEnumerable<ReportingViewModel> GetNewlyAddedPropertyList(DateTime fromDate);

        IEnumerable<Core.Reporting.Property> AllReportingProperty();

        IEnumerable<ReportingViewModel> GetAllTenantList();
        IEnumerable<ReportingViewModel> GetTenantListWithPeriod(DateTime startDate, DateTime endDate);
        IEnumerable<ReportingViewModel> GetNewlyAddedTenantList(DateTime fromDate);

        ReportingViewModel GetPropertyDetails(int id);

        #endregion
    }
}
