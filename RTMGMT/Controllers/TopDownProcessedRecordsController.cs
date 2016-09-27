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
    public class TopDownProcessedRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: TopDownProcessedRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.TopDownProcessedRecords.ToListAsync());
        }

        // GET: TopDownProcessedRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopDownProcessedRecord topDownProcessedRecord = await db.TopDownProcessedRecords.FindAsync(id);
            if (topDownProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(topDownProcessedRecord);
        }

        // GET: TopDownProcessedRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopDownProcessedRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORT_LEVEL,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] TopDownProcessedRecord topDownProcessedRecord)
        {
            if (ModelState.IsValid)
            {
                db.TopDownProcessedRecords.Add(topDownProcessedRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(topDownProcessedRecord);
        }

        // GET: TopDownProcessedRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopDownProcessedRecord topDownProcessedRecord = await db.TopDownProcessedRecords.FindAsync(id);
            if (topDownProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(topDownProcessedRecord);
        }

        // POST: TopDownProcessedRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORT_LEVEL,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] TopDownProcessedRecord topDownProcessedRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topDownProcessedRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topDownProcessedRecord);
        }

        // GET: TopDownProcessedRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopDownProcessedRecord topDownProcessedRecord = await db.TopDownProcessedRecords.FindAsync(id);
            if (topDownProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(topDownProcessedRecord);
        }

        // POST: TopDownProcessedRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TopDownProcessedRecord topDownProcessedRecord = await db.TopDownProcessedRecords.FindAsync(id);
            db.TopDownProcessedRecords.Remove(topDownProcessedRecord);
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