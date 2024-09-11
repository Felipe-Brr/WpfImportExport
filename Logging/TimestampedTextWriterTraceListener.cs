using System;
using System.Diagnostics;
using System.IO;

namespace WpfImportExport.Logging
{
    public class TimestampedTextWriterTraceListener : TextWriterTraceListener
    {
        public TimestampedTextWriterTraceListener(TextWriter writer) : base(writer) { }

        public override void Write(string message)
        {
            base.Write(FormatMessage(message));
        }

        public override void WriteLine(string message)
        {
            base.WriteLine(FormatMessage(message));
        }

        private string FormatMessage(string message)
        {
            // Prepend the current date and time to the message
            return $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} - {message}";
        }
    }
}
