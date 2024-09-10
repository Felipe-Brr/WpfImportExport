using System.Windows.Controls;
using WpfImportExport.ViewModels;

namespace WpfImportExport.Views
{
    public partial class AbrirProjeto : Page
    {
        public AbrirProjeto()
        {
            InitializeComponent();
            DataContext = MainViewModel.ProjetoViewModel;
        }
    }
}
