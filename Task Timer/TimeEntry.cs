using System;

namespace Task_Timer
{
    internal class TimeEntry
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public long DurationInTicks()
        {
            if (Stop.Year != 1)
                return Stop.Ticks - Start.Ticks;
            return DateTime.Now.Ticks - Start.Ticks;
        }

        public long DurationInSeconds()
        {
            long seconds;
            if (Stop.Year != 1)
            {
                seconds = (Stop.Ticks - Start.Ticks)/TimeSpan.TicksPerSecond;
                if ((Stop.Ticks - Start.Ticks)%TimeSpan.TicksPerSecond > 0)
                    seconds++;
            }
            else
            {
                seconds = (DateTime.Now.Ticks - Start.Ticks)/TimeSpan.TicksPerSecond;
                if ((DateTime.Now.Ticks - Start.Ticks)%TimeSpan.TicksPerSecond > 0)
                    seconds++;
            }
            return seconds;
        }
    }
}