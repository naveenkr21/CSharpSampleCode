using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpSampleCodeWithMVC.Models
{
    public class DocumentProcessors
    {
        private int processorId;

        public int ProcessorId
        {
            get { return processorId; }
            set { processorId = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private string curDirectory;

        public string CurDirectory
        {
            get { return curDirectory; }
            set { curDirectory = value; }
        }

        private List<string> validExtensions;

        public List<string> ValidExtensions
        {
            get { return validExtensions; }
            set { validExtensions = value; }
        }

        private int order;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }
    }
}