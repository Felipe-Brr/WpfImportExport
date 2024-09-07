using System.Diagnostics;
using System.IO;

namespace WpfImportExport.Logging
{
    public class TextWriterTraceListener : TraceListener
    {
        private readonly TextWriter _textWriter;

        public TextWriterTraceListener(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public override void Write(string message)
        {
            _textWriter.Write(message);
        }

        public override void WriteLine(string message)
        {
            _textWriter.WriteLine(message);
        }
    }
}
