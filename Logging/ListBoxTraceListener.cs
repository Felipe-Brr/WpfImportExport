using System;
using System.Diagnostics;
using WpfImportExport.Services;

namespace WpfImportExport.Logging
{
    public class ListBoxTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            // Use LogService to add timestamped message
            LogService.Instance.AddMessage(FormatMessage(message));
        }

        public override void WriteLine(string message)
        {
            // Use LogService to add timestamped message
            LogService.Instance.AddMessage(FormatMessage(message));
        }

        private string FormatMessage(string message)
        {
            // Format the message with the current date and time
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        }
    }
}
