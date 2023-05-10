using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Views
{
    internal class DocumentOpener : IDocumentOpener
    {
        String filePath;
        public DocumentOpener(string filePath)
        {
            this.filePath = filePath;
        }

        public DocumentOpener() { }

        public void OpenFile()
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(filePath)
            {
                UseShellExecute = true,
            };
            p.Start();
        }

        public void SetFilePath(string filepath)
        {
            this.filePath = filepath;
        }

        public void ConvertToPdf()
        {
            //TODO maybe if there is time
        }
    }
}
