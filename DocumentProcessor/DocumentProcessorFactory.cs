using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor
{
    /// <summary>
    /// Document Processor Factory. This class will provide object of available concrete class according to requirement.
    /// This class provides abastraction between class consumer(client) and supplier(service).
    /// </summary>
 public   class  DocumentProcessorFactory
    {
        int processorType = 0; string dir = ""; List<string> ValidExtensions =null;

        /// <summary>
        /// Parameterized constructor taking required inputs to set informations for concreate class.
        /// </summary>
        /// <param name="_processorType">Int Type of processor</param>
        /// <param name="_dir">string dirpath associated with current document processor</param>
        /// <param name="_ValidExtensions">List<string> List of valid extenstions for this processor.</string></param>
        public DocumentProcessorFactory(int _processorType, string _dir, List<string> _ValidExtensions)  
        {
            processorType = _processorType;
            dir = _dir;
            ValidExtensions = _ValidExtensions;
        }

        /// <summary>
        /// Method to get appropriate concrete class object from factory according given type of factory. 
        /// </summary>
        /// <returns>IDocumentProcessor obj</returns>
        public IDocumentProcessor GetDocumentProcessorObject()
        {
            IDocumentProcessor Obj = null;
            switch (processorType)
            {
                case 1:
                    Obj = new SimpleDocProcessor(new DirectoryInfo(dir), ValidExtensions);
                    break;
                case 2:
                    Obj = new ZipDocumentProcessor(new DirectoryInfo(dir), ValidExtensions);
                    break;
                default:
                    throw new Exception("Invalid processor type.");
            }
            return Obj;
        }
    }
}
