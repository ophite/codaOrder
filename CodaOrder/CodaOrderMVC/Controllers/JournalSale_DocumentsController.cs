using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Entity;
using System.Data.Entity.Core.Objects;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebApplication3.Controllers
{
    public class JournalSale_DocumentsController : Controller
    {
        #region Properties

        private IUow _uow;

        #endregion
        #region Methods

        public JournalSale_DocumentsController(IUow uow)
        {
            this._uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string GetDocumentsPost(FormCollection form)
        {
            var data = form["model"];
            JObject js = (JObject)JsonConvert.DeserializeObject(data);
            return _uow.DocumentRepository.GetLinesJson(js);
        }

        // GET: JournalSale_Documents
        //public string GetDocuments(string subjectID,
        //    string dateBegin,
        //    string dateEnd,
        //    string docTypeClasses,
        //    int pageSize,
        //    int currentPage,
        //    string fullTextFilter,
        //    string whereText)
        //{
        //    return _uow.DocumentRepository.GetLinesJson(subjectID,
        //        dateBegin,
        //        dateEnd,
        //        docTypeClasses,
        //        pageSize,
        //        currentPage,
        //        fullTextFilter,
        //        whereText);
        //}

        // GET: JournalSale_Documents/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents journalSale_Documents = _uow.DocumentRepository.GetById((long)id);
            if (journalSale_Documents == null)
                return HttpNotFound();

            return View(journalSale_Documents);
        }

        // GET: JournalSale_Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JournalSale_Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents journalSale_Documents)
        {
            if (ModelState.IsValid)
            {
                _uow.DocumentRepository.Create(journalSale_Documents);
                return RedirectToAction("Index");
            }

            return View(journalSale_Documents);
        }

        // GET: JournalSale_Documents/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents journalSale_Documents = _uow.DocumentRepository.GetById((long)id);
            if (journalSale_Documents == null)
                return HttpNotFound();

            return View(journalSale_Documents);
        }

        // POST: JournalSale_Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents journalSale_Documents)
        {
            if (ModelState.IsValid)
            {
                _uow.DocumentRepository.Edit(journalSale_Documents);
                return RedirectToAction("Index");
            }
            return View(journalSale_Documents);
        }

        // GET: JournalSale_Documents/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents journalSale_Documents = _uow.DocumentRepository.GetById((long)id);
            if (journalSale_Documents == null)
                return HttpNotFound();

            return View(journalSale_Documents);
        }

        // POST: JournalSale_Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _uow.DocumentRepository.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion
        #region IDispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _uow.Dispose();

            base.Dispose(disposing);
        }

        #endregion
    }
}
