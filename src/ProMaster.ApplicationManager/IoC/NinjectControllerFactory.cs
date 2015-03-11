using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ProMaster.Repository.UserProfile;
using ProMaster.DAL.UserProfile;
using ProMaster.DAL.Landlord;
using ProMaster.Repository.Landlord;
using ProMaster.DAL.Property;
using ProMaster.Repository.Property;
using ProMaster.DAL.Contract;
using ProMaster.Repository.Contract;
using ProMaster.DAL.Tenant;
using ProMaster.Repository.Tenant;
using ProMaster.DAL.Lease;
using ProMaster.Repository.Lease;
using ProMaster.DAL.Document;
using ProMaster.Repository.Document;
using ProMaster.DAL.Accounting;
using ProMaster.Repository.Accounting;
using ProMaster.DAL.Vendor;
using ProMaster.Repository.Vendor;
using ProMaster.DAL.StrataCouncil;
using ProMaster.Repository.StrataCouncil;
using ProMaster.Repository.Reporting;
using ProMaster.DAL.Reporting;
using ProMaster.DAL.ClientPortal;
using ProMaster.Repository.ClientPortal;
using ProMaster.DAL.Logging;
using ProMaster.Repository.Logging;


namespace ProMaster.ApplicationManager.IoC
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;


        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            _kernel.Bind<IPropertyOwnerRepository>().To<PropertyOwnerRepository>();
            _kernel.Bind<IPropertyRepository>().To<PropertyRepository>();
            _kernel.Bind<IManagementContractRepository>().To<ManagementContractRepository>();
            _kernel.Bind<ITenantRepository>().To<TenantRepository>();
            _kernel.Bind<ILeaseRepository>().To<LeaseRepository>();
            _kernel.Bind<IDocumentRepository>().To<DocumentRepository>();
            _kernel.Bind<IAccountingRepository>().To<AccountingRepository>();
            _kernel.Bind<IVendorRepository>().To<VendorRepository>();
            _kernel.Bind<IStrataCouncilRepository>().To<StrataCouncilRepository>();
            _kernel.Bind<IReportingRepository>().To<ReportingRepository>();
            _kernel.Bind<IClientPortalRepository>().To<ClientPortalRepository>();
            _kernel.Bind<ILogReportingRepository>().To<LogReportingReposioty>();
        }
    }
}
