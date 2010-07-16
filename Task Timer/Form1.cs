using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Task_Timer
{
    public partial class Form1 : Form
    {
        private Point mouseposition;
        private TaskList taskList = new TaskList();
        private string lastTaskname = String.Empty;
        private bool ShowQuittingTimeMessage = true;
        private int contextMenuTaskStartIndex = 0;
        private string filename = String.Empty;
        private Color defaultContextMenuStripBackColor;
        private DateTime TaskTimerStartTime = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            defaultContextMenuStripBackColor = contextMenuStrip1.Items[0].BackColor;
            var defaultTasks = Properties.Settings.Default.DefaultTasks.Split(';');
            foreach (var defaultTask in defaultTasks)
                comboBox1.Items.Add(defaultTask);
            foreach (var item in comboBox1.Items)
            {
                taskList.AddTask(item.ToString());
                var toolStripItem = new ToolStripMenuItem();
                toolStripItem.Text = item.ToString();
                toolStripItem.MouseUp += new MouseEventHandler(toolStripItem_MouseUp);
                contextMenuStrip1.Items.Add(toolStripItem);
            }
            for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                //if (contextMenuStrip1.Items[i].Text.Equals(defaultTasks[0]))
                if (contextMenuStrip1.Items[i].Text.Equals("Beenden"))
                {
                    contextMenuTaskStartIndex = i + 2;
                    break;
                }
            SetTrayTaskListColors(defaultTasks[0]);
            TopMostToolStripMenuItem.Checked = Properties.Settings.Default.AlwaysOnTop;
            this.TopMost = TopMostToolStripMenuItem.Checked;
            autoHideToolStripMenuItem.Checked = Properties.Settings.Default.AutoHide;

            //set version information
            versionToolStripMenuItem.Text = "Über " + Application.ProductName + " v" + Application.ProductVersion;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseposition = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseposition.X, mouseposition.Y);
                Location = mousePos;
            }
        }

        /// <summary>
        /// Start/Stop button
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            formIsActive = true;

            if (!comboBox1.Text.Equals(lastTaskname)) // set combobox to current task
                comboBox1.Text = lastTaskname;

            if (comboBox1.Text.Equals(String.Empty))
                return;

            if (button1.BackColor == Color.Red)
            {
                button1.BackColor = Color.Lime;
                if (!taskList.Contains(comboBox1.Text))
                    taskList.AddTask(comboBox1.Text);
                taskList.Start(comboBox1.Text);
                timer1_Tick(null, null);
                timer1.Enabled = true;
            }
            else
            {
                button1.BackColor = Color.Red;
                taskList.Stop(comboBox1.Text);
                timer1.Enabled = false;
            }

            if (button1.BackColor == Color.Red)
                startStopToolStripMenuItem.Text = "Start";
            else
                startStopToolStripMenuItem.Text = "Stop";

            SetTrayTaskListColors(lastTaskname);

            if (button1.BackColor == Color.Red && Properties.Settings.Default.SaveOnStop)
                SaveFile(String.Empty);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                if (!comboBox1.Text.Equals(String.Empty))
                    AddOrChangeTask(comboBox1.Text);
                else
                    comboBox1.Text = lastTaskname;
        }

        private void AddOrChangeTask(string newTask)
        {
            comboBox1.BackColor = Color.White;
            if (!taskList.Contains(newTask))
            {
                taskList.AddTask(newTask);
                var toolStripItem = new ToolStripMenuItem();
                toolStripItem.Text = newTask;
                toolStripItem.MouseUp += new MouseEventHandler(toolStripItem_MouseUp);
                contextMenuStrip1.Items.Add(toolStripItem);
                comboBox1.Items.Add(newTask);
            }
            if (button1.BackColor == Color.Lime)
            {
                taskList.Stop(lastTaskname);
                taskList.Start(newTask);
            }
            SetTrayTaskListColors(newTask);
            lastTaskname = newTask;
            SetNotifyIconToolTip(lastTaskname);

            timer1_Tick(null, null); // Update label
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals(lastTaskname))
                comboBox1.BackColor = Color.LightPink;
            else
                comboBox1.BackColor = Color.White;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rotze (gezeiteter task darf sich erst durch "enter" ändern)
            /*
            if (button1.BackColor == Color.Lime)
            {
                taskList.Stop(lastTaskname);
                taskList.Start(comboBox1.Text);
            }
            */
            //lastTaskname = comboBox1.Text;
            comboBox1_TextChanged(null, null);

        }

        private double GetBATTFromMinutes(long minutes)
        {
            if (minutes % 15 == 0)
                return minutes / 15 / 4D;
            else
                return ((minutes / 15) + 1) / 4D;
        }

        private void beendenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile(String.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Countdown)
                SetLabelCountdown();
            else
                SetLabelCountup();
        }

        private void SetLabelCountup()
        {
            if (!Properties.Settings.Default.UseBATTTime)
            {
                long totalMinutes = 0;
                foreach (var task in taskList.Tasks)
                {
                    long minutes = task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                }
                if (Properties.Settings.Default.TimeDisplayDecimal)
                {
                    string duration = (Math.Round(((double)totalMinutes / 60), 1)).ToString();
                    label1.Text = duration.Length > 1 ? duration : duration + ",0";
                }
                else
                {
                    int hours = (int)totalMinutes / 60;
                    int minutes = (int)totalMinutes - hours * 60;
                    label1.Text = hours.ToString() + ":" + minutes.ToString().PadLeft(2, '0');
                }

                MacheDichHeemeMeiner(totalMinutes);
                SetLabelColor(totalMinutes);
            }
            else // TODO - UseBATTTime
            {
                long totalMinutes = 0;
                foreach (var task in taskList.Tasks)
                {
                    long minutes = task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                }

                label1.Text = GetTotalBATTBookingDuration().ToString();
                if (label1.Text.Length == 1)
                    label1.Text += ",0";

                MacheDichHeemeMeiner(totalMinutes);
                SetLabelColor(totalMinutes);
            }

            AppendCurrentTaskDuration();
        }

        private void SetLabelCountdown()
        {
            if (!Properties.Settings.Default.UseBATTTime)
            {
                long totalMinutes = 0;
                foreach (var task in taskList.Tasks)
                {
                    long minutes = task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                }
                if (Properties.Settings.Default.TimeDisplayDecimal)
                {
                    decimal hours = Math.Round(((decimal)totalMinutes / 60), 1);
                    decimal hoursRemaining = Properties.Settings.Default.WorkHours - hours;
                    string duration = Math.Abs(hoursRemaining).ToString();
                    label1.Text = duration.Length > 1 ? duration : duration + ",0";
                }
                else
                {
                    int minutesRemaining = Math.Abs((int)(Properties.Settings.Default.WorkHours * 60) - (int)totalMinutes);
                    int hours = minutesRemaining / 60;
                    int minutes = minutesRemaining - hours * 60;
                    label1.Text = hours.ToString() + ":" + minutes.ToString().PadLeft(2, '0');
                }

                MacheDichHeemeMeiner(totalMinutes);
                SetLabelColor(totalMinutes);
            }
            else // TODO - UseBATTTime
            {
                long totalMinutes = 0;
                foreach (var task in taskList.Tasks)
                {
                    long minutes = task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                }

                label1.Text = GetTotalBATTBookingDuration().ToString();
                if (label1.Text.Length == 1)
                    label1.Text += ",0";

                MacheDichHeemeMeiner(totalMinutes);
                SetLabelColor(totalMinutes);
            }

            AppendCurrentTaskDuration();
        }

        private void AppendCurrentTaskDuration()
        {
            string _appendix = " (";
            if (!Properties.Settings.Default.UseBATTTime)
                if (Properties.Settings.Default.TimeDisplayDecimal)
                {
                    decimal hours = Math.Round(((decimal)taskList.Get(lastTaskname).DurationInMinutes(DateTime.Now) / 60), 1);
                    string duration = hours.ToString();
                    _appendix += duration.Length > 1 ? duration : duration + ",0";
                }
                else
                {
                    int minutes = (int)taskList.Get(lastTaskname).DurationInMinutes(DateTime.Now);
                    int hours = minutes / 60;
                    minutes = minutes - hours * 60;
                    _appendix += hours.ToString() + ":" + minutes.ToString().PadLeft(2, '0');
                }
            else
            {
                string _currentTaskDuration = GetBATTFromMinutes(taskList.Get(lastTaskname).DurationInMinutes(DateTime.Now)).ToString();
                if (_currentTaskDuration.Length == 1)
                    _currentTaskDuration += ",0";
                _appendix += _currentTaskDuration;
            }
            _appendix += ")";
            label1.Text += _appendix;
        }

        private void MacheDichHeemeMeiner(long duration)
        {
            if (Properties.Settings.Default.QuittingTimeMessage && ShowQuittingTimeMessage &&
                duration == Properties.Settings.Default.WorkHours * 60)
            {
                string text = Properties.Settings.Default.QuittingTimeMessageText.Replace("\\", "\r\n");
                //MessageBox.Show(text, "Feierabend", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                notifyIcon1.ShowBalloonTip(15000, "Feierabend", text, ToolTipIcon.Info);
                ShowQuittingTimeMessage = false;
                SetLabelColor(Color.Red);
            }
        }

        private void SetLabelColor(Color color)
        {
            label1.ForeColor = color;
        }

        private void SetLabelColor(long totalMinutes)
        {
            if (Properties.Settings.Default.TimeDisplayDecimal)
            {
                decimal hours = Math.Round(((decimal)totalMinutes / 60), 1);
                if (hours > Properties.Settings.Default.WorkHours)
                    SetLabelColor(Color.Red);
                else
                    SetLabelColor(Color.Black);
            }
            else
            { 
                if (totalMinutes > Properties.Settings.Default.WorkHours * 60)
                    SetLabelColor(Color.Red);
                else
                    SetLabelColor(Color.Black);
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ConfigForm(notifyIcon1);
            if (form.ShowDialog() == DialogResult.OK)
            {
                timer1_Tick(null, null);
                // TODO - position merken und zurücksetzen nur noch auf wunsch
                this.SetDesktopLocation(
                    Screen.PrimaryScreen.Bounds.Right -
                    this.Size.Width -
                    Properties.Settings.Default.OffsetRight, 0);
                DebugMode();
            }
        }

        private void DebugMode()
        {
            timerDebug_Tick(null, null);
            timerDebug.Enabled = Properties.Settings.Default.DebugMode;
            listBox1.Visible = Properties.Settings.Default.DebugMode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DebugMode();

            SetCountDownCountUp();

            // autostart
            comboBox1.Text = Properties.Settings.Default.DefaultTasks.Split(';')[0];
            SetNotifyIconToolTip(comboBox1.Text);
            taskList.Start(comboBox1.Text);
            lastTaskname = comboBox1.Text;
            button1.BackColor = Color.Lime;
            comboBox1.BackColor = Color.White;
            SetTrayTaskListColors(comboBox1.Text);
            timer1_Tick(null, null);
            timer1.Enabled = true;

            this.SetDesktopLocation(
                Screen.PrimaryScreen.Bounds.Right -
                this.Size.Width -
                Properties.Settings.Default.OffsetRight, 0);
        }

        private void countdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Countdown = true;
            Properties.Settings.Default.Save();
            SetCountDownCountUp();
            timer1_Tick(null, null);
        }

        private void countupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Countdown = false;
            Properties.Settings.Default.Save();
            SetCountDownCountUp();
            timer1_Tick(null, null);
        }

        private void SetCountDownCountUp()
        {
            if (Properties.Settings.Default.Countdown)
            {
                countdownToolStripMenuItem.Checked = true;
                countupToolStripMenuItem.Checked = false;
            }
            else
            {
                countdownToolStripMenuItem.Checked = false;
                countupToolStripMenuItem.Checked = true;
            }
        }

        private void TopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMostToolStripMenuItem.Checked = !TopMostToolStripMenuItem.Checked;
            this.TopMost = TopMostToolStripMenuItem.Checked;
            Properties.Settings.Default.AlwaysOnTop = TopMostToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void startStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
            if (button1.BackColor == Color.Red)
                startStopToolStripMenuItem.Text = "Start";
            else
                startStopToolStripMenuItem.Text = "Stop";
        }

        private void saveFiletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(String.Empty);
            if (!filename.Equals(String.Empty))
                System.Diagnostics.Process.Start(filename);
        }

        private void SaveFile(string _filename)
        {
            string directory = Properties.Settings.Default.StorageLocation;
            try
            {
                CreateDirectory(directory);
                filename = directory + "\\";
                if (!_filename.Equals(String.Empty))
                    filename += _filename;
                else
                    filename += DateTime.Now.Year +
                                DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                DateTime.Now.Day.ToString().PadLeft(2, '0') + "_" +
                                DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                                DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                                DateTime.Now.Second.ToString().PadLeft(2, '0') + "_" +
                                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(DateTime.Now.DayOfWeek)].ToString() +
                                "_(" + GetTotalBATTBookingDuration() + "h)" +
                                ".csv";
                StreamWriter Writer = new StreamWriter(filename, false, Encoding.Default);
                Writer.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                Writer.WriteLine();
                Writer.WriteLine(DateTime.Now.ToLongDateString());
                Writer.WriteLine("Beginn:;" + TaskTimerStartTime.ToShortTimeString() + " Uhr");
                Writer.WriteLine("Ende:;" + DateTime.Now.ToShortTimeString() + " Uhr");
                Writer.WriteLine();
                Writer.WriteLine("BATT;Minuten;Eintrag");
                /*
                foreach (var task in taskList.Tasks)
                {
                    long minutes = task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                    double batt = GetBATTFromMinutes(minutes);
                    totalBatt += batt;
                    Writer.WriteLine(batt + ";" + minutes + ";" + task.Taskname);
                }
                */
                var sortedTasks = new Dictionary<string, long>();
                foreach (var task in taskList.Tasks)
                {
                    sortedTasks.Add(task.Taskname, task.DurationInSeconds());
                }
                var items = from k in sortedTasks.Keys
                            orderby sortedTasks[k] descending
                            select k;

                long totalMinutes = 0;
                double totalBatt = 0;
                foreach (var item in items)
                {
                    Task _task = taskList.Get(item);
                    long minutes = _task.DurationInMinutes(DateTime.Today);
                    totalMinutes += minutes;
                    double batt = GetBATTFromMinutes(minutes);
                    totalBatt += batt;
                    if (minutes > 0)
                        Writer.WriteLine(batt + ";" + minutes + ";" + _task.Taskname);
                }
                Writer.WriteLine("Summe");
                Writer.WriteLine(totalBatt + ";" + totalMinutes);
                Writer.Close();
            }
            catch (Exception ex)
            {
                if (filename.EndsWith("autosave.csv"))
                    notifyIcon1.ShowBalloonTip(15000, "Autosave fehlgeschlagen",
                        "Ist die Autosave-Datei geöffnet?",
                        ToolTipIcon.Error);
                else
                    MessageBox.Show(ex.Message + "\r\n\r\n" +
                        "Existiert das Verzeichnis " + directory + " ?",
                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double GetTotalBATTBookingDuration()
        {
            double totalBatt = 0;
            foreach (var task in taskList.Tasks)
            {
                long minutes = task.DurationInMinutes(DateTime.Today);
                totalBatt += GetBATTFromMinutes(minutes);
            }
            return totalBatt;
        }

        private List<string> GetTaskListStringsByDuration()
        {
            var _taskList = new List<string>();

            var sortedTasks = new Dictionary<string, long>();
            foreach (var task in taskList.Tasks)
            {
                sortedTasks.Add(task.Taskname, task.DurationInSeconds());
            }
            var items = from k in sortedTasks.Keys
                        orderby sortedTasks[k] descending
                        select k;

            foreach (var item in items)
            {
                Task _task = taskList.Get(item);
                long minutes = _task.DurationInMinutes(DateTime.Today);
                long seconds = _task.DurationInSeconds(DateTime.Today);
                long correction = _task.TimeDifferenceInMinutes;
                double batt = GetBATTFromMinutes(minutes);
                _taskList.Add(batt + "\t" + minutes + "\t" + seconds + "\t" + correction + "\t" + _task.Taskname);
            }
            return _taskList;
        }

        private double GetTotalBATTDuration()
        {
            long totalMinutes = 0;
            foreach (var task in taskList.Tasks)
                totalMinutes += task.DurationInMinutes(DateTime.Today);
            return GetBATTFromMinutes(totalMinutes);
        }

        private void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) // reset combobox value on ESC
            {
                if (!comboBox1.Text.Equals(lastTaskname))
                    comboBox1.Text = lastTaskname;
            }

            if (e.KeyValue == 113) // F2
            {
                RenameTask();
            }
            if (e.KeyValue == 114) // F3
            {
                CreateUnspecificTask();
            }
            if (e.KeyValue == 117) // F6
            {
                // open ticket
                openTicketToolStripMenuItem_Click(null, null);
            }
            if (e.KeyValue == 118) // F7
            { 
                // correct booked time
                var _task = taskList.Get(comboBox1.Text);
                if (_task != null)
                    CorrectBookedTime(_task);
            }
            if (e.KeyValue == 119) // F8
            {
                // delete task
                DeleteTask();
            }
            if (e.KeyValue == 122) // F11
            {
                Properties.Settings.Default.DebugMode = !Properties.Settings.Default.DebugMode;
                Properties.Settings.Default.Save();
                DebugMode();
            }
            if (e.KeyValue == 123) // F12
            {
                // show task info in balloon tip
                ShowTaskInfo(comboBox1.Text);
            }
        }

        private void RenameTask()
        {
            // rename current task
            if (taskList.Contains(comboBox1.Text))
            {
                string tmpLastTaskname = comboBox1.Text;
                bool setLastTaskname = lastTaskname.Equals(comboBox1.Text);
                var renameForm = new TaskEntryForm("Task umbenennen", comboBox1.Text);
                if (renameForm.ShowDialog() == DialogResult.OK)
                {
                    if (renameForm.Taskname.Equals(String.Empty))
                    {
                        MessageBox.Show("Unbenannte Tasks sind nicht erlaubt.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (taskList.Contains(renameForm.Taskname))
                    {
                        MessageBox.Show("Dieser Task existiert bereits.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    taskList.Get(comboBox1.Text).Taskname = renameForm.Taskname;
                    if (setLastTaskname)
                        lastTaskname = renameForm.Taskname;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (comboBox1.Items[i].Equals(comboBox1.Text))
                        {
                            comboBox1.Items[i] = "";
                            comboBox1.Items[i] = renameForm.Taskname;
                        }
                    }
                    comboBox1.Text = renameForm.Taskname;
                    for (int i = contextMenuTaskStartIndex; i < contextMenuStrip1.Items.Count; i++)
                    {
                        if (contextMenuStrip1.Items[i].Text.Equals(tmpLastTaskname))
                            contextMenuStrip1.Items[i].Text = renameForm.Taskname;
                    }
                    SetNotifyIconToolTip(comboBox1.Text);
                }
                ActivateComboBox();
            }
        }

        private void CorrectBookedTime(Task _task)
        {
            var _form = new CorrectBookedTimeForm(_task.DurationInMinutes(DateTime.Now));
            if (_form.ShowDialog() == DialogResult.OK)
            {
                if (_form.TimeCorrection != 0)
                {
                    _task.AddMinutes(_form.TimeCorrection);
                    timer1_Tick(null, null);
                }
            }
        }

        private void RefreshTrayTaskItems()
        { 
            // remove all tasks from tray menu
            for (int i = contextMenuStrip1.Items.Count - 1; i > contextMenuTaskStartIndex - 1; i--)
                contextMenuStrip1.Items.RemoveAt(i);

            // add all tasks to tray menu
            foreach (var _task in taskList.Tasks)
            {
                var toolStripItem = new ToolStripMenuItem();
                // TODO - Superfeature
                /*
                string _battDuration = GetBATTFromMinutes(_task.DurationInMinutes()).ToString();
                if (_battDuration.Length == 1)
                    _battDuration += ",";
                toolStripItem.Text = "[" + _battDuration.PadRight(4,'0') + "] " + _task.Taskname;
                */
                toolStripItem.Text = _task.Taskname;
                toolStripItem.MouseUp += new MouseEventHandler(toolStripItem_MouseUp);
                contextMenuStrip1.Items.Add(toolStripItem);
            }
            SetTrayTaskListColors(comboBox1.Text.Equals(String.Empty) ? lastTaskname : comboBox1.Text);
        }

        private void RefreshComboboxContents()
        {
            comboBox1.Items.Clear();
            foreach (var _task in taskList.Tasks)
            {
                comboBox1.Items.Add(_task.Taskname);
            }
        }

        private bool IsTimerActive()
        {
            if (button1.BackColor == Color.Lime)
                return true;
            return false;
        }

        private void DeleteTask()
        {
            if (UseExperimentalFeatures())
            {
                if (!comboBox1.Text.Equals(comboBox1.Items[0]))
                {
                    var _task = taskList.Get(comboBox1.Text);
                    if (_task != null)
                    {
                        if (MessageBox.Show("Diesen Task wirklich löschen?\r\n\r\n" + _task.Taskname,
                            "Task Timer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // TODO: versucht bereits gelöschten task zu stoppen
                            if (DeleteTask(_task))
                            {
                                comboBox1.Text = lastTaskname;
                                comboBox1.Items.Remove(_task.Taskname);
                                if (comboBox1.Text.Equals(String.Empty))
                                    AddOrChangeTask(comboBox1.Items[0].ToString());
                                comboBox1.Text = lastTaskname;
                                RefreshTrayTaskItems();
                            }
                            else
                                MessageBox.Show("Fehler bei löschen von Task:\r\n\r\n" + _task.Taskname,
                                    "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private bool DeleteTask(Task _task)
        {
            if (_task.Taskname.Equals(comboBox1.Items[0]))
                //throw new NotSupportedException(); //TODO
                return false;
            TransferTaskTime(_task);
            if (taskList.Delete(_task))
                return true;
            return false;
        }

        private void TransferTaskTime(Task srcTask)
        {
            var _form = new TransferTimeForm(GetTaskListAsStringList(), srcTask.Taskname, srcTask.DurationInMinutes());
            if (_form.ShowDialog() == DialogResult.Yes)
            {
                var _task = taskList.Get(_form.DestinationTask);
                TransferTaskTime(srcTask, _task);
            }
        }

        private void TransferTaskTime(Task srcTask, Task dstTask)
        {
            dstTask.AddSeconds(srcTask.DurationInSeconds());
        }

        private List<String> GetTaskListAsStringList()
        {
            var _list = new List<String>();
            foreach (var item in taskList.Tasks)
            {
                _list.Add(item.Taskname);
            }
            return _list;
        }

        private void CreateUnspecificTask()
        {
            if (MessageBox.Show("Neuen Task anlegen?", "Task Timer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var newTask = "Unbestimmte Aufgabe (angelegt um " + DateTime.Now.ToShortTimeString() + " Uhr)";
                if (taskList.Get(newTask) != null)
                {
                    newTask += " " + DateTime.Now.Ticks;
                }
                comboBox1.Text = newTask;
                AddOrChangeTask(newTask);
            }
        }

        private void timerAlwaysOnTop_Tick(object sender, EventArgs e)
        {
            // TODO
            //if (!this.Focused)
            //    this.TopMost = TopMostToolStripMenuItem.Checked;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = (MouseEventArgs)e;
            if (mouseEvent.Button == MouseButtons.Left)
                ActivateComboBox();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = (MouseEventArgs)e;
            if (mouseEvent.Button == MouseButtons.Left)
            {
                // create new task (TODO: dialog is not active after double click)
                //var newTaskForm = new TaskEntryForm("Neuer Task", "");
                //if (newTaskForm.ShowDialog() == DialogResult.OK)
                //{
                //    AddOrChangeTask(newTaskForm.Taskname);
                //    comboBox1.Text = newTaskForm.Taskname;
                //}

                this.TopMost = true;
                this.TopMost = TopMostToolStripMenuItem.Checked;

                button1_Click(null, null); // start/stop
            }
        }

        private void autoHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleAutoHide();
        }

        private long lastInvisibility = DateTime.Now.Ticks;
        private const long TWO_SECONDS_IN_TICKS = TimeSpan.TicksPerSecond * 2;
        private const long ONE_HALF_SECOND_IN_TICKS = TimeSpan.TicksPerSecond / 2;
        private void globalEventProvider1_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("AutoHide: " + disableAutoHide.ToString() + ", ActiveForm: " + formIsActive.ToString());
            if (autoHideToolStripMenuItem.Checked && 
                TopMostToolStripMenuItem.Checked && 
                !disableAutoHide && 
                !formIsActive)
            {
                int offset = Properties.Settings.Default.AutoHideOffset;
                int dblOffset = offset * 2;
                Rectangle rect = new Rectangle(
                    this.Location.X - offset,
                    this.Location.Y - offset,
                    this.Size.Width + dblOffset,
                    this.Size.Height + dblOffset);
                this.Visible = !rect.Contains(e.Location);
            }

            if (!this.Visible)
                lastInvisibility = DateTime.Now.Ticks;

            if (DateTime.Now.Ticks - lastInvisibility < TWO_SECONDS_IN_TICKS &&
                e.X < 10)
                ActivateComboBox();


            if (!this.Visible && e.Delta > 0)
            {
                this.Visible = true;
                ActivateComboBox();
            }


            /*
            if (!this.Visible)
            {
                if (DateTime.Now.Ticks - lastInvisibility < ONE_HALF_SECOND_IN_TICKS)
                {
                    this.Visible = true;
                    ActivateComboBox();
                }
                else
                    lastInvisibility = DateTime.Now.Ticks;
            }
            */
        }

        private bool disableAutoHide = false;
        private bool IsCtrlDown = false;
        private bool IsWinDown = false;
        private void globalEventProvider1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 162 || e.KeyValue == 163)
            {
                IsCtrlDown = true;
                disableAutoHide = true;
            }
            else if (e.KeyValue == 91)
                IsWinDown = true;
        }

        private void globalEventProvider1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 162 || e.KeyValue == 163)
            {
                IsCtrlDown = false;
                disableAutoHide = false;
            }
            else if (e.KeyValue == 91)
                IsWinDown = false;
            //if (e.KeyValue == 84 && IsCtrlDown && IsWinDown)
            //if (e.KeyValue == 84 && e.Control && e.Shift)
            //{
            //    e.Handled = true;
            //    e.SuppressKeyPress = true;
            //    ActivateComboBox();
            //}
        }

        private bool formIsActive = false;
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            label1.Focus();
            formIsActive = false;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            formIsActive = true;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            formIsActive = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            formIsActive = true;
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToggleAutoHide();
            if (autoHideToolStripMenuItem.Checked)
                this.Visible = false;
        }

        private void ToggleAutoHide()
        {
            autoHideToolStripMenuItem.Checked = !autoHideToolStripMenuItem.Checked;
            Properties.Settings.Default.AutoHide = autoHideToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void globalEventProvider1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ctrl+Win+T (T=116?)
            //if (e.KeyChar == 20 && IsCtrlDown && IsWinDown)
            //{
            //    this.BringToFront();
            //    this.Activate();
            //    comboBox1.Focus();
            //}
        }

        private void ActivateComboBox()
        {
            this.BringToFront();
            this.Activate();
            comboBox1.Focus();
            formIsActive = true;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            // bug: shouldn't run when form is only made visible
            //formIsActive = true;
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(comboBox1.Text, true);
            }
            catch (Exception)
            {
                MessageBox.Show("Text konnte nicht in Zwischenablage geschrieben werden.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void toolStripItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                comboBox1.Text = sender.ToString();
                AddOrChangeTask(sender.ToString());
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Properties.Settings.Default.CopyOnTrayClick)
                {
                    try
                    {
                        Clipboard.SetDataObject(sender.ToString(), true);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Text konnte nicht in Zwischenablage geschrieben werden.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (Properties.Settings.Default.OpenBATTOnTrayClick)
                {
                    // REDUNDANT!
                    // TODO: Experimental
                    if (UseExperimentalFeatures())
                    {
                        var list = GetTicketNumbers(sender.ToString());
                        if (list.Count > 0)
                            OpenTicket(list[0]);
                    }
                }
                ShowTaskInfo(sender.ToString());
            }
        }

        private void SetTrayTaskListColors(string task)
        {
            Color trayItemBackColor = defaultContextMenuStripBackColor;
            //Color trayItemForeColor = Color.Black;
            if (button1.BackColor == Color.Red)
            {
                trayItemBackColor = Color.Red;
                //trayItemForeColor = Color.White;
            }
            else
            {
                trayItemBackColor = Color.LightGreen;
                //trayItemForeColor = Color.Black;
            }

            for (int i = contextMenuTaskStartIndex; i < contextMenuStrip1.Items.Count; i++)
            {
                contextMenuStrip1.Items[i].BackColor = 
                    contextMenuStrip1.Items[i].Text.Equals(task) ? trayItemBackColor : defaultContextMenuStripBackColor;
                //contextMenuStrip1.Items[i].ForeColor =
                    //contextMenuStrip1.Items[i].Text.Equals(task) ? trayItemForeColor : defaultContextMenuStripBackColor;
            }
        }

        private void pasteFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                try
                {
                    comboBox1.Text = Clipboard.GetText();
                    ActivateComboBox();
                }
                catch (Exception)
                {
                    MessageBox.Show("Text konnte nicht aus Zwischenablage gelesen werden.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowTaskInfo(string _taskname)
        { 
            Task _task = taskList.Get(_taskname);
            if (_task != null)
            {
                long _minutes = _task.DurationInMinutes(DateTime.Today);
                double _batt = GetBATTFromMinutes(_minutes);
                long _correction = _task.TimeDifferenceInMinutes;
                notifyIcon1.ShowBalloonTip(15000, _task.Taskname, 
                    "Erfasste Zeit: " + _batt + " Std. (" + _minutes + " Min.)\r\n" +
                    "Korrekturwert: " + _correction + " Min.",
                    ToolTipIcon.Info);
            }
        }

        private void SetNotifyIconToolTip(string newToolTip)
        {
            int _length = newToolTip.Length > 63 ? 63 : newToolTip.Length;
            notifyIcon1.Text = newToolTip.Substring(0, _length);
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Properties.Settings.Default.StorageLocation);
            }
            catch (Exception)
            {
                MessageBox.Show("Fehler bei Öffnen des Auswertungsordners.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyTicketNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Experimental
            if (UseExperimentalFeatures())
                CopyTicketNumberToClipboard(comboBox1.Text);
        }

        private void CopyTicketNumberToClipboard(string _taskname)
        {
            var list = GetTicketNumbers(_taskname);
            if (list.Count > 0)
            {
                try
                {
                    Clipboard.SetDataObject(list[0].ToString(), true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Text konnte nicht in Zwischenablage geschrieben werden.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void openTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Experimental
            if (UseExperimentalFeatures())
            {
                var list = GetTicketNumbers(comboBox1.Text);
                if (list.Count > 0)
                    OpenTicket(list[0]);
            }
        }

        private List<String> GetTicketNumbers(string _taskname)
        {
            var list = new List<String>();
            var regex = new System.Text.RegularExpressions.Regex(@"((I|S)\d{8})|((P)\d{5})");
            var matches = regex.Matches(_taskname);
            foreach (var match in matches)
            {
                list.Add(match.ToString());
            }
            return list;
        }

        private void OpenTicket(string _ticketNumber)
        {
            var type = _ticketNumber.Substring(0, 1);
            string command = String.Empty;
            if (type.Equals("I"))
                command = "navision://client/run?ntauthentication=ja&servername=zeus&database=development&company=Development&" + 
                    "target=Form 79201&view=SORTING(Field10)&position=Field10=0(" + 
                    _ticketNumber + 
                    ")&servertype=MSSQL";
            if (type.Equals("S"))
                command = "navision://client/run?ntauthentication=ja&servername=zeus&database=development&company=Development&" + 
                    "target=Form 79237&view=SORTING(Field1)&position=Field1=0(" + 
                    _ticketNumber + 
                    ")&servertype=MSSQL";
            if (type.Equals("P"))
                command = "navision://client/run?ntauthentication=ja&servername=zeus&database=development&company=Development&" + 
                    "target=Form 79274&view=SORTING(Field1)&position=Field1=0(" + 
                    _ticketNumber + 
                    ")&servertype=MSSQL";
            if (command != String.Empty)
                try
                {
                    System.Diagnostics.Process.Start(command);
                }
                catch (Exception)
                {
                    MessageBox.Show("Navision konnte nicht gestartet werden.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private bool UseExperimentalFeatures()
        {
            if (!Properties.Settings.Default.Experimental)
            {
                MessageBox.Show(
                    "Diese Funktion ist noch in Entwicklung und kann fehlerhaft sein oder zu Abstürzen führen!\r\n\n" +
                    "Bitte im Konfigurationsdialog freischalten.", "Task Timer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void createQuickTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateUnspecificTask();
        }

        private void timerDebug_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var list = GetTaskListStringsByDuration();
            listBox1.Items.Add("BATT\tMin\tSec\tCorr\tTask");
            foreach (var task in list)
            {
                var x = task.Split('\t');
                if (taskList.Get(x[4]).IsActive)
                    listBox1.Items.Add("*" + task);
                else
                    listBox1.Items.Add(task);
            }
        }

        private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 23 && DateTime.Now.Minute > 58)
                Application.Exit();
        }

        private void correctBookedTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // correct booked time
            var _task = taskList.Get(comboBox1.Text);
            if (_task != null)
                CorrectBookedTime(_task);
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void timerAutosave_Tick(object sender, EventArgs e)
        {
            SaveFile("autosave.csv");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            RefreshTrayTaskItems();
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DebugMode = !Properties.Settings.Default.DebugMode;
            Properties.Settings.Default.Save();
            DebugMode();
        }

        private void renameTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameTask();
        }
    }
}
