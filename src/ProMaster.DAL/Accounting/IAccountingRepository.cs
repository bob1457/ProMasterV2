using System;
using System.Collections.Generic;
using ProMaster.Core.Accounting.ViewModels;

namespace ProMaster.DAL.Accounting
{
    public interface IAccountingRepository
    {
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

        

        IEnumerable<AccountingCostViewModel> TotalExpenses(int id); //id is property id
        IEnumerable<AccountingCostViewModel> TotalGrossRevenue (int id); //id is property id
        IEnumerable<AccountingCostViewModel> TotalNetIncome (int id); //id is property id

        IEnumerable<ManagingAccountingViewModel> GetEventByProperty(int id, DateTime sPeriod, DateTime ePeriod); //id is property id
        IEnumerable<ManagingAccountingViewModel> GetFeeByProperty(int id, DateTime sPeriod, DateTime ePeriod); //id is property id

    }
}
