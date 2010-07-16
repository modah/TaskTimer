using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_Timer
{
    internal class Task : IComparable
    {
        public Task(string taskName)
        {
            Taskname = taskName;
            TimeEntries = new List<TimeEntry>();
        }

        public List<TimeEntry> TimeEntries { get; set; }

        public string Taskname { get; set; }

        public bool IsActive { get; private set; }

        public long TimeDifference { get; private set; }

        public long TimeDifferenceInMinutes
        {
            get
            {
                long minutes = TimeDifference/60;
                if (TimeDifference%60 > 0)
                    minutes++;
                return minutes;
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return 0;
        }

        #endregion

        public override bool Equals(object obj)
        {
            return Taskname.Equals(((Task) obj).Taskname);
        }

        public void Start()
        {
            TimeEntries.Add(new TimeEntry());
            TimeEntries[TimeEntries.Count - 1].Start = DateTime.Now;
            IsActive = true;
        }

        public void Stop()
        {
            TimeEntries[TimeEntries.Count - 1].Stop = DateTime.Now;
            IsActive = false;
        }

        public long DurationInSeconds()
        {
            long seconds = TimeEntries.Sum(timeEntry => timeEntry.DurationInSeconds());
            long duration = seconds + TimeDifference;
            if (duration > 0)
                return duration;
            return 0;
        }

        public long DurationInMinutes()
        {
            long seconds = DurationInSeconds();
            long minutes = seconds/60;
            if (seconds%60 > 0)
                minutes++;
            return minutes;
        }

        public long DurationInSeconds(DateTime date)
        {
            long seconds =
                TimeEntries
                    .Where(timeEntry => timeEntry.Start.Date.Equals(date.Date) || timeEntry.Stop.Date.Equals(date.Date))
                    .Sum(timeEntry => timeEntry.DurationInSeconds());
            long duration = seconds + TimeDifference;
            if (duration > 0)
                return duration;
            return 0;
        }

        public long DurationInMinutes(DateTime date)
        {
            long seconds = DurationInSeconds(date);
            long minutes = seconds/60;
            if (seconds%60 > 0)
                minutes++;
            return minutes;
        }

        public void AddSeconds(long seconds)
        {
            //TODO
            //if (DurationInSeconds()+_seconds<0
            TimeDifference += seconds;
        }

        public void AddMinutes(long minutes)
        {
            AddSeconds(minutes*60);
        }
    }
}