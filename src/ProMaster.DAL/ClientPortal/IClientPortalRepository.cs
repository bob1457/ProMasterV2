using System;
using System.Collections.Generic;
using ProMaster.Core.ClientPortal.ViewModels;

namespace ProMaster.DAL.ClientPortal
{
    public interface IClientPortalRepository
    {
        int GetTenantId(string userName);
        int GetLandlordId(string userName);
        DisplayClientPortalViewModel GetLandlordInfo(int id);
        DisplayClientPortalViewModel GetTenantInfo(int id);
        DisplayClientPortalViewModel GetLeaseByTenant(int id);
        IEnumerable<DisplayClientPortalViewModel> GetRentPaymentHistory(int id);
        IEnumerable<DisplayClientPortalViewModel> GetRentPaymentHistoryByPeriod(int id, DateTime startDate, DateTime endDate);
        IEnumerable<DisplayClientPortalViewModel> GetRentRevenueHistory(int id);
        IEnumerable<DisplayClientPortalViewModel> GetRentRevenueHistoryByperiod(int id, DateTime startDate, DateTime endDate);

        IEnumerable<DisplayClientPortalViewModel> GetFeeRevenueHistory(int id);
        IEnumerable<DisplayClientPortalViewModel> GetFeeRevenueHistoryByPeriod(int id, DateTime startDate, DateTime endDate);

        DisplayClientPortalViewModel GetContractByLandlord(int id);

        IEnumerable<DisplayClientPortalViewModel> GetWorkOrdersByProperty(int id);
        IEnumerable<DisplayClientPortalViewModel> GetWorkOrdersByPropertyByPeriod(int id, DateTime startDate, DateTime endDate);
    }
}
