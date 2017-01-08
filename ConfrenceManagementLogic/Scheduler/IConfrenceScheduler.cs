using ConfrenceManagementLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Scheduler
{
    public interface IConfrenceScheduler
    {
        Confrence ScheduleConfrence();
    }
}
