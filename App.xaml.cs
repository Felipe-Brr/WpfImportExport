using System.Diagnostics;
using System.IO;
using System.Windows;
using WpfImportExport.Logging;

namespace WpfImportExport
{
    public partial class App : Application
    {
        private StreamWriter _streamWriter;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize StreamWriter
            _streamWriter = new StreamWriter("app.log", append: true)
            {
                AutoFlush = true
            };

            // Add TimestampedTextWriterTraceListener to log to app.log
            var fileListener = new TimestampedTextWriterTraceListener(_streamWriter);
            Trace.Listeners.Add(fileListener);

            // Add the custom ListBoxTraceListener for UI updates
            var listBoxListener = new ListBoxTraceListener();
            Trace.Listeners.Add(listBoxListener);

            // Log application startup
            Trace.WriteLine("Application starting up");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Log application shutdown
            Trace.WriteLine("Application shutting down");

            // Flush and close all trace listeners
            foreach (TraceListener listener in Trace.Listeners)
            {
                listener.Flush();
                listener.Close();
            }

            // Close the StreamWriter
            _streamWriter?.Close();

            base.OnExit(e);
        }
    }
}
