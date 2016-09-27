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
    public class FixRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: FixRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.FixRecords.ToListAsync());
        }

        // GET: FixRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixRecord fixRecord = await db.FixRecords.FindAsync(id);
            if (fixRecord == null)
            {
                return HttpNotFound();
            }
            return View(fixRecord);
        }

        // GET: FixRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FixRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] FixRecord fixRecord)
        {
            if (ModelState.IsValid)
            {
                db.FixRecords.Add(fixRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fixRecord);
        }

        // GET: FixRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixRecord fixRecord = await db.FixRecords.FindAsync(id);
            if (fixRecord == null)
            {
                return HttpNotFound();
            }
            return View(fixRecord);
        }

        // POST: FixRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] FixRecord fixRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fixRecord);
        }

        // GET: FixRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixRecord fixRecord = await db.FixRecords.FindAsync(id);
            if (fixRecord == null)
            {
                return HttpNotFound();
            }
            return View(fixRecord);
        }

        // POST: FixRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FixRecord fixRecord = await db.FixRecords.FindAsync(id);
            db.FixRecords.Remove(fixRecord);
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