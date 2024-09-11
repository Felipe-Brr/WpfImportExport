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

namespace WpfImportExport.Views
{
    /// <summary>
    /// Interação lógica para ImportarExportar.xam
    /// </summary>
    public partial class ImportarExportar : Page
    {
        public ImportarExportar()
        {
            InitializeComponent();
            abrirProjeto.Content = new AbrirProjeto();
            exportarBloco.Content = new ExportarBloco();
            importarBloco.Content = new ImportarBloco();
            message.Content = new Views.Message();
        }
    }
}
