using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Model
{
    public class Track
    {
        public List<Session> sessions { get; }

        public Track()
        {
            sessions = new List<Session>();
        }
    }
}
