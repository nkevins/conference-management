using ConfrenceManagementLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Scheduler
{
    public class ConfrenceScheduler
    {
        private List<Event> events;

        public ConfrenceScheduler(List<Event> events)
        {
            this.events = events;
        }

        public Confrence ScheduleConfrence()
        {
            Confrence confrence = new Confrence();

            events = events.OrderByDescending(x => x.duration).ToList();

            foreach (Event e in events)
            {
                AddEventToConfrence(e, ref confrence);
            }

            return confrence;
        }

        private void AddEventToConfrence(Event e, ref Confrence confrence)
        {
            bool canAssignIntoExistingTracks = false;

            // Attempt to assign event into existing tracks
            for (int i = 0; i < confrence.tracks.Count; i++)
            {
                Track t = confrence.tracks[i];
                if (AddEventToTrack(e, ref t))
                {
                    canAssignIntoExistingTracks = true;
                    break;
                }
            }

            // If cannot assign in existing tracks, create new track and assign event into it
            if (!canAssignIntoExistingTracks)
            {
                Track t = new Track();
                if (!AddEventToTrack(e, ref t))
                {
                    Console.WriteLine("Unable to assign slot for following session: " + e.title + " " + e.duration + "min");
                }
                else
                {
                    confrence.tracks.Add(t);
                }
            }
        }

        private bool AddEventToTrack(Event e, ref Track track)
        {
            bool canAssignIntoExistingSession = false;

            // Assign event to available session slot
            for (int i = 0; i < track.sessions.Count; i++)
            {
                Session s = track.sessions[i];

                if (s.availableSlotMinutes >= e.duration)
                {
                    s.AddTalkEvent(e);
                    canAssignIntoExistingSession = true;
                    break;
                }
            }

            return canAssignIntoExistingSession;
        }
    }
}
