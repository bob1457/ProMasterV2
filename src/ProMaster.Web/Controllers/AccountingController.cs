using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using RazorPDF;
using ProMaster.Core.Accounting.ViewModels;
using ProMaster.DAL.Accounting;
using ProMaster.DAL.Property;
using ProMaster.Core.Property.ViewModels;

using ProMaster.Core.Property;

namespace ProMaster.Web.Controllers
{
    public class AccountingController : Controller
    {
        #region DI Configuration

        IAccountingRepository _accountingyRepository;
        IPropertyRepository _propertyRepository;


        public AccountingController(IAccountingRepository accountingyRepository, IPropertyRepository propertyRepository)  //depending on interface instead of implementation
        {
            _accountingyRepository = accountingyRepository;
            _propertyRepository = propertyRepository;
            //_propertyOwnerRepository = propertyOwnerRepository;
            //_contractRepository = contractRepository;
            //_tenantRepository = tenantRepository;
            //_leaseRepository = leaseRepository;
            //_vendorRepository = vendorRepository;
        }

        #endregion

        //
        // GET: /Accounting/

        public ActionResult Index()
        {
            int pmId = _propertyRepository.GetPmId(User.Identity.Name);

            DisplayDashboardViewModel dashBoard = new DisplayDashboardViewModel();

            dashBoard.MyProperty = _accountingyRepository.MyProperty(pmId);
            dashBoard.RentedProperty = _accountingyRepository.RentedProperty(pmId, 1);

            dashBoard.NumberOfPromperties = dashBoard.MyProperty.Count();
            
            

            return View("Index", dashBoard);
        }

        
        public ActionResult WorkOrdersForProperty(int id, AccountingCostViewModel model)  //id is property id
        {           

            IEnumerable<AccountingCostViewModel> orders = _accountingyRepository.GetWorkOrdersByProperty(id);
            
            IEnumerable<AccountingCostViewModel> costs = _accountingyRepository.GetOtherCostByProperty(id);

            IEnumerable<AccountingCostViewModel> fees = _accountingyRepository.GetManagementFeeByProperty(id);


            //Other cost
            //
            var accountingCostViewModels = costs as AccountingCostViewModel[] ?? costs.ToArray();
            model.CostList = accountingCostViewModels;
            if (!model.CostList.Any())
            {
                ViewBag.msg = "No other costs found.";
                //ViewBag.total = 0;
            }
            else
            {
                AccountingCostViewModel accountingCostViewModel = accountingCostViewModels.FirstOrDefault();
                if (accountingCostViewModel != null)
                    model.OtherCostId = accountingCostViewModel.OtherCostId;
                var firstOrDefault = accountingCostViewModels.FirstOrDefault();
                if (firstOrDefault != null)
                    model.OtherCostName = firstOrDefault.OtherCostName;
                var costViewModel = accountingCostViewModels.FirstOrDefault();
                if (costViewModel != null)
                    model.CostAmount = costViewModel.CostAmount;
                var orDefault = accountingCostViewModels.FirstOrDefault();
                if (orDefault != null)
                    model.IsPaid = orDefault.IsPaid;
                var viewModel = accountingCostViewModels.FirstOrDefault();
                if (viewModel != null)
                    model.CostAddDate = viewModel.CostAddDate;
                var @default = accountingCostViewModels.FirstOrDefault();
                if (@default != null)
                    model.UpdateDate = @default.UpdateDate;
                model.CostCategory = accountingCostViewModels.FirstOrDefault().CostCategory;
            }

            //work order and cost
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
                ViewBag.msg = "No work orders found.";
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
                    TotalFee = orders.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalRepairCost = TotalFee;

                ViewBag.totalfee = TotalFee;

            

            }
            else 
            {
                ViewBag.msg = "No fees found.";
                ViewBag.totalfee = 0;
            }
            
            return View("WorkOrdersForProperty", orders);
        } //For future use only

        public ActionResult RepairCost( AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> cost = _accountingyRepository.GetAllRepairCost();
            
            return View("RepairCost", model);
        }

