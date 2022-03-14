using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Models
{
    public class BinaryFile
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual string ContentType { get; set; }

        public virtual HttpPostedFileBase PostedFile { get; set; }

        public virtual byte[] Content { get; set; }
    }
}