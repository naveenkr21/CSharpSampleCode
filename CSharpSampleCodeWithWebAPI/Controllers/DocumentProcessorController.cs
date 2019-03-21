using CSharpSampleCodeWithMVC.Models;
using DocumentProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CSharpSampleCodeWithWebAPI.Controllers
{


    public class DocumentProcessorController : ApiController
    {
        public async Task<string> Get()
        {
            List<DocumentProcessors> docp = null;
            try
            {
                //Load data from SQL or any other db or config file. Here I am hardcoding to run my application and not using SQL.
                docp = new List<DocumentProcessors>
                {
                   new DocumentProcessors() {ProcessorId=1, Name = "SimpleDocProcessor", CurDirectory = @"c:\temp\simplefiles", Order = 1, ValidExtensions = new List<string> { ".Pdf", ".tif", ".txt", ".docx"} },
                   new DocumentProcessors() {ProcessorId=2, Name = "ZipDocProcessor", CurDirectory = @"c:\temp\zipfiles", Order = 1, ValidExtensions = new List<string> { ".Pdf", ".tif", ".jpf", ".txt", ".docx" } }
                };

                IDocumentProcessor docprocessor = null;

                foreach (DocumentProcessors prc in docp)
                {
                    try
                    {
                        if (!Directory.Exists(utility.cmspath))
                        {
                            Directory.CreateDirectory(utility.cmspath);
                        }

                        if (!Directory.Exists(prc.CurDirectory))
                        {
                            Directory.CreateDirectory(prc.CurDirectory);
                        }

                        DocumentProcessorFactory dfactory = new DocumentProcessorFactory(prc.ProcessorId, prc.CurDirectory, prc.ValidExtensions);

                        using (docprocessor = dfactory.GetDocumentProcessorObject())
                        {
                            await docprocessor.ProcessFilesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        utility.logFile("CallerApi", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }
    }
}
