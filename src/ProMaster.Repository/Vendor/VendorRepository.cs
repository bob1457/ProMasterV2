using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Vendor;
using ProMaster.Core.Vendor.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.Vendor;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.Repository.Vendor
{
    public class VendorRepository :IVendorRepository
    {
        PromMasterVendorDataEntities entities = new PromMasterVendorDataEntities();

        public IEnumerable<VendorSpecialty> GetSpecialtyList()
        {
            //throw new NotImplementedException();
            return entities.VendorSpecialties;
        }


        //public IEnumerable<DisplayWorkOrderViewModel> ListAllVendors()
        //{
        //    //throw new NotImplementedException();
        //    var result = from vendor in entities.Vendors
        //                 select new DisplayWorkOrderViewModel
        //                 {
        //                     VendorFirstName = vendor.FirstName,
        //                     VendorId = vendor.VendorId,
        //                     VendorLastName = vendor.LastName,
        //                     VendorName = vendor.VendorBusinessName
        //                 };
        //    return result;
        //}
        

        public IEnumerable<ManageDisplayViewModel> GetAllVendors()
        {
            //throw new NotImplementedException();
            var result = from vendor in entities.Vendors
                         join specialty in entities.VendorSpecialties on vendor.VendorSpecialtyId equals specialty.VendorSpecialtyId
                         where vendor.IsActive
                         select new ManageDisplayViewModel 
                         {
                             VendorId = vendor.VendorId,
                             VendorBusinessName = vendor.VendorBusinessName,
                             VendorFirstName = vendor.FirstName,
                             VendorLastName = vendor.LastName,
                             VendorContactEmail = vendor.VendorContactEmail,
                             VendorContactTelephone1 = vendor.VendorContactTelephone1,
                             VendorContactTelephone2 = vendor.VendorContactTelephone2,
                             AddDate = vendor.CreationDate,
                             ModifyDate = vendor.UpdateDate,
                             IsActive = vendor.IsActive,
                             VendorSepcialtyName = specialty.VendorSpecialtyName


                         };

            return result.OrderByDescending(d => d.AddDate);
        }

        public IEnumerable<DisplayVendorViewModel> GetVendorDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from vendor in entities.Vendors
                         join specialty in entities.VendorSpecialties on vendor.VendorSpecialtyId equals specialty.VendorSpecialtyId
                         where vendor.VendorId == id
                         select new DisplayVendorViewModel
                         {
                             VendorId = vendor.VendorId,
                             VendorBusinessName = vendor.VendorBusinessName,
                             FistName = vendor.FirstName,
                             LastName = vendor.LastName,
                             ContactEmail = vendor.VendorContactEmail,
                             ContactTelephone1 = vendor.VendorContactTelephone1,
                             ContactTelephone2 = vendor.VendorContactTelephone2,
                             CreationDate = vendor.CreationDate,
                             UpdateDate = vendor.UpdateDate,
                             VendorSepcialtyName = specialty.VendorSpecialtyName,
                             VendorSpecialtyId = specialty.VendorSpecialtyId,
                             IsActive = vendor.IsActive

                         };

            return result;
        }

        public Core.Vendor.Vendor VendorForEdit(int id)
        {
            //throw new NotImplementedException();
            return entities.Vendors.FirstOrDefault(v => v.VendorId == id);
        }



        public IEnumerable<WorkOrderCategory> GetAllWorkOrderCategories()
        {
            //throw new NotImplementedException();
            return entities.WorkOrderCategories;
        }

        public IEnumerable<WorkOrderStatu> GetWorkOrderStatusList()
        {
            //throw new NotImplementedException();
            return entities.WorkOrderStatus;
        }

        public IEnumerable<Core.Vendor.Vendor> GetVendorList()
        {
            //throw new NotImplementedException();
            return entities.Vendors;
        }

        public IEnumerable<WorkOrderType> GetWorkOrderType()
        {
            //throw new NotImplementedException();
            return entities.WorkOrderTypes;
        }

        public IEnumerable<WorkOrder> GetWorkOrderHistoryByPropertyId(int id)
        {
            //throw new NotImplementedException();
            return entities.WorkOrders.Where(p => p.PropertyId == id);
        }

        public IEnumerable<DisplayPropertyViewModel> GetWorkOrdersByPropertyId(int id)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join  vendor in entities.Vendors on order.VendorId equals vendor.VendorId
                         join property in entities.Properties on order.PropertyId equals property.PropertyId
                         where (order.PropertyId == id)
                         select new DisplayPropertyViewModel 
                         {
                             PropertyId = property.PropertyId,
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             WorkOrderId = order.WorkOrderId,
                             StartDate = order.StartDate,
                             EndDate = order.EndDate,
                             IsPaid = order.IsPaid,
                             VendorName = vendor.VendorBusinessName,
                             InvoiceAmount = order.InvoiceAmount,

                             WorkOrderCategory = category.CategoryName,
                             WorkOrderStatus = status.WorkOrderStatus  
                         };

            return result;
        }


        public DisplayWorkOrderViewModel GetWorkOrderDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join vendor in entities.Vendors on order.VendorId equals vendor.VendorId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         join property in entities.Properties on order.PropertyId equals property.PropertyId
                         join address in entities.PropertyAddresses on property.PropertyAddressId equals address.PropertyAddressId
                         
                         where (order.WorkOrderId == id)
                         select new DisplayWorkOrderViewModel
                         {                             
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             WorkOrderId = order.WorkOrderId,
                             StartDate = order.StartDate,
                             EndDate = order.EndDate,
                             IsPaid = order.IsPaid,
                             IsAuthorized = order.IsAuthorized,
                             RecordCreationDate = order.RecordCreationDate,
                             PaidDate = order.PaymentDate,
                             PaymentMethod = order.PaymentMethod,
                             VendorName = vendor.VendorBusinessName,
                             InvoiceAmount = order.InvoiceAmount,
                             InvoiceDate = order.InvoiceDate,
                             WorkOrderTypeName = type.WorkOrderTypeName,
                             RecordUpdateDate = order.RecordUpdateDate,
                             PaymentAmount = order.PaymentAmount,
                             VendorId = vendor.VendorId,

                             PropertyId = property.PropertyId,
                             PropertyAddressCity = address.PropertyCity,
                             PropertyAddressStreet = address.PropertyStreet,
                             PropertyAddressNumber = address.PropertyNumber,
                             PropertyAddressSuoteNumber = address.PropertySuiteNumber,
                             PropertyAddressProvState = address.PropertyStateProvince,
                             PropertyAddressPostZipCodet = address.PropertyZipPostCode,
                             PropertyName = property.PropertyName,
                             
                             WorkOrderCategory = category.CategoryName,
                             WorkOrderStatus = status.WorkOrderStatus


                         };

            return result.FirstOrDefault();
        }

        public CreateWorkOrderViewModel GetWorkOrderForEdit(int id)
        {
            //throw new NotImplementedException();
            var result = from order in entities.WorkOrders
                         join status in entities.WorkOrderStatus on order.WorkOrderStatusId equals status.WorkOrderStatusId
                         join category in entities.WorkOrderCategories on order.WorkOrderCategoryId equals category.WorkOrderCategoryId
                         join vendor in entities.Vendors on order.VendorId equals vendor.VendorId
                         join type in entities.WorkOrderTypes on order.WorkOrderTypeId equals type.WorkOrderTypeId
                         //join property in entities.Properties on order.PropertyId equals property.PropertyId

                         where (order.WorkOrderId == id)
                         select new CreateWorkOrderViewModel
                         {
                             WorkOrderName = order.WorkOrderName,
                             WorkOrderDetails = order.WorkOrderDetails,
                             WorkOrderId = order.WorkOrderId,
                             StartDate = order.StartDate,
                             EndDate = order.EndDate,
                             InvoiceDate = order.InvoiceDate,
                             InvoiceAmount = order.InvoiceAmount,
                             IsPaid = order.IsPaid,
                             IsAuthorized = order.IsAuthorized,
                             RecordCreationDate = order.RecordCreationDate,
                             PaidDate = order.PaymentDate,
                             PaymentMethod = order.PaymentMethod,
                             VendorName = vendor.VendorBusinessName,
                             WorkOrderCategoryId = order.WorkOrderCategoryId,
                             WorkOrderTypeId = order.WorkOrderTypeId,

                             WorkOrderCategory = category.CategoryName,
                             WorkOrderStatusId = status.WorkOrderStatusId
                         };

            return result.FirstOrDefault();
        }


        public WorkOrder WorkOrderForEdit(int id)
        {
            //throw new NotImplementedException();
            return entities.WorkOrders.FirstOrDefault(w => w.WorkOrderId == id);
        }

        public IEnumerable<CostCategory> GetAllCostCategories()
        {
            //throw new NotImplementedException();
            return entities.CostCategories;
        }


        public IEnumerable<DisplayPropertyViewModel> GetOtherCostByPropertyId(int id)
        {
            //throw new NotImplementedException();
            var result = from cost in entities.OtherCosts
                         join category in entities.CostCategories on cost.CostCategoryId equals category.CostCategoryId
                         where cost.PropertyId == id
                         select new DisplayPropertyViewModel 
                         { 
                             CostAddedDate = cost.CostAddedDate,
                             CostAmount = cost.CostAmount,
                             CostName = cost.CostName,
                             CostDetails = cost.CostDesc,
                             OtherCostId = cost.OtherCostId,
                             IsCostPaid = cost.IsPaid
                         };

            return result;
        }


        public IEnumerable<SiteSearchViewModel> ShowAllVendorResult(string searchString)
        {
            //throw new NotImplementedException();
            var result = from vendor in entities.Vendors
                         where vendor.FirstName.ToUpper().Contains(searchString.ToUpper()) || vendor.LastName.ToUpper().Contains(searchString.ToUpper())
                         || vendor.VendorBusinessName.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel
                         {

                         };

            return result;
                         
    
        }



        public void DeleteVendor(Core.Vendor.Vendor vendor)
        {
            //throw new NotImplementedException();
            entities.DeleteObject(vendor);
        }


        public void AddWorkOrder(WorkOrder order)
        {
            //throw new NotImplementedException();
            entities.WorkOrders.AddObject(order);
        }

        public void AddVendor(Core.Vendor.Vendor vendor)
        {
            //throw new NotImplementedException();
            entities.Vendors.AddObject(vendor);
        }


        public OtherCostViewModel CostDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from cost in entities.OtherCosts
                         join category in entities.CostCategories on cost.CostCategoryId equals category.CostCategoryId
                         join property in entities.Properties on cost.PropertyId equals property.PropertyId
                         where cost.OtherCostId == id
                         select new OtherCostViewModel 
                         { 
                             OtherCostId = cost.OtherCostId,
                             OtherCostName = cost.CostName,
                             CostDetails = cost.CostDesc,
                             CostAmount = cost.CostAmount,
                             IsPaid = cost.IsPaid,
                             CostAddDate = cost.CostAddedDate,
                             CostCategory = category.CostCategoryName,
                             CostCategoryId = cost.CostCategoryId,
                             PropertyName = property.PropertyName
                         };
            return result.FirstOrDefault();
        }

        public OtherCost CostForEdit(int id)
        {
            //throw new NotImplementedException();
            return entities.OtherCosts.Where(c => c.OtherCostId == id).FirstOrDefault();
        }


        public void AddOtherCost(OtherCost cost)
        {
            //throw new NotImplementedException();
            entities.OtherCosts.AddObject(cost);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }
































    }
}
