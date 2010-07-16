using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_Timer
{
    class TimeEntry
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public long DurationInTicks()
        {
            if (Stop.Year != 1)
                return Stop.Ticks - Start.Ticks;
            else
                return DateTime.Now.Ticks - Start.Ticks;
        }

        public long DurationInSeconds()
        {
            long seconds = 0;
            if (Stop.Year != 1)
            {
                seconds = (Stop.Ticks - Start.Ticks) / TimeSpan.TicksPerSecond;
                if ((Stop.Ticks - Start.Ticks) % TimeSpan.TicksPerSecond > 0)
                    seconds++;
            }
            else
            {
                seconds = (DateTime.Now.Ticks - Start.Ticks) / TimeSpan.TicksPerSecond;
                if ((DateTime.Now.Ticks - Start.Ticks) % TimeSpan.TicksPerSecond > 0)
                    seconds++;
            }
            return seconds;
        }

        /*
        public long DurationInMinutes()
        {
            long minutes = 0;
            if (Stop.Year != 1)
            {
                minutes = (Stop.Ticks - Start.Ticks) / TimeSpan.TicksPerMinute;
            }
            else
            {
                minutes = (DateTime.Now.Ticks - Start.Ticks) / TimeSpan.TicksPerMinute;
            }
            return minutes;
        }
        */
    }
}
