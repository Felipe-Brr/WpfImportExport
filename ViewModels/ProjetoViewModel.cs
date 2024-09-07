using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfImportExport.Models;
using WpfImportExport.Services;

namespace WpfImportExport.ViewModels
{
    internal class ProjetoViewModel : INotifyPropertyChanged
    {
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

        public ICommand BrowseProject { get; set; }

        public ProjetoViewModel()
        {
            Projeto = new Projeto(); // Initialize Projeto
            BrowseProject = new RelayCommand(ProcurarProjeto);
        }

        private void ProcurarProjeto(object obj)
        {
            Projeto.ProcurarProjeto();
            OnPropertyChanged(nameof(Projeto));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
