﻿using System;
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
using WpfImportExport.ViewModels;

namespace WpfImportExport.Views
{
    /// <summary>
    /// Interação lógica para ExportarBloco.xam
    /// </summary>
    public partial class ExportarBloco : Page
    {
        public ExportarBloco()
        {
            InitializeComponent();
            DataContext = MainViewModel.ProjetoViewModel;
        }
    }
}
