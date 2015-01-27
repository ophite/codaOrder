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
    public partial class DocumentController : Controller
    {
        #region Properties

        private IUow _uow;

        #endregion
        #region Methods

        public DocumentController(IUow uow)
        {
            this._uow = uow;
        }

        [Authorize]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public string GetDocumentsPost(FormCollection form)
        {
            var data = form["model"];
            JObject js = (JObject)JsonConvert.DeserializeObject(data);
            return _uow.DocumentRepository.GetLinesJson(js);
        }

        // GET: Document/Details/5
        public virtual ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents docs = _uow.DocumentRepository.GetById((long)id);
            if (docs == null)
                return HttpNotFound();

            return View(docs);
        }

        // GET: Document/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents docs)
        {
            if (ModelState.IsValid)
            {
                _uow.DocumentRepository.Create(docs);
                return RedirectToAction("Index");
            }

            return View(docs);
        }

        // GET: Document/Edit/5
        public virtual ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents docs = _uow.DocumentRepository.GetById((long)id);
            if (docs == null)
                return HttpNotFound();

            return View(docs);
        }

        // POST: Document/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents docs)
        {
            if (ModelState.IsValid)
            {
                _uow.DocumentRepository.Edit(docs);
                return RedirectToAction("Index");
            }
            return View(docs);
        }

        // GET: Document/Delete/5
        public virtual ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            JournalSale_Documents docs = _uow.DocumentRepository.GetById((long)id);
            if (docs == null)
                return HttpNotFound();

            return View(docs);
        }

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
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
