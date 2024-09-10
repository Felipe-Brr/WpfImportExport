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

        public SiemensApiWrapper SiemensApiWrapper => _apiWrapper;

        public string ProjectPath
        {
            get => _projectPath;
            set
            {
                if (_projectPath != value)
                {
                    _projectPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ProcurarProjeto([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            var openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*" // You can set your own filter here
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                ProjectPath = openFileDialog.FileName;
            }
        }

        public void AbrirProjeto([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            _apiWrapper.DoOpenTiaPortal();
            _apiWrapper.DoOpenProject(ProjectPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
