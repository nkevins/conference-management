using ConfrenceManagementLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Input
{
    public interface IInputReader
    {
        List<Event> ReadInput();
    }
}
