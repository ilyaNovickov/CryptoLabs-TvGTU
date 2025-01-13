using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
    public class Sender
    {
        public void SendMessegaTo(string message, Reciever reciever)
        {
            reciever.RecieveCryptedMessage(message);
        }
    }
}
