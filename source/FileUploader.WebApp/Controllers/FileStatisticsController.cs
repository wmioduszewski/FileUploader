using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FileUploader.WebApp.DAL;
using FileUploader.WebApp.Models;

namespace FileUploader.WebApp.Controllers
{
    public class FileStatisticsController : Controller
    {
        private StatisticsContext db = new StatisticsContext();

        // GET: FileStatistics
        public ActionResult Index()
        {
            return View(db.FileStatisticsEntities.ToList());
        }

        // GET: FileStatistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = db.FileStatisticsEntities.Find(id);
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
        public ActionResult Create(
            [Bind(Include = "ID,Filename,WordsCount,LinesCount")] FileStatisticsEntity fileStatisticsEntity)
        {
            if (ModelState.IsValid)
            {
                db.FileStatisticsEntities.Add(fileStatisticsEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileStatisticsEntity);
        }

        // GET: FileStatistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = db.FileStatisticsEntities.Find(id);
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
            [Bind(Include = "ID,Filename,WordsCount,LinesCount")] FileStatisticsEntity fileStatisticsEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileStatisticsEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileStatisticsEntity);
        }

        // GET: FileStatistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStatisticsEntity fileStatisticsEntity = db.FileStatisticsEntities.Find(id);
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
            FileStatisticsEntity fileStatisticsEntity = db.FileStatisticsEntities.Find(id);
            db.FileStatisticsEntities.Remove(fileStatisticsEntity);
            db.SaveChanges();
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