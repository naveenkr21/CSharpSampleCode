using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace DocumentProcessor
{
    /// <summary>
    /// Developer :- Naveen Kumar Singh
    /// Dated on - 20-Mar-2019
    /// Purpose : Base Class, will contain all common functionality related to project.
    /// </summary>
    public abstract class DocumentProcessorBase
    {
        /// <summary>
        /// Method to validate the file naming convetion.
        /// </summary>
        /// <param name="filename">String Filename</param>
        /// <returns>Bool true/False.</returns>
        public  bool IsValidNamingConvention(string filename)
        {

            //Logic to check file naming convention.
            if (filename.Contains("_"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to check valid extension of a document.
        /// </summary>
        /// <param name="finfo">FileInfo object of current file.</param>
        /// <param name="ValidExtention"></param>
        /// <returns></returns>
        public bool IsValidExtention(FileInfo finfo, List<string> ValidExtention)
        {
            return ValidExtention.Where(x => x.ToUpper() == finfo.Extension.ToUpper()).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Method to extract all the compressed files on same place. 
        /// Using recursion it is traversing in all subsequent folders also.
        /// </summary>
        /// <param name="d">DirectoryInfo d source directory path</param>
        protected virtual void decompress(DirectoryInfo d)
        {
            if (d.Exists)
            {
                foreach (FileInfo finfo in d.GetFiles("*.zip")) //iterating all the subsequent files and trying to decompress and delete decompressd zip files
                {
                    ZipFile.ExtractToDirectory(finfo.FullName, d.FullName);
                    finfo.Delete();
                }


                foreach (DirectoryInfo di in d.GetDirectories()) //Iterationg all the subsequent directories and calling same method recursively.
                {
                    if (di.Name.ToUpper() == "EXCEPTION")
                        continue;

                    decompress(di);
                }
            }
        }

        /// <summary>
        /// A method to move document in exception, by adding proper error in file name.
        /// </summary>
        /// <param name="d">DirectoryInfo object</param>
        /// <param name="error">docError error</param>
        /// <param name="fname">FileInfo finfo</param>
        protected virtual void MoveToException(DirectoryInfo d, docError error, FileInfo fname)
        {
            if (!Directory.Exists(Path.Combine(d.FullName, "Exception")))
                Directory.CreateDirectory(Path.Combine(d.FullName, "Exception"));
            fname.MoveTo(Path.Combine(d.FullName, "Exception", fname.Name.Replace(fname.Extension, "_")+ error.ToString()+ fname.Extension));
        }

        /// <summary>
        /// Enumrated value for possible error types.
        /// </summary>
        protected enum docError
        {
            InvalidFileNamingConvention,
            InvalidExtention,
            InvalidFileLength
        }

        /// <summary>
        /// Method to move document to root/target folder from its subsequent folder recursively. 
        /// 
        /// </summary>
        /// <param name="dsource">DirectoryInfo source</param>
        /// <param name="dtarget">DirectoryInfo Target</param>
        protected virtual void DocumentMoveToRoot(DirectoryInfo dsource, DirectoryInfo dtarget)
        {
            if (dsource.Exists && dtarget.Exists)
            {

                if (dsource != dtarget)
                {
                    foreach (var finfo in dsource.GetFiles())
                    {
                        if (finfo.Extension.ToUpper() == ".DB")
                        {
                            try { finfo.Delete(); } catch { }
                            continue;
                        }
                        int ctr = 0;
                        while (File.Exists(Path.Combine(dtarget.FullName, finfo.Name.Replace(finfo.Extension, "_") + ctr.ToString() + finfo.Extension)))
                        {
                            ctr++;
                        }
                        finfo.MoveTo(Path.Combine(dtarget.FullName, finfo.Name.Replace(finfo.Extension, "_") + ctr.ToString() + finfo.Extension));
                    }
                }

                foreach (var dir in dsource.GetDirectories())
                {
                    if (dir.Name.ToUpper() == "EXCEPTION")
                        continue;
                    DocumentMoveToRoot(dir, dtarget);
                    if (dir.GetFiles().Length == 0)
                        dir.Delete();

                }
            }
        }

        /// <summary>
        /// Method to move files in cms
        /// </summary>
        /// <param name="finfo"></param>
        protected void MoveDocumentInCMS(FileInfo finfo)
        {
            int ctr = 0;
            while (File.Exists(Path.Combine(utility.cmspath, finfo.Name.Replace(finfo.Extension, "_") + ctr.ToString() + finfo.Extension)))
            {
                ctr++;
            }
            finfo.MoveTo(Path.Combine(utility.cmspath, finfo.Name.Replace(finfo.Extension, "_") + ctr.ToString() + finfo.Extension));
        }
    }
}