        public ActionResult OtherCostsForProperty(int id, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> costs = _accountingyRepository.GetOtherCostByProperty(id);

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

            IEnumerable<AccountingCostViewModel> orders = _accountingyRepository.GetWorkOrdersByProperty(id);

            //decimal? repairCost = orders.FirstOrDefault().PaymentAmount;
            IEnumerable<AccountingCostViewModel> costs = _accountingyRepository.GetOtherCostByProperty(id);

            IEnumerable<AccountingCostViewModel> revenue = _accountingyRepository.GetRentPaymentHistory(id);

            IEnumerable<AccountingCostViewModel> fees = _accountingyRepository.GetManagementFeeByProperty(id);

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            //Other Costs
            //
            var accountingCostViewModels = costs as AccountingCostViewModel[] ?? costs.ToArray();
            model.CostList = accountingCostViewModels;
            if (model.CostList.Count() != 0)
            {
                model.OtherCostId = accountingCostViewModels.FirstOrDefault().OtherCostId;
                model.OtherCostName = accountingCostViewModels.FirstOrDefault().OtherCostName;
                model.CostAmount = accountingCostViewModels.FirstOrDefault().CostAmount;
                model.IsPaid = accountingCostViewModels.FirstOrDefault().IsPaid;
                model.CostAddDate = accountingCostViewModels.FirstOrDefault().CostAddDate;
                model.UpdateDate = accountingCostViewModels.FirstOrDefault().UpdateDate;
                model.CostCategory = accountingCostViewModels.FirstOrDefault().CostCategory;
                

                decimal? TotalOtherCost = 0;

                foreach (var item in accountingCostViewModels)
                {
                    TotalOtherCost = accountingCostViewModels.Sum(p => p.CostAmount);

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
            var costViewModels = orders as AccountingCostViewModel[] ?? orders.ToArray();
            model.WorkOrderList = costViewModels;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = costViewModels.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = costViewModels.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = costViewModels.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = costViewModels.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = costViewModels.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = costViewModels.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = costViewModels.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = costViewModels.FirstOrDefault().PaidDate;
                model.IsPaid = costViewModels.FirstOrDefault().IsPaid;
                model.PaymentAmount = costViewModels.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = costViewModels.FirstOrDefault().WorkOrderStatus;

                decimal? totalRepaireCost = 0;

                foreach (var item in costViewModels)
                {
                    totalRepaireCost = costViewModels.Sum(p => p.PaymentAmount);

                }

                model.TotalRepairCost = totalRepaireCost;

                ViewBag.total = totalRepaireCost;
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

                decimal? totalRentRevenue = 0;

                foreach (var item in revenue)
                {
                    totalRentRevenue = revenue.Sum(p => p.RentPaymentAmount);

                }

                model.TotalRentRevenue = totalRentRevenue;

                ViewBag.totalrevenue = totalRentRevenue;
            }
            else
            {
                ViewBag.msg3 = "No revenue found.";
                ViewBag.totalrevenue = 0;
            }


            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
            ViewBag.totalExpenses = TotalExpenses;

            decimal totalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = totalRevenue;

            decimal NetIncome = totalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;

            return View("CostAndRevenueByProperty", model);
           
        }

        public ActionResult FinancialStatementByProperty(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            IEnumerable<AccountingCostViewModel> orders = _accountingyRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> costs = _accountingyRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> revenue = _accountingyRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> fees = _accountingyRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

            TempData["start"] = startPeriod;
            TempData["end"] = endPeriod;
            //Property Info
            //
            var propertyAddresses = address as PropertyAddress[] ?? address.ToArray();
            model.PropertyAddress = propertyAddresses.FirstOrDefault();

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            model.PropertyAddressStreetNumber = propertyAddresses.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = propertyAddresses.FirstOrDefault().PropertySuiteNumber ;
            model.PropertyAddressCity = propertyAddresses.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = propertyAddresses.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = propertyAddresses.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = propertyAddresses.FirstOrDefault().PropertyCountry;

            
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

        public ActionResult ExportPDF1(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            IEnumerable<AccountingCostViewModel> orders = _accountingyRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> costs = _accountingyRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> revenue = _accountingyRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> fees = _accountingyRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);


            //Property Info
            //
            var propertyAddresses = address as PropertyAddress[] ?? address.ToArray();
            model.PropertyAddress = propertyAddresses.FirstOrDefault();

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            model.PropertyAddressStreetNumber = propertyAddresses.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = propertyAddresses.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = propertyAddresses.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = propertyAddresses.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = propertyAddresses.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = propertyAddresses.FirstOrDefault().PropertyCountry;


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
            }
            else
            {
                ViewBag.msg2 = "No work orders found.";
                ViewBag.total = 0;
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
            }
            else
            {
                ViewBag.msg1 = "No other costs found.";
                ViewBag.totalcost = 0;
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



            }
            else
            {
                ViewBag.msg = "No fees found.";
                ViewBag.totalfee = 0;
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
            }
            else
            {
                ViewBag.msg3 = "No revenue found.";
                ViewBag.totalrevenue = 0;
            }

            #endregion

            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
            ViewBag.totalExpenses = TotalExpenses;

            decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = TotalRevenue;

            decimal NetIncome = TotalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;

            return PartialView("ExportPDF", model);
        }

