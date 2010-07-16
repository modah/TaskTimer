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
    public partial class TaskEntryForm : Form
    {
        public TaskEntryForm()
        {
            InitializeComponent();
        }

        public TaskEntryForm(string caption, string taskname)
        {
            InitializeComponent();
            this.Text = caption;
            textBoxTaskName.Text = taskname;
        }

        public string Taskname
        {
            get
            {
                return textBoxTaskName.Text;
            }
        }

        private void TaskEntryForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.KeyChar == 27)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
