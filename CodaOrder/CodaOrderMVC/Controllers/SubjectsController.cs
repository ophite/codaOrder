using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using iOrder.Entity;

namespace iOrder.Controllers
{
    public partial class SubjectsController : Controller
    {
        private codaJournal db = new codaJournal();

        // GET: Subjects
        //public async Task<ActionResult> Index()
        public virtual ActionResult Index()
        {
            //return View(await db.Subjects.Where( i => !i.IsDeleted).Take(10).ToListAsync());
            return View();
        }

        public class EntityClassSetting
        {
            public string ClassName { get; set; }
            public string Json { get; set; }
            public int Index { get; set; }
            public object Items { get; set; }
        }

        public string GetSubjectPageInfo()
        {
            return string.Empty;
        }

        public string GetDocuments()
        {
            string json = "";
            using (var db = new codaJournal())
            {
                long OID = 8000010580984;
                string filter = "<Filter><ID>8000005797829</ID><ID>8000000241646</ID></Filter>";
                string docOID = "";
                long? objectID = null;
                DateTime? dateBegin = DateTime.Now.AddDays(-7);
                DateTime? dateEnd = DateTime.Now;
                dateBegin = new DateTime(2013, 5, 16);
                dateEnd = new DateTime(2013, 5, 23);
                string docFilterClasses = "<DocumentClasses><ClassName>DocSale</ClassName></DocumentClasses>";
                string statusFilter = "<Status/>";
                bool isExtended = true;
                bool showDeleted = false;
                bool? checkOperation = null;
                string securityUser = @"TRITON\kobernik.u";
                string securityGroup = "<Groups><ID>20</ID><ID>11</ID><ID>92</ID><ID>48</ID><ID>44</ID></Groups>";
                int perPage = 50;
                int pageNumber = 1;
                string fullTextFilter = "<Root/>";
                string orderFilter = "<Root/>";
                ObjectParameter totalRows = new ObjectParameter("TotalRows", typeof(int));
                ObjectParameter pages = new ObjectParameter("Pages", typeof(int));
                string whereQuery = "";
                string WhereQueryTableAlias = "_journalalias_";
                ObjectParameter tST = new ObjectParameter("TST", typeof(byte[]));

                var qDocs = db.GetDocuments(OID, filter, docOID, objectID, dateBegin, dateEnd, docFilterClasses, statusFilter, isExtended, showDeleted,
                    checkOperation, securityUser, securityGroup, perPage, pageNumber, fullTextFilter, orderFilter, totalRows, pages, whereQuery,
                    WhereQueryTableAlias, tST);

                int index = 0;
                List<EntityClassSetting> items = new List<EntityClassSetting>();

                EntityClassSetting item = new EntityClassSetting() { ClassName = "JournalSale_Documents", Index = ++index, Items = qDocs.ToList() };
                items.Add(item);

                var qFirm = qDocs.GetNextResult<Firm>();
                item = new EntityClassSetting() { ClassName = "Firm", Index = ++index, Items = qFirm.ToList() };
                items.Add(item);

                var qFilial = qFirm.GetNextResult<Filial>();
                item = new EntityClassSetting() { ClassName = "Filial", Index = ++index, Items = qFilial.ToList() };
                items.Add(item);

                var qClassification = qFilial.GetNextResult<Classification>();
                item = new EntityClassSetting() { ClassName = "Classification", Index = ++index, Items = qClassification.ToList() };
                items.Add(item);

                var qEmployee = qClassification.GetNextResult<Employee>();
                item = new EntityClassSetting() { ClassName = "Employee", Index = ++index, Items = qEmployee.ToList() };
                items.Add(item);

                var qDirectory = qEmployee.GetNextResult<Directory>();
                item = new EntityClassSetting() { ClassName = "Directory", Index = ++index, Items = qDirectory.ToList() };
                items.Add(item);

                var qSubject = qDirectory.GetNextResult<Subject>();
                item = new EntityClassSetting() { ClassName = "Subject", Index = ++index, Items = qSubject.ToList() };
                items.Add(item);

                var qRevenue = qSubject.GetNextResult<Revenue>();
                item = new EntityClassSetting() { ClassName = "Revenue", Index = ++index, Items = qRevenue.ToList() };
                items.Add(item);

                var qContract = qRevenue.GetNextResult<Contract>();
                item = new EntityClassSetting() { ClassName = "Contract", Index = ++index, Items = qContract.ToList() };
                items.Add(item);

                var qDepartment = qContract.GetNextResult<Department>();
                item = new EntityClassSetting() { ClassName = "Department", Index = ++index, Items = qDepartment.ToList() };
                items.Add(item);

                var qAddress = qDepartment.GetNextResult<Address>();
                item = new EntityClassSetting() { ClassName = "Address", Index = ++index, Items = qAddress.ToList() };
                items.Add(item);

                var qTaxType = qAddress.GetNextResult<TaxType>();
                item = new EntityClassSetting() { ClassName = "TaxType", Index = ++index, Items = qTaxType.ToList() };
                items.Add(item);

                var qBusinessUnit = qTaxType.GetNextResult<BusinessUnit>();
                item = new EntityClassSetting() { ClassName = "BusinessUnit", Index = ++index, Items = qBusinessUnit.ToList() };
                items.Add(item);

                var qDocBoxLine = qBusinessUnit.GetNextResult<DocBoxLine>();
                item = new EntityClassSetting() { ClassName = "DocBoxLine", Index = ++index, Items = qDocBoxLine.ToList() };
                items.Add(item);

                var qBox = qDocBoxLine.GetNextResult<Box>();
                item = new EntityClassSetting() { ClassName = "Box", Index = ++index, Items = qBox.ToList() };
                items.Add(item);

                var qAction = qBox.GetNextResult<Entity.Action>();
                item = new EntityClassSetting() { ClassName = "Action", Index = ++index, Items = qAction.ToList() };
                items.Add(item);

                foreach (EntityClassSetting cls in items.Select(i => i).OrderBy(i => i.Index))
                    cls.Json = JsonConvert.SerializeObject(cls.Items);

                Dictionary<string, object> resultJson = new Dictionary<string, object>() {
                    {"Documents", items.Where( i => i.Index == 1).Select( i => i.Json).First()},
                    {"PageNumber", pageNumber},
                    {"PageCount", pages.Value},
                    {"Rows", totalRows.Value},
                    {"PerPage", perPage},
                };

                json = JsonConvert.SerializeObject(resultJson);
            }

            return json;
        }

