using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
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
                if (_projeto != value)
                {
                    _projeto = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand cProcurarProjeto { get; }
        public ICommand cAbrirProjeto { get; }

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
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            Projeto.ProcurarProjeto();
            OnPropertyChanged(nameof(Projeto));
            Trace.WriteLine($"Project path updated: {Projeto.ProjectPath}");
        }

        private async void AbrirProjeto(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            await Task.Run(() => Projeto.AbrirProjeto());
        }

        #endregion
    }
}
