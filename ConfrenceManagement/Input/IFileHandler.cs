using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Input
{
    public interface IFileHandler
    {
        bool FileExist(string path);
        List<string> ReadFile(string path);
    }
}