        public string GetSubjects()
        {
            var subjects = db.Subject.Where(i => !i.IsDeleted).Take(100).ToList();

            string json = JsonConvert.SerializeObject(subjects);
            return json;
        }

        public virtual ActionResult filterHeaderTemplate()
        {
            return View();
        }

        // GET: Subjects/Details/5
        public virtual async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subject.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create([Bind(Include = "ID,CID,AccountantID,AddressID,Comments,DelDate,DirectorID,Email,ExtraCode,Fax,FullName,FullTextSearch,GUID,IconIndex,IndividualID,IsCentralized,IsCustomer,IsDeleted,IsImporter,IsPrivate,IsSupplier,Kind,LawAddressID,Name,OwnerID,PaperRegistrationCode,PaperVATCode,PaperVATExtraNO,ParentID,PaymentTypeID,Phone,RemindTime,ShortName,SID,SpecificationID,TaxTypeID,TST,WithoutStamp")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subject.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subject.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit([Bind(Include = "ID,CID,AccountantID,AddressID,Comments,DelDate,DirectorID,Email,ExtraCode,Fax,FullName,FullTextSearch,GUID,IconIndex,IndividualID,IsCentralized,IsCustomer,IsDeleted,IsImporter,IsPrivate,IsSupplier,Kind,LawAddressID,Name,OwnerID,PaperRegistrationCode,PaperVATCode,PaperVATExtraNO,ParentID,PaymentTypeID,Phone,RemindTime,ShortName,SID,SpecificationID,TaxTypeID,TST,WithoutStamp")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public virtual async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subject.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteConfirmed(long id)
        {
            Subject subject = await db.Subject.FindAsync(id);
            db.Subject.Remove(subject);
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
