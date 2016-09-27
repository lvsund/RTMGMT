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
    public class ReportsToRecordStagingsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: ReportsToRecordStagings
        public async Task<ActionResult> Index()
        {
            return View(await db.ReportsToRecordStagings.ToListAsync());
        }

        // GET: ReportsToRecordStagings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecordStaging reportsToRecordStaging = await db.ReportsToRecordStagings.FindAsync(id);
            if (reportsToRecordStaging == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecordStaging);
        }

        // GET: ReportsToRecordStagings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportsToRecordStagings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] ReportsToRecordStaging reportsToRecordStaging)
        {
            if (ModelState.IsValid)
            {
                db.ReportsToRecordStagings.Add(reportsToRecordStaging);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reportsToRecordStaging);
        }

        // GET: ReportsToRecordStagings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecordStaging reportsToRecordStaging = await db.ReportsToRecordStagings.FindAsync(id);
            if (reportsToRecordStaging == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecordStaging);
        }

        // POST: ReportsToRecordStagings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] ReportsToRecordStaging reportsToRecordStaging)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportsToRecordStaging).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reportsToRecordStaging);
        }

        // GET: ReportsToRecordStagings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecordStaging reportsToRecordStaging = await db.ReportsToRecordStagings.FindAsync(id);
            if (reportsToRecordStaging == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecordStaging);
        }

        // POST: ReportsToRecordStagings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReportsToRecordStaging reportsToRecordStaging = await db.ReportsToRecordStagings.FindAsync(id);
            db.ReportsToRecordStagings.Remove(reportsToRecordStaging);
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