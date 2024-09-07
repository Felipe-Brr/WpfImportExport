using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfImportExport.Interfaces;
using WpfImportExport.Models;
using WpfImportExport.Services;
using System;

namespace WpfImportExport.ViewModels
{
    internal class BuscarProjetoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Projeto Projeto { get; set; }
        public ICommand BrowseProject {  get; set; }

        public BuscarProjetoViewModel()
        {
            BrowseProject = new RelayCommand(ProcurarProjeto);
        }

        public void ProcurarProjeto(object obj)
        {
            new Projeto().ProcurarProjeto();
            NotifyPropertyChanged("Projeto");
        }
        public void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine("FUNCIONOU");
        }
    }
}
