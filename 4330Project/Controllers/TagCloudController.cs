using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4330Project.Models;
//using Sparc.TagCloud;

namespace _4330Project.Controllers
{
    public class TagCloudController : Controller
    {
        private AzureEntities db = new AzureEntities();

        // GET: TagCloud
        private class KeywordClass
        {
            int numRepeat;
            string word;

            public KeywordClass(int numRepeat, string word)
            {
                this.numRepeat = numRepeat;
                this.word = word;
            }
            public List<string> ToList()
            {
                List<string> ret = new List<string>();

                for (int i = 0; i < numRepeat; i++)
                {
                    ret.Add(word);
                }
                return ret;

                
            }
        }
        public ActionResult Index(int resourceID)
        {
            //var resources = db.Resources.Include(r => r.AspNetUser);
            var resources = db.Resources.Where(x => x.id == resourceID).FirstOrDefault();
            if(resources == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            List<string> phrases = new List<string>();

            //make all the keys 1-10
            if(resources.NumOfKey1 != null)
            {
                KeywordClass k1 = new KeywordClass((int)resources.NumOfKey1, resources.Keyword1);
                phrases.AddRange(k1.ToList());
            }
      
            if(resources.NumOfKey2 != null)
            {
                KeywordClass k2 = new KeywordClass((int)resources.NumOfKey2, resources.Keyword2);
                phrases.AddRange(k2.ToList());
            }
          


            var model = new TagCloudAnalyzer()
                .ComputeTagCloud(phrases)
                .Shuffle();
            return View(model);*/
            return View();
        }

        // GET: TagCloud/Details/5
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

        // GET: TagCloud/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: TagCloud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,Doc_Name,KEYWORD,TIMES_REPEATED")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", resource.user_id);
            return View(resource);
        }

        // GET: TagCloud/Edit/5
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

        // POST: TagCloud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,Doc_Name,KEYWORD,TIMES_REPEATED")] Resource resource)
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

        // GET: TagCloud/Delete/5
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

        // POST: TagCloud/Delete/5
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
