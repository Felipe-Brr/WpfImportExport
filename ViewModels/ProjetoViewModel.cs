using Siemens.Engineering.SW.Blocks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfImportExport.Models;
using WpfImportExport.Services;

namespace WpfImportExport.ViewModels
{
    public class ProjetoViewModel : ViewModelBase
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
                    OnPropertyChanged(nameof(Projeto));
                }
            }
        }

        private PlcBlock _blocoSelecionado;
        public PlcBlock BlocoSelecionado
        {
            get => _blocoSelecionado;
            set
            {
                if (_blocoSelecionado != value)
                {
                    _blocoSelecionado = value;
                    OnPropertyChanged(nameof(BlocoSelecionado));
                }
            }
        }

        public ICommand cProcurarProjeto { get; }
        public ICommand cAbrirProjeto { get; }
        public ICommand cProcurarCaminhoExport { get; }
        public ICommand cBlockExport { get; }
        public ICommand cBlockImport { get; }
        public ICommand cProcurarArquivoImportar { get; }

        public ObservableCollection<string> TraceMessages => LogService.Instance.TraceMessages;
        #endregion
        #region methods
        public ProjetoViewModel()
        {
            Projeto = new Projeto();
            cProcurarProjeto = new RelayCommand(ProcurarProjeto);
            cAbrirProjeto = new RelayCommand(AbrirProjeto);
            cProcurarCaminhoExport = new RelayCommand(ProcurarCaminhoExport);
            cBlockExport = new RelayCommand(BlockExport);
            cBlockImport = new RelayCommand(BlockImport);
            cProcurarArquivoImportar = new RelayCommand(ProcurarArquivoImportar);
        }

        private void ProcurarProjeto(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            Projeto.ProcurarProjeto();
            Trace.WriteLine($"Caminho de projeto atualizado: {Projeto.ProjectPath}");
        }

        private async void AbrirProjeto(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            await Task.Run(() => Projeto.AbrirProjeto());

        }
        private void ProcurarArquivoImportar(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            Projeto.ProcurarArquivoImportar();
            Trace.WriteLine($"Arquivo carregado: {Projeto.ImportFilePath}");
        }
        private void BlockExport(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            Projeto.ExportarBlocos(BlocoSelecionado);
        }

        private async void BlockImport(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            await Task.Run(() => Projeto.ImportarBlocos());
        }

        private void ProcurarCaminhoExport(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} comando executado");
            Projeto.ProcurarCaminhoExport();
            Trace.WriteLine($"Caminho para exportar atualizado: {Projeto.ExportPath}");
        }
        #endregion
    }
}
