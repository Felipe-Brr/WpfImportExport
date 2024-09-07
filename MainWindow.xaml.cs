using Microsoft.Win32;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using WpfImportExport.Interfaces;
using Siemens.Engineering.HW.Features;
using System.Windows.Forms; // Note: This is Windows Forms
using System.Windows.Forms.Integration;
using WpfImportExport.Views; 


namespace WpfImportExport
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields

        private readonly TraceWriter _traceWriter;
        Project project = null;
        List<PlcSoftware> sw = null;

        #endregion // fields

        public MainWindow()
        {
            InitializeComponent();
            abrirProjeto.Content = new AbrirProjeto();
            exportarBloco.Content = new ExportarBloco();
            message.Content = new Views.Message();

        }
    }
}
