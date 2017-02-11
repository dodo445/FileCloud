using FileCloud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileCloud.FileMethods
{
    public class DirectoryOperations
    {


        public FileModel GetDirectoryContent(string parentid) // gets parent id and shows it's content
        {
            Folder directory = new Folder();
            Folder file_model;
            directory.filename = "New Directory";
          
            int parentID = Int32.Parse(parentid);
            using (var db = new FileCloudDatabaseEntities())
            {
                var Files = db.File.Where(F => F.parentId == parentID).ToList();
                //directory.filename = db.File.Where(F => F.fileID == parentID).FirstOrDefault().filename;

                foreach (var file in Files)
                {
                    if (!file.isDirectory) { file_model = new PrimiteFile(); }
                    else { file_model = new Folder(); }

                   
                    file_model.filename = file.filename;
                    file_model.parentId = file.parentId;
                    file_model.size = file.size.GetValueOrDefault();
                    file_model.isDirectory = file.isDirectory;
                    file_model.lastNodeID = file.lastNodeID.GetValueOrDefault();
                    file_model.modifydate = file.modifydate;
                    directory.AddChild(file_model);
                }

            }

            directory.OrderElements(directory, "ByType");
            return directory;
          }
        
        public void SaveFileToDatabase(HttpPostedFileBase uploadedFile,string ID)
        {
            int parentID = Int32.Parse(ID);
            using (var db = new FileCloudDatabaseEntities())
            {
                var new_file = new File();
                new_file.filename = uploadedFile.FileName;
                new_file.modifydate = DateTime.Now.ToShortDateString();
                new_file.lastNodeID = 0;
                new_file.size =uploadedFile.ContentLength;
                new_file.parentId = parentID;
                new_file.isDirectory = false;
                db.File.Add(new_file);
                db.SaveChanges();

                var addedFile = db.File.Where(f => f.filename == new_file.filename && f.parentId == new_file.parentId);


                uploadedFile.SaveAs(Path.Combine(@"C:\Users\dodo1\Desktop\DEYTEKPROJE\FileCloud\FileCloud\MetaData\" + addedFile.FirstOrDefault().fileID+Path.GetExtension(uploadedFile.FileName)));
                UpdateState(parentID,new_file.size.GetValueOrDefault());
             }
            }
        public void SaveFileToDatabase(string foldername,int id)
        {   
            
            using (var db = new FileCloudDatabaseEntities())
            {
                var new_file = new File();
                new_file.filename = foldername;
                new_file.modifydate = DateTime.Now.ToShortDateString();
                new_file.lastNodeID = 0;
                new_file.size =0;
                new_file.parentId = id;
                new_file.isDirectory = true;
                db.File.Add(new_file);
                db.SaveChanges();
            }
        }
        public string ConvertSize(double size)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (size >= 1024 && ++order < sizes.Length)
            {
                size = size / 1024;
            }
            return String.Format("{0:0.##} {1}", size, sizes[order]);
        }

        public void UpdateState(int id , int size)

        {
            using (var db = new FileCloudDatabaseEntities())
            {   
                var dbFile = db.File.Where(F => F.fileID == id).FirstOrDefault();
                if (dbFile != null)
                {
                    dbFile.size += size;
                    db.SaveChanges();
                    UpdateState(dbFile.parentId, dbFile.size.GetValueOrDefault());
                }

                
            }
        }


    }
}