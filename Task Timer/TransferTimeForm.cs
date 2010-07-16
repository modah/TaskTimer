using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Task_Timer
{
    public partial class TransferTimeForm : Form
    {
        private List<String> taskList;
        private string srcTask;
        private long duration;

        public TransferTimeForm()
        {
            InitializeComponent();
        }

        public TransferTimeForm(List<String> _taskList, string _srcTask, long _duration)
        {
            InitializeComponent();
            taskList = _taskList;
            srcTask = _srcTask;
            duration = _duration;
        }

        public string DestinationTask
        {
            get 
            {
                return comboBoxTransferTimeTo.Text;
            }
        }

        private void TransferTimeForm_Load(object sender, EventArgs e)
        {
            label1.Text += "(" + duration + " Minute";
            label1.Text += duration == 1 ? "):" : "n):";
            foreach (var _task in taskList)
            {
                if (!_task.Equals(srcTask))
                    comboBoxTransferTimeTo.Items.Add(_task);
            }
            comboBoxTransferTimeTo.Text = comboBoxTransferTimeTo.Items[0].ToString();
            if (comboBoxTransferTimeTo.Items.Count == 1)
                comboBoxTransferTimeTo.Enabled = false;
        }

        private void TransferTimeForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            if (e.KeyChar == 27)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }
    }
}
