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
    public class Projeto : INotifyPropertyChanged
    {

        #region properties
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

        private string _importFilePath;
        public string ImportFilePath
        {
            get => _importFilePath;
            set
            {
                if (_importFilePath != value)
                {
                    _importFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<PlcBlock> _blocos;
        public ObservableCollection<PlcBlock> Blocos
        {
            get => _blocos;
            set
            {
                if (_blocos != value)
                {
                    _blocos = value;
                    OnPropertyChanged(nameof(Blocos));
                }
            }
        }
        #endregion

        #region methods
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
            Blocos = new ObservableCollection<PlcBlock>(ListarBlocos());
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
        public List<PlcBlock> ListarBlocos([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            return SiemensApiWrapper.ListBlocks();
        }

        public void ExportarBlocos(PlcBlock plcBlock, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            SiemensApiWrapper.ExportRegularBlock(plcBlock,ExportPath);
        }
        public void ProcurarArquivoImportar([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*" // You can set your own filter here
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                ImportFilePath = openFileDialog.FileName;
            }
        }
        public void ImportarBlocos([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            SiemensApiWrapper.ImportBlocks(ImportFilePath);
            Blocos = new ObservableCollection<PlcBlock>(ListarBlocos());
        }
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
