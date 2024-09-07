using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfImportExport.Models;
using WpfImportExport.ViewModels;

namespace WpfImportExport.Views
{
    /// <summary>
    /// Interação lógica para AbrirProjeto.xam
    /// </summary>
    public partial class AbrirProjeto : Page
    {
        public AbrirProjeto()
        {
            InitializeComponent();
            DataContext = new BuscarProjetoViewModel();
        }
    }
}
