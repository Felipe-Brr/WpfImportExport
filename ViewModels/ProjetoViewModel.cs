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

        public ProjetoViewModel()
        {
            Projeto = new Projeto();
            Blocos = new ObservableCollection<PlcBlock>();
            cProcurarProjeto = new RelayCommand(ProcurarProjeto);
            cAbrirProjeto = new RelayCommand(AbrirProjeto);
            cProcurarCaminhoExport = new RelayCommand(ProcurarCaminhoExport);
            cBlockExport = new RelayCommand(BlockExport);
            cBlockImport = new RelayCommand(BlockImport);
            cProcurarArquivoImportar = new RelayCommand(ProcurarArquivoImportar);
        }

        private void ProcurarProjeto(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            Projeto.ProcurarProjeto();
            Trace.WriteLine($"Project path updated: {Projeto.ProjectPath}");
        }

        private async void AbrirProjeto(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            await Task.Run(() => Projeto.AbrirProjeto());
            Blocos.Clear();
            foreach (var bloco in Projeto.ListarBlocos())
            {
                Blocos.Add(bloco);// Adiciona o bloco à coleção Blocos
            }

        }
        private void ProcurarArquivoImportar(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            Projeto.ProcurarArquivoImportar();
            Trace.WriteLine($"Arquivo carregado: {Projeto.ImportFilePath}");
        }
        private void BlockExport(object obj)
        {
            Projeto.ExportarBlocos(BlocoSelecionado);
        }

        private async void BlockImport(object obj)
        {
            await Task.Run(() => Projeto.ImportarBlocos());
        }

        private void ProcurarCaminhoExport(object obj)
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()} command executed");
            Projeto.ProcurarCaminhoExport();
            Trace.WriteLine($"Export path updated: {Projeto.ExportPath}");
        }
    }
}
