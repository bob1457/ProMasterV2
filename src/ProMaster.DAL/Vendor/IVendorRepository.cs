using System.Collections.Generic;
using ProMaster.Core.ViewModels;
using ProMaster.Core.Vendor.ViewModels;
using ProMaster.Core.Vendor;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.Vendor
{
    public interface IVendorRepository
    {
        IEnumerable<VendorSpecialty> GetSpecialtyList();

        IEnumerable<ManageDisplayViewModel> GetAllVendors();

        IEnumerable<DisplayVendorViewModel> GetVendorDetails(int id);
        Core.Vendor.Vendor VendorForEdit(int id);

        IEnumerable<WorkOrderCategory> GetAllWorkOrderCategories();
        IEnumerable<WorkOrderStatu> GetWorkOrderStatusList();
        IEnumerable<Core.Vendor.Vendor> GetVendorList();

        //IEnumerable<DisplayWorkOrderViewModel> ListAllVendors();


        IEnumerable<WorkOrderType> GetWorkOrderType();

        IEnumerable<SiteSearchViewModel> ShowAllVendorResult(string searchString);

        IEnumerable<CostCategory> GetAllCostCategories();
        OtherCostViewModel CostDetails(int id);
        OtherCost CostForEdit(int id);

        //IEnumerable<Core.Vendor.WorkOrder> GetWorkOrderHistoryByPropertyId(int id);
        IEnumerable<DisplayPropertyViewModel> GetWorkOrdersByPropertyId(int id);
        DisplayWorkOrderViewModel GetWorkOrderDetails(int id);
        CreateWorkOrderViewModel GetWorkOrderForEdit(int id);
        IEnumerable<DisplayPropertyViewModel> GetOtherCostByPropertyId(int id);

        WorkOrder WorkOrderForEdit(int id);

        void DeleteVendor(Core.Vendor.Vendor vendor);
        void AddVendor(Core.Vendor.Vendor vendor);
        void AddWorkOrder(WorkOrder order);
        void AddOtherCost(OtherCost cost);


        void Save();
    }
}
