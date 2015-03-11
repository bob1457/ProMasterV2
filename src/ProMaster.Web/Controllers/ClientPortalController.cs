using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ProMaster.Core.ClientPortal.ViewModels;
using ProMaster.Core.Property;
using ProMaster.DAL.ClientPortal;
using ProMaster.DAL.Landlord;
using ProMaster.DAL.Tenant;
using ProMaster.DAL.Contract;
using ProMaster.DAL.Reporting;
using ProMaster.DAL.Property;
using ProMaster.Core.Reporting.ViewModels;
using ProMaster.Core.Property.ViewModels;

namespace ProMaster.Web.Controllers
{
    public class ClientPortalController : Controller
    {
        #region DI Implementation
        //
        IClientPortalRepository _clientPortalRepository;
        IPropertyOwnerRepository _propertyOwnerRepository;
        ITenantRepository _tenantRepository;
        IManagementContractRepository _contractRepository;
        IReportingRepository _reportingRepository;
        IPropertyRepository _propertyRepository;

        public ClientPortalController(IClientPortalRepository clientPortalRepository, IPropertyOwnerRepository propertyOwnerRepository,
            ITenantRepository tenantRepository, IManagementContractRepository contractRepository, IReportingRepository reportingRepository, IPropertyRepository propertyRepository)
        {
            _clientPortalRepository = clientPortalRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _reportingRepository = reportingRepository;
            _propertyRepository = propertyRepository;
        }

        #endregion
        //
        // GET: /ClientPortal/

        [Authorize]
        public ActionResult Index()
        {
            DisplayClientPortalViewModel clientPortal = new DisplayClientPortalViewModel();

            string uName = User.Identity.Name;

            //string[] userRoles = Roles.GetRolesForUser(uName);

            DisplayClientPortalViewModel UserInfo;


            if(Roles.GetRolesForUser(uName).Contains("Tenant"))
            {
                int tenantId = _clientPortalRepository.GetTenantId(uName);

                ViewBag.TenantId = tenantId;
                ViewBag.ClientRole = "tenant";

                ViewBag.LandlordId = 1; //fake id to avoid javascript error

                UserInfo = _clientPortalRepository.GetTenantInfo(tenantId);

                DisplayClientPortalViewModel clientLease = _clientPortalRepository.GetLeaseByTenant(tenantId);
                if (clientLease == null) throw new ArgumentNullException("clientLease");

                clientPortal.LeaseId = clientLease.LeaseId;
                clientPortal.LeaseTitle = clientLease.LeaseTitle;
                clientPortal.LeaseDesc = clientLease.LeaseDesc;
                clientPortal.LeaseAddedDate = clientLease.LeaseAddedDate;
                clientPortal.LeaseStartDate = clientLease.LeaseStartDate;
                clientPortal.LeaseEndDate = clientLease.LeaseEndDate;
                clientPortal.LeaseSignDate = clientLease.LeaseSignDate;
                clientPortal.Addendum = clientLease.Addendum;
                clientPortal.IsLeaseActive = clientLease.IsLeaseActive;
                clientPortal.RentDueOn = clientLease.RentDueOn;
                clientPortal.RentAmount = clientLease.RentAmount;
                clientPortal.DamageDepositAmount = clientLease.DamageDepositAmount;
                clientPortal.PetDepositAmount = clientLease.PetDepositAmount;
                clientPortal.RentFrequency = clientLease.RentFrequency;
                clientPortal.LeaseTerm = clientLease.LeaseTerm;
                clientPortal.Notes = clientLease.Notes;

                clientPortal.PropertyName = clientLease.PropertyName;
                clientPortal.PropertyDesc = clientLease.PropertyDesc;
                clientPortal.PropertyBuildYear = clientLease.PropertyBuildYear;

                clientPortal.PaymentHisotry = _clientPortalRepository.GetRentPaymentHistory(tenantId);//RentPaymentHistory;

                clientPortal.DocumentList = _tenantRepository.DocumentByTenantId(tenantId, 4); //only lease contract/agreement or so

            }
            else
            {
                int landlordId = _clientPortalRepository.GetLandlordId(uName);
                UserInfo = _clientPortalRepository.GetLandlordInfo(landlordId);

                ViewBag.LandlordId = landlordId;                
                ViewBag.ClientRole = "owner";
                
                ViewBag.TenantId = 1; //fake information to solve js error in view

                clientPortal.PropertyList = _propertyOwnerRepository.GetPropertytByOwnerId(landlordId);

                var ownerPropertyList = _propertyOwnerRepository.GetPropertytByOwnerId(landlordId);
                clientPortal.OwnerPropertyList = ownerPropertyList.ToAllPropertyList(-1);

                clientPortal.ContractList = _propertyOwnerRepository.GetContractByPropertyId(landlordId);

                
                clientPortal.PaymentHisotry = _clientPortalRepository.GetRentPaymentHistory(clientPortal.PropertyId);

                var contractList = _contractRepository.ListAllContracts();
                clientPortal.OwnerContractList = contractList.ToContractList(-1);

                //IEnumerable<DisplayClientPortalViewModel> orders =

                clientPortal.WorkOrderList =  _clientPortalRepository.GetWorkOrdersByProperty(clientPortal.PropertyId); //orders;

                //ClientPortal.FeePaymentHisotry = _clientPortalRepository.GetFeeRevenueHistory(id);

            }

            clientPortal.ClientFirstName = UserInfo.ClientFirstName;
            clientPortal.ClientLastName = UserInfo.ClientLastName;
            clientPortal.ClientContactTel1 = UserInfo.ClientContactTel1;
            clientPortal.ClientContactTel2 = UserInfo.ClientContactTel2;
            clientPortal.ClientContactEmail = UserInfo.ClientContactEmail;
            clientPortal.ClientAvatarImgUrl = UserInfo.ClientAvatarImgUrl;
            clientPortal.ClientCreateDate = UserInfo.ClientCreateDate;


            return View("Index", clientPortal);
        }

        public ActionResult GetRentPaymentHistory(int id)
        {
            DisplayClientPortalViewModel clientPortal = new DisplayClientPortalViewModel();           

            clientPortal.PaymentHisotry = _clientPortalRepository.GetRentPaymentHistory(id);


            return PartialView("_RentPaymentHistory", clientPortal);
        }


        public ActionResult GetRentPaymentHistoryByPeriod(int id, DateTime sDate, DateTime eDate)
        {

            var clientPortal = new DisplayClientPortalViewModel
            {
                PaymentHisotry = _clientPortalRepository.GetRentPaymentHistoryByPeriod(id, sDate, eDate)
            };


            return PartialView("_RentPaymentHistory", clientPortal);
            
        }


        public ActionResult GetRentRevenueHistory(int id)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> revenueHistory = _clientPortalRepository.GetRentRevenueHistory(id);

            model.RentRevenueHisotry = revenueHistory;
            
            return PartialView("_OwnedPropertyList", model);

        }


        public ActionResult GetRentRevenueHistoryByPeriod(int id, DateTime sDate, DateTime eDate)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> RevenueHistory = _clientPortalRepository.GetRentRevenueHistoryByperiod(id, sDate, eDate);

            model.RentRevenueHisotry = RevenueHistory;

            return PartialView("_OwnedPropertyList", model);

        }

        public ActionResult GetFeePaymentHistory(int id)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> FeePaymentHistory = _clientPortalRepository.GetFeeRevenueHistory(id);

            model.FeePaymentHisotry = FeePaymentHistory;

            return PartialView("_FeePaymentList", model);
        }

        public ActionResult GetFeePaymentHistoryByPeriod(int id, DateTime sDate, DateTime eDate)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> feePaymentHistory = _clientPortalRepository.GetFeeRevenueHistoryByPeriod(id, sDate, eDate);

            model.FeePaymentHisotry = feePaymentHistory;

            return PartialView("_FeePaymentList", model);
        }


        

        public ActionResult GetWorkOrderList(int id)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> WorkOrderList = _clientPortalRepository.GetWorkOrdersByProperty(id);

            model.WorkOrderList = WorkOrderList;

            return PartialView("_WorkOrderList", model);
        }


        public ActionResult GetWorkOrderListByPeriod(int id, DateTime sDate, DateTime eDate)
        {
            DisplayClientPortalViewModel model = new DisplayClientPortalViewModel();

            IEnumerable<DisplayClientPortalViewModel> workOrderList = _clientPortalRepository.GetWorkOrdersByPropertyByPeriod(id, sDate, eDate);

            model.WorkOrderList = workOrderList;

            return PartialView("_WorkOrderList", model);
        }


        public ActionResult GetMonthlyFinancialStatement(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            ViewBag.id = id;
            ViewBag.StartDate = startPeriod;
            ViewBag.EndDate = endPeriod;


            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);

            var displayPropertyViewModels = property as IList<DisplayPropertyViewModel> ?? property.ToList();
            var displayPropertyViewModel = displayPropertyViewModels.FirstOrDefault();
            if (displayPropertyViewModel != null)
            {
                var address = _propertyRepository.GetAddressById(displayPropertyViewModel.PropertyAddressId);

                var propertyAddresses = address as IList<PropertyAddress> ?? address.ToList();
                model.PropertyAddressSuiteNumber = propertyAddresses.FirstOrDefault().PropertySuiteNumber;
                model.PropertyAddressStreetNumber = propertyAddresses.FirstOrDefault().PropertyNumber;
                model.PropertyAddressStreetName = propertyAddresses.FirstOrDefault().PropertyStreet;
                model.PropertyAddressCity = propertyAddresses.FirstOrDefault().PropertyCity;
                model.PropertyAddressProvState = propertyAddresses.FirstOrDefault().PropertyStateProvince;
                model.PropertyAddressPostZipCode = propertyAddresses.FirstOrDefault().PropertyZipPostCode;
                model.PropertyAddressCountry = propertyAddresses.FirstOrDefault().PropertyCountry;
            }
            model.PropertyImageUrl = displayPropertyViewModels.FirstOrDefault().PropertyImageUrl;
            model.PropertyDesc = displayPropertyViewModels.FirstOrDefault().PropertyDesc;
            model.PropertyName = displayPropertyViewModels.FirstOrDefault().PropertyName;
            

            

            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

            TotalMaintenanceCost(id, startPeriod, endPeriod, model);

            TotalManagementCost(id, startPeriod, endPeriod, model);

            TotalRentRevenue(id, startPeriod, endPeriod, model);

            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
             ViewBag.totalExpenses = TotalExpenses;
            model.totalExpenses = TotalExpenses;

            decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = TotalRevenue;
            model.TotalRentRevenue = TotalRevenue;

            decimal NetIncome = TotalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;
            model.totalNetIncome = NetIncome;


            return PartialView("_Report", model);
        }

        public ActionResult ExportStatementPDF(int id, DateTime startPeriod, DateTime endPeriod)
        {
            AccountingCostViewModel model = new AccountingCostViewModel();

            IEnumerable<DisplayPropertyViewModel> property = _propertyRepository.PropertyDetails(id);

            IEnumerable<Core.Property.PropertyAddress> address = _propertyRepository.GetAddressById(property.FirstOrDefault().PropertyAddressId);

            model.PropertyAddressSuiteNumber = address.FirstOrDefault().PropertySuiteNumber;
            model.PropertyAddressStreetNumber = address.FirstOrDefault().PropertyNumber;
            model.PropertyAddressStreetName = address.FirstOrDefault().PropertyStreet;
            model.PropertyAddressCity = address.FirstOrDefault().PropertyCity;
            model.PropertyAddressProvState = address.FirstOrDefault().PropertyStateProvince;
            model.PropertyAddressPostZipCode = address.FirstOrDefault().PropertyZipPostCode;
            model.PropertyAddressCountry = address.FirstOrDefault().PropertyCountry;
            model.PropertyImageUrl = property.FirstOrDefault().PropertyImageUrl;
            model.PropertyDesc = property.FirstOrDefault().PropertyDesc;
            model.PropertyName = property.FirstOrDefault().PropertyName;




            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

            TotalMaintenanceCost(id, startPeriod, endPeriod, model);

            TotalManagementCost(id, startPeriod, endPeriod, model);

            TotalRentRevenue(id, startPeriod, endPeriod, model);

            decimal TotalExpenses = Convert.ToDecimal(model.TotalRepairCost) + Convert.ToDecimal(model.TotalOtherCost) + Convert.ToDecimal(model.TotalFee);
            ViewBag.totalExpenses = TotalExpenses;
            model.totalExpenses = TotalExpenses;

            decimal TotalRevenue = Convert.ToDecimal(model.TotalRentRevenue);
            ViewBag.totalRevenue = TotalRevenue;
            model.TotalRentRevenue = TotalRevenue;

            decimal NetIncome = TotalRevenue - TotalExpenses;
            ViewBag.totalNetIncome = NetIncome;
            model.totalNetIncome = NetIncome;

            return new RazorPDF.PdfResult(model, "ExportStatementPDF");
        }
        
        void TotalMaintenanceCost(int id, DateTime startPeriod, DateTime endPeriod, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> orders = _reportingRepository.GetWorkOrdersByPropertyByPeriod(id, startPeriod, endPeriod);
            IEnumerable<AccountingCostViewModel> costs = _reportingRepository.GetOtherCostByPropertyByPeriod(id, startPeriod, endPeriod);

            decimal? TotalMainternanceCosts = 0;

            //Repair Costs
            //
            var accountingCostViewModels = orders as IList<AccountingCostViewModel> ?? orders.ToList();
            model.WorkOrderList = accountingCostViewModels;
            if (model.WorkOrderList.Count() != 0)
            {
                model.WorkOrderId = accountingCostViewModels.FirstOrDefault().WorkOrderId;
                model.WorkOrderName = accountingCostViewModels.FirstOrDefault().WorkOrderName;
                model.WorkOrderStatus = accountingCostViewModels.FirstOrDefault().WorkOrderStatus;
                model.InvoiceAmount = accountingCostViewModels.FirstOrDefault().InvoiceAmount;
                model.InvoiceDate = accountingCostViewModels.FirstOrDefault().InvoiceDate;
                model.WorkOrderCategory = accountingCostViewModels.FirstOrDefault().WorkOrderCategory;
                model.WorkOrderTypeName = accountingCostViewModels.FirstOrDefault().WorkOrderTypeName;
                model.PaidDate = accountingCostViewModels.FirstOrDefault().PaidDate;
                model.IsPaid = accountingCostViewModels.FirstOrDefault().IsPaid;
                model.PaymentAmount = accountingCostViewModels.FirstOrDefault().PaymentAmount;
                model.WorkOrderStatus = accountingCostViewModels.FirstOrDefault().WorkOrderStatus;

                decimal? TotalRepaireCost = 0;

                foreach (var item in accountingCostViewModels)
                {
                    TotalRepaireCost = accountingCostViewModels.Sum(p => p.PaymentAmount);

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
                //TotalMainternanceCosts = 0;
            }
        }

        void TotalManagementCost(int id, DateTime startPeriod, DateTime endPeriod, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> fees = _reportingRepository.GetManagementFeeByPropertyByPeriod(id, startPeriod, endPeriod);

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
                //model.TotalFee = 0;
            }
        }


        void TotalRentRevenue(int id, DateTime startPeriod, DateTime endPeriod, AccountingCostViewModel model)
        {
            IEnumerable<AccountingCostViewModel> revenue = _reportingRepository.GetRentPaymentHistoryByPeriod(id, startPeriod, endPeriod);

            //Rent Revenue
            //
            var accountingCostViewModels = revenue as AccountingCostViewModel[] ?? revenue.ToArray();
            model.RentRevenueList = accountingCostViewModels;
            if (model.RentRevenueList.Count() != 0)
            {
                model.RentPaymentId = accountingCostViewModels.FirstOrDefault().RentPaymentId;
                model.RentPaymentMonth = accountingCostViewModels.FirstOrDefault().RentPaymentMonth;
                model.RentPamentYear = accountingCostViewModels.FirstOrDefault().RentPamentYear;
                model.RentPaymentAmount = accountingCostViewModels.FirstOrDefault().RentPaymentAmount;

                decimal? TotalRentRevenue = 0;

                foreach (var item in accountingCostViewModels)
                {
                    TotalRentRevenue = accountingCostViewModels.Sum(p => p.RentPaymentAmount);

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
               // model.TotalRentRevenue = 0;
            }
        }



        //public ActionResult MyLease() //For tenant: property, lease
        //{

        //    return View();
        //}

        //public ActionResult MyContract()  //For landlord: management contract
        //{

        //    return View();
        //}




/*

        #region others

        //
        // GET: /ClientPortal/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ClientPortal/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ClientPortal/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ClientPortal/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ClientPortal/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ClientPortal/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ClientPortal/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion
 */

    }
}
