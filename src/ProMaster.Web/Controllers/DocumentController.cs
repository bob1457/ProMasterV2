using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using ProMaster.Core.Documents;
using ProMaster.Core.Documents.ViewModels;
using ProMaster.DAL.Document;
using ProMaster.Infrastructure.Utilities;
using ProMaster.DAL.Tenant;


namespace ProMaster.Web.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {

        #region DI Configuration

        IDocumentRepository _documentRepository;
        ITenantRepository _tenantRepository;


        public DocumentController(IDocumentRepository documentRepository, ITenantRepository tenantRepository)  //depending on interface instead of implementation
        {
            ViewBag.CurrentPage = "document";

            _documentRepository = documentRepository;
            _tenantRepository = tenantRepository;
        }

        #endregion
        //
        // GET: /Document/

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "document";

            if(User.IsInRole("PropertyManager") || User.IsInRole("SuperAdmin"))
            { 
                var model = new DisplayDcoumentViewModel();

                IEnumerable<DisplayDcoumentViewModel> docs = _documentRepository.ListAllDocuments();

                

                var displayDcoumentViewModels = docs as IList<DisplayDcoumentViewModel> ?? docs.ToList();

                if (displayDcoumentViewModels.Count() != 0)
                { 
                    model.PropertyDocuments = _documentRepository.DocumentsByCategory(1);
                    model.TenantDocuments = _documentRepository.DocumentsByCategory(3);
                    model.LeaseDocuments = _documentRepository.DocumentsByCategory(4);
                    model.ContractDocuments = _documentRepository.DocumentsByCategory(2);

                    model.DocumentId = displayDcoumentViewModels.FirstOrDefault().DocumentId;
                    model.DocumentType = displayDcoumentViewModels.First().DocumentType;
                    model.DocumentPrincipalTypeId = displayDcoumentViewModels.FirstOrDefault().DocumentPrincipalTypeId;
                    model.DocumentTitle = displayDcoumentViewModels.FirstOrDefault().DocumentTitle;
                    model.DocumentDetails = displayDcoumentViewModels.FirstOrDefault().DocumentDetails;
                    model.DocumentUrl = displayDcoumentViewModels.FirstOrDefault().DocumentUrl;
                    model.CreateDate = displayDcoumentViewModels.FirstOrDefault().CreateDate;
                    model.UpdateDate = displayDcoumentViewModels.FirstOrDefault().UpdateDate;

                    model.TenantName = displayDcoumentViewModels.FirstOrDefault().TenantName;
                    model.PropertyName = displayDcoumentViewModels.FirstOrDefault().PropertyName;
                    model.ContractTitle = displayDcoumentViewModels.FirstOrDefault().ContractTitle;
                    model.LandlordName = displayDcoumentViewModels.FirstOrDefault().LandlordName;
                    model.LeaseTitle = displayDcoumentViewModels.FirstOrDefault().LeaseTitle;
            
                }

                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index", "ClientPortal");
            }
            
