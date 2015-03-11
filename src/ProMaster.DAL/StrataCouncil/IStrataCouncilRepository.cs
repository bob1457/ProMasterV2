using System.Collections.Generic;
using ProMaster.Core.StrataCouncil.ViewModels;
using ProMaster.Core.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.StrataCouncil
{
    public interface IStrataCouncilRepository
    {
        IEnumerable<ManageDisplayViewModel> GetAllStrataCouncils();
        DisplayStrataCouncilViewModel GetStrataCouncilDetails(int id);
        Core.StrataCouncil.Property GetPropertyById(int id);
        Core.StrataCouncil.StrataCouncil GetCouncilById(int id);

        IEnumerable<DisplayStrataCouncilViewModel> GetPropertyByCouncil(int id);

        IEnumerable<SiteSearchViewModel> ShowAllCouncilResult(string searchString);


        IEnumerable<Core.StrataCouncil.Property> GetCondoPropertyList();

        void AddStrataCouncil(Core.StrataCouncil.StrataCouncil council);
        void DeleteStrataCouncil(Core.StrataCouncil.StrataCouncil council);
        void Save();
    }
}
