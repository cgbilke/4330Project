using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4330Project.Models;
using Sparc.TagCloud;

namespace _4330Project.Controllers
{
    public class TagCloudController : Controller
    {
        private AzureEntities db = new AzureEntities();

        public class TagCloudMeta
        {
            public System.Collections.Generic.IEnumerable<TagCloudTag> tagcloud;
            public Resource resources;
        }
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

            //TODO make all the keys 1-10
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

            if (resources.NumOfKey3 != null)
            {
                KeywordClass k3 = new KeywordClass((int)resources.NumOfKey3, resources.Keyword3);
                phrases.AddRange(k3.ToList());
            }

            if (resources.NumOfKey4 != null)
            {
                KeywordClass k4 = new KeywordClass((int)resources.NumOfKey4, resources.Keyword4);
                phrases.AddRange(k4.ToList());
            }

            if (resources.NumOfKey5 != null)
            {
                KeywordClass k5 = new KeywordClass((int)resources.NumOfKey5, resources.Keyword5);
                phrases.AddRange(k5.ToList());
            }

            if (resources.NumOfKey6 != null)
            {
                KeywordClass k6 = new KeywordClass((int)resources.NumOfKey6, resources.Keyword6);
                phrases.AddRange(k6.ToList());
            }

            if (resources.NumOfKey7 != null)
            {
                KeywordClass k7 = new KeywordClass((int)resources.NumOfKey7, resources.Keyword7);
                phrases.AddRange(k7.ToList());
            }

            if (resources.NumOfKey8 != null)
            {
                KeywordClass k8 = new KeywordClass((int)resources.NumOfKey8, resources.Keyword8);
                phrases.AddRange(k8.ToList());
            }

            if (resources.NumOfKey9 != null)
            {
                KeywordClass k9 = new KeywordClass((int)resources.NumOfKey9, resources.Keyword9);
                phrases.AddRange(k9.ToList());
            }

            if (resources.NumOfKey10 != null)
            {
                KeywordClass k10 = new KeywordClass((int)resources.NumOfKey10, resources.Keyword10);
                phrases.AddRange(k10.ToList());
            }
            //here
            TagCloudMeta objectToReturn = new TagCloudMeta();

            //List<TagCloudTag> model = new TagCloudAnalyzer()
            //    .ComputeTagCloud(phrases)
            //    .Shuffle().ToList();

            //objectToReturn.tagcloud = model;
            objectToReturn.resources = resources;

            var model = new TagCloudAnalyzer()
                .ComputeTagCloud(phrases)
                .Shuffle();
            return View(model.ToList());
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