            //return View(docs);
        }

        //
        // GET: /Document/Details/5

        public ActionResult DocumentDetails(int id)
        {
            DisplayDcoumentViewModel document = _documentRepository.GetDocumentDetailsById(id).FirstOrDefault();

            //could be improved so that for different type of document, i.e. contract, rental agreement, etc. a different data access code to get type specific information.

            return View("DocumentDetails", document);
        }

        //
        // GET: /Document/Create

        public ActionResult AddDocument(int id, int pId)
        {
            CreateDcoumentViewModel model = new CreateDcoumentViewModel();

            ViewData["uid"] = id;

            //ViewData["pid"] = pId;

            var docTypeList = _documentRepository.GetDocType();
            var principalTypeList = _documentRepository.GetPrincipaltype();

            string returnUrl = "";

            if (HttpContext.Request.UrlReferrer != null)
            {
                returnUrl = HttpContext.Request.UrlReferrer.PathAndQuery;
            }

            TempData["url"] = returnUrl;

            model.DocumentTypeList = docTypeList.ToDocTypeList(-1);
            model.DocumentTyPrincipalpeList = principalTypeList.ToPrincipalTypeList(-1);

            
            return View("AddDocument", model);
        }

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult AddDocument(int id, CreateDcoumentViewModel model)//, HttpPostedFileBase file) // CreateDcoumentViewModel model) //id is TenantId, pId is PrincipaltypeId
        {



            //Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

            //CreateDcoumentViewModel model = new CreateDcoumentViewModel();

            if (ModelState.IsValid) { 
            
                try
                {
                    foreach (string file in Request.Files)
                    { 
                    
                        HttpPostedFileBase postFileBase = Request.Files[file];

                    
                    
                        


                    // TODO: Add insert logic here
                    var document = new Document();

                    //HttpPostedFileBase photo = Request.Files["doc"];

                    //WebImage file = new WebImage(Request.InputStream);

                    //*************************************
                    //original code 

                    // var file = WebImage.GetImageFromRequest(); //Get uploaded file  --- To be resotred
                    

                    //string Url0 =  TempData["url"] as string;

                    //string Url = Url0.Remove(1, 7);

                        if (TempData["url"] != null)
                    {
                        string Url0 = TempData["url"] as string;
                    }

                        document.DocumentPrincipalTypeId = model.DocumentPrincipalTypeId;

                    int pId = model.DocumentPrincipalTypeId; //Convert.ToInt32( ViewData["pid"]); //

                    string url;

                    switch (pId)
                    {
                        case 1:
                            url = "PropertyDetails";
                            break;
                        case 2:
                            url="ContractDetails";
                            break;
                        case 3:
                            url = "TenantDetails";
                            break;
                        case 4:
                            url = "LeaseDetails";
                            break;
                        default:
                            url = "Index";
                            break;
                    }

                    //*************************************

                    #region File Upload

                    if (postFileBase.ContentLength != 0)
                    {
                        var origFileName = Path.GetFileName(postFileBase.FileName);

                        var fileExtension = FileProcessor.GetFileExtension(origFileName);

                        //int counter = 001;
                        Guid guid = Guid.NewGuid();

                        switch (pId)
                        {
                            case 1:
                                var newFileName = "Property" + "_" + id + "_" + guid + fileExtension;
                                //counter += counter;
                                postFileBase.SaveAs(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Property/", newFileName));
                                document.DocumentUrl = Path.Combine("~/Content/Documents/Property/", newFileName);
                                break;
                            case 2:
                                newFileName = "Contract" + "_" + id + "_" + guid + fileExtension;
                                //counter += counter;
                                postFileBase.SaveAs(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Landlord/", newFileName));
                                document.DocumentUrl = Path.Combine("~/Content/Documents/Landlord/", newFileName);
                                break;
                            case 3:
                                Core.Tenant.Tenant tenant = _tenantRepository.GetTenantById(id);

                                newFileName = "Tenant" + "_" + tenant.FirstName + "-" + tenant.LastName + "_" + id + "_" + guid + fileExtension;
                                //counter += counter;
                                postFileBase.SaveAs(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Tenant/", newFileName));
                                document.DocumentUrl = Path.Combine("~/Content/Documents/Tenant/", newFileName);
                                break;
                            case 4:
                                newFileName = "Lease" + "_" + id + "_" + guid + fileExtension;
                                //counter += counter;
                                postFileBase.SaveAs(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Lease/", newFileName));
                                document.DocumentUrl = Path.Combine("~/Content/Documents/Lease/", newFileName);
                                break;
                            default:
                                newFileName = "Others" + "_" + id + "_" + guid + fileExtension;
                                //counter += counter;
                                postFileBase.SaveAs(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newFileName));
                                document.DocumentUrl = Path.Combine("~/Content/DocumentsOthers/", newFileName);
                                break;
                        }
                    #endregion

                    document.DocumentName = Request.Form["DocumentTitle"];
                    document.DcoumentDetails = Request.Form["DocumentDetails"];
                    document.DocumentPrincipalId = id; //tenant id
                    document.DocumentPrincipalTypeId = pId;
                    //document.DocumentPrincipalTypeId = model.DocumentTypeId;
                    document.DocumentTypeId = model.DocumentTypeId;
                    document.IsActive = true;
                    document.CreationDate = DateTime.Now;
                    document.UpdateDate = DateTime.Now;

                    _documentRepository.AddDocument(document);
                    _documentRepository.SaveDocument();


                    }
                    else
                    {
                        return RedirectToAction(url, "Manage", new { id = id }); //"LeaseDetails/" + id);
                        //return Json("Error occured!");
                    }

                   
                    //return RedirectToAction(Url, "Manage");
                    //if (Url.Length > 0)
                    //{
                    //    return RedirectToAction(Url, "Manage");
                    //}
                    //else
                    //{
                    return RedirectToAction(url, "Manage", new { id = id });
                    //return Json("Success!");
                    //}

                    }    
                }
                catch
                {
                    return RedirectToAction("Index", "Document"); //, new { id = id });
                }
            
            }

            return RedirectToAction("LeaseDetails", "Manage", new { id = id });
            //return Json("Success!");
        }


        public void UploadDocument(int id, int pId, Document document)
        {
            //Document document = _documentRepository.GetDocumentById(id);

            var doc = WebImage.GetImageFromRequest(); //Get uploaded file

            if (doc != null)
            {
                var origFileName = Path.GetFileName(doc.FileName);

                var fileExtension = FileProcessor.GetFileExtension(origFileName);

                int counter = 001;

                switch (pId)
                {
                    case 1:
                        var newFileName = "Property" + "_" + id + "_" + counter + fileExtension;
                        counter += counter;
                        doc.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Property/", newFileName));
                        document.DocumentUrl = Path.Combine("~/Content/Documents/Property/", newFileName);
                        break;
                    case 2:
                        newFileName = "Landlord" + "_" + id + "_" + counter + fileExtension;
                        counter += counter;
                        doc.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Landlord/", newFileName));
                        document.DocumentUrl = Path.Combine("~/Content/Documents/Landlord/", newFileName);
                        break;
                    case 3:
                        newFileName = "Tenant" + "_" + id + "_" + counter + fileExtension;
                        counter += counter;
                        doc.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Tenant/", newFileName));
                        document.DocumentUrl = Path.Combine("~/Content/Documents/Tenant/", newFileName);
                        break;
                    case 4:
                        newFileName = "Lease" + "_" + id + "_" + counter + fileExtension;
                        counter += counter;
                        doc.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Lease/", newFileName));
                        document.DocumentUrl = Path.Combine("~/Content/Documents/Lease/", newFileName);
                        break;
                    default:
                        newFileName = "Others" + "_" + id + "_" + counter + fileExtension;
                        counter += counter;
                        doc.Save(Path.Combine(@Server.MapPath("~") + "/Content/Documents/Others/", newFileName));
                        document.DocumentUrl = Path.Combine("~/Content/DocumentsOthers/", newFileName);
                        break;

                }

                //_documentRepository.SaveDocument();


            }
            //else
            //{
            //    return View();
            //}
            
            //return RedirectToAction("");
        }

        //
        // GET: /Document/Edit/5
        
        public ActionResult EditDocument(int id)
        {
            //Core.Documents.Document document = _documentRepository.GetDocumentById(id);

            CreateDcoumentViewModel document = _documentRepository.DocumentById(id);

            var docTypeList = _documentRepository.GetDocType();            
            document.DocumentTypeList = docTypeList.ToDocTypeList(-1);

            return View("EditDocument", document);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult EditDocument(int id, CreateDcoumentViewModel model) //, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                var document = _documentRepository.GetDocumentById(id);

                //upload new file image
                //
                //UploadDocument(document.DocumentId, document.DocumentPrincipalTypeId, document);




                #region Update document image


                foreach (string file in Request.Files) 
                {
                    HttpPostedFileBase postFileBase = Request.Files[file];


                    if (postFileBase.ContentLength != 0)
                    {
                        //handle the error  
                        //ViewBag.ErrorMessage = "Please choose the image file to continue!";

                        //Find existing file name
                        //                        

                        string path = document.DocumentUrl;

                        string subPath = "";

                        string existingFileName = path.Substring(path.LastIndexOf("/") + 1);

                        if (existingFileName.Contains("Tenant"))
                        {
                            subPath = "Tenant";
                        }
                        else
                        {
                            if (existingFileName.Contains("Lease"))
                            {
                                subPath = "Lease";
                            }
                            else
                            {
                                if (existingFileName.Contains("Property"))
                                {
                                    subPath = "Property";
                                }
                                else
                                {
                                    if (existingFileName.Contains("Landlord"))
                                    {
                                        subPath = "Landlord";
                                    }
                                    else
                                    {
                                        subPath = "Others";
                                    }
                                }

                            }
                        }

                        //var origFileName = Path.GetFileName(postFileBase.FileName);

                        //var fileExtension = FileProcessor.GetFileExtension(origFileName);


                        //string fileType = Path.GetExtension(file.FileName);//Get the file type by extension
                        //string uploadFileName = Path.GetFileName(file.FileName);//get the full file name of uploading file

                        //string finalFileName = User.Identity.Name + "_new" + fileType;


                        string savedFileName = Path.Combine(Server.MapPath("~/Content/Documents/" + subPath + "/"), existingFileName);
                        postFileBase.SaveAs(savedFileName);
                        
                        return RedirectToAction("EditDocument/" + id);
                    }
                    else
                    {
                        
                        ModelState.AddModelError("", "Please choose the image file to continue!");

                        
                    }
               } 
                
                #endregion



                document.DocumentName = Request.Form["DocumentTitle"];
                document.DcoumentDetails = Request.Form["DocumentDetails"];
                //document.DocumentUrl = Request.Form["DocumentUrl"];
                document.DocumentTypeId = model.DocumentTypeId;
                //document.DocumentPrincipalTypeId = model.DocumentTypeId;
                document.UpdateDate = DateTime.Now;
                _documentRepository.SaveDocument();

                return RedirectToAction("DocumentDetails/" + id);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Document/Delete/5

        [HttpPost]
        public ActionResult DeleteDocument(int id)
        {
            Document document = _documentRepository.GetDocumentById(id);

            _documentRepository.DeleteDocument(document);
            _documentRepository.SaveDocument();

            return Content(Boolean.TrueString);
        }

        //
        // POST: /Document/Delete/5

        //[HttpPost]
        //public ActionResult DeleteDocument(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult SearchDocuments(string searchText) //advanced search for documents
        {
            //throw new NotImplementedException();
            DisplayDcoumentViewModel searchModel = new DisplayDcoumentViewModel();

            IEnumerable<DisplayDcoumentViewModel> propertyDoc = _documentRepository.PropertyDocSearchResults(searchText).Distinct();
            IEnumerable<DisplayDcoumentViewModel> tenantDoc = _documentRepository.TenantDocSearchResults(searchText).Distinct();
            IEnumerable<DisplayDcoumentViewModel> contractDoc = _documentRepository.ContractDocSearchResults(searchText).Distinct();
            IEnumerable<DisplayDcoumentViewModel> leaseDoc = _documentRepository.LeaseDocSearchResults(searchText).Distinct();

            searchModel.PropertyDocuments = propertyDoc;
            searchModel.TenantDocuments = tenantDoc;
            searchModel.ContractDocuments = contractDoc;
            searchModel.LeaseDocuments = leaseDoc;

            return View("SearchDocuments", searchModel);
        }
    }
}
