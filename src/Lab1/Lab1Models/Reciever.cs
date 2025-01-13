using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
    public class Reciever
    {
        private string gecryptedMessage = "";

        public string GetMessage()
        {
            return gecryptedMessage;
        }

        internal void RecieveCryptedMessage(string message)
        {

        }
    }
}
