using System;
using System.IO;

namespace Lab4WF
{
    public static class Logger
    {
        private const string LogFilePath = "log.txt";

        public static void Log(string eventType, string username, string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {eventType} | Пользователь: {username} | {message}";

            try
            {
                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи лога: {ex.Message}");
            }
        }
    }
}
