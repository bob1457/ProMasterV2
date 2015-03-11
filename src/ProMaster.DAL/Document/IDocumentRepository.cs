using System;
using System.Collections.Generic;
using ProMaster.Core.Documents;
using ProMaster.Core.Documents.ViewModels;
using ProMaster.Core.SiteSearch.ViewModels;

namespace ProMaster.DAL.Document
{
    public interface IDocumentRepository
    {
        IEnumerable<DocumentType> GetDocType();
        IEnumerable<DocumentPrincipalType> GetPrincipaltype();
        IEnumerable<DisplayDcoumentViewModel> GetDocumentDetailsById(int id);
        //IEnumerable<Core.Documents.Document> ListAllDocuments();
        IEnumerable<DisplayDcoumentViewModel> ListAllDocuments();
        //IEnumerable<Core.Documents.Document> GetDocumentById(int id);

        IEnumerable<DisplayDcoumentViewModel> DocumentsByCategory(int id); //id = DocumentPrincipalTypeId

        IEnumerable<DisplayDcoumentViewModel> PropertyDocSearchResults(string searchTerm);
        IEnumerable<DisplayDcoumentViewModel> TenantDocSearchResults(string searchTerm);
        IEnumerable<DisplayDcoumentViewModel> ContractDocSearchResults(string searchTerm);
        IEnumerable<DisplayDcoumentViewModel> LeaseDocSearchResults(string searchTerm);
        //IEnumerable<DisplayDcoumentViewModel> LandlordDocSearchResults(string searchTerm);
        IEnumerable<DocSearchViewModel> GetDocumentsByDocSearch(string keyWord, int type, int category, DateTime period);

        Core.Documents.Document GetDocumentById(int id);
        CreateDcoumentViewModel DocumentById(int id);

        void DeleteDocument(Core.Documents.Document doc);
        void AddDocument(Core.Documents.Document doc);
        void SaveDocument();
    }
}
