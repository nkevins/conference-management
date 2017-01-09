using ConfrenceManagementLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Scheduler
{
    public class ConfrenceScheduler : IConfrenceScheduler
    {
        private List<Event> eventsInput;
        private Confrence confrence;

        public ConfrenceScheduler(List<Event> events)
        {
            this.eventsInput = events;
        }

        public Confrence ScheduleConfrence()
        {
            confrence = new Confrence();

            // Use First Fit Decreasing Algorithm to assign event
            eventsInput = eventsInput.OrderByDescending(x => x.duration).ToList();
            foreach (Event e in eventsInput)
            {
                AddEventToConfrence(e);
            }

            // Add lunch and networking event
            foreach (Track t in confrence.tracks)
            {
                foreach (Session s in t.sessions)
                {
                    if (s.sessionType == Session.SessionType.Morning)
                    {
                        s.AddNonTalkEvent(new Event("Lunch", 0, Event.EventType.Lunch, 720));
                    }
                    else if (s.sessionType == Session.SessionType.Afternoon)
                    {
                        // Networking event must only start later than 4 PM
                        int networkingStartTime;
                        if (s.availableSlotMinutes < 60)
                        {
                            networkingStartTime = s.endTime - s.availableSlotMinutes;
                        } else
                        {
                            networkingStartTime = 960;
                        }

                        s.AddNonTalkEvent(new Event("Networking Event", 0, Event.EventType.Networking, networkingStartTime));
                    }
                }
            }

            return confrence;
        }

        private Track GenerateTrack()
        {
            Track track = new Track();

            // Initialize morning and afternoon session by default
            track.sessions.Add(new Session(Session.SessionType.Morning, 540, 720));
            track.sessions.Add(new Session(Session.SessionType.Afternoon, 780, 1020));

            return track;
        }

        private void AddEventToConfrence(Event e)
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
                Track t = GenerateTrack();
                if (!AddEventToTrack(e, ref t))
                {
                    throw new ApplicationException("Unable to assign slot for following session: " + e.title + " " + e.duration + "min. Duration is greater than available slot.");
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
                    // Ensure lunch and networking session at the last
                    s.AddTalkEvent(e);
                    canAssignIntoExistingSession = true;
                    break;
                }
            }

            return canAssignIntoExistingSession;
        }
    }
}
