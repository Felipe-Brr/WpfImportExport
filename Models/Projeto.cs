using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using WpfImportExport.Services;

namespace WpfImportExport.Models
{
    internal class Projeto : INotifyPropertyChanged
    {
        private string _projectPath;

        private readonly SiemensApiWrapper _apiWrapper = new SiemensApiWrapper();
        public SiemensApiWrapper SiemensApiWrapper
        { 
            get => _apiWrapper;
            set { }
            
        }
        public string projectPath
        {
            get => _projectPath;
            set
            {
                _projectPath = value;
                OnPropertyChanged();
            }
        }

        public void ProcurarProjeto([CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase.ReflectedType != null) Trace.WriteLine(methodBase.ReflectedType.Name + "." + methodBase.Name + " called from " + caller);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // You can set your own filter here

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                projectPath = openFileDialog.FileName;
            }
        }

        public void AbrirProjeto([CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase.ReflectedType != null) Trace.WriteLine(methodBase.ReflectedType.Name + "." + methodBase.Name + " called from " + caller);
            _apiWrapper.DoOpenTiaPortal();
            var result = _apiWrapper.DoOpenProject(projectPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
