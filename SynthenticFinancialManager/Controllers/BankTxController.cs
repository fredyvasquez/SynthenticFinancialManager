using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynthenticFinancialManager.Models;
using SynthenticFinancialManager.Filters;

namespace SynthenticFinancialManager.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class BankTxController : Controller
    {
        //private TXContext db = new TXContext();
        private Business.BankTxManager txManager = new Business.BankTxManager();

        //IsFraud
        //NameDest
        //TransactionDate
        // GET: /BankTx/
        [Authorize(Roles = "Manager, Administrator, Superintendent")]
        public ActionResult Index(string searchString, bool? fraud)
        {
            var transactions = txManager.Search(searchString, fraud);
            return View( transactions.ToList() );
        }

        //
        // GET: /BankTx/Details/5

        public ActionResult Details(int id = 0)
        {
            BankTX banktx = txManager.Details(id);
            if (banktx == null)
            {
                return HttpNotFound();
            }
            return View(banktx);
        }

        //
        // GET: /BankTx/Create
        [Authorize(Roles = "Assistant, Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BankTx/Create

        [HttpPost]
        [Authorize(Roles = "Assistant, Administrator")]
        public ActionResult Create(BankTX banktx)
        {
            if (ModelState.IsValid)
            {
                txManager.Create(banktx);
                return RedirectToAction("Index");
            }

            return View(banktx);
        }

        //
        // GET: /BankTx/Edit/5
        [Authorize(Roles = "Administrator, Superintendent")]
        public ActionResult Edit(int id = 0)
        {
            BankTX banktx = txManager.Get(id);
            if (banktx == null)
            {
                return HttpNotFound();
            }
            return View(banktx);
        }

        //
        // POST: /BankTx/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administrator, Superintendent")]
        public ActionResult Edit(BankTX banktx)
        {
            if (ModelState.IsValid)
            {
                txManager.Get(banktx);
                return RedirectToAction("Index");
            }
            return View(banktx);
        }

        //
        // GET: /BankTx/Edit/5
        [Authorize(Roles = "Administrator, Superintendent")]
        public ActionResult MarkAsFraud(int id)
        {
            BankTX banktx = txManager.Get(id);

            return MarkAsFraud(banktx);
        }

        //
        // GET: /BankTx/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Superintendent")]
        public ActionResult MarkAsFraud(BankTX banktx)
        { 
            //BankTX banktx = txManager.Get(id);
            if (banktx == null)
            {
                return HttpNotFound();
            }

            banktx.isFlaggedFraud = !banktx.isFlaggedFraud;
            banktx.isFraud = !banktx.isFraud;
            txManager.Get(banktx);

            //return View(banktx);
            return RedirectToAction("Index");
        }

        //
        // GET: /BankTx/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            BankTX banktx = txManager.Get(id);
            if (banktx == null)
            {
                return HttpNotFound();
            }
            return View(banktx);
        }

        //
        // POST: /BankTx/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            
            txManager.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            txManager.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}