using RTMGMT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RTMGMT.Controllers
{
    public class DuplicateRTRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: DuplicateRTRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.DuplicateRTRecords.ToListAsync());
        }

        // GET: DuplicateRTRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuplicateRTRecord duplicateRTRecord = await db.DuplicateRTRecords.FindAsync(id);
            if (duplicateRTRecord == null)
            {
                return HttpNotFound();
            }
            return View(duplicateRTRecord);
        }

        // GET: DuplicateRTRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DuplicateRTRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] DuplicateRTRecord duplicateRTRecord)
        {
            if (ModelState.IsValid)
            {
                db.DuplicateRTRecords.Add(duplicateRTRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(duplicateRTRecord);
        }

        // GET: DuplicateRTRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuplicateRTRecord duplicateRTRecord = await db.DuplicateRTRecords.FindAsync(id);
            if (duplicateRTRecord == null)
            {
                return HttpNotFound();
            }
            return View(duplicateRTRecord);
        }

        // POST: DuplicateRTRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] DuplicateRTRecord duplicateRTRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duplicateRTRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(duplicateRTRecord);
        }

        // GET: DuplicateRTRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuplicateRTRecord duplicateRTRecord = await db.DuplicateRTRecords.FindAsync(id);
            if (duplicateRTRecord == null)
            {
                return HttpNotFound();
            }
            return View(duplicateRTRecord);
        }

        // POST: DuplicateRTRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DuplicateRTRecord duplicateRTRecord = await db.DuplicateRTRecords.FindAsync(id);
            db.DuplicateRTRecords.Remove(duplicateRTRecord);
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