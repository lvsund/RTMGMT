using CsvHelper;
using RTMGMT.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RTMGMT.Controllers
{
    public class RequiredCorrectionsSetsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: RequiredCorrectionsSets
        public async Task<ActionResult> Index()
        {
            return View(await db.RequiredCorrectionsSets.ToListAsync());
        }

        // GET: RequiredCorrectionsSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequiredCorrectionsSet requiredCorrectionsSet = await db.RequiredCorrectionsSets.FindAsync(id);
            if (requiredCorrectionsSet == null)
            {
                return HttpNotFound();
            }
            return View(requiredCorrectionsSet);
        }

        // GET: RequiredCorrectionsSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequiredCorrectionsSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,REPORTS_TO_ID")] RequiredCorrectionsSet requiredCorrectionsSet)
        {
            if (ModelState.IsValid)
            {
                db.RequiredCorrectionsSets.Add(requiredCorrectionsSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(requiredCorrectionsSet);
        }

        // GET: RequiredCorrectionsSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequiredCorrectionsSet requiredCorrectionsSet = await db.RequiredCorrectionsSets.FindAsync(id);
            if (requiredCorrectionsSet == null)
            {
                return HttpNotFound();
            }
            return View(requiredCorrectionsSet);
        }

        // POST: RequiredCorrectionsSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,REPORTS_TO_ID")] RequiredCorrectionsSet requiredCorrectionsSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requiredCorrectionsSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(requiredCorrectionsSet);
        }

        // GET: RequiredCorrectionsSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequiredCorrectionsSet requiredCorrectionsSet = await db.RequiredCorrectionsSets.FindAsync(id);
            if (requiredCorrectionsSet == null)
            {
                return HttpNotFound();
            }
            return View(requiredCorrectionsSet);
        }

        // POST: RequiredCorrectionsSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RequiredCorrectionsSet requiredCorrectionsSet = await db.RequiredCorrectionsSets.FindAsync(id);
            db.RequiredCorrectionsSets.Remove(requiredCorrectionsSet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public void ExportCorrections()
        {
            //            StringWriter sw = new StringWriter();

            //            //sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\"\"");
            //            //sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\",\n");
            //            sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\"");

            //            Response.ClearContent();
            //            Response.AddHeader("content-disposition", "attachment;filename=registereduser.csv");
            //            Response.ContentType = "application/octet-stream";

            //            var rcsets = db.RequiredCorrectionsSets.ToList();

            //            foreach (var rcset in rcsets)
            //            {
            //                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\"",
            //                //sw.WriteLine(string.Format("{0},{1},{2}\n",

            //rcset.Id,
            //                rcset.REPORTING_ID,
            //                rcset.REPORTS_TO_ID
            //                ));
            //            }
            //            Response.Write(sw.ToString());
            //            Response.End();
            //        }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=RequiredCorrectionsExport.csv");
            Response.ContentType = "application/octet-stream";
            StringWriter sw = new StringWriter();

            var writer = new CsvWriter(sw);
            IEnumerable records = (db.RequiredCorrectionsSets.ToList());
            writer.WriteRecords(records);
            foreach (RequiredCorrectionsSet record in records)
            {
                writer.WriteRecord(record);
                //writer.NextRecord();
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public ActionResult ClearCorrections()
        {
            var affectedtables = db.Database.ExecuteSqlCommand("DeleteCorrections");

            ViewBag.Message = "Corrections Cleared";

            return View();
        }

        public ActionResult TestYourCorrections()
        {
            var affectedtables = db.Database.ExecuteSqlCommand("UpdateReportsToWithCorrections");

            ViewBag.Message = " Reports To updated with Corrections - please retest by regeneration the hierarchy";

            return View();
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