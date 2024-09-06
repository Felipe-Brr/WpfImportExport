using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfImportExport.Interfaces;

namespace WpfImportExport.ViewModels
{
    internal class TraceWriterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TraceWriter _traceWriter {  get; set; } 

        public void NotifyPropertyChanged(string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
