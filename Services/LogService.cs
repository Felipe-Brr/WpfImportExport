using System.Collections.ObjectModel;

namespace WpfImportExport.Services
{
    public class LogService
    {
        // Singleton instance
        private static readonly LogService _instance = new LogService();
        public static LogService Instance => _instance;

        // ObservableCollection to store trace messages
        public ObservableCollection<string> TraceMessages { get; private set; }

        // Private constructor to prevent external instantiation
        private LogService()
        {
            TraceMessages = new ObservableCollection<string>();
        }

        // Method to add a message to the trace log
        public void AddMessage(string message)
        {
            TraceMessages.Add(message);
        }
    }
}
