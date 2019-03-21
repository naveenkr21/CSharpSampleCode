using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor
{
  public  class ZipDocumentProcessor:DocumentProcessorBase, IDocumentProcessor, IDisposable
    {
        public readonly DirectoryInfo CurDirtoProcess = null;
        public readonly List<string> ValidExtentions = null;
        bool disposed = false;//flag to determine, has Dispose already been called.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true); //Intantiate a SafeHandle instance
            
        /// <summary>
        /// Parameterized Constructor setting fields for further processing.
        /// </summary>
        /// <param name="_CurDirtoProcess">DirectoryInfo current Dir for process</param>
        /// <param name="_ValidExtentions">List<string> Valid extensions</param>
        public ZipDocumentProcessor(DirectoryInfo _CurDirtoProcess, List<string> _ValidExtentions)
        {
            CurDirtoProcess = _CurDirtoProcess;
            ValidExtentions = _ValidExtentions;

        }

        int totalfileCount, ExceptionFileCount, TotalSuccessfullyProcessed, existingException = 0;

        /// <summary>
        /// Method to process simple files which are already decompressed and indexed.
        /// </summary>
        /// <returns>Bool True/False</returns>
        public async Task ProcessFilesAsync()
        {
            try
            {
                if (CurDirtoProcess != null && CurDirtoProcess.Exists) // Checking whether given directory exists?
                {
                    decompress(CurDirtoProcess);
                    DocumentMoveToRoot(CurDirtoProcess, CurDirtoProcess); //Move all files from all subsequent folders to root folder.

                    totalfileCount = CurDirtoProcess.GetFiles().Length; //get total file available
                    foreach (var finfo in CurDirtoProcess.GetFiles()) //Getting all the available files in given root directory
                    {

                        //Validations
                        if (finfo.Length > 1000) //File length should not be more than 1000 bytes. if valiations fails move file in exception
                        {
                            MoveToException(CurDirtoProcess, docError.InvalidFileLength, finfo);
                            ExceptionFileCount++;
                            continue;
                        }

                        if (!IsValidNamingConvention(finfo.Name)) //File naming convention must be followed as per instruction given. else move file in exception
                        {
                            MoveToException(CurDirtoProcess, docError.InvalidFileNamingConvention, finfo);
                            ExceptionFileCount++;
                            continue;
                        }

                        if (!IsValidExtention(finfo, ValidExtentions)) //Files type should be from specified extention
                        {
                            MoveToException(CurDirtoProcess, docError.InvalidExtention, finfo);
                            ExceptionFileCount++;
                            continue;
                        }

                        MoveDocumentInCMS(finfo);
                        TotalSuccessfullyProcessed++;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log Exception
                utility.logFile(this.GetType().ToString(), ex);
            }
            finally
            {
                //log document processing detail
                utility.logFile(this.GetType().ToString(), totalfileCount, ExceptionFileCount, TotalSuccessfullyProcessed, existingException);
            }
        }

        /// <summary>
        /// Public implementation of dispose pattern callable by consumers
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected Implementation of Dispose pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                handle.Dispose();

            disposed = true;

        }
    }
}
