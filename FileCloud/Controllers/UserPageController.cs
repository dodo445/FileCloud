using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileCloud.SecurityService;
using FileCloud.Models;
using System.IO;
using FileCloud.FileMethods;
using System.Diagnostics;

namespace FileCloud.Controllers
{
    
    public class UserPageController : Controller
    {
        DirectoryOperations d_operations = new DirectoryOperations();
        // GET: UserPage
        [UserAuthorize]
        public ActionResult Index()
        {
            
            if (Session["LogedUser"] == null) { return RedirectToAction("Index", "Home"); }
            //return View(d_operations.GetDirectoryContent());
            return View(d_operations.GetDirectoryContent("17"));

        }
        
        public void CreateNewFolder(Folder filemodel)

        {
            d_operations.SaveFileToDatabase("FolderDeneme4", filemodel.fileID);
            Debug.WriteLine("Function Called...");
            
        }

    
        
        public ActionResult SaveUploadedFile(string id)
        {
            
          
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {  
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];                             
                    d_operations.SaveFileToDatabase(file,"17");                               
                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
              Json(new { Message = fName });
                return Json(new { Message = "Successfully saved" });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
} 
