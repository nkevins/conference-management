using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Model
{
    public class Confrence
    {
        public List<Track> tracks { get; }

        public Confrence()
        {
            tracks = new List<Track>();
            Track track = new Track();
            tracks.Add(track);
        }
    }
}
