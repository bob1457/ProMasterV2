using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using RazorPDF;
using ProMaster.DAL.Property;
using ProMaster.Core.Property.ViewModels;
using ProMaster.Core.Reporting.ViewModels;
using ProMaster.DAL.Reporting;

namespace ProMaster.Web.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        //
        // GET: /Reporting/
        #region DI Implementation

        IReportingRepository _reportingRepository;
        IPropertyRepository _propertyRepository;


        public ReportingController(IReportingRepository reportingRepository, IPropertyRepository propertyRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "reporting";

            _reportingRepository = reportingRepository;
            _propertyRepository = propertyRepository;
        }

        #endregion

       
        //Reporting home page: key performance indicators and entry point of genearting reports
        //
        public ActionResult Index()
        {
            ViewBag.CurrentPage = "reporting";

            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            var dashBoard = new ProMaster.Core.Reporting.ViewModels.DisplayDashboardViewModel();

            dashBoard.MyProperty = _reportingRepository.MyProperty(pmId);
            dashBoard.RentedProperty = _reportingRepository.RentedProperty(pmId, 1);

            dashBoard.NumberOfPromperties = dashBoard.MyProperty.Count();

            var property = _reportingRepository.AllReportingProperty();

            dashBoard.PropertyList = property.ToPropertyList(-1);

            //******************************************//
            //**** New Implemetaion of Indicators******//
            //*****************************************//

            

















            return View("Index", dashBoard);

        }

        #region Financial Reporting and Accounting

        public ActionResult RepairCost(AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> cost = _reportingRepository.GetAllRepairCost();

            return View("RepairCost", model);
        }

        public ActionResult OtherCostsForProperty(int id, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByProperty(id);

            model.CostList = costs;
            if (model.CostList.Count() != 0)
            {

            }
            else
            {
                ViewBag.msg = "No work orders found.";
                //ViewBag.total = 0;
            }

            return View("OtherCostsForProperty", costs);
        }

        public ActionResult CostAndRevenueByProperty(int id, AccountingCostViewModel model)
        {
            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);

            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByProperty(id);

            //decimal? repairCost = orders.FirstOrDefault().PaymentAmount;
            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByProperty(id);

            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistory(id);

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByProperty(id);

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            //Other Costs
            //
            model.CostList = costs;
            if (model.CostList.Count() != 0)
            {
                model.OtherCostId = costs.FirstOrDefault().OtherCostId;
                model.OtherCostName = costs.FirstOrDefault().OtherCostName;
                model.CostAmount = costs.FirstOrDefault().CostAmount;
                model.IsPaid = costs.FirstOrDefault().IsPaid;
                model.CostAddDate = costs.FirstOrDefault().CostAddDate;
                model.UpdateDate = costs.FirstOrDefault().UpdateDate;
                model.CostCategory = costs.FirstOrDefault().CostCategory;


                decimal? TotalOtherCost = 0;

                foreach (var item in costs)
                {
                    TotalOtherCost = costs.Sum(p => p.CostAmount);

                }

                model.TotalOtherCost = TotalOtherCost;
                ViewBag.totalcost = TotalOtherCost;
            }
            else
            {
                ViewBag.msg1 = "No other costs found.";
                ViewBag.totalcost = 0;
            }

            //Repair Costs
            //
            model.WorkOrderList = orders;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = orders.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = orders.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = orders.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = orders.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = orders.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = orders.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = orders.FirstOrDefault().PaidDate;
                model.IsPaid = orders.FirstOrDefault().IsPaid;
                model.PaymentAmount = orders.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;

                decimal? TotalRepaireCost = 0;

                foreach (var item in orders)
                {
                    TotalRepaireCost = orders.Sum(p => p.PaymentAmount);

                }

                model.TotalRepairCost = TotalRepaireCost;

                ViewBag.total = TotalRepaireCost;
            }
            else
            {
                ViewBag.msg2 = "No work orders found.";
                ViewBag.total = 0;
            }

            //management fee and cost (only on-going monthly management fees)
            //
            model.FeeList = fees;
            if (model.FeeList.Count() != 0)
            {
                model.FeeList = fees;
                model.ManagementFeeAmount = fees.FirstOrDefault().ManagementFeeAmount;
                model.ManagementFeeId = fees.FirstOrDefault().ManagementFeeId;
                model.IsManagementFeeReceived = fees.FirstOrDefault().IsManagementFeeReceived;

                decimal? TotalFee = 0;

                foreach (var item in fees)
                {
                    TotalFee = fees.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalFee = TotalFee;

                ViewBag.totalfee = TotalFee;



            }
            else
            {
                ViewBag.msg = "No fees found.";
                ViewBag.totalfee = 0;
            }


            //Rent Revenue
            //
            model.RentRevenueList = revenue;
            if (model.RentRevenueList.Count() != 0)
            {
                model.RentPaymentId = revenue.FirstOrDefault().RentPaymentId;
                model.RentPaymentMonth = revenue.FirstOrDefault().RentPaymentMonth;
                model.RentPamentYear = revenue.FirstOrDefault().RentPamentYear;
                model.RentPaymentAmount = revenue.FirstOrDefault().RentPaymentAmount;

                decimal? TotalRentRevenue = 0;

                foreach (var item in revenue)
                {
                    TotalRentRevenue = revenue.Sum(p => p.RentPaymentAmount);

                }

                model.TotalRentRevenue = TotalRentRevenue;

                ViewBag.totalrevenue = TotalRentRevenue;
            }
            else
            {
                ViewBag.msg3 = "No revenue found.";
                ViewBag.totalrevenue = 0;
            }


            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
            ViewBag.totalExpenses = TotalExpenses;

            decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = TotalRevenue;

            decimal NetIncome = TotalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;

            return View("CostAndRevenueByProperty", model);

        }

        //public ActionResult ManagementAccounting(int id)
        public ActionResult FinancialReporting(int id)
        {
            ManagingAccountingViewModel model = new ManagingAccountingViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            //Property Info
            //
            //model.PropertyAddress = address.FirstOrDefault();

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;

            //return View("ManagementAccounting", model);
            return View("FinancialReporting", model);
        }

        public ActionResult FinancialStatementByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

            TempData["start"] = startPeriod;
            TempData["end"] = endPeriod;

            //Property Info
            //
            //model.PropertyAddress = address.FirstOrDefault();

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;


            #region Work order info
            //
            model.WorkOrderList = orders;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = orders.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = orders.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = orders.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = orders.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = orders.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = orders.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = orders.FirstOrDefault().PaidDate;
                model.IsPaid = orders.FirstOrDefault().IsPaid;
                model.PaymentAmount = orders.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;

                decimal? TotalRepaireCost = 0;

                foreach (var item in orders)
                {
                    TotalRepaireCost = orders.Sum(p => p.PaymentAmount);

                }

                model.TotalRepairCost = TotalRepaireCost;

                ViewBag.total = TotalRepaireCost;

                TempData["repaircost"] = TotalRepaireCost;
            }
            else
            {
                ViewBag.msg2 = "No work orders found.";
                ViewBag.total = 0;
                TempData["repaircost"] = 0;
            }
            #endregion

            #region Other cost info
            //
            model.CostList = costs;
            if (model.CostList.Count() != 0)
            {
                model.OtherCostId = costs.FirstOrDefault().OtherCostId;
                model.OtherCostName = costs.FirstOrDefault().OtherCostName;
                model.CostAmount = costs.FirstOrDefault().CostAmount;
                model.IsPaid = costs.FirstOrDefault().IsPaid;
                model.CostAddDate = costs.FirstOrDefault().CostAddDate;
                model.UpdateDate = costs.FirstOrDefault().UpdateDate;
                model.CostCategory = costs.FirstOrDefault().CostCategory;


                decimal? TotalOtherCost = 0;

                foreach (var item in costs)
                {
                    TotalOtherCost = costs.Sum(p => p.CostAmount);

                }

                model.TotalOtherCost = TotalOtherCost;
                ViewBag.totalcost = TotalOtherCost;

                TempData["othercost"] = TotalOtherCost;
            }
            else
            {
                ViewBag.msg1 = "No other costs found.";
                ViewBag.totalcost = 0;
                TempData["othercost"] = 0;
            }

            #endregion

            #region Management fee info
            //
            model.FeeList = fees;
            if (model.FeeList.Count() != 0)
            {
                model.FeeList = fees;
                model.ManagementFeeAmount = fees.FirstOrDefault().ManagementFeeAmount;
                model.ManagementFeeId = fees.FirstOrDefault().ManagementFeeId;
                model.IsManagementFeeReceived = fees.FirstOrDefault().IsManagementFeeReceived;

                decimal? TotalFee = 0;

                foreach (var item in fees)
                {
                    TotalFee = fees.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalFee = TotalFee;

                ViewBag.totalfee = TotalFee;

                TempData["fee"] = TotalFee;

            }
            else
            {
                ViewBag.msg = "No fees found.";
                ViewBag.totalfee = 0;
                TempData["fee"] = 0;
            }



            #endregion

            #region Rent Revenue

            model.RentRevenueList = revenue;
            if (model.RentRevenueList.Count() != 0)
            {
                model.RentPaymentId = revenue.FirstOrDefault().RentPaymentId;
                model.RentPaymentMonth = revenue.FirstOrDefault().RentPaymentMonth;
                model.RentPamentYear = revenue.FirstOrDefault().RentPamentYear;
                model.RentPaymentAmount = revenue.FirstOrDefault().RentPaymentAmount;

                decimal? TotalRentRevenue = 0;

                foreach (var item in revenue)
                {
                    TotalRentRevenue = revenue.Sum(p => p.RentPaymentAmount);

                }

                model.TotalRentRevenue = TotalRentRevenue;

                ViewBag.totalrevenue = TotalRentRevenue;

                TempData["revenue"] = TotalRentRevenue;
            }
            else
            {
                ViewBag.msg3 = "No revenue found.";
                ViewBag.totalrevenue = 0;
                TempData["revenue"] = 0;
            }

            #endregion

            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
            ViewBag.totalExpenses = TotalExpenses;

            decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = TotalRevenue;

            decimal NetIncome = TotalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;

            return PartialView("_FinancialStatement", model);
        }

        public ActionResult ExportPDF()
        {
            FinancialStatementViewModel statement = new FinancialStatementViewModel();

            string repaircosts = TempData["repaircost"].ToString();
            string othercosts = TempData["othercost"].ToString();
            string fee = TempData["fee"].ToString();
            string revenue = TempData["revenue"].ToString();


            statement.TotalRepairCosts = decimal.Parse(repaircosts);
            statement.TotalOtherCosts = decimal.Parse(othercosts);
            statement.TotalFees = decimal.Parse(fee);
            statement.TotalRentRevenue = decimal.Parse(revenue);

            //statement.StartDate =  (TempData["start"]);
            //statement.EndDate = TempData["end"];
            statement.StartDate = Convert.ToDateTime(TempData["start"]);
            statement.EndDate = Convert.ToDateTime(TempData["end"]);

            statement.TotalNetIncome = statement.TotalRentRevenue - statement.TotalOtherCosts - statement.TotalFees - statement.TotalRepairCosts;

            return new PdfResult(statement, "ExportPDF");
        }

        #endregion

        #region Management Reporting

        public ActionResult PropertyReport()
        {
            ReportingViewModel model = new ReportingViewModel();

            IEnumerable<ReportingViewModel> AllPropertyList = _reportingRepository.GetAllPropertyList();
            //IEnumerable<ReportingViewModel> PropertyListOnPeriod = 

            model.PropertyList = AllPropertyList;

            model.PropertyId = AllPropertyList.FirstOrDefault().PropertyId;
            model.PropertyName = AllPropertyList.FirstOrDefault().PropertyName;
            model.PropertyDesc = AllPropertyList.FirstOrDefault().PropertyDesc;
            model.PropertyAddressSuiteNumber = AllPropertyList.FirstOrDefault().PropertyAddressSuiteNumber;
            model.PropertyAddressStreetNumber = AllPropertyList.FirstOrDefault().PropertyAddressStreetNumber;
            model.PropertyAddressStreetName = AllPropertyList.FirstOrDefault().PropertyAddressStreetName;
            model.PropertyAddressCity = AllPropertyList.FirstOrDefault().PropertyAddressCity;
            model.PropertyAddressProvState = AllPropertyList.FirstOrDefault().PropertyAddressProvState;
            model.PropertyAddressPostZipCode = AllPropertyList.FirstOrDefault().PropertyAddressPostZipCode;
            model.PropertyAddressCountry = AllPropertyList.FirstOrDefault().PropertyAddressCountry;
            model.PropertyBuildYear = AllPropertyList.FirstOrDefault().PropertyBuildYear;
            model.TenantAvartaImgUrl = AllPropertyList.FirstOrDefault().TenantAvartaImgUrl;
            model.PropertyCreationDate = AllPropertyList.FirstOrDefault().PropertyCreationDate;
            model.TenantUpdateDate = AllPropertyList.FirstOrDefault().PropertyUpdateDate;



            return View("PropertyReport", model);
        }

        public ActionResult GetPropertyReport()
        {
            ReportingViewModel model = new ReportingViewModel();

            IEnumerable<ReportingViewModel> AllPropertyList = _reportingRepository.GetAllPropertyList();            

            model.PropertyList = AllPropertyList;

            model.PropertyId = AllPropertyList.FirstOrDefault().PropertyId;
            model.PropertyName = AllPropertyList.FirstOrDefault().PropertyName;
            model.PropertyDesc = AllPropertyList.FirstOrDefault().PropertyDesc;
            model.PropertyAddressSuiteNumber = AllPropertyList.FirstOrDefault().PropertyAddressSuiteNumber;
            model.PropertyAddressStreetNumber = AllPropertyList.FirstOrDefault().PropertyAddressStreetNumber;
            model.PropertyAddressStreetName = AllPropertyList.FirstOrDefault().PropertyAddressStreetName;
            model.PropertyAddressCity = AllPropertyList.FirstOrDefault().PropertyAddressCity;
            model.PropertyAddressProvState = AllPropertyList.FirstOrDefault().PropertyAddressProvState;
            model.PropertyAddressPostZipCode = AllPropertyList.FirstOrDefault().PropertyAddressPostZipCode;
            model.PropertyAddressCountry = AllPropertyList.FirstOrDefault().PropertyAddressCountry;
            model.PropertyBuildYear = AllPropertyList.FirstOrDefault().PropertyBuildYear;
            model.TenantAvartaImgUrl = AllPropertyList.FirstOrDefault().TenantAvartaImgUrl;
            model.PropertyCreationDate = AllPropertyList.FirstOrDefault().PropertyCreationDate;
            model.TenantUpdateDate = AllPropertyList.FirstOrDefault().PropertyUpdateDate;


            return PartialView("_PropertyReport", model);
        }

        public ActionResult GetPropertyReportByPeriod(DateTime sDate, DateTime eDate)
        {
            ReportingViewModel model = new ReportingViewModel();

            IEnumerable<ReportingViewModel> AllPropertyList = _reportingRepository.GetPropertyListWithPeriod(sDate, eDate);

            model.PropertyList = AllPropertyList;

            if (model.PropertyList.Count() != 0)
            {
                model.PropertyId = AllPropertyList.FirstOrDefault().PropertyId;
                model.PropertyName = AllPropertyList.FirstOrDefault().PropertyName;
                model.PropertyDesc = AllPropertyList.FirstOrDefault().PropertyDesc;
                model.PropertyAddressSuiteNumber = AllPropertyList.FirstOrDefault().PropertyAddressSuiteNumber;
                model.PropertyAddressStreetNumber = AllPropertyList.FirstOrDefault().PropertyAddressStreetNumber;
                model.PropertyAddressStreetName = AllPropertyList.FirstOrDefault().PropertyAddressStreetName;
                model.PropertyAddressCity = AllPropertyList.FirstOrDefault().PropertyAddressCity;
                model.PropertyAddressProvState = AllPropertyList.FirstOrDefault().PropertyAddressProvState;
                model.PropertyAddressPostZipCode = AllPropertyList.FirstOrDefault().PropertyAddressPostZipCode;
                model.PropertyAddressCountry = AllPropertyList.FirstOrDefault().PropertyAddressCountry;
                model.PropertyBuildYear = AllPropertyList.FirstOrDefault().PropertyBuildYear;
                model.TenantAvartaImgUrl = AllPropertyList.FirstOrDefault().TenantAvartaImgUrl;
                model.PropertyCreationDate = AllPropertyList.FirstOrDefault().PropertyCreationDate;
                model.TenantUpdateDate = AllPropertyList.FirstOrDefault().PropertyUpdateDate;

                return PartialView("_PropertyReport", model);

            }
            else
            {
                return PartialView("_NoRecords", model);
            }
        }

        public ActionResult GetPropertyDetails(int id) //Get details from view by ajax(jquery)
        {
            ReportingViewModel property = _reportingRepository.GetPropertyDetails(id);



            return View();
        }

        #endregion

        #region NEW IMPLEMENTATION

        //****************************//
        //**** New Implementation ****//
        //****************************//

        #region Modules

        //Modules (functions)
        //
        void TotalMaintenanceCost(int id, DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {

            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, sDate, eDate);
            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, sDate, eDate);

            decimal? TotalMainternanceCosts = 0;

            //Repair Costs
            //
            model.WorkOrderList = orders;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = orders.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = orders.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = orders.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = orders.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = orders.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = orders.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = orders.FirstOrDefault().PaidDate;
                model.IsPaid = orders.FirstOrDefault().IsPaid;
                model.PaymentAmount = orders.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;

                decimal? TotalRepaireCost = 0;

                foreach (var item in orders)
                {
                    TotalRepaireCost = orders.Sum(p => p.PaymentAmount);

                }

                model.TotalRepairCost = TotalRepaireCost;
                TotalMainternanceCosts = TotalRepaireCost;

                ViewBag.total = TotalRepaireCost;
                TempData["repaircost"] = TotalRepaireCost;
                
            }
            else
            {
                ViewBag.msg2 = "No work orders found.";
                ViewBag.total = 0;
                TempData["repaircost"] = 0;
            }



            //Other Costs
            //
            model.CostList = costs;
            if (model.CostList.Count() != 0)
            {
                model.OtherCostId = costs.FirstOrDefault().OtherCostId;
                model.OtherCostName = costs.FirstOrDefault().OtherCostName;
                model.CostAmount = costs.FirstOrDefault().CostAmount;
                model.IsCostPaid = costs.FirstOrDefault().IsCostPaid;
                model.CostAddDate = costs.FirstOrDefault().CostAddDate;
                model.UpdateDate = costs.FirstOrDefault().UpdateDate;
                model.CostCategory = costs.FirstOrDefault().CostCategory;


                decimal? TotalOtherCost = 0;

                foreach (var item in costs)
                {
                    TotalOtherCost = costs.Sum(p => p.CostAmount);

                }

                model.TotalOtherCost = TotalOtherCost;
                TotalMainternanceCosts = TotalMainternanceCosts + TotalOtherCost;

                ViewBag.totalcost = TotalOtherCost;
                TempData["othercost"] = TotalOtherCost;
            }
            else
            {
                ViewBag.msg1 = "No other costs found.";
                ViewBag.totalcost = 0;
                TempData["othercost"] = 0;
            }

            ViewBag.totalMaintCosts = TotalMainternanceCosts;
            model.totalMaintenanceCosts = TotalMainternanceCosts;

 
        }

        void TotalMaintenanceCost(DateTime sDate, DateTime eDate, AccountingCostViewModel model) //For all properties
        { 
            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersForAllPropertyByPeriod(sDate, eDate);
            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostForAllPropertyByPeriod(sDate, eDate);

            decimal? TotalMainternanceCosts = 0;

            //Repair Costs
            //
            model.WorkOrderList = orders;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = orders.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = orders.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = orders.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = orders.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = orders.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = orders.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = orders.FirstOrDefault().PaidDate;
                model.IsPaid = orders.FirstOrDefault().IsPaid;
                model.PaymentAmount = orders.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = orders.FirstOrDefault().WorkOrderStatus;

                decimal? TotalRepaireCost = 0;

                foreach (var item in orders)
                {
                    TotalRepaireCost = orders.Sum(p => p.PaymentAmount);

                }

                model.TotalRepairCost = TotalRepaireCost;
                TotalMainternanceCosts = TotalRepaireCost;

                ViewBag.total = TotalRepaireCost;
                TempData["repaircost"] = TotalRepaireCost;

            }
            else
            {
                ViewBag.msg2 = "No work orders found.";
                ViewBag.total = 0;
                TempData["repaircost"] = 0;
            }



            //Other Costs
            //
            model.CostList = costs;
            if (model.CostList.Count() != 0)
            {
                model.OtherCostId = costs.FirstOrDefault().OtherCostId;
                model.OtherCostName = costs.FirstOrDefault().OtherCostName;
                model.CostAmount = costs.FirstOrDefault().CostAmount;
                model.IsCostPaid = costs.FirstOrDefault().IsCostPaid;
                model.CostAddDate = costs.FirstOrDefault().CostAddDate;
                model.UpdateDate = costs.FirstOrDefault().UpdateDate;
                model.CostCategory = costs.FirstOrDefault().CostCategory;


                decimal? TotalOtherCost = 0;

                foreach (var item in costs)
                {
                    TotalOtherCost = costs.Sum(p => p.CostAmount);

                }

                model.TotalOtherCost = TotalOtherCost;
                TotalMainternanceCosts = TotalMainternanceCosts + TotalOtherCost;

                ViewBag.totalcost = TotalOtherCost;
                TempData["othercost"] = TotalOtherCost;
            }
            else
            {
                ViewBag.msg1 = "No other costs found.";
                ViewBag.totalcost = 0;
                TempData["othercost"] = 0;
            }

            ViewBag.totalMaintCosts = TotalMainternanceCosts;
 
        }

        void TotalManagementCost(int id, DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, sDate, eDate);

            //management fee and cost (only on-going monthly management fees)
            //
            model.FeeList = fees;
            if (model.FeeList.Count() != 0)
            {
                model.FeeList = fees;
                model.ManagementFeeAmount = fees.FirstOrDefault().ManagementFeeAmount;
                model.ManagementFeeId = fees.FirstOrDefault().ManagementFeeId;
                model.IsManagementFeeReceived = fees.FirstOrDefault().IsManagementFeeReceived;

                decimal? TotalFee = 0;

                foreach (var item in fees)
                {
                    TotalFee = fees.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalFee = TotalFee;

                ViewBag.totalfee = TotalFee;
                TempData["fee"] = TotalFee;


            }
            else
            {
                ViewBag.msg = "No management fees.";
                ViewBag.totalfee = 0;
                TempData["fee"] = 0;
            }

            
        }

        void TotalManagementCost(DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeForAllPropertyByPeriod(sDate, eDate);

            //management fee and cost (only on-going monthly management fees)
            //
            model.FeeList = fees;
            if (model.FeeList.Count() != 0)
            {
                model.FeeList = fees;
                model.ManagementFeeAmount = fees.FirstOrDefault().ManagementFeeAmount;
                model.ManagementFeeId = fees.FirstOrDefault().ManagementFeeId;
                model.IsManagementFeeReceived = fees.FirstOrDefault().IsManagementFeeReceived;

                decimal? TotalFee = 0;

                foreach (var item in fees)
                {
                    TotalFee = fees.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalFee = TotalFee;

                ViewBag.totalfee = TotalFee;
                TempData["fee"] = TotalFee;


            }
            else
            {
                ViewBag.msg = "No management fees.";
                ViewBag.totalfee = 0;
                TempData["fee"] = 0;
            }


        }

        void TotalRentRevenue(int id, DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, sDate, eDate);

            //Rent Revenue
            //
            model.RentRevenueList = revenue;
            if (model.RentRevenueList.Count() != 0)
            {
                model.RentPaymentId = revenue.FirstOrDefault().RentPaymentId;
                model.RentPaymentMonth = revenue.FirstOrDefault().RentPaymentMonth;
                model.RentPamentYear = revenue.FirstOrDefault().RentPamentYear;
                model.RentPaymentAmount = revenue.FirstOrDefault().RentPaymentAmount;

                decimal? TotalRentRevenue = 0;

                foreach (var item in revenue)
                {
                    TotalRentRevenue = revenue.Sum(p => p.RentPaymentAmount);

                }

                model.TotalRentRevenue = TotalRentRevenue;

                ViewBag.totalrevenue = TotalRentRevenue;
                TempData["revenue"] = TotalRentRevenue;
            }
            else
            {
                ViewBag.msg3 = "No rent revenue.";
                ViewBag.totalrevenue = 0;
                TempData["revenue"] = 0;
            }
        }

        void TotalRentRevenue(DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryForAllPropertiesByPeriod(sDate, eDate);

            //Rent Revenue
            //
            model.RentRevenueList = revenue;
            if (model.RentRevenueList.Count() != 0)
            {
                model.RentPaymentId = revenue.FirstOrDefault().RentPaymentId;
                model.RentPaymentMonth = revenue.FirstOrDefault().RentPaymentMonth;
                model.RentPamentYear = revenue.FirstOrDefault().RentPamentYear;
                model.RentPaymentAmount = revenue.FirstOrDefault().RentPaymentAmount;

                decimal? TotalRentRevenue = 0;

                foreach (var item in revenue)
                {
                    TotalRentRevenue = revenue.Sum(p => p.RentPaymentAmount);

                }

                model.TotalRentRevenue = TotalRentRevenue;

                ViewBag.totalrevenue = TotalRentRevenue;
                TempData["revenue"] = TotalRentRevenue;
            }
            else
            {
                ViewBag.msg3 = "No retn revenue.";
                ViewBag.totalrevenue = 0;
                TempData["revenue"] = 0;
            }
        }


        //
        //End of Modules(functions)

        #endregion


        public ActionResult CostReportByProperty(AccountingCostViewModel model)
        {
            //int id = 1; for testing

            var property = _reportingRepository.AllReportingProperty();

            model.PropertyList = property.ToPropertyList(-1);            

            return View("CostReportByProperty", model);
        
        }

        public ActionResult ManagementCostReportByProperty(AccountingCostViewModel model)
        {
            //int id = 1; for testing

            var property = _reportingRepository.AllReportingProperty();

            model.PropertyList = property.ToPropertyList(-1);

            return View("ManagementCostReportByProperty", model);

        }

        public ActionResult GetMaintenanceCostsByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            if (id != 0)
            {
                IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
                IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

                TempData["start"] = startPeriod.ToString("M/d/yyyy");
                TempData["end"] = endPeriod.ToString("M/d/yyyy");

                //Property Info
                //
                //model.PropertyAddress = address.FirstOrDefault();

                model.PropertyId = property.FirstOrDefault().PropertyId;
                model.PropertyName = property.FirstOrDefault().PropertyName;
                model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

                model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
                model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
                model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
                model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
                model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
                model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
                model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
                model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

                TotalMaintenanceCost(id, startPeriod, endPeriod, model);

                // Set parameters to pass to view
                //
                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_MaintenanceCostReport", model);
            }
            else
            {
                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                TotalMaintenanceCost(startPeriod, endPeriod, model);

                return PartialView("_MaintenanceCostAllPropertyReport", model);
            }

            

            
        }


        public ActionResult GetMaintenanceCostsForAllProperty(DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            

            TempData["start"] = startPeriod.ToString("M/d/yyyy");
            TempData["end"] = endPeriod.ToString("M/d/yyyy");

           

            TotalMaintenanceCost(startPeriod, endPeriod, model);

            
            ViewBag.start = startPeriod.ToString("M/d/yyyy");
            ViewBag.end = endPeriod.ToString("M/d/yyyy");

            return PartialView("_MaintenanceCostAllPropertyReport", model);
        }


        public ActionResult GetManagementCostsByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            if (id != 0)
            {
                IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.GetPropertyDetails(id);
                IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

                TempData["start"] = startPeriod.ToString("M/d/yyyy");
                TempData["end"] = endPeriod.ToString("M/d/yyyy");

                //Property Info
                //
                //model.PropertyAddress = address.FirstOrDefault();

                model.PropertyId = property.FirstOrDefault().PropertyId;
                model.PropertyName = property.FirstOrDefault().PropertyName;
                model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

                model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
                model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
                model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
                model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
                model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
                model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
                model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
                model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

                TotalManagementCost(id, startPeriod, endPeriod, model);

                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_ManagementCostReport", model);
            }
            else
            {
                TotalManagementCost(startPeriod, endPeriod, model);

                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_ManagementCostAllPropertyReport", model); 
            }
            
        }

        //public ActionResult FeeReportbyProperty(int id, AccountingCostViewModel model)
        //{
        //    TotalManagementCost(id, model);

        //    decimal totalManagementFee = Convert.ToDecimal(model.TotalFee);
        //    ViewBag.totalExpenses = totalManagementFee;

        //    return View("FeeReportbyProperty", model);
        //}



        public ActionResult RentRevenueReportByProperty(AccountingCostViewModel model)
        {
            var property = _reportingRepository.AllReportingProperty();

            model.PropertyList = property.ToPropertyList(-1);

            return View("RentRevenueReportByProperty", model);
        }

        public ActionResult GetRentRevenueByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            if (id != 0)
            {
                IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
                IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

                TempData["start"] = startPeriod.ToString("M/d/yyyy");
                TempData["end"] = endPeriod.ToString("M/d/yyyy");

                //Property Info
                //
                //model.PropertyAddress = address.FirstOrDefault();

                model.PropertyId = property.FirstOrDefault().PropertyId;
                model.PropertyName = property.FirstOrDefault().PropertyName;
                model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

                model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
                model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
                model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
                model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
                model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
                model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
                model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
                model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

                TotalRentRevenue(id, startPeriod, endPeriod, model);

                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_RentRevenueReport", model);
            }
            else
            {
                TotalRentRevenue(startPeriod, endPeriod, model);

                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_RentRevenueAllPropertyReport", model);
            }
            
        }

        public ActionResult FinancialStatement(AccountingCostViewModel model)
        {
            var property = _reportingRepository.AllReportingProperty();

            model.PropertyList = property.ToPropertyList(-1);

            return View("FinancialStatement", model);
        }

        public ActionResult GetFinancialStatementByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            if (id != 0)
            {
                IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);

                IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

                IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

                IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

                IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

                IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

                TempData["start"] = startPeriod.ToString("M/d/yyyy");
                TempData["end"] = endPeriod.ToString("M/d/yyyy");

                //Property Info
                //
                //model.PropertyAddress = address.FirstOrDefault();

                model.PropertyId = property.FirstOrDefault().PropertyId;
                model.PropertyName = property.FirstOrDefault().PropertyName;
                model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

                model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
                model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
                model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
                model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
                model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
                model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
                model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
                model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

                TotalMaintenanceCost(id, startPeriod, endPeriod, model);

                TotalManagementCost(id, startPeriod, endPeriod, model);

                TotalRentRevenue(id, startPeriod, endPeriod, model);

                decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
                ViewBag.totalExpenses = TotalExpenses;

                decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
                ViewBag.totalRevenue = TotalRevenue;

                decimal NetIncome = TotalRevenue - TotalExpenses;
                ViewBag.totalNetIncome = NetIncome;


                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_FinancialStatement", model);
            }
            else
            {
                TotalMaintenanceCost(startPeriod, endPeriod, model);

                TotalManagementCost(startPeriod, endPeriod, model);

                TotalRentRevenue(startPeriod, endPeriod, model);

                decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
                ViewBag.totalExpenses = TotalExpenses;

                decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
                ViewBag.totalRevenue = TotalRevenue;

                decimal NetIncome = TotalRevenue - TotalExpenses;
                ViewBag.totalNetIncome = NetIncome;


                ViewBag.pId = id;
                ViewBag.start = startPeriod.ToString("M/d/yyyy");
                ViewBag.end = endPeriod.ToString("M/d/yyyy");

                return PartialView("_FinancialStatementAllProperties", model);
 
            }
            
        }

        public ActionResult BusinessReportByProperty()
        {
            return RedirectToAction("Construction", "Home");
        }

        public ActionResult ManagementAccountingByProperty()
        { 
            return RedirectToAction("Construction", "Home");
        }

        public ActionResult LoggingTracking()
        {
            return RedirectToAction("Construction", "Home");
        }

        private void GetAddressInfo(int id, DateTime sDate, DateTime eDate, AccountingCostViewModel model)
        {
            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;
            model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
            model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
            model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;
 
        }




        public ActionResult ExportReportPDF(int id, int tId, DateTime startDate, DateTime endDate)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            TempData["start"] = startDate.ToString("M/d/yyyy");
            TempData["end"] = endDate.ToString("M/d/yyyy");
          
            model.reportType = tId;
            model.StartPeriod = startDate;
            model.EndPeriod = endDate;

            //switch (tId) // tId is the type of report, but id is propertyId
            //{
            //    case 1:

                    if (id != 0)
                    {
                            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
                            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);
                            
                            model.PropertyId = property.FirstOrDefault().PropertyId;
                            model.PropertyName = property.FirstOrDefault().PropertyName;
                            model.PropertyDesc = property.FirstOrDefault().PropertyDesc;

                            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
                            model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
                            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
                            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
                            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
                            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
                            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
                            model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;

                        TotalMaintenanceCost(id, startDate, endDate, model);
                        TotalManagementCost(id, startDate, endDate, model);
                        TotalRentRevenue(id, startDate, endDate, model);

                    }
                    else
                    {
                        TotalMaintenanceCost( startDate, endDate, model);
                        TotalManagementCost(startDate, endDate, model);
                        TotalRentRevenue(startDate, endDate, model);
                    }

                //    break;
                //case 2:
                //    if (id != 0)
                //    {
                //        GetAddressInfo(id, startDate, endDate, model);

                //        TotalManagementCost(id, startDate, endDate, model);
                //    }
                //    else
                //    {
                //        TotalManagementCost(startDate, endDate, model);
                //    }
                //    break;
                //case 3:
                //    if (id != 0)
                //    {
                //        GetAddressInfo(id, startDate, endDate, model);

                //        TotalRentRevenue(id, startDate, endDate, model);
                //    }
                //    else
                //    {
                //        TotalRentRevenue(startDate, endDate, model);
                //    }
                //    break;
                //case 4:
                //    if (id != 0)
                //    {

                //    }
                //    else
                //    {

                //    }
                //    break;
                //default:
                //    break;
            //}
                    decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
                    model.totalExpenses = TotalExpenses;

                    decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
                    model.TotalRentRevenue = TotalRevenue;

                    decimal NetIncome = TotalRevenue - TotalExpenses;
                    model.totalNetIncome = NetIncome;


            return new RazorPDF.PdfResult(model, "ExportReportPDF");
            //return View("ExportReportPDF", model);
        }

        public ActionResult PDFTest()
        {
            //return View();
            ListingViewModel model = new ListingViewModel();

            return new RazorPDF.PdfResult(model, "PDFTest");
        }

    }

    

        #endregion



}
