using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Contract;
using ProMaster.Core.Contract;
using ProMaster.Core.Contract.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;


namespace ProMaster.Repository.Contract
{
    public class ManagementContractRepository : IManagementContractRepository
    {
        ProMasterManagementContractDataEntities entities = new ProMasterManagementContractDataEntities();

        public IEnumerable<ManagementFeeFrequency> GetFeeFrequency()
        {
            //throw new NotImplementedException();
            return entities.ManagementFeeFrequencies;
        }

        public IEnumerable<Core.Contract.Property> GetMyProperty(int pmId)
        {
            //throw new NotImplementedException();
            //var result = from property in entities.Properties 
            //             where property.PropertyManagerId == pmId
            //             select new ProMaster.Core.Contract.Property
            //             {
            //                 PropertyId = property.PropertyId,
            //                 PropertyName = property.PropertyName                                                
            //             };

            //return result;
            return entities.Properties.Where(p=>p.PropertyManagerId == pmId && p.PropertyMgmntlStatusId == 0);
        }


        public Core.Contract.Property GetManagedProperty(int id)
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(p => p.PropertyId == id).FirstOrDefault();
        }


        public IEnumerable<Core.Contract.Property> GetPropertyByOwner(int oId)
        {
            //throw new NotImplementedException();
            return entities.Properties.Where(o => o.PropertyOwnerId == oId);
        }


        public IEnumerable<ManagementContract> ListAllContracts()
        {
            //throw new NotImplementedException();
            return entities.ManagementContracts;
        }


        public IEnumerable<ManageDisplayViewModel> GetMyContract(int pmId)
        {
            //throw new NotImplementedException();
            var result = from contract in entities.ManagementContracts
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         join frequency in entities.ManagementFeeFrequencies on contract.ManagementFeeFrequencyId equals frequency.ManagementFeeFrequencyId
                         where property.PropertyManagerId == pmId && contract.IsActive
                         select new ManageDisplayViewModel 
                         { 
                             ContractId = contract.ManagementContractId,
                             ContractTitle = contract.ManagementContractTitile,
                             StartDate = contract.StartDate,
                             EndDate = contract.EndDate,
                             SignDate = contract.ContractSignDate,
                             ListingFeeScale = contract.ListingFeeScale,
                             FeeScale = contract.ManagementFeeScale,

                             ManagementFeeFrequency = frequency.ManagementFeeFrequency1,
                             PropertyName = property.PropertyName,

                             LandlordId = landlord.PropertyOwnerId,
                             LandlordName = landlord.LastName + ", " + landlord.FirstName,

                             CreateDate = contract.CreateDate,
                             UpdateDate = contract.UpdateDate

                         };
            return result.OrderByDescending(d=>d.CreateDate);
        }

