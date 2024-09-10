using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using Siemens.Engineering.SW.Blocks;
using WpfImportExport.Services;

namespace WpfImportExport.Models
{
    internal class Projeto : INotifyPropertyChanged
    {
        
        
        private readonly SiemensApiWrapper _apiWrapper = new SiemensApiWrapper();
        public SiemensApiWrapper SiemensApiWrapper => _apiWrapper;

        private string _projectPath;
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

        private string _exportPath;
        public string ExportPath
        {
            get => _exportPath;
            set
            {
                if (_exportPath != value)
                {
                    _exportPath = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public void ProcurarProjeto([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
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
            SiemensApiWrapper.DoOpenTiaPortal();
            SiemensApiWrapper.DoOpenProject(ProjectPath);
        }

        public List<PlcBlock> ListarBlocos()
        {
            return SiemensApiWrapper.ListBlocks();
        }

        public void ProcurarCaminhoExport([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            OpenFileDialog dialogo = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = " Selecione uma pasta"
            };

            bool? resultado = dialogo.ShowDialog();
            if (resultado == true)
            {
                string caminhoPasta = System.IO.Path.GetDirectoryName(dialogo.FileName);
                ExportPath = caminhoPasta;
            }
            else
            {
                ExportPath = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
