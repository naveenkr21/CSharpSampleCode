using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor
{
  public interface IDocumentProcessor:IDisposable
    {
        Task ProcessFilesAsync();
    }
}
