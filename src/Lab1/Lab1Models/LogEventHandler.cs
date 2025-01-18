using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
    /// <summary>
    /// Делегат для логгирования действий
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Параметр с доп информацией</param>
    public delegate void LogEventHandler(object sender, LogEventArgs e);
    
}
