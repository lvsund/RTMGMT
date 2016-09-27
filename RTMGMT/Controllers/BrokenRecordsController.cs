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
    public class BrokenRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: BrokenRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.BrokenRecords.ToListAsync());
        }

        // GET: BrokenRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokenRecord brokenRecord = await db.BrokenRecords.FindAsync(id);
            if (brokenRecord == null)
            {
                return HttpNotFound();
            }
            return View(brokenRecord);
        }

        // GET: BrokenRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrokenRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] BrokenRecord brokenRecord)
        {
            if (ModelState.IsValid)
            {
                db.BrokenRecords.Add(brokenRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(brokenRecord);
        }

        // GET: BrokenRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokenRecord brokenRecord = await db.BrokenRecords.FindAsync(id);
            if (brokenRecord == null)
            {
                return HttpNotFound();
            }
            return View(brokenRecord);
        }

        // POST: BrokenRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,RT_STRING,EMPLOYEE_ID")] BrokenRecord brokenRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brokenRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brokenRecord);
        }

        // GET: BrokenRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokenRecord brokenRecord = await db.BrokenRecords.FindAsync(id);
            if (brokenRecord == null)
            {
                return HttpNotFound();
            }
            return View(brokenRecord);
        }

        // POST: BrokenRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BrokenRecord brokenRecord = await db.BrokenRecords.FindAsync(id);
            db.BrokenRecords.Remove(brokenRecord);
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