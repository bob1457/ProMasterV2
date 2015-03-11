using System.Collections.Generic;
using ProMaster.Core.Contract;
using ProMaster.Core.Contract.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;


namespace ProMaster.DAL.Contract
{
    public interface IManagementContractRepository
    {
        IEnumerable<ManagementFeeFrequency> GetFeeFrequency();
        IEnumerable<Core.Contract.Property> GetMyProperty(int pmId);
        IEnumerable<Core.Contract.Property> GetPropertyByOwner(int oId);

        Core.Contract.Property GetManagedProperty(int id);

        IEnumerable<ManageDisplayViewModel> GetMyContract(int pmId);
        IEnumerable<DisplayContractViewModel> GeteContractDetails(int id);
        CreateContractViewModel GetContractById(int id);
        ManagementContract GetContract(int id);

        IEnumerable<ManagementContract> ListAllContracts();

        IEnumerable<DisplayContractViewModel> GetFeeHistoryByContract(int id); //is is contract id

        IEnumerable<SiteSearchViewModel> ShowAllContractResult(string searchString);

        IEnumerable<Core.Contract.Document> DocumenetByContractId(int id, int pid);

        DisplayFeePaymentViewModel GetFeePaymentById(int id);

        void AddManagementContract(ManagementContract contract);
        void AddManagementFee(ManagementFee fee);

        void Save();
    }
}
