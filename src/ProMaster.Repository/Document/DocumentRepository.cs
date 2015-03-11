using System;
using System.Collections.Generic;
using System.Linq;
using ProMaster.DAL.Document;
using ProMaster.Core.Documents;
using ProMaster.Core.Documents.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;
            


namespace ProMaster.Repository.Document
{
    public class DocumentRepository : IDocumentRepository
    {
        ProMasterDocumentDataEntities entities = new ProMasterDocumentDataEntities();

        public IEnumerable<DocumentType> GetDocType()
        {
            //throw new NotImplementedException();
            return entities.DocumentTypes;
        }


        public IEnumerable<DocumentPrincipalType> GetPrincipaltype()
        {
            //throw new NotImplementedException();
            return entities.DocumentPrincipalTypes;
        }

        public Core.Documents.Document GetDocumentById(int id)
        {
            //throw new NotImplementedException();
            return entities.Documents.FirstOrDefault(d => d.DocumentId == id);
        }

        public IEnumerable<DisplayDcoumentViewModel> GetDocumentDetailsById(int id)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         join type in entities.DocumentTypes on document.DocumentTypeId equals type.DocumentTypeId
                         join category in entities.DocumentPrincipalTypes on document.DocumentPrincipalTypeId equals category.DocumentPrincipalTypeId
                         where document.DocumentId == id
                         select new DisplayDcoumentViewModel 
                         { 
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentDetails = document.DcoumentDetails,
                             DocumentType = type.DocumentType1,
                             DocumentTypeId = document.DocumentTypeId,
                             DocumentUrl = document.DocumentUrl,
                             DocumentCategory = category.DocumentPrincipalType1,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         
                         };
            return result;
        }


        public IEnumerable<DisplayDcoumentViewModel> ListAllDocuments()
        {
            //throw new NotImplementedException();
            var result = from doc in entities.Documents
                         join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId
                         //join property in entities.Properties on doc.DocumentPrincipalId equals property.PropertyId
                         //join tenant in entities.Tenants on doc.DocumentPrincipalId equals tenant.TenantId
                         //join lease in entities.Leases on doc.DocumentPrincipalId equals lease.LeaseId
                         //join contract in entities.ManagementContracts on doc.DocumentPrincipalId equals contract.ManagementContractId
                         select new DisplayDcoumentViewModel 
                         { 
                             DocumentId = doc.DocumentId,
                             DocumentType = type.DocumentType1,
                             DocumentTitle = doc.DocumentName,
                             //TenantName = tenant.LastName + " " + tenant.FirstName,
                             //PropertyName = property.PropertyName,
                             //LeaseTitle = lease.LeaseTitle,
                             //ContractTitle = contract.ManagementContractTitile,
                             CreateDate = doc.CreationDate,
                             UpdateDate = doc.UpdateDate
                         };
            return result;
        }


