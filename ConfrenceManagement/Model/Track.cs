using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Model
{
    public class Track
    {
        public List<Session> sessions { get; }

        public Track()
        {
            sessions = new List<Session>();

            // Initialize morning and afternoon session by default
            Session morningSession = new Session(Session.SessionType.Morning);
            sessions.Add(morningSession);
            Session afternoonSession = new Session(Session.SessionType.Afternoon);
            sessions.Add(afternoonSession);
        }
    }
}
