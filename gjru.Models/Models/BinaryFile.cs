using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gjru.Models.Models
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
