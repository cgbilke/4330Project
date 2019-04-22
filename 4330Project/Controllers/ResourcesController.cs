using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;
using _4330Project.Models;

namespace _4330Project.Controllers
{
    public class ResourcesController : Controller
    {
        private AzureEntities db = new AzureEntities();

        // GET: Resources
        public ActionResult Index()
        {
            var resources = db.Resources.Include(r => r.AspNetUser);
            return View(resources.ToList());
        }

        public ActionResult AddDoc()
        {
            var resources = db.Resources.Include(r => r.AspNetUser);
            return View(resources.ToList());
        }

        private string SaveDoc(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }

        // GET: Resources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,Doc_Name,Keyword1,Doc,Doc_Path")] Resource resource)
        {
            if (ModelState.IsValid)
            {

              
                var _Doc_Path = SaveDoc(resource.Doc);

                string words = DocumentHandler.convertDocToString(_Doc_Path);
                var wordsParsed = DocumentHandler.parseString(words, DocumentHandler.stopWords);

                var uploadFile = new Resource()
                {
                    id = resource.id,
                    user_id = resource.user_id,
                    Doc_Name = resource.Doc_Name,
                    Keyword1 = wordsParsed[0].Key,
                    NumOfKey1 = wordsParsed[0].Value,
                    Doc_Path = _Doc_Path
                };
                db.Resources.Add(uploadFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", resource.user_id);
            return View(resource);
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", resource.user_id);
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,title")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", resource.user_id);
            return View(resource);
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
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
