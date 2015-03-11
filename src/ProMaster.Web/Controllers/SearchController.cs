using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ProMaster.DAL.Property;
using ProMaster.DAL.Landlord;
using ProMaster.DAL.Contract;
using ProMaster.DAL.Tenant;
using ProMaster.DAL.Lease;
using ProMaster.DAL.Vendor;
using ProMaster.DAL.StrataCouncil;
using ProMaster.DAL.Document;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.Web.Controllers
{
    public class SearchController : Controller
    {

        #region DI Configuration

        IPropertyRepository _propertyRepository;
        IPropertyOwnerRepository _propertyOwnerRepository;
        IManagementContractRepository _contractRepository;
        ITenantRepository  _tenantRepository;
        ILeaseRepository _leaseRepository;
        IVendorRepository _vendorRepository;
        IStrataCouncilRepository _strataRepository;
        IDocumentRepository _documentRepository;

        public SearchController(IPropertyRepository propertyRepository, IPropertyOwnerRepository propertyOwnerRepository, 
            IManagementContractRepository contractRepository, ITenantRepository tenantRepository, ILeaseRepository leaseRepository,
            IVendorRepository vendorRepository, IStrataCouncilRepository strataRepository, IDocumentRepository documentRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "manage";

            _propertyRepository = propertyRepository;
            _propertyOwnerRepository = propertyOwnerRepository;
            _contractRepository = contractRepository;
            _tenantRepository = tenantRepository;
            _leaseRepository = leaseRepository;
            _vendorRepository = vendorRepository;
            _strataRepository = strataRepository;
            _documentRepository = documentRepository;
        }

        #endregion

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        #region Site Search

        [HttpPost]
        public ActionResult SiteSearch(string searchText)
        {

            if(User.Identity.IsAuthenticated && User.IsInRole("PropertyManager"))
            { 
                SiteSearchViewModel searchModel = new SiteSearchViewModel();

                IEnumerable<SiteSearchViewModel> properties = _propertyRepository.ShowPropertySearchResults(searchText);
                IEnumerable<SiteSearchViewModel> tenants = _tenantRepository.ShowAllTenantSearchResult(searchText);

                IEnumerable<SiteSearchViewModel> leases = _leaseRepository.ShowAllLeaseResults(searchText);

                IEnumerable<SiteSearchViewModel> contracts = _contractRepository.ShowAllContractResult(searchText);

                IEnumerable<SiteSearchViewModel> vendors = _tenantRepository.ShowAllTenantSearchResult(searchText);
                IEnumerable<SiteSearchViewModel> councils = _tenantRepository.ShowAllTenantSearchResult(searchText);
                IEnumerable<SiteSearchViewModel> landlords = _propertyOwnerRepository.ShowAllLandlordResult(searchText);

                searchModel.ProopertyResults = properties;
                searchModel.TenantResults = tenants;
                searchModel.ContractResults = contracts;
                searchModel.LeaseResults = leases;
                searchModel.LandlordResults = landlords;
                searchModel.VendorResults = vendors;
                searchModel.CouncilResults = councils;


                return View("SiteSearch", searchModel);
            }
            //Search public area - to be implemented

            return RedirectToAction("Construction", "Home");
        }

        #endregion

        #region Document Search

        public ActionResult DocumentSearch(string keyWord, int type, int category, DateTime lastPeriod)
        {
            DocSearchViewModel searchModel = new DocSearchViewModel();

            if(keyWord == null)
            {
                keyWord = "";
            }


            var docs = _documentRepository.GetDocumentsByDocSearch(keyWord, type, category, lastPeriod);

            searchModel.Documents = docs;

            return PartialView("_DocSearch", searchModel);
        }

        #endregion
    }
}
