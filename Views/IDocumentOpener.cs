using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Views
{
    internal interface IDocumentOpener
    {
        void SetFilePath(String filepath);

        void OpenFile();
    }
}