        public IEnumerable<DisplayDcoumentViewModel> DocumentsByCategory(int id)
        {
            //throw new NotImplementedException();
            

            switch (id)
            {
                case 1:
                    var result1 = from doc in entities.Documents
                                 join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId
                                 join property in entities.Properties on doc.DocumentPrincipalId equals property.PropertyId
                                 //join tenant in entities.Tenants on doc.DocumentPrincipalId equals tenant.TenantId
                                 where doc.DocumentPrincipalTypeId == id
                                 select new DisplayDcoumentViewModel
                                 {
                                     DocumentId = doc.DocumentId,
                                     DocumentType = type.DocumentType1,
                                     DocumentTitle = doc.DocumentName,
                                     DocumentDetails = doc.DcoumentDetails,
                                     //TenantName = tenant.LastName + " " + tenant.FirstName,
                                     PropertyName = property.PropertyName,
                                     CreateDate = doc.CreationDate,
                                     UpdateDate = doc.UpdateDate
                                 };
                    return result1;
                case 2:
                    var result2 = from doc in entities.Documents
                                 join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId                                  
                                  join contract in entities.ManagementContracts on doc.DocumentPrincipalId equals contract.ManagementContractId
                                  join property in entities.Properties on contract.PropertyId equals property.PropertyId
                                  join owner in entities.PropertyOwners on property.PropertyOwnerId equals owner.PropertyOwnerId
                                 where doc.DocumentPrincipalTypeId == id
                                 select new DisplayDcoumentViewModel
                                 {
                                     DocumentId = doc.DocumentId,
                                     DocumentType = type.DocumentType1,
                                     DocumentTitle = doc.DocumentName,
                                     DocumentDetails = doc.DcoumentDetails,
                                     //TenantName = tenant.LastName + " " + tenant.FirstName,
                                     PropertyName = property.PropertyName,
                                     LandlordName = owner.FirstName + " " + owner.LastName,
                                     CreateDate = doc.CreationDate,
                                     UpdateDate = doc.UpdateDate
                                 };
                    return result2;
                case 3:
                    var result3 = from doc in entities.Documents
                             join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId
                             //join property in entities.Properties on doc.DocumentPrincipalId equals property.PropertyId
                             join tenant in entities.Tenants on doc.DocumentPrincipalId equals tenant.TenantId                             
                             where doc.DocumentPrincipalTypeId == id
                             select new DisplayDcoumentViewModel
                             {
                                 DocumentId = doc.DocumentId,
                                 DocumentType = type.DocumentType1,
                                 DocumentTitle = doc.DocumentName,
                                 DocumentDetails = doc.DcoumentDetails,
                                 TenantName = tenant.LastName + " " + tenant.FirstName,
                                 //PropertyName = property.PropertyName,                                 
                                 CreateDate = doc.CreationDate,
                                 UpdateDate = doc.UpdateDate
                             };
                    return result3;
                case 4:
                    var result4 = from doc in entities.Documents
                             join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId
                             //join property in entities.Properties on doc.DocumentPrincipalId equals property.PropertyId
                             //join tenant in entities.Tenants on doc.DocumentPrincipalId equals tenant.TenantId
                             //join owner in entities.PropertyOwners on doc.DocumentPrincipalId equals owner.PropertyOwnerId
                             where doc.DocumentPrincipalTypeId == id
                             select new DisplayDcoumentViewModel
                             {
                                 DocumentId = doc.DocumentId,
                                 DocumentType = type.DocumentType1,
                                 DocumentTitle = doc.DocumentName,
                                 DocumentDetails = doc.DcoumentDetails,
                                 //TenantName = tenant.LastName + " " + tenant.FirstName,
                                 //PropertyName = property.PropertyName,
                                 //LandlordName = owner.FirstName + " " + owner.LastName,
                                 CreateDate = doc.CreationDate,
                                 UpdateDate = doc.UpdateDate
                             };
                    return result4;
                default:
                    var result5 = from doc in entities.Documents
                                 join type in entities.DocumentTypes on doc.DocumentTypeId equals type.DocumentTypeId
                                 //join property in entities.Properties on doc.DocumentPrincipalId equals property.PropertyId
                                 //join tenant in entities.Tenants on doc.DocumentPrincipalId equals tenant.TenantId
                                 where doc.DocumentPrincipalTypeId == id
                                 select new DisplayDcoumentViewModel
                                 {
                                     DocumentId = doc.DocumentId,
                                     DocumentType = type.DocumentType1,
                                     DocumentTitle = doc.DocumentName,
                                     DocumentDetails = doc.DcoumentDetails,
                                     //TenantName = tenant.LastName + " " + tenant.FirstName,
                                     //PropertyName = property.PropertyName,
                                     CreateDate = doc.CreationDate,
                                     UpdateDate = doc.UpdateDate
                                 };
                                 
                                 return result5;
            }


            

            

        }

        public IEnumerable<DisplayDcoumentViewModel> PropertyDocSearchResults(string searchTerm)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         where (document.DocumentName.ToUpper().Contains(searchTerm.ToUpper()) || document.DcoumentDetails.ToUpper().Contains(searchTerm.ToUpper()))
                         && document.DocumentPrincipalTypeId == 1
                         select new DisplayDcoumentViewModel 
                         {
                             DocumentDetails = document.DcoumentDetails,
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };

