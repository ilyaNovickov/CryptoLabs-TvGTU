using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string message) 
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}
