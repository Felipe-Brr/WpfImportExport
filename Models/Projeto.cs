using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace WpfImportExport.Models
{
    internal class Projeto : INotifyPropertyChanged
    {
        private string _projectPath;

        public string projectPath
        {
            get => _projectPath;
            set
            {
                _projectPath = value;
                OnPropertyChanged();
            }
        }

        public void ProcurarProjeto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // You can set your own filter here

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                projectPath = openFileDialog.FileName;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
