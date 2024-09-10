using Siemens.Engineering;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace WpfImportExport.Services
{
    internal class SiemensApiWrapper : INotifyPropertyChanged
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
