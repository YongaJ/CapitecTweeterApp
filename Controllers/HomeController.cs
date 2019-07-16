using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapitecPayRoll.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeleteFollower() 
        {
            ViewBag.Message = "Deleting followers from user.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
       
                return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {

            foreach (var file in files)
            {
                if (file.FileName.ToLower() == "user.txt" || file.FileName.ToLower() == "tweet.txt")
                {
                    string filePath = file.FileName;
                    string pathExist = Path.Combine(Server.MapPath("~/UploadedFiles").ToString(), filePath);
                    string newPath = Path.Combine(Server.MapPath("~/UploadedFilesBackup").ToString(), filePath + DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
                    if (System.IO.File.Exists(pathExist))
                    {
                        System.IO.File.Move(pathExist, newPath);
                        file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), filePath));
                        return Json("Uploaded successfully. File backup made");
                    }
                    else if (!System.IO.File.Exists(pathExist))
                    {
                        file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), filePath));
                        return Json("Uploaded successfully");

                    }

                }
            }


            return Json("Only user.txt and tweet.txt files allowed");
        }
        

    }
}