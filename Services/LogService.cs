using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace WpfImportExport.Services
{
    public class LogService
    {
        // Singleton instance
        private static readonly LogService _instance = new LogService();
        public static LogService Instance => _instance;
        private Dispatcher _dispatcher;

        // ObservableCollection to store trace messages
        public ObservableCollection<string> TraceMessages { get; private set; }

        // Private constructor to prevent external instantiation
        private LogService()
        {
            TraceMessages = new ObservableCollection<string>();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        // Method to add a message to the trace log
        public void InsertMessage(string message)
        {

            if (_dispatcher.CheckAccess())
            {
                TraceMessages.Insert(0, message);
            }
            else
            {
                _dispatcher.Invoke(() => TraceMessages.Insert(0,message));
            }
        }
    }
}
