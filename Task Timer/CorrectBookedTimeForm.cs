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
    public partial class CorrectBookedTimeForm : Form
    {
        public CorrectBookedTimeForm()
        {
            InitializeComponent();
        }

        public CorrectBookedTimeForm(long _minutes)
        {
            InitializeComponent();
            labelTaskTime.Text += _minutes + " Minute";
            if (_minutes == 0 || _minutes > 1)
                labelTaskTime.Text += "n";
        }

        public int TimeCorrection
        {
            get
            {
                return Convert.ToInt32(numericUpDownCorrection.Value);
            }
        }

        private void CorrectBookedTimeForm_Load(object sender, EventArgs e)
        {
            numericUpDownCorrection.Select(0, 1);
        }

        private void CorrectBookedTimeForm_KeyPress(object sender, KeyPressEventArgs e)
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
