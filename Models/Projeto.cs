using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WpfImportExport.Models
{
    internal class Projeto
    {

        public string projectPath { get; set; }


        public void ProcurarProjeto()
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // You can set your own filter here

            if (openFileDialog.ShowDialog() == true)
            {
                // The user selected a file, and you can get the file path
                string filePath = openFileDialog.FileName;
                // You can perform further actions with the file here
                projectPath = filePath;
            }

        }
    }
}
