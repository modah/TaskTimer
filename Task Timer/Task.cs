using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Task_Timer
{
    class Task : IComparable
    {

        private long timeDifference = 0; //in seconds

        public Task(string taskName)
        {
            Taskname = taskName;
            TimeEntries = new List<TimeEntry>();
        }

        public List<TimeEntry> TimeEntries { get; set; }

        public string Taskname { get; set; }

        public bool IsActive {get; private set;}

        public long TimeDifference
        {
            get { return timeDifference; }
        }
        public long TimeDifferenceInMinutes
        {
            get
            {
                long minutes = timeDifference / 60;
                if (timeDifference % 60 > 0)
                    minutes++;
                return minutes;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Taskname.Equals(((Task)obj).Taskname);
        }

        public int CompareTo(object obj)
        {
            return 0;
        }

        public void Start()
        {
            TimeEntries.Add(new TimeEntry());
            TimeEntries[TimeEntries.Count - 1].Start = DateTime.Now;
            isActive = true;
        }

        public void Stop()
        {
            TimeEntries[TimeEntries.Count - 1].Stop = DateTime.Now;
            isActive = false;
        }

        public long DurationInSeconds()
        {
            long seconds = 0;
            foreach (var timeEntry in TimeEntries)
            {
                seconds += timeEntry.DurationInSeconds();
            }
            long _duration = seconds + timeDifference;
            if (_duration > 0)
                return _duration;
            else
                return 0;
        }

        public long DurationInMinutes()
        {
            long seconds = this.DurationInSeconds();
            long minutes = seconds / 60;
            if (seconds % 60 > 0)
                minutes++;
            return minutes;
        }

        public long DurationInSeconds(DateTime date)
        {
            long seconds = 0;
            foreach (var timeEntry in TimeEntries)
            {
                if (timeEntry.Start.Date.Equals(date.Date) ||
                    timeEntry.Stop.Date.Equals(date.Date))
                    seconds += timeEntry.DurationInSeconds();
            }
            long _duration = seconds + timeDifference;
            if (_duration > 0)
                return _duration;
            else
                return 0;
        }

        public long DurationInMinutes(DateTime date)
        {
            long seconds = this.DurationInSeconds(date);
            long minutes = seconds / 60;
            if (seconds % 60 > 0)
                minutes++;
            return minutes;
        }

        public void AddSeconds(long _seconds)
        {
            //TODO
            //if (DurationInSeconds()+_seconds<0
            timeDifference += _seconds;
        }

        public void AddMinutes(long minutes)
        {
            AddSeconds(minutes * 60);
        }
    }
}
