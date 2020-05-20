using System.IO;

namespace Capri.Web.ViewModels.Common
{
    public class FileDescriptionv2
    {
        public string Name { get; set; }
        public MemoryStream Bytes { get; set; }
    }
}