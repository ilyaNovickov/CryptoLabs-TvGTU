using System;

namespace Lab1Models
{
    /// <summary>
    /// Класс с доп информацией для события логгирования
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Сообщение логгирования
        /// </summary>
        public string Message { get; private set; }
    }
}
