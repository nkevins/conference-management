using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConfrenceManagement.Input
{
    public class FileHandler : IFileHandler
    {
        public bool FileExist(string path)
        {
            return File.Exists(path);
        }

        public List<string> ReadFile(string path)
        {
            return File.ReadAllLines(path).ToList();
        }
    }
}
