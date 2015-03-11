using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.Core.Tenant;
using ProMaster.Core.Tenant.ViewModels;

using ProMaster.DAL.Tenant;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.Repository.Tenant
{
    public class TenantRepository : ITenantRepository
    {
        readonly ProMasterTenantDataEntities _entities = new ProMasterTenantDataEntities();
        //ProMasterPropertyDataEntities entities2 = new ProMasterPropertyDataEntities();

        public IEnumerable<DisplayTenantViewModel> GetTenantDetailsById(int id)
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         //join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         //join term in entities.LeaseTerms on lease.LeaseTermId equals term.LeaseTermId
                         //join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.TenantId == id
                         select new DisplayTenantViewModel 
                         { 
                             TenantId = tenant.TenantId,
                             FistName = tenant.FirstName,
                             LastName = tenant.LastName,
                             AvartaImgUrl = tenant.UserAvartaImgUrl,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1,
                             ContactTelephone2= tenant.ContactTelephone2,
                             OnLineAccessEnabled = tenant.OnlineAccessEnbaled,
                             CreationDate = tenant.CreateDate,
                             UpdateDate = tenant.UpdateDate,
                             IsActive = tenant.IsActive,
                             UserName = tenant.UserName,

                             //PropertyId = property.PropertyId,
                             //PropertyName = property.PropertyName,
                             //PropertyDesc = property.PropertyDesc,
                             //PropertyBuildYear = property.PropertyBuildYear,

                             //LeaseId = lease.LeaseId,
                             //LeaseTitle = lease.LeaseTitle,
                             //LeaseDesc = lease.LeaseDesc,
                             //LeaseSignDate = lease.LeaseSignDate,
                             //LeaseAddedDate = lease.CreationDate,
                             //LeaseStartDate = lease.LeaseStartDate,
                             //LeaseEndDate = lease.LeaseEndDate,

                             //LeaseTerm = term.LeaaseTerm,
                             //RentAmount = lease.RentAmount,
                             //RentFrequency = lease.RentFrequency,
                             //DamageDepositAmount = lease.DamageDepositAmount,
                             //PetDepositAmount = lease.PetDepositAmount,
                             //Addendum = lease.Addendum


                         };

            return result;
        }


        public IEnumerable<DisplayTenantViewModel> GetActiveTenantDetailsById(int id)
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         join lease in _entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join term in _entities.LeaseTerms on lease.LeaseTermId equals term.LeaseTermId
                         join property in _entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.TenantId == id
                         select new DisplayTenantViewModel
                         {
                             TenantId = tenant.TenantId,
                             FistName = tenant.FirstName,
                             LastName = tenant.LastName,
                             AvartaImgUrl = tenant.UserAvartaImgUrl,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1,
                             ContactTelephone2 = tenant.ContactTelephone2,
                             OnLineAccessEnabled = tenant.OnlineAccessEnbaled,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyBuildYear = property.PropertyBuildYear,
                             PropertyImageUrl = property.PropertyImgUrl,
                             CreationDate = property.CreatedDate,
                             UpdateDate = property.UpdateDate,


                             LeaseId = lease.LeaseId,
                             LeaseTitle = lease.LeaseTitle,
                             LeaseDesc = lease.LeaseDesc,
                             LeaseStartDate = lease.LeaseStartDate,
                             LeaseEndDate = lease.LeaseEndDate,
                             LeaseSignDate = lease.LeaseSignDate,
                             RentAmount = lease.RentAmount,
                             DamageDepositAmount = lease.DamageDepositAmount,
                             LeaseTerm = term.LeaaseTerm

                         };

            return result;
        }


        public IEnumerable<Core.Tenant.Lease> LeaseById(int id)
        {
            //throw new NotImplementedException();
            return _entities.Leases.Where(l => l.LeaseId == id);
        }

        IEnumerable<Core.Tenant.Property> ITenantRepository.PropertyById(int id)
        {
            //throw new NotImplementedException();
            return _entities.Properties.Where(p => p.PropertyId == id);
        }

        public IEnumerable<Core.Tenant.Document> DocumentByTenantId(int id, int pid)
        {
            //throw new NotImplementedException();
            return _entities.Documents.Where(t => t.DocumentPrincipalId == id && t.DocumentPrincipalTypeId == pid);
        }


        public Core.Tenant.Tenant GetTenantById(int id)
        {
            //throw new NotImplementedException();
            return _entities.Tenants.Where(t => t.TenantId == id).FirstOrDefault();
        }

        public IEnumerable<Core.Tenant.Property> GetListedPropertyByPmId(int id)
        {
            //throw new NotImplementedException();
            return _entities.Properties.Where(s => s.RentalStatusId == 2 && s.PropertyManagerId == id);
        }



        public IEnumerable<Core.Tenant.Property> GetAllPropertyByPmId(int id)
        {
            //throw new NotImplementedException();
            return _entities.Properties.Where(s => s.PropertyManagerId == id);
        }



        public TenancyApplication ApplicationById(int id)
        {
            //throw new NotImplementedException();
            return _entities.TenancyApplications.FirstOrDefault(a => a.TenancyApplicationId == id);
        }


        public TenantScreenViewModel GetApplicationById(int id)
        {
            //throw new NotImplementedException();
            /*
            var result = from application in entities.TenancyApplications
                         //join creditReport in entities.CreditReports on application.TenancyApplicationId equals creditReport.TenancyApplicanionId
                         //join docChecklist in entities.DocumentCheckLists on application.TenancyApplicationId equals docChecklist.TenancyApplicanionId
                         join screen in entities.ScreenProcesses on application.TenancyApplicationId equals screen.TenancyApplicanionId
                         where application.TenancyApplicationId == id
                         select new TenantScreenViewModel
                         {
                             ApplicaitonDate = application.ApplicationSentDate,
                             ApplicantFirstName = application.ApplicantFirstName,
                             ApplicantLastName = application.ApplicantLastName,
                             ApplicantContactEmail = application.ApplicantContactEmail,
                             ApplicantContactTel = application.ApplicantContactTel,
                             ApplicantCurrentAddress = application.CurrentAddress,
                             ApplicantPreviousAddress = application.PreviousAddress,
                             ApplicantCurrentEmploerContact = application.CurrentEmployerContact,
                             ApplicantPreviousEmploerContact = application.PreviousEmployerContact,
                             ApplicantCreditScore = application.CreditReportScore,
                             ApplicantCurrentLandlorContact = application.CurrentLandlordContact,
                             ApplicantPreviousLandlorContact = application.PreviousLandlordContact,
                             ApplicantOtherIncome = application.CurrentMonthlyIncome,
                             NumberOfOccupantse = application.NumberOfTenant,
                             NumberOfChildren = application.NumberOfChildren,
                             IsApplicationActive = application.IsActive,
                             IsAuthorizedToCheckReference = application.IsAuthorizedForRefCheck,   
                             ApplicantEmploymentIncome = application.CurrentMonthlyIncome,
                             ApplicantSIN = application.ApplicantSIN,
                             ApplicationId = application.TenancyApplicationId,

                             CreditReportCheckStatus = screen.CreditReportCheckStatus,
                             IncomeCheckStatus = screen.RentToIncomeRatioCheckStatus,
                             IsScreenProcessCompleted = screen.IsScreenProcessCompleted,

                             CurrentLandLordReferenceCheckStatus = screen.PrevioustLandlordCheckStatus,
                             PreviousLandlordReferenceCheckStatus = screen.PrevioustLandlordCheckStatus,
                             PreviousEmployerReferenceCheckStatus = screen.PreviousEmploerCheckStatus,
                             CurrentEmployerReferenceCheckStatus = screen.CurrentEmploerCheckStatus

                             //IsAllDocumentsReady = docChecklist.doc

                         };
             */

            var result = from application in _entities.TenancyApplications
                         join property in _entities.Properties on application.PropertyId equals property.PropertyId
                         //join screen in entities.TenantScreenings on application.TenancyApplicationId equals screen.TeancyApplicationId -- this table is not being used anymore!!!
                         //join screen in entities.ScreenProcesses on application.TenancyApplicationId equals screen.TenancyApplicanionId
                         //join checkType in entities.ScreeningCheckTypes on screen.ScreeningCheckTypeId equals checkType.ScreeningCheckTypeId
                         //join checkStatus in entities.ScreeningCheckStatus on screen.ScreeningCheckStatusId equals checkStatus.ScreeningCheckStatusId
                         //join process in entities.ScreenProcesses on application.TenancyApplicationId equals process.TenancyApplicanionId
                         //join doclist in entities.DocumentCheckLists on application.TenancyApplicationId equals doclist.TenancyApplicanionId  -- Add later
                         where application.TenancyApplicationId == id
                         select new TenantScreenViewModel 
                         {
                             ApplicationId = application.TenancyApplicationId,
                             ApplicaitonDate = application.ApplicationSentDate,
                             ApplicantFirstName = application.ApplicantFirstName,
                             ApplicantLastName = application.ApplicantLastName,
                             ApplicantContactEmail = application.ApplicantContactEmail,
                             ApplicantContactTel = application.ApplicantContactTel,
                             ApplicantCurrentAddress = application.CurrentAddress,
                             ApplicantPreviousAddress = application.PreviousAddress,
                             ApplicantCurrentEmploerContact = application.CurrentEmployerContact,
                             ApplicantPreviousEmploerContact = application.PreviousEmployerContact,
                             ApplicantCreditScore = application.CreditReportScore,
                             ApplicantCurrentLandlorContact = application.CurrentLandlordContact,
                             ApplicantPreviousLandlorContact = application.PreviousLandlordContact,
                             ApplicantOtherIncome = application.CurrentMonthlyIncome,
                             NumberOfOccupantse = application.NumberOfTenant,
                             NumberOfChildren = application.NumberOfChildren,
                             IsApplicationActive = application.IsActive,
                             IsAuthorizedToCheckReference = application.IsAuthorizedForRefCheck,
                             ApplicantEmploymentIncome = application.CurrentMonthlyIncome,                            
                             ApplicantSIN = application.ApplicantSIN,//,
                             IsApplicationApproved = application.IsApproved,
                             ScreeningNotes = application.ScreeningNotes,
                             //Rental unit information
                             //
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             PropertyBuildYear = property.PropertyBuildYear

                             //Document Chekck List  -- to be added in processing app module
                             //
                             //IdentificationDocAvailable = doclist.IsIdentificationProvided,
                             //EmployerReferenceDocAvailable = doclist.IsEmployerReferenceProvided,
                             //LandlordReferenceDocAvailable = doclist.IsLandLordReferenceProvided,
                             //IncomeDocAvailable = doclist.IsIncomeStatementProvided,
                             //IsCreditReportAvailable = doclist.IsCreditReportProvided,
                             //DocumentCheckLiatCompletionDate = doclist.DocumentCheckListCompletionDate,



                             //Screen Process Update -- to be added in processing app module
                             //
                             //IncomeCheckStatus = screen.RentToIncomeRatioCheckStatus,     
                             //CurrentLandLordReferenceCheckStatus = screen.PrevioustLandlordCheckStatus,
                             //PreviousLandlordReferenceCheckStatus = screen.PrevioustLandlordCheckStatus,
                             //PreviousEmployerReferenceCheckStatus = screen.PreviousEmploerCheckStatus,
                             //CurrentEmployerReferenceCheckStatus = screen.CurrentEmploerCheckStatus,
                             //FinalScreeningStatus = screen.ScreenFinalStatus,
                             //CreditReportCheckStatus = screen.CreditReportCheckStatus,
                             //IsScreenProcessCompleted = screen.IsScreenProcessCompleted
                             
                         
                         };

            return result.FirstOrDefault();
        }



        int ITenantRepository.GetLeaseIdByTenant(int id)
        {
            //throw new NotImplementedException();
            if (_entities == null) return 0;
            var firstOrDefault = _entities.Tenants.FirstOrDefault(t => t.LeaseId == id);
            return firstOrDefault != null ? firstOrDefault.LeaseId : 0;
        }


        public void GetLeaseIdByTenant(int id)
        {
            throw new NotImplementedException();
        }


        public Core.Tenant.Tenant GetEmailByTenantUserName(string userName)
        {
            //throw new NotImplementedException();
            return _entities.Tenants.Where(u => u.UserName == userName).FirstOrDefault();
        }


        public IEnumerable<ManageDisplayViewModel> GetTenantList()
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         join lease in _entities.Leases on tenant.LeaseId equals lease.LeaseId
                         join property in _entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.LeaseId != 0
                         select new ManageDisplayViewModel                         
                         { 
                            TenantId = tenant.TenantId,
                            TenantFirstName = tenant.FirstName,
                            TenantLastName = tenant.LastName,
                            TenantContactEmail = tenant.ContactEmail,
                            TenantContactTelephone1 = tenant.ContactTelephone1,
                            TenantAddedDate = tenant.CreateDate,
                            OnLineAccessEnabled = tenant.OnlineAccessEnbaled,
                            AvartaImgUrl = tenant.UserAvartaImgUrl,
                            IsActive = tenant.IsActive
                            
                            //PropertyName = property.PropertyName,
                            //LeaseTitle = lease.LeaseTitle
                         };

            return result.OrderByDescending(d=>d.TenantAddedDate);
        }

        public IEnumerable<ManageDisplayViewModel> GetTenantCandidates()
        {

            var result = from tenant in _entities.Tenants
                         //join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         //join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.LeaseId == 0
                         select new ManageDisplayViewModel
                         {
                             TenantId = tenant.TenantId,
                             TenantFirstName = tenant.FirstName,
                             TenantLastName = tenant.LastName,
                             TenantContactEmail = tenant.ContactEmail,
                             TenantContactTelephone1 = tenant.ContactTelephone1,
                             TenantAddedDate = tenant.CreateDate,
                             OnLineAccessEnabled = tenant.OnlineAccessEnbaled,
                             AvartaImgUrl = tenant.UserAvartaImgUrl,
                             IsActive = tenant.IsActive

                             //PropertyName = property.PropertyName,
                             //LeaseTitle = lease.LeaseTitle
                         };

            return result;
        }

        public IEnumerable<ManageDisplayViewModel> GetActiveTenantList()
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         //join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         //join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.LeaseId != 0
                         select new ManageDisplayViewModel
                         {
                             TenantId = tenant.TenantId,
                             TenantFirstName = tenant.FirstName,
                             TenantLastName = tenant.LastName,
                             TenantContactEmail = tenant.ContactEmail,
                             TenantContactTelephone1 = tenant.ContactTelephone1,
                             TenantAddedDate = tenant.CreateDate,
                             OnLineAccessEnabled = tenant.OnlineAccessEnbaled,
                             AvartaImgUrl = tenant.UserAvartaImgUrl,
                             IsActive = tenant.IsActive

                             //PropertyName = property.PropertyName,
                             //LeaseTitle = lease.LeaseTitle
                         };

            return result;

        }

        public IEnumerable<ManageDisplayViewModel> GetAllTenantByPm()
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         //join lease in entities.Leases on tenant.LeaseId equals lease.LeaseId
                         //join property in entities.Properties on lease.PropertyId equals property.PropertyId
                         where tenant.LeaseId != 0
                         select new ManageDisplayViewModel
                         {
                             TenantId = tenant.TenantId,
                             TenantFirstName = tenant.FirstName,
                             TenantLastName = tenant.LastName,
                             TenantContactEmail = tenant.ContactEmail,
                             TenantContactTelephone1 = tenant.ContactTelephone1,
                             TenantAddedDate = tenant.CreateDate,
                             OnLineAccessEnabled = tenant.OnlineAccessEnbaled,
                             IsActive = tenant.IsActive

                             //PropertyName = property.PropertyName,
                             //LeaseTitle = lease.LeaseTitle
                         };

            return result;
        }

        public IEnumerable<Core.Tenant.Tenant> GetTenantsByLeaseId(int id)
        {
            //throw new NotImplementedException();
            return _entities.Tenants.Where(l => l.LeaseId == id && l.IsActive);
        }




        public IEnumerable<Core.Tenant.Tenant> GetNewTenantsList()
        {
            //throw new NotImplementedException();
            return _entities.Tenants.Where(a => a.IsActive == false);
        }


        public ScreenProcess GetScreeenProcessByAppId(int id)
        {
            //throw new NotImplementedException();
            return _entities.ScreenProcesses.Where(s => s.TenancyApplicanionId == id).FirstOrDefault();
        }


        public IEnumerable<TenantScreenViewModel> GetCheckStatusByApplicantId(int id)
        {
            //throw new NotImplementedException();
            var result = from type in _entities.ScreeningCheckTypes
                         //join status in entities.ScreeningCheckStatus on screen.ScreeningCheckStatusId equals status.ScreeningCheckStatusId
                         join screen in _entities.TenantScreenings on type.ScreeningCheckTypeId equals screen.ScreeningCheckTypeId
                         where screen.TeancyApplicationId == id
                         select new TenantScreenViewModel
                         {
                             ScreenCheckType = type.ScreeningCheckTypeName,
                             ScreenCheckTypeId = type.ScreeningCheckTypeId
                         };

            return result;
        }

        public IEnumerable<TenantScreenViewModel> GetCheckTypeBypplicantId(int id)
        {
            //throw new NotImplementedException();
            var result = from status in _entities.ScreeningCheckStatus
                         //join status in entities.ScreeningCheckStatus on screen.ScreeningCheckStatusId equals status.ScreeningCheckStatusId
                         join screen in _entities.TenantScreenings on status.ScreeningCheckStatusId equals screen.ScreeningCheckStatusId
                         where screen.TeancyApplicationId == id
                         select new TenantScreenViewModel
                         {
                             ScreenCheckStatus = status.ScreeningCheckStatusName,
                             ScreenCheckStatusId = status.ScreeningCheckStatusId
                         };

            return result;
        }


        public IEnumerable<TenantScreenViewModel> ScreeningById(int id)
        {
            //throw new NotImplementedException();
            var result = from screen in _entities.TenantScreenings
                         join type in _entities.ScreeningCheckTypes on screen.ScreeningCheckTypeId equals type.ScreeningCheckTypeId
                         join status in _entities.ScreeningCheckStatus on screen.ScreeningCheckStatusId equals status.ScreeningCheckStatusId
                         where screen.TeancyApplicationId == id
                         select new TenantScreenViewModel 
                         { 
                             ApplicationId = screen.TeancyApplicationId,
                             ScreenCheckTypeId = screen.ScreeningCheckTypeId,
                             ScreenCheckType = type.ScreeningCheckTypeName,
                             ScreenCheckStatusId = screen.ScreeningCheckStatusId,
                             ScreenNotes = screen.ScreenNotes,
                             ScreenCheckStatus = status.ScreeningCheckStatusName                             
                         };

            return result;

        }


        public IEnumerable<DisplayTenantViewModel> GetRentPaymentLogByTenant(int id)
        {
            //throw new NotImplementedException();
            var result = from payment in _entities.RentPayments
                         join method in _entities.RentPaymentMethods on payment.RentPaymentMethodId equals method.RentPaymentMethodId
                         where payment.TenantId == id
                         select new DisplayTenantViewModel
                         {
                             RentPaymentId = payment.RentPaymentId,
                             TenantId = payment.TenantId,
                             RentReciptMonth = payment.RentPaymentMonth,
                             RentAmount = payment.RentPaidAmount,
                             PaymentDate = payment.RentPaymentDate,
                             PaymentMoth = payment.RentPaymentMonth,
                             PaymentYear = payment.RentPaymentYear,
                             NumberOfLatePayment = payment.NumberOfLatePayment,
                             Notes = payment.Notes,
                             RentOnTime = payment.RentIsPaidOntime
                         };

            return result;
        }

        public int GetNumberOfLatePayment(int id)
        {
            return _entities.RentPayments.Where(o => o.TenantId == id && o.RentIsPaidOntime == false).Count();            
        }


        public TenantScreening ScreenProcessByAppId(int id, int pId)
        {
            //throw new NotImplementedException();
            return _entities.TenantScreenings.Where(a => a.TeancyApplicationId == id && a.ScreeningCheckTypeId == pId).FirstOrDefault();
        }

        //public IEnumerable<TenantScreenViewModel> GetScreenTypeList(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<TenantScreenViewModel> GetScreenStatusList(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<TenantScreening> ScreenByAppId(int id)
        {
            //throw new NotImplementedException();
            return _entities.TenantScreenings.Where(a => a.TeancyApplicationId == id);
        }

        public DocumentCheckList GetDocumentCheckListByAppId(int id)
        {
            //throw new NotImplementedException();
            return _entities.DocumentCheckLists.Where(a => a.TenancyApplicanionId == id).FirstOrDefault();
        }


        public IEnumerable<TenantScreenViewModel> DocCheckListByAppId(int id)
        {
            //throw new NotImplementedException();
            var result = from checklist in _entities.DocumentCheckLists
                         where checklist.TenancyApplicanionId == id
                         select new TenantScreenViewModel
                         {
                             IsCreditReportAvailable = checklist.IsCreditReportProvided,
                             IncomeDocAvailable = checklist.IsIncomeStatementProvided,
                             IdentificationDocAvailable = checklist.IsIdentificationProvided,
                             EmployerReferenceDocAvailable = checklist.IsEmployerReferenceProvided,
                             LandlordReferenceDocAvailable = checklist.IsLandLordReferenceProvided,
                             DocumentCheckLiatCompletionDate = checklist.DocumentCheckListCompletionDate
                         };

            return  result;
        }


        public IEnumerable<ScreeningCheckStatu> GetScreenStatus()
        {
            //throw new NotImplementedException();
            return _entities.ScreeningCheckStatus;
        }



        public TenantScreening GetScreeningApp(int id)
        {
            //throw new NotImplementedException();
            return _entities.TenantScreenings.Where(a => a.TeancyApplicationId == id).FirstOrDefault();
            //var result = from screen in entities.TenantScreenings
            //             join application in entities.TenancyApplications on screen.TeancyApplicationId equals application.TenancyApplicationId
            //             where application.TenancyApplicationId == id
            //             select new TenantScreening
            //             {
            //                 TenantScreeningId = screen.TenantScreeningId,
            //                 TeancyApplicationId = application.TenancyApplicationId,
            //                 ScreeningCheckTypeId = screen.ScreeningCheckTypeId,
            //                 ScreeningCheckStatusId = screen.ScreeningCheckStatusId,
                             
            //             };
            //return result.FirstOrDefault();
        }


        public TenantScreening GetScreenStatus(int id, int tId)
        {
            //throw new NotImplementedException();
            return _entities.TenantScreenings.Where(a => a.TeancyApplicationId == id && a.ScreeningCheckTypeId == tId).FirstOrDefault();
        }

        public string GetScreenNotes(int id, int tId)
        {
            //throw new NotImplementedException();
            if (_entities == null) return null;
            var firstOrDefault = _entities.TenantScreenings.FirstOrDefault(a => a.TeancyApplicationId == id && a.ScreeningCheckTypeId == tId);
            return firstOrDefault != null ? firstOrDefault.ScreenNotes : null;
        }

        public IEnumerable<SiteSearchViewModel> ShowAllTenantSearchResult(string searchString)
        {
            //throw new NotImplementedException();
            var result = from tenant in _entities.Tenants
                         where tenant.FirstName.ToUpper().Contains(searchString.ToUpper()) || tenant.LastName.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel 
                         {
                             TenantAddDate = tenant.CreateDate,
                             TenantId = tenant.TenantId,
                             FirstName = tenant.FirstName,
                             LastName = tenant.LastName,
                             ContactEmail = tenant.ContactEmail,
                             ContactTelephone1 = tenant.ContactTelephone1
 
                         };

            return result;
        }


        public IEnumerable<TenantScreenViewModel> GatActiveApplications()
        {
            //throw new NotImplementedException();
            var result = from apps in _entities.TenancyApplications
                         join property in _entities.Properties on apps.PropertyId equals property.PropertyId
                         where apps.IsActive
                         select new TenantScreenViewModel 
                         {
                             ApplicationId = apps.TenancyApplicationId,
                             ApplicantFirstName = apps.ApplicantFirstName,
                             ApplicantLastName = apps.ApplicantLastName,
                             ApplicantSIN = apps.ApplicantSIN,
                             ApplicantContactTel = apps.ApplicantContactTel,
                             ApplicantContactEmail = apps.ApplicantContactEmail,
                             ApplicaitonDate = apps.ApplicationSentDate,
                             NumberOfOccupantse = apps.NumberOfTenant,
                             IsAuthorizedToCheckReference = apps.IsAuthorizedForRefCheck,

                             PropertyName = property.PropertyName
                         };

            return result;
        }



        public IEnumerable<TenantScreenViewModel> GatActiveApplications(int id)
        {
            //throw new NotImplementedException();
            var result = from apps in _entities.TenancyApplications
                         join property in _entities.Properties on apps.PropertyId equals property.PropertyId
                         where apps.IsActive && property.PropertyId == id
                         select new TenantScreenViewModel
                         {
                             ApplicationId = apps.TenancyApplicationId,
                             ApplicantFirstName = apps.ApplicantFirstName,
                             ApplicantLastName = apps.ApplicantLastName,
                             ApplicantSIN = apps.ApplicantSIN,
                             ApplicantContactTel = apps.ApplicantContactTel,
                             ApplicantContactEmail = apps.ApplicantContactEmail,
                             ApplicaitonDate = apps.ApplicationSentDate,
                             NumberOfOccupantse = apps.NumberOfTenant,
                             IsAuthorizedToCheckReference = apps.IsAuthorizedForRefCheck,

                             PropertyName = property.PropertyName
                         };

            return result;
        }

        public IEnumerable<TenantScreenViewModel> GatCompletedApplications(int id)
        {
            //throw new NotImplementedException();
            var result = from apps in _entities.TenancyApplications
                         join property in _entities.Properties on apps.PropertyId equals property.PropertyId
                         where apps.IsActive == false && property.PropertyId == id
                         select new TenantScreenViewModel
                         {
                             ApplicationId = apps.TenancyApplicationId,
                             ApplicantFirstName = apps.ApplicantFirstName,
                             ApplicantLastName = apps.ApplicantLastName,
                             ApplicantSIN = apps.ApplicantSIN,
                             ApplicantContactTel = apps.ApplicantContactTel,
                             ApplicantContactEmail = apps.ApplicantContactEmail,
                             ApplicaitonDate = apps.ApplicationSentDate,
                             NumberOfOccupantse = apps.NumberOfTenant,
                             IsAuthorizedToCheckReference = apps.IsAuthorizedForRefCheck,
                             IsApplicationApproved = apps.IsApproved,

                             PropertyName = property.PropertyName
                         };

            return result;
        }

        public IEnumerable<TenantScreenViewModel> GatCompletedApplications()
        {
            //throw new NotImplementedException();
            var result = from apps in _entities.TenancyApplications
                         join property in _entities.Properties on apps.PropertyId equals property.PropertyId
                         where apps.IsActive == false
                         select new TenantScreenViewModel 
                         {
                             ApplicationId = apps.TenancyApplicationId,
                             ApplicantFirstName = apps.ApplicantFirstName,
                             ApplicantLastName = apps.ApplicantLastName,
                             ApplicantSIN = apps.ApplicantSIN,
                             ApplicantContactTel = apps.ApplicantContactTel,
                             ApplicantContactEmail = apps.ApplicantContactEmail,
                             ApplicaitonDate = apps.ApplicationSentDate,
                             NumberOfOccupantse = apps.NumberOfTenant,
                             IsAuthorizedToCheckReference = apps.IsAuthorizedForRefCheck,
                             IsApplicationApproved = apps.IsApproved,

                             PropertyName = property.PropertyName
                         };

            return result;
        
        }



        public void AddTenant(Core.Tenant.Tenant tenant)
        {
            //throw new NotImplementedException();
            _entities.Tenants.AddObject(tenant);
        }

        //public void AddTenantScreen(ScreenProcess process)
        //{
        //    //throw new NotImplementedException();
        //    entities.ScreenProcesses.AddObject(process);
        //}

        public void AddTenantScreen(TenantScreening screen)
        {
            //throw new NotImplementedException();
            _entities.TenantScreenings.AddObject(screen);
        }

        public void AddDocCheckList(DocumentCheckList list)
        {
            //throw new NotImplementedException();
            _entities.DocumentCheckLists.AddObject(list);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            _entities.SaveChanges();
        }


























    }
}
