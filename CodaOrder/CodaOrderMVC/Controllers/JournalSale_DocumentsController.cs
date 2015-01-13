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

namespace WebApplication3.Controllers
{
    public class JournalSale_DocumentsController : Controller
    {
        private IDocuments<JournalSale_Documents, codaJournal> _repository { get; set; }
        private codaJournal db = new codaJournal();

        // GET: JournalSale_Documents
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.JournalSale_Documents.ToListAsync());
        //}

        public JournalSale_DocumentsController()
            : this(new DocumentRepository<JournalSale_Documents, codaJournal>(new codaJournal()))
        {
        }

        public JournalSale_DocumentsController(IDocuments<JournalSale_Documents, codaJournal> repository)
        {
            this._repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: JournalSale_Documents
        public string GetDocuments()
        {
            return _repository.GetLinesJson();
        }

        // GET: JournalSale_Documents/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JournalSale_Documents journalSale_Documents = await db.JournalSale_Documents.FindAsync(id);
            if (journalSale_Documents == null)
            {
                return HttpNotFound();
            }
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
        public async Task<ActionResult> Create([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents journalSale_Documents)
        {
            if (ModelState.IsValid)
            {
                db.JournalSale_Documents.Add(journalSale_Documents);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(journalSale_Documents);
        }

        // GET: JournalSale_Documents/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JournalSale_Documents journalSale_Documents = await db.JournalSale_Documents.FindAsync(id);
            if (journalSale_Documents == null)
            {
                return HttpNotFound();
            }
            return View(journalSale_Documents);
        }

        // POST: JournalSale_Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OID,CID,Amount,BranchID,ChangeDate,Comments,CreateDate,DelDate,DocCode,DocDate,EmployeeID,DeliverID,ReceiverID,FirmID,FullName,GUID,IconIndex,IsDeleted,Name,NodeID,RGB,SourceDocID,DocGroupID,DocGroupPosition,TST,PriceSum,VatSum,ContractID,CustomerID,DepartmentID,CorrespondID,TaxCode,TaxDate,FilialID,PrintComments,IsForeign,RevenueID,DelayLimit,VatPercent,SourceDocCode,IsChangedAfterPrint,CasefillrateID,SectorID,CreditComments,VATNNSum,TaxInvoiceType,LineCount,Coordinate,BrandName")] JournalSale_Documents journalSale_Documents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journalSale_Documents).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(journalSale_Documents);
        }

        // GET: JournalSale_Documents/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JournalSale_Documents journalSale_Documents = await db.JournalSale_Documents.FindAsync(id);
            if (journalSale_Documents == null)
            {
                return HttpNotFound();
            }
            return View(journalSale_Documents);
        }

        // POST: JournalSale_Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            JournalSale_Documents journalSale_Documents = await db.JournalSale_Documents.FindAsync(id);
            db.JournalSale_Documents.Remove(journalSale_Documents);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