            return result;

        }

        public IEnumerable<DisplayDcoumentViewModel> TenantDocSearchResults(string searchTerm)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         where (document.DocumentName.ToUpper().Contains(searchTerm.ToUpper()) || document.DcoumentDetails.ToUpper().Contains(searchTerm.ToUpper()))
                         && document.DocumentPrincipalTypeId == 3
                         select new DisplayDcoumentViewModel
                         {
                             DocumentDetails = document.DcoumentDetails,
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };

            return result;
        }

        public IEnumerable<DisplayDcoumentViewModel> ContractDocSearchResults(string searchTerm)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         where (document.DocumentName.ToUpper().Contains(searchTerm.ToUpper()) || document.DcoumentDetails.ToUpper().Contains(searchTerm.ToUpper()))
                         && document.DocumentPrincipalTypeId == 2
                         select new DisplayDcoumentViewModel
                         {
                             DocumentDetails = document.DcoumentDetails,
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };

            return result;
        }

        public IEnumerable<DisplayDcoumentViewModel> LeaseDocSearchResults(string searchTerm)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         where (document.DocumentName.ToUpper().Contains(searchTerm.ToUpper()) || document.DcoumentDetails.ToUpper().Contains(searchTerm.ToUpper()))
                         && document.DocumentPrincipalTypeId == 4
                         select new DisplayDcoumentViewModel
                         {
                             DocumentDetails = document.DcoumentDetails,
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };

            return result;
        }


        public CreateDcoumentViewModel DocumentById(int id)
        {
            //throw new NotImplementedException();
            var result = from document in entities.Documents
                         join type in entities.DocumentTypes on document.DocumentTypeId equals type.DocumentTypeId
                         where document.DocumentId == id
                         select new CreateDcoumentViewModel
                         {
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentDetails = document.DcoumentDetails,
                             DocumentType = type.DocumentType1,
                             DocumentPrincipalId = document.DocumentPrincipalTypeId,
                             DocumentTypeId = document.DocumentTypeId,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };

            return result.FirstOrDefault();
        }

        public IEnumerable<DocSearchViewModel> GetDocumentsByDocSearch(string keyWord, int type, int category, DateTime period)
        {
            //throw new NotImplementedException();
            DateTime endDate = DateTime.Now;
            double numberDate = (endDate - period).TotalDays;
            
            DateTime dateSince = DateTime.Now.AddDays(-numberDate);

            var result = from document in entities.Documents
                         join type1 in entities.DocumentTypes on document.DocumentTypeId equals type1.DocumentTypeId
                         join category1 in entities.DocumentPrincipalTypes on document.DocumentPrincipalTypeId equals category1.DocumentPrincipalTypeId
                         where (document.DocumentName.ToUpper().Contains(keyWord.ToUpper()) || document.DcoumentDetails.ToUpper().Contains(keyWord.ToUpper()))
                         && (document.DocumentTypeId == type) && (document.DocumentPrincipalTypeId == category)
                         && (document.CreationDate > dateSince)
                         select new DocSearchViewModel
                         {
                             DocumentId = document.DocumentId,
                             DocumentTitle = document.DocumentName,
                             DocumentDetails = document.DcoumentDetails,
                             DocumentType = type1.DocumentType1,
                             DocumentPrincipalTypeId = document.DocumentPrincipalTypeId,
                             DocumentTypeId = document.DocumentTypeId,
                             DocumentUrl = document.DocumentUrl,
                             CreateDate = document.CreationDate,
                             UpdateDate = document.UpdateDate
                         };
            return result;
        }
        

        public void AddDocument(Core.Documents.Document doc)
        {
            //throw new NotImplementedException();
            entities.Documents.AddObject(doc);
        }


        public void DeleteDocument(Core.Documents.Document doc)
        {
            //throw new NotImplementedException();
            entities.DeleteObject(doc);
        }

        public void SaveDocument()
        {
            //throw new NotImplementedException();
            entities.SaveChanges();
        }

























        
    }
}
