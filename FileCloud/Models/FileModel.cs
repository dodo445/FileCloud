using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FileCloud.Models
{
    public abstract class FileModel
    {
        abstract public void AddChild(Folder D);
        abstract public FileModel GetChild(int element_index);
        abstract public int listSize();
        abstract public string getSize();
        //abstract public string ConvertSize(double size);
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

        public string extension { get; set; }
        public string modifydate { get; set; }
        public int size { get; set; }
        public int parentId { get; set; }
        public int fileID { get; set; }
        public string status { get; set; }
        public bool isDirectory { get; set; }

        public string filename { get; set; }


        public int lastNodeID { get; set; } // to keep deepest element

    }

   public class PrimiteFile : Folder // Primitive type
    {
        public override void AddChild(Folder D)
        {
            
        }
        public override int listSize()
        {
                return 0;
            //throw new NotImplementedException();
        }
        public override string getSize()
        {
            return ConvertSize(size);
            //throw new NotImplementedException();
        }

        public override FileModel GetChild(int element_index)
        {
            return null;
            //throw new NotImplementedException();
        }
    }

   public class Folder : FileModel // Composite type

    {
        private List<FileModel>FileList = new List<FileModel>();
        public override string getSize()
        {
            int total_size = 0;
            foreach (var file in FileList)
            {
                total_size += file.size;
            }

            return ConvertSize(total_size);
            
        }
      
        public override int listSize()
        {
            return FileList.Count();
            //throw new NotImplementedException();
        }
        public override void AddChild(Folder D)
        {
            FileList.Add(D);
            //throw new NotImplementedException();
        }

        public override FileModel GetChild(int element_index)
        {
            return FileList.ElementAt(element_index);
            throw new NotImplementedException();
        }

        public void OrderElements(Folder folder,string option)
        {
            switch (option)
            {
                case "ByType": folder.FileList=(folder.FileList.OrderByDescending(d => d.isDirectory)).ToList(); break;
                case "ByName": folder.FileList=(folder.FileList.OrderBy(d => d.filename)).ToList(); break;
                case "ByDate": folder.FileList=(folder.FileList.OrderBy(d => d.modifydate)).ToList(); break;
                          
            }
        }

      

        public void CreateNew()
        {
              
        }

    }


        
}

//public string extension { get; set;}
//public string modifydate { get; set;}
//public string size { get; set; }
//public int parentId { get;set;}
//public int fileID { get; set;}

//public string status { get;set;}
//public bool isDirectory { get; set; }

//public string filename { get; set;}


//public int lastNodeID { get; set;} // to keep deepest element