        public IEnumerable<DisplayContractViewModel> GeteContractDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from contract in entities.ManagementContracts
                         join frequency in entities.ManagementFeeFrequencies on contract.ManagementFeeFrequencyId equals frequency.ManagementFeeFrequencyId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         where contract.ManagementContractId == id
                         select new DisplayContractViewModel
                         {
                             ContractId = contract.ManagementContractId,
                             ContractTitle = contract.ManagementContractTitile,
                             StartDate = contract.StartDate,
                             EndDate = contract.EndDate,
                             FeeScale = contract.ManagementFeeScale,
                             ListingFeeScale = contract.ListingFeeScale,
                             ManagementFeeFrequency = frequency.ManagementFeeFrequency1,
                             CreateDate = contract.CreateDate,
                             UpdateDate = contract.UpdateDate,
                             SignDate = contract.ContractSignDate,

                             LandlordId = landlord.PropertyOwnerId,
                             LandlordFirstName = landlord.LastName,
                             LandlordLastName = landlord.FirstName,
                             LandlordEmail = landlord.ContactEmail,
                             LandlordTelepone = landlord.ContactTelephone1,

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName
                             
                         };
            return result;
        }

        public CreateContractViewModel GetContractById(int id)
        {
            //throw new NotImplementedException();
            var result = from contract in entities.ManagementContracts
                         where contract.ManagementContractId == id
                         
                         select new CreateContractViewModel 
                         { 
                             ContractId = contract.ManagementContractId,
                             ContractTitle = contract.ManagementContractTitile,
                             StartDate = contract.StartDate,
                             EndDate = contract.EndDate,
                             SignDate = contract.ContractSignDate,
                             FeeFrequencyId = contract.ManagementFeeFrequencyId,
                             ListingFeeScale = contract.ListingFeeScale,
                             FeeScale = contract.ManagementFeeScale,
                             CreateDate = contract.CreateDate,
                             UpdateDAte = contract.UpdateDate
                         };

            return result.FirstOrDefault();
            
        }

        public IEnumerable<DisplayContractViewModel> GetFeeHistoryByContract(int id)
        {
            //throw new NotImplementedException();
            var result = from fee in entities.ManagementFees
                         join type in entities.ManagementFeeTypes on fee.ManagementFeeTypeId equals type.ManagementFeeTypeId
                         where fee.ManagementContractId == id
                         select new DisplayContractViewModel
                         {
                             FeePaymentId = fee.ManagementFeeId,
                             ContractId = fee.ManagementContractId,
                             FeeMonth = fee.ManagementFeeMonth,
                             FeeYear = fee.ManagementFeeYear,
                             FeePaidAmount = fee.ManagementFeeAmount,
                             FeeType = type.ManagementFeeType1,
                             FeePaidDate = fee.ReceivedDate,
                             FeeNotes = fee.Notes

                         };
            return result;

        }

        public ManagementContract GetContract(int id)
        {
            //throw new NotImplementedException();
            return entities.ManagementContracts.Where(c => c.ManagementContractId == id).FirstOrDefault();
        }
        
        public DisplayFeePaymentViewModel GetFeePaymentById(int id)
        {
            //throw new NotImplementedException();m
            var result = from fee in entities.ManagementFees
                         join feeType in entities.ManagementFeeTypes on fee.ManagementFeeTypeId equals feeType.ManagementFeeTypeId
                         join contract in entities.ManagementContracts on fee.ManagementContractId equals contract.ManagementContractId
                         join property in entities.Properties on contract.PropertyId equals property.PropertyId
                         join landlord in entities.PropertyOwners on property.PropertyOwnerId equals landlord.PropertyOwnerId
                         where fee.ManagementFeeId == id
                         select new DisplayFeePaymentViewModel 
                         {
                             FeePaidAmount = fee.ManagementFeeAmount,
                             FeePaidDate = fee.ReceivedDate,
                             FeeMonth = fee.ManagementFeeMonth,
                             FeeYear = fee.ManagementFeeYear,
                             FeeType = feeType.ManagementFeeType1,
                             

                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             
                             ContractTitle = contract.ManagementContractTitile,
                             ContractId = contract.ManagementContractId,
                             
                             LandlordLastName = landlord.LastName,
                             LandlordFisrtName = landlord.FirstName,
                             LandlordContactEmail = landlord.ContactEmail

                             
                         };
            return result.FirstOrDefault();


        }


        public IEnumerable<Core.Contract.Document> DocumenetByContractId(int id, int pid)
        {
            //throw new NotImplementedException();
            return entities.Documents.Where(p => p.DocumentPrincipalId == id && p.DocumentPrincipalTypeId == pid);
        }



        public IEnumerable<SiteSearchViewModel> ShowAllContractResult(string searchString)
        {
            //throw new NotImplementedException();
            var result = from contract in entities.ManagementContracts
                         where contract.ManagementContractTitile.ToUpper().Contains(searchString)
                         select new SiteSearchViewModel
                         {
                             ContractId = contract.ManagementContractId,
                             ContractTitle = contract.ManagementContractTitile,
                             ContractAddDate = contract.CreateDate,
                             StartDate = contract.StartDate
                         };

            return result;

        }


        public void AddManagementContract(ManagementContract contract)
        {
            //throw new NotImplementedException();
            entities.ManagementContracts.AddObject(contract);
        }

        public void AddManagementFee(ManagementFee fee)
        {
            //throw new NotImplementedException();
            entities.ManagementFees.AddObject(fee);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }


















    }
}
