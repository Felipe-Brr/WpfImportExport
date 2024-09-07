using Siemens.Engineering;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfImportExport.Services
{
    internal class SiemensApiWrapper : INotifyPropertyChanged
    {
        #region properties
        public TiaPortal TiaPortal { get; set; }
        public bool TiaPortalIsDisposed { get; set; }
        public Project CurrentProject { get; set; }

        #endregion

        private Dispatcher _dispatcher;

        public SiemensApiWrapper([CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            Trace.WriteLine(methodBase.Name + " called from " + caller);
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void DoOpenTiaPortal([CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase.ReflectedType != null) Trace.Write(methodBase.ReflectedType.Name + "." + methodBase.Name + " called from " + caller);

            if (TiaPortal != null)
            {
                Trace.Write("Tia Portal já está aberto");
                return;
            }

            _dispatcher.Invoke(() =>
            {
                try
                {
                    Trace.Write("Abrindo Tia Portal");
                    TiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface);
                    Trace.Write("Tia portal aberto");
                }
                catch (Exception e)
                {
                    Trace.Write(e);
                }
            });
        }

        public async Task<bool> DoOpenProject(string path, [CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase.ReflectedType != null) Trace.Write(methodBase.ReflectedType.Name + "." + methodBase.Name + " called from " + caller);

            var result = false;

            DoCloseProject();

            var task = _dispatcher.Invoke(async () =>
            {
                Trace.Write("Abrindo projeto");
                try
                {
                    FileInfo filepath = new FileInfo(path);
                    var newProject = TiaPortal.Projects.Open(filepath);
                    Trace.Write($"TiaPortal.Projects.Open({path})");
                    if (newProject != null)
                    {
                        CurrentProject = newProject;
                        result = true;
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                return result;
            });

            return await task;
        }

        public void DoCloseProject([CallerMemberName] string caller = "")
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase.ReflectedType != null) Trace.Write(methodBase.ReflectedType.Name + "." + methodBase.Name + " called from " + caller);

            _dispatcher.Invoke(() =>
            {
                if (!TiaPortalIsDisposed && TiaPortal?.Projects.Count > 0)
                {
                    var project = TiaPortal.Projects.FirstOrDefault();
                    project?.Close();
                }
                CurrentProject = null;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
