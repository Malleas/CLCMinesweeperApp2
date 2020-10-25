using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLCMineSweeperApp2.Models
{
    public class StatsObject
    {
        public int Time { get; set; }
        public int Clicks { get; set; }

        public StatsObject(int time, int clicks)
        {
            Time = time;
            Clicks = clicks;
        }
    }
}