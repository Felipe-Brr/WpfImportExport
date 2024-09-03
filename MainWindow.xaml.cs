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
        FileInfo projectPath = null;
        List<PlcSoftware> sw = null;

        #endregion // fields

        public MainWindow()
        {
            InitializeComponent();
            _traceWriter = new TraceWriter(lbMessage);
            var methodBase = MethodBase.GetCurrentMethod();
            _traceWriter.Write(methodBase.Name + " called from");
        }

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            _traceWriter.Write(methodBase.Name);

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // You can set your own filter here

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                string filePath = openFileDialog.FileName;
                // You can perform further actions with the file here
                tbPath.Text = filePath;
            }
        }

        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {

            var methodBase = MethodBase.GetCurrentMethod();
            _traceWriter.Write(methodBase.Name);

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set the description of the dialog
                folderBrowserDialog.Description = "Select a folder";

                // Show the folder browser dialog and get the result
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // The user selected a folder, and you can get the folder path
                    string folderPath = folderBrowserDialog.SelectedPath;
                    // You can perform further actions with the folder path here
                    tbPathExport.Text = folderPath;
                }
            }
        }

        private void btnOpenProject_Click(object sender, RoutedEventArgs e)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            _traceWriter.Write(methodBase.Name);
            
            TiaPortal tiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface);

            try
            {
                // Open an existing project
                projectPath = new FileInfo(tbPath.Text);
                project = tiaPortal.Projects.Open(projectPath);
                sw = GetAllPlcSoftwares(project).ToList();

            }
            catch (Exception ex)
            {
                _traceWriter.Write($"An error occurred: {ex.Message}");
            }
        }
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportRegularBlock(sw[0]);
        }

        //Exports a regular block
        private void ExportRegularBlock(PlcSoftware plcSoftware)
        {
            var methodBase = MethodBase.GetCurrentMethod();
            _traceWriter.Write(methodBase.Name);

            PlcBlock plcBlock = plcSoftware.BlockGroup.Blocks.Find("Main");
            FileInfo exportPath = new FileInfo(tbPathExport.Text + $"\\{plcBlock.Name}.xml");

            try
            {
                plcBlock.Export(exportPath, ExportOptions.WithDefaults);
            }
            catch (Exception ex) 
            {
                _traceWriter.Write($"An error occurred: {ex.Message}");
            }

        }

        public static IEnumerable<PlcSoftware> GetAllPlcSoftwares(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project), "Parameter is null");
            foreach (var device in project.Devices)
            {
                var ret = GetPlcSoftware(device);
                if (ret != null)
                    yield return ret;
            }
        }

        public static PlcSoftware GetPlcSoftware(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Parameter is null");
            foreach (var devItem in device.DeviceItems)
            {
                var target = ((IEngineeringServiceProvider)devItem).GetService<SoftwareContainer>();
                if (target != null && target.Software is PlcSoftware)
                    return (PlcSoftware)target.Software;
            }
            return null;
        }

    }
}
