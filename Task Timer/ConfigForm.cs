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
    public partial class ConfigForm : Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;

        public ConfigForm()
        {
            InitializeComponent();
        }

        public ConfigForm(System.Windows.Forms.NotifyIcon notifyIcon)
        {
            InitializeComponent();
            this.notifyIcon = notifyIcon;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Properties.Settings.Default.WorkHours;
            radioButtonHoursDecimal.Checked = Properties.Settings.Default.TimeDisplayDecimal;
            radioButtonHoursHrMin.Checked = !Properties.Settings.Default.TimeDisplayDecimal;
            checkBoxUseBATTTime.Checked = Properties.Settings.Default.UseBATTTime;
            textBoxDefaultTasks.Text = Properties.Settings.Default.DefaultTasks;
            checkBoxQuittingMessage.Checked = Properties.Settings.Default.QuittingTimeMessage;
            textBoxQuittingMessage.Text = Properties.Settings.Default.QuittingTimeMessageText;
            numericUpDownOffsetRight.Maximum = Screen.PrimaryScreen.Bounds.Right - 10;
            numericUpDownOffsetRight.Value = Properties.Settings.Default.OffsetRight;
            numericUpDownAutoHideOffset.Value = Properties.Settings.Default.AutoHideOffset;
            checkBoxCopyOnTrayMenuClick.Checked = Properties.Settings.Default.CopyOnTrayClick;
            checkBoxOpenBATTOnTrayMenuClick.Checked = Properties.Settings.Default.OpenBATTOnTrayClick;
            checkBoxSaveOnStop.Checked = Properties.Settings.Default.SaveOnStop;
            checkBoxExperimental.Checked = Properties.Settings.Default.Experimental;
            checkBoxDebugMode.Checked = Properties.Settings.Default.DebugMode;

            checkBoxQuittingMessage_CheckedChanged(null, null);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WorkHours = numericUpDown1.Value;
            Properties.Settings.Default.TimeDisplayDecimal = radioButtonHoursDecimal.Checked;
            Properties.Settings.Default.UseBATTTime = checkBoxUseBATTTime.Checked;
            if (textBoxDefaultTasks.Text.Equals(String.Empty))
                textBoxDefaultTasks.Text = "Sonstiges";
            Properties.Settings.Default.DefaultTasks = textBoxDefaultTasks.Text;
            Properties.Settings.Default.QuittingTimeMessage = checkBoxQuittingMessage.Checked;
            Properties.Settings.Default.QuittingTimeMessageText = textBoxQuittingMessage.Text;
            Properties.Settings.Default.OffsetRight = (int)numericUpDownOffsetRight.Value;
            Properties.Settings.Default.AutoHideOffset = (int)numericUpDownAutoHideOffset.Value;
            Properties.Settings.Default.CopyOnTrayClick = checkBoxCopyOnTrayMenuClick.Checked;
            Properties.Settings.Default.OpenBATTOnTrayClick = checkBoxOpenBATTOnTrayMenuClick.Checked;
            Properties.Settings.Default.SaveOnStop = checkBoxSaveOnStop.Checked;
            Properties.Settings.Default.Experimental = checkBoxExperimental.Checked;
            Properties.Settings.Default.DebugMode = checkBoxDebugMode.Checked;
            Properties.Settings.Default.Save();
        }

        private void ConfigForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonOK_Click(null, null);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (e.KeyChar == 27)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void checkBoxQuittingMessage_CheckedChanged(object sender, EventArgs e)
        {
            textBoxQuittingMessage.Enabled = checkBoxQuittingMessage.Checked;
        }

        private void buttonTestQuittingMessage_Click(object sender, EventArgs e)
        {
            string text = textBoxQuittingMessage.Text.Replace("\\", "\r\n");
            notifyIcon.ShowBalloonTip(15000, "Feierabend", text, ToolTipIcon.Info);
        }

        private void checkBoxUseBATTTime_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonHoursDecimal.Enabled = !checkBoxUseBATTTime.Checked;
            radioButtonHoursHrMin.Enabled = !checkBoxUseBATTTime.Checked;
        }
    }
}
