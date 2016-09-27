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
    public class BottomUpProcessedRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: BottomUpProcessedRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.BottomUpProcessedRecords.ToListAsync());
        }

        // GET: BottomUpProcessedRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottomUpProcessedRecord bottomUpProcessedRecord = await db.BottomUpProcessedRecords.FindAsync(id);
            if (bottomUpProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(bottomUpProcessedRecord);
        }

        // GET: BottomUpProcessedRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BottomUpProcessedRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RT_STRING")] BottomUpProcessedRecord bottomUpProcessedRecord)
        {
            if (ModelState.IsValid)
            {
                db.BottomUpProcessedRecords.Add(bottomUpProcessedRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bottomUpProcessedRecord);
        }

        // GET: BottomUpProcessedRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottomUpProcessedRecord bottomUpProcessedRecord = await db.BottomUpProcessedRecords.FindAsync(id);
            if (bottomUpProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(bottomUpProcessedRecord);
        }

        // POST: BottomUpProcessedRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RT_STRING")] BottomUpProcessedRecord bottomUpProcessedRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bottomUpProcessedRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bottomUpProcessedRecord);
        }

        // GET: BottomUpProcessedRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottomUpProcessedRecord bottomUpProcessedRecord = await db.BottomUpProcessedRecords.FindAsync(id);
            if (bottomUpProcessedRecord == null)
            {
                return HttpNotFound();
            }
            return View(bottomUpProcessedRecord);
        }

        // POST: BottomUpProcessedRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BottomUpProcessedRecord bottomUpProcessedRecord = await db.BottomUpProcessedRecords.FindAsync(id);
            db.BottomUpProcessedRecords.Remove(bottomUpProcessedRecord);
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