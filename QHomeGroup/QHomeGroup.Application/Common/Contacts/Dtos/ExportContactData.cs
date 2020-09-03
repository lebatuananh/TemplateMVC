using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QHomeGroup.Application.Common.Contacts.Dtos
{
    public class ExportContactData
    {
        public MemoryStream Data { get; set; }

        public string FileName { get; set; }
    }
}
