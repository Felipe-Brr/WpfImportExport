using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfImportExport.Models;
using WpfImportExport.Services;

namespace WpfImportExport.ViewModels
{
    internal class ProjetoViewModel : ViewModelBase
    {
        #region properties
        private Projeto _projeto;

        public Projeto Projeto
        {
            get => _projeto;
            set
            {
                _projeto = value;
                OnPropertyChanged();
            }
        }

        public ICommand cProcurarProjeto { get; set; }
        public ICommand cAbrirProjeto { get; set; }

        public ObservableCollection<string> TraceMessages => LogService.Instance.TraceMessages;
        #endregion

        #region ctor
        public ProjetoViewModel()
        {
            Projeto = new Projeto(); // Initialize Projeto
            cProcurarProjeto = new RelayCommand(ProcurarProjeto);
            cAbrirProjeto = new RelayCommand(AbrirProjeto);
        }
        #endregion

        #region methods
        private void ProcurarProjeto(object obj)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            Trace.WriteLine(methodBase + " command executed");
            Projeto.ProcurarProjeto();
            OnPropertyChanged(nameof(Projeto));
            Trace.WriteLine($"Project path updated: {Projeto.projectPath}");
        }

        private void AbrirProjeto(object obj)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            Trace.WriteLine(methodBase + " command executed");
            Projeto.AbrirProjeto();
        }

        #endregion
    }
}
