using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.StrataCouncil;
using ProMaster.Core.StrataCouncil;
using ProMaster.Core.StrataCouncil.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;


namespace ProMaster.Repository.StrataCouncil
{
    public class StrataCouncilRepository : IStrataCouncilRepository
    {
        readonly ProMasterStrataCouncilDataEntities _entities = new ProMasterStrataCouncilDataEntities();


        public IEnumerable<Core.ViewModels.ManageDisplayViewModel> GetAllStrataCouncils()
        {
            //throw new NotImplementedException();
            var result = from council in _entities.StrataCouncils
                         where council.IsActive
                         select new Core.ViewModels.ManageDisplayViewModel
                         {
                             StrataCouncilId = council.StrataCouncilId,
                             StrataCouncilName = council.StrataCouncilTitle,
                             StrataManagerLastName = council.StrataManagerLastName,
                             StrataManageFirstName = council.StrataManagerFirstName,
                             StrataCounvilMailingAddress = council.StrataCouncilMailingAddress,
                             StrataManagerContactEmail = council.StrataManagerEmail,
                             StrataManagerContactTel = council.StrataManagerTelephone,
                             StrataCreationDate = council.CreateDate
                         };

            return result.OrderByDescending(d=>d.StrataCreationDate);
        }

        public DisplayStrataCouncilViewModel GetStrataCouncilDetails(int id)
        {
            //throw new NotImplementedException();
            var result = from council in _entities.StrataCouncils
                         //join property in entities.Properties on council.StrataCouncilId equals property.StrataCouncilId
                         where council.StrataCouncilId == id
                         select new DisplayStrataCouncilViewModel
                         {
                             StrataCouncilId = council.StrataCouncilId,
                             StrataCouncilName = council.StrataCouncilTitle,
                             StrataCounvilMailingAddress = council.StrataCouncilMailingAddress,
                             StrataManageFirstName = council.StrataManagerFirstName,
                             StrataManagerLastName = council.StrataManagerLastName,
                             StrataManagerContactEmail = council.StrataManagerEmail,
                             StrataManagerContactTel = council.StrataManagerTelephone

                         };
            return result.FirstOrDefault();
        }

        public IEnumerable<DisplayStrataCouncilViewModel> GetPropertyByCouncil(int id)
        {
            //throw new NotImplementedException();
            var result = from property in _entities.Properties
                         join status in _entities.RentalStatus on property.RentalStatusId equals status.RentalStatusId
                         where property.StrataCouncilId == id
                         select new DisplayStrataCouncilViewModel
                         {
                             PropertyId = property.PropertyId,
                             PropertyName = property.PropertyName,
                             PropertyDesc = property.PropertyDesc,
                             RentalStatus = status.Status

                         };
            return result;
        }

        public IEnumerable<Core.StrataCouncil.Property> GetCondoPropertyList()
        {            
            return _entities.Properties.Where(t => t.PropertyTypeId == 2 || t.PropertyTypeId == 3 );
        }

        public Core.StrataCouncil.Property GetPropertyById(int id)
        {
            //throw new NotImplementedException();
            return _entities.Properties.Where(p => p.PropertyId == id).FirstOrDefault();
        }

        public Core.StrataCouncil.StrataCouncil GetCouncilById(int id)
        {
            //throw new NotImplementedException();
            return _entities.StrataCouncils.Where(s => s.StrataCouncilId == id).FirstOrDefault();
        }

        public IEnumerable<SiteSearchViewModel> ShowAllCouncilResult(string searchString)
        {
            //throw new NotImplementedException();
            var result = from council in _entities.StrataCouncils
                         where council.StrataCouncilTitle.ToUpper().Contains(searchString.ToUpper()) || council.StrataManagerFirstName.ToUpper().Contains(searchString.ToUpper())
                         || council.StrataManagerLastName.ToUpper().Contains(searchString.ToUpper())
                         select new SiteSearchViewModel
                         {
                             StrataCouncilId = council.StrataCouncilId,
                             StrataManageFirstName = council.StrataManagerFirstName,
                             StrataCouncilName = council.StrataCouncilTitle,
                             StrataManagerLastName = council.StrataManagerLastName
                         };

            return result;
        }


        public void DeleteStrataCouncil(Core.StrataCouncil.StrataCouncil council)
        {
            //throw new NotImplementedException();
            _entities.DeleteObject(council);
        }

        public void AddStrataCouncil(Core.StrataCouncil.StrataCouncil council)
        {
            //throw new NotImplementedException();
            _entities.StrataCouncils.AddObject(council);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            _entities.SaveChanges();
        }

















    }
}
