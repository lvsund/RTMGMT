using CsvHelper;

using CsvHelper;

using RTMGMT.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RTMGMT.Controllers
{
    public class ReportsToRecordsController : Controller
    {
        private RTMGMTEntities db = new RTMGMTEntities();

        // GET: ReportsToRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.ReportsToRecords.ToListAsync());
        }

        // GET: ReportsToRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecord reportsToRecord = await db.ReportsToRecords.FindAsync(id);
            if (reportsToRecord == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecord);
        }

        // GET: ReportsToRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportsToRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] ReportsToRecord reportsToRecord)
        {
            if (ModelState.IsValid)
            {
                db.ReportsToRecords.Add(reportsToRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reportsToRecord);
        }

        // GET: ReportsToRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecord reportsToRecord = await db.ReportsToRecords.FindAsync(id);
            if (reportsToRecord == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecord);
        }

        // POST: ReportsToRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,REPORTING_ID,TITLE,NAME,REPORTS_TO_ID,EMPLOYEE_ID")] ReportsToRecord reportsToRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportsToRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reportsToRecord);
        }

        // GET: ReportsToRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportsToRecord reportsToRecord = await db.ReportsToRecords.FindAsync(id);
            if (reportsToRecord == null)
            {
                return HttpNotFound();
            }
            return View(reportsToRecord);
        }

        // POST: ReportsToRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReportsToRecord reportsToRecord = await db.ReportsToRecords.FindAsync(id);
            db.ReportsToRecords.Remove(reportsToRecord);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult CreateReportingHierarchy()
        {
            var affectedtables = db.Database.ExecuteSqlCommand("ProcessReportsToRecords");

            ViewBag.Message = " Reporting Hierarchy Created";

            return View();
        }

        public ActionResult DeleteAllData()
        {
            var affectedtables = db.Database.ExecuteSqlCommand("DeleteAllRecords");

            ViewBag.Message = "Delete Confirmed";

            return View();
        }

        public ActionResult ClearProcessingTables()
        {
            var affectedtables = db.Database.ExecuteSqlCommand("TruncateProcessedRecordsExceptCorrections");

            ViewBag.Message = " Processing Tables Cleared";

            return View();
        }

        public async Task<ActionResult> ImportRecords(HttpPostedFileBase file)
        {
            string path = null;

            List<ReportsToRecord> RTRs = new List<ReportsToRecord>();

            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\uploads\\" + fileName;
                    file.SaveAs(path);
                    var csv = new CsvReader(new StreamReader(path));
                    var RTRecordlist = csv.GetRecords<ReportsToRecord>();

                    foreach (var r in RTRecordlist)
                    {
                        ReportsToRecord RTR = new ReportsToRecord();
                        //RTR.Id = r.Id;
                        RTR.REPORTING_ID = r.REPORTING_ID;
                        RTR.TITLE = r.TITLE;
                        RTR.NAME = r.NAME;
                        RTR.REPORTS_TO_ID = r.REPORTS_TO_ID;
                        RTR.EMPLOYEE_ID = r.EMPLOYEE_ID;
                        RTRs.Add(RTR);
                        db.ReportsToRecords.Add(RTR);
                    }

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewData["error"] = "Upload Failed";
            }

            return View();
        }

        public void ExportReportsToRecords()
        {
            //StringWriter sw = new StringWriter();

            ////sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\"\"");
            ////sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\",\n");
            //sw.WriteLine("\"Id\",\"REPORTING_ID\",\"REPORTS_TO_ID\"");

            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment;filename=registereduser.csv");
            //Response.ContentType = "application/octet-stream";

            //var rcsets = db.RequiredCorrectionsSets.ToList();

            //            foreach (var rcset in rcsets)
            //            {
            //                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\"",
            //                //sw.WriteLine(string.Format("{0},{1},{2}\n",

            //rcset.Id,
            //                rcset.REPORTING_ID,
            //                rcset.REPORTS_TO_ID
            //                ));
            //            }

            //Response.Write(sw.ToString());
            //Response.End();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ReportsToRecordsExport.csv");
            Response.ContentType = "application/octet-stream";
            StringWriter sw = new StringWriter();

            var writer = new CsvWriter(sw);
            IEnumerable records = (db.ReportsToRecords.ToList());
            writer.WriteRecords(records);
            foreach (ReportsToRecord record in records)
            {
                writer.WriteRecord(record);
                // writer.NextRecord();
            }
            Response.Write(sw.ToString());
            Response.End();
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