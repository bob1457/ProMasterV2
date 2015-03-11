using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using ProMaster.Core.Vendor;

namespace ProMaster.DAL.Vendor
{
    public static class Extension
    {
        public static IEnumerable<SelectListItem> ToVendorSpecialtyList(this IEnumerable<VendorSpecialty> specialties, int id)
        {
            return specialties.OrderBy(specialty => specialty.VendorSpecialtyName)
                .Select(specialty => new SelectListItem
                {
                    Selected = (specialty.VendorSpecialtyId == id),
                    Text = specialty.VendorSpecialtyName,
                    Value = specialty.VendorSpecialtyId.ToString(CultureInfo.InvariantCulture)
                });
        }

        public static IEnumerable<SelectListItem> ToWorkOrderCategoryList(this IEnumerable<WorkOrderCategory> categories, int id)
        {
            return categories.OrderBy(category => category.CategoryName)
                .Select(category => new SelectListItem
                {
                    Selected = ( category.WorkOrderCategoryId == id),
                    Text = category.CategoryName,
                    Value = category.WorkOrderCategoryId.ToString(CultureInfo.InvariantCulture)
                });
        }

        

        public static IEnumerable<SelectListItem> ToWorkOrderStatusList(this IEnumerable<WorkOrderStatu> Status, int id)
        {
            if (Status == null) throw new ArgumentNullException("Status");
            return Status//.OrderBy(status => status.WorkOrderStatus)
                .Select(status => new SelectListItem
                {
                    Selected = (status.WorkOrderStatusId == id),
                    Text = status.WorkOrderStatus,
                    Value = status.WorkOrderStatusId.ToString()
                });
        }

        public static IEnumerable<SelectListItem> ToVendorList(this IEnumerable<Core.Vendor.Vendor> vendors, int id)
        {
            return vendors.OrderBy(vendor => vendor.LastName +", " + vendor.FirstName)
                .Select(vendor => new SelectListItem
                {
                    Selected = (vendor.VendorId == id),
                    Text = vendor.LastName + ", " + vendor.FirstName,
                    Value = vendor.VendorId.ToString(CultureInfo.InvariantCulture)
                });
        }

        public static IEnumerable<SelectListItem> ToWorkOrderTypeList(this IEnumerable<Core.Vendor.WorkOrderType> types, int id)
        {
            return types.OrderBy(type => type.WorkOrderTypeName)
                .Select(type => new SelectListItem
                {
                    Selected = (type.WorkOrderTypeId == id),
                    Text = type.WorkOrderTypeName,
                    Value = type.WorkOrderTypeId.ToString(CultureInfo.InvariantCulture)
                });
        }

        public static IEnumerable<SelectListItem> ToCostCategoryList(this IEnumerable<CostCategory> costs, int id)
        {
            return costs.OrderBy(cost => cost.CostCategoryName)
                .Select(cost => new SelectListItem 
                { 
                    Selected = (cost.CostCategoryId == id),
                    Text = cost.CostCategoryName,
                    Value = cost.CostCategoryId.ToString(CultureInfo.InvariantCulture)
                });

        }

       
    }
}
