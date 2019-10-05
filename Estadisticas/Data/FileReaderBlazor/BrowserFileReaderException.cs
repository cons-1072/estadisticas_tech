using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estadisticas.Data.FileReaderBlazor
{
    public class BrowserFileReaderException : Exception
    {
        internal BrowserFileReaderException(string message) : base(message)
        {
        }
    }
}
