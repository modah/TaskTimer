using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Task_Timer
{
    class TaskList
    {
        public TaskList()
        {
            Tasks = new List<Task>();
        }

        public List<Task> Tasks { get; set; }

        public void AddTask(string taskName)
        {
            Tasks.Add(new Task(taskName));
        }

        public Task Get(string taskName)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].Taskname.Equals(taskName))
                    return Tasks[i];            
            }
            return null;
        }

        public bool Contains(string taskName)
        {
            return Tasks.Contains(new Task(taskName));
        }

        public bool Delete(string taskName)
        {
            try
            {
                Tasks.Remove(Get(taskName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Task _task)
        {
            try
            {
                Tasks.Remove(_task);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Start(string taskName)
        {
            Tasks[Tasks.IndexOf(new Task(taskName))].Start();
        }

        public void Stop(string taskName)
        {
            //TODO: quickhack, da gelöschter task nicht mehr gestoppt werden kann
            try
            {
                Tasks[Tasks.IndexOf(new Task(taskName))].Stop();
            }
            catch (Exception)
            {
            }
        }
    }
}
