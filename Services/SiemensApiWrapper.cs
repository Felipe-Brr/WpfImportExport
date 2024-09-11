using Siemens.Engineering;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Siemens.Engineering.SW.Blocks;

namespace WpfImportExport.Services
{
    public class SiemensApiWrapper : INotifyPropertyChanged
    {
        #region properties
        public TiaPortal TiaPortal { get; private set; }
        public bool TiaPortalIsDisposed { get; private set; }
        public Project CurrentProject { get; private set; }
        #endregion

        private readonly Dispatcher _dispatcher;

        #region ctor
        public SiemensApiWrapper([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            _dispatcher = Dispatcher.CurrentDispatcher;
        }
        #endregion

        #region methods
        public void DoOpenTiaPortal([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            if (TiaPortal != null)
            {
                Trace.WriteLine("Tia Portal já está aberto");
                return;
            }

            try
            {
                Trace.WriteLine("Abrindo Tia Portal");
                TiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface);
                TiaPortalIsDisposed = false;
                Trace.WriteLine("Tia Portal aberto");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        public void DoOpenProject(string path, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            DoCloseProject();

            try
            {
                Trace.WriteLine("Abrindo projeto");
                var filepath = new FileInfo(path);
                CurrentProject = TiaPortal.Projects.Open(filepath);
                Trace.WriteLine($"TiaPortal.Projects.Open({path})");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        public void DoCloseProject([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            _dispatcher.Invoke(() =>
            {
                if (!TiaPortalIsDisposed && TiaPortal?.Projects.Count > 0)
                {
                    TiaPortal.Projects.FirstOrDefault()?.Close();
                }
                CurrentProject = null;
            });
        }

        public List<PlcBlock> ListBlocks([CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            List<PlcBlock> listablocks = new List<PlcBlock>();
            try
            {
                foreach (var plcSoftware in GetAllPlcSoftwares(CurrentProject))
                {
                    foreach (var block in plcSoftware.BlockGroup.Blocks)
                    {
                        listablocks.Add(block);
                    }
                }
            }catch(Exception ex)
            {
                Trace.WriteLine($"An error occurred: {ex.Message}");
            }
            
            return listablocks;
        }

        public void ExportRegularBlock(PlcBlock plcBlock, string ExportPath, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            FileInfo exportPath = new FileInfo(ExportPath + $"\\{plcBlock.Name}.xml");

            try
            {
                plcBlock.Export(exportPath, ExportOptions.WithDefaults);
                Trace.WriteLine($"Bloco: {plcBlock.Name} exportado em: {exportPath.FullName}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        public void ImportBlocks(string ImportFilePath, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");
            foreach (var plcSoftware in GetAllPlcSoftwares(CurrentProject))
            {
                PlcBlockGroup blockGroup = plcSoftware.BlockGroup;
                FileInfo importPath = new FileInfo(ImportFilePath);
                try
                {
                    IList<PlcBlock> blocks = blockGroup.Blocks.Import(importPath, ImportOptions.Override);
                    Trace.WriteLine($"Bloco: {importPath.Name} importado");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }


        public static IEnumerable<PlcSoftware> GetAllPlcSoftwares(Project project, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

            if (project == null)
                throw new ArgumentNullException(nameof(project), "Parameter is null");
            foreach (var device in project.Devices)
            {
                var ret = GetPlcSoftware(device);
                if (ret != null)
                    yield return ret;
            }
        }

        public static PlcSoftware GetPlcSoftware(Device device, [CallerMemberName] string caller = "")
        {
            Trace.WriteLine($"{MethodBase.GetCurrentMethod()?.ReflectedType?.Name}.{MethodBase.GetCurrentMethod()?.Name} called from {caller}");

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
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
