using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfImportExport.Interfaces
{
    internal class TraceWriter : TextWriter
    {
        #region fields

        private readonly ListBox _list;
        private StringBuilder _content;

        #endregion // fields

        #region ctor

        public TraceWriter(ListBox list)
        {
            _content = new StringBuilder();
            _list = list;
        }

        #endregion // ctor

        #region properties

        public override Encoding Encoding => Encoding.UTF8;

        #endregion // properties

        #region methods

        /// <summary>
        /// Writes a log message to trace output
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            base.Write(value);

            var input = ReplaceSpecialCharacters(value);
            var timeStamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
            _content.Append(timeStamp + "\t" + input);
            _list.Items.Add(_content.ToString());
            _list.SelectedIndex = _list.Items.Count - 1;
            _content = new StringBuilder();

        }


        /// <summary>
        /// Replace special characters with space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ReplaceSpecialCharacters(string value)
        {
            var result = Regex.Replace(value, "\n|\r|\t", " ");

            return result;
        }

        #endregion // methods
    }
}

