
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms; // Note: This is Windows Forms
using System.Windows.Forms.Integration;
using WpfImportExport.ViewModels;
using WpfImportExport.Views; 


namespace WpfImportExport
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProjetoViewModel _projetoViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _projetoViewModel = new ProjetoViewModel(); // Inicialização padrão
            DataContext = _projetoViewModel;
            MainContent.Content = new ImportarExportar();
        }

        // Construtor existente
        public MainWindow(ProjetoViewModel projetoViewModel)
        {
            InitializeComponent();
            _projetoViewModel = projetoViewModel;
            DataContext = _projetoViewModel;
            MainContent.Content = new ImportarExportar();
        }
    }
}