        public ActionResult ExportPdf()
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
            statement.StartDate = Convert.ToDateTime (TempData["start"]);
            statement.EndDate = Convert.ToDateTime(TempData["end"]);

            statement.TotalNetIncome = statement.TotalRentRevenue - statement.TotalOtherCosts - statement.TotalFees - statement.TotalRepairCosts;

            return new PdfResult(statement, "ExportPDF");
        }

        public ActionResult ManagementAccounting(int id)
        {
            ManagingAccountingViewModel model = new ManagingAccountingViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            //Property Info
            //
            //model.PropertyAddress = address.FirstOrDefault();

            model.PropertyId = property.FirstOrDefault().PropertyId;
            model.PropertyName = property.FirstOrDefault().PropertyName;

            var propertyAddresses = address as IList<PropertyAddress> ?? address.ToList();
            model.PropertyAddressStreetNumber = propertyAddresses.FirstOrDefault().PropertyStreet;
            model.PropertyAddressSuiteNumber = propertyAddresses.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressCity = propertyAddresses.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = propertyAddresses.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = propertyAddresses.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = propertyAddresses.FirstOrDefault().PropertyCountry;

            return View("ManagementAccounting", model);
        }

        public ActionResult GetManagementAccountingData(int id, DateTime startPeriod, DateTime endPeriod)
        {
            ManagingAccountingViewModel model = new ManagingAccountingViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);
            IEnumerable<PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            IEnumerable<ManagingAccountingViewModel> fees = _accountingyRepository.GetFeeByProperty(id, startPeriod, endPeriod);
            IEnumerable<ManagingAccountingViewModel> Events = _accountingyRepository.GetEventByProperty(id, startPeriod, endPeriod);

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

            //Event information
            //
            var managingAccountingViewModels = Events as ManagingAccountingViewModel[] ?? Events.ToArray();
            model.EventList = managingAccountingViewModels;

            if (model.EventList.Count() != 0)
            {
                model.EventId = managingAccountingViewModels.FirstOrDefault().EventId;
                model.EventTitle = managingAccountingViewModels.FirstOrDefault().EventTitle;
                model.MileageIncurred = managingAccountingViewModels.FirstOrDefault().MileageIncurred;
                model.VisitTimeSpent = managingAccountingViewModels.FirstOrDefault().VisitTimeSpent;
                model.EventDate = managingAccountingViewModels.FirstOrDefault().EventDate;

                decimal? TotalHours = 0;
                decimal? TotalMileage = 0;

                foreach (var item in managingAccountingViewModels)
                {
                    TotalMileage = managingAccountingViewModels.Sum(h => h.MileageIncurred);
                    TotalHours = managingAccountingViewModels.Sum(h => h.VisitTimeSpent);
                }

                model.TotalMiles = TotalMileage;
                model.TotalHours = TotalHours;

                ViewBag.totalHours = TotalHours;
                ViewBag.totalMiles = TotalMileage;

            }
            else
            {
                ViewBag.msg3 = "No event found.";
                model.TotalMiles = 0;
                model.TotalHours = 0;
            }



            //Fee information
            //
            var accountingViewModels = fees as IList<ManagingAccountingViewModel> ?? fees.ToList();
            model.FeeList = accountingViewModels;

            if (model.FeeList.Count() != 0)
            {
                model.ManagementFeeId = accountingViewModels.FirstOrDefault().ManagementFeeId;
                model.ManagementFeeAmount = accountingViewModels.FirstOrDefault().ManagementFeeAmount;
                model.ManagementFeeType = accountingViewModels.FirstOrDefault().ManagementFeeType;
                model.IsManagementFeeReceived = accountingViewModels.FirstOrDefault().IsManagementFeeReceived;
                model.ManagementFeeReceivedDate = accountingViewModels.FirstOrDefault().ManagementFeeReceivedDate;

                decimal? TotalFee = 0;

                foreach (var item in accountingViewModels)
                {
                    TotalFee = accountingViewModels.Sum(p => p.ManagementFeeAmount);

                }

                model.TotalFee = TotalFee;

                ViewBag.totalfee = TotalFee;
            }
            else
            {
                ViewBag.msg = "No fees found.";
                ViewBag.totalfee = 0;
            }



            return PartialView("_ManagementAccounting", model);
        }
    
    }
}


