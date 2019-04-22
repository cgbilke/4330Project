using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4330Project.Models;

namespace _4330Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var model = new UploadFileViewModel();
            //return View(model);
            return View();
        }

        /*[HttpPost]
        public ActionResult Index(UploadFileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] uploadedFile = new byte[model.File.InputStream.Length];
            model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            // now you could pass the byte array to your model and store wherever
            // you intended to store it

            return Content("File has been uploaded.");
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddDoc()
        {
            ViewBag.Message = "Your add documents page.";

            return View();
        }
    }
}
