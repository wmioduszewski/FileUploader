using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUploader.FileAnalyzer;
using FileUploader.WebApp.DAL;
using FileUploader.WebApp.Models;

namespace FileUploader.WebApp.Controllers
{
    public class FileStatisticsController : Controller
    {
        private readonly StatisticsContext _db = new StatisticsContext();
        private readonly FileAnalyzerClient _fileAnalyzerClient = new FileAnalyzerClient();
        private readonly string modalErrorKey = "FileUpload";

        // GET: FileStatistics
        public ActionResult Index()
        {
            var fileStatistics = _db.FileStatisticsEntities.OrderByDescending(x => x.WordsCount);
            return View(fileStatistics.ToList());            
        }

        // GET: FileStatistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = _db.FileStatisticsEntities.Find(id);
            if (fileStatisticsEntity == null)
            {
                return HttpNotFound();
            }
            return View(fileStatisticsEntity);
        }

        // GET: FileStatistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileStatistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase upload)
        {
            ValidateUploadedFile(upload);
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    try
                    {
                        var stats = _fileAnalyzerClient.ComputeStatistics(upload);
                        var fileStatisticsEntity = new FileStatisticsEntity(stats);
                        _db.FileStatisticsEntities.Add(fileStatisticsEntity);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(modalErrorKey, string.Format("Unexpected exception {0}", ex.Message));
                    }
                }
            }
            return View();
        }

        private void ValidateUploadedFile(HttpPostedFileBase httpPostedFileBase)
        {
            if (httpPostedFileBase == null)
            {
                ModelState.AddModelError(modalErrorKey, "This field is required");
                return;
            }

            if (httpPostedFileBase.ContentLength > MvcApplication.MaxRequestLengthInKB * 1024)
            {
                ModelState.AddModelError(modalErrorKey, "Chosen file is too large.");
                return;
            }

            var extension = Path.GetExtension(httpPostedFileBase.FileName);
            if (extension == null || !_fileAnalyzerClient.SupportedExtensions.Contains(extension.ToLower()))
            {
                ModelState.AddModelError(modalErrorKey, "Please choose handled file extension.");
            }
        }

        // GET: FileStatistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = _db.FileStatisticsEntities.Find(id);
            if (fileStatisticsEntity == null)
            {
                return HttpNotFound();
            }
            return View(fileStatisticsEntity);
        }

        // POST: FileStatistics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ID")] FileStatisticsEntity fileStatisticsEntity, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (fileStatisticsEntity == null)
                {
                    return HttpNotFound();
                }

                ValidateUploadedFile(upload);
                var stats = _fileAnalyzerClient.ComputeStatistics(upload);
                var newFileStatisticsEntity = new FileStatisticsEntity(stats) {ID = fileStatisticsEntity.ID};
                _db.Entry(newFileStatisticsEntity).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FileStatistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = _db.FileStatisticsEntities.Find(id);
            if (fileStatisticsEntity == null)
            {
                return HttpNotFound();
            }
            return View(fileStatisticsEntity);
        }

        // POST: FileStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileStatisticsEntity fileStatisticsEntity = _db.FileStatisticsEntities.Find(id);
            _db.FileStatisticsEntities.Remove(fileStatisticsEntity);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}