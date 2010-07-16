namespace Task_Timer
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonHoursDecimal = new System.Windows.Forms.RadioButton();
            this.radioButtonHoursHrMin = new System.Windows.Forms.RadioButton();
            this.checkBoxQuittingMessage = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownOffsetRight = new System.Windows.Forms.NumericUpDown();
            this.textBoxQuittingMessage = new System.Windows.Forms.TextBox();
            this.buttonTestQuittingMessage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownAutoHideOffset = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDefaultTasks = new System.Windows.Forms.TextBox();
            this.checkBoxExperimental = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxOpenBATTOnTrayMenuClick = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyOnTrayMenuClick = new System.Windows.Forms.CheckBox();
            this.checkBoxDebugMode = new System.Windows.Forms.CheckBox();
            this.checkBoxUseBATTTime = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveOnStop = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoHideOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arbeitsdauer";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(84, 12);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(170, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(98, 350);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(179, 350);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stundenanzeige";
            // 
            // radioButtonHoursDecimal
            // 
            this.radioButtonHoursDecimal.AutoSize = true;
            this.radioButtonHoursDecimal.Checked = true;
            this.radioButtonHoursDecimal.Location = new System.Drawing.Point(102, 39);
            this.radioButtonHoursDecimal.Name = "radioButtonHoursDecimal";
            this.radioButtonHoursDecimal.Size = new System.Drawing.Size(40, 17);
            this.radioButtonHoursDecimal.TabIndex = 3;
            this.radioButtonHoursDecimal.TabStop = true;
            this.radioButtonHoursDecimal.Text = "5,3";
            this.radioButtonHoursDecimal.UseVisualStyleBackColor = true;
            // 
            // radioButtonHoursHrMin
            // 
            this.radioButtonHoursHrMin.AutoSize = true;
            this.radioButtonHoursHrMin.Location = new System.Drawing.Point(148, 39);
            this.radioButtonHoursHrMin.Name = "radioButtonHoursHrMin";
            this.radioButtonHoursHrMin.Size = new System.Drawing.Size(46, 17);
            this.radioButtonHoursHrMin.TabIndex = 4;
            this.radioButtonHoursHrMin.Text = "5:18";
            this.radioButtonHoursHrMin.UseVisualStyleBackColor = true;
            // 
            // checkBoxQuittingMessage
            // 
            this.checkBoxQuittingMessage.AutoSize = true;
            this.checkBoxQuittingMessage.Location = new System.Drawing.Point(15, 109);
            this.checkBoxQuittingMessage.Name = "checkBoxQuittingMessage";
            this.checkBoxQuittingMessage.Size = new System.Drawing.Size(123, 17);
            this.checkBoxQuittingMessage.TabIndex = 8;
            this.checkBoxQuittingMessage.Text = "Feierabend-Meldung";
            this.checkBoxQuittingMessage.UseVisualStyleBackColor = true;
            this.checkBoxQuittingMessage.CheckedChanged += new System.EventHandler(this.checkBoxQuittingMessage_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Abstand von Bildschirmrand";
            // 
            // numericUpDownOffsetRight
            // 
            this.numericUpDownOffsetRight.Location = new System.Drawing.Point(158, 160);
            this.numericUpDownOffsetRight.Name = "numericUpDownOffsetRight";
            this.numericUpDownOffsetRight.Size = new System.Drawing.Size(96, 20);
            this.numericUpDownOffsetRight.TabIndex = 12;
            // 
            // textBoxQuittingMessage
            // 
            this.textBoxQuittingMessage.Enabled = false;
            this.textBoxQuittingMessage.Location = new System.Drawing.Point(15, 132);
            this.textBoxQuittingMessage.Name = "textBoxQuittingMessage";
            this.textBoxQuittingMessage.Size = new System.Drawing.Size(185, 20);
            this.textBoxQuittingMessage.TabIndex = 9;
            // 
            // buttonTestQuittingMessage
            // 
            this.buttonTestQuittingMessage.Location = new System.Drawing.Point(206, 130);
            this.buttonTestQuittingMessage.Name = "buttonTestQuittingMessage";
            this.buttonTestQuittingMessage.Size = new System.Drawing.Size(48, 23);
            this.buttonTestQuittingMessage.TabIndex = 10;
            this.buttonTestQuittingMessage.Text = "Test";
            this.buttonTestQuittingMessage.UseVisualStyleBackColor = true;
            this.buttonTestQuittingMessage.Click += new System.EventHandler(this.buttonTestQuittingMessage_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Abstand für aut. ausblenden";
            // 
            // numericUpDownAutoHideOffset
            // 
            this.numericUpDownAutoHideOffset.Location = new System.Drawing.Point(158, 191);
            this.numericUpDownAutoHideOffset.Name = "numericUpDownAutoHideOffset";
            this.numericUpDownAutoHideOffset.Size = new System.Drawing.Size(96, 20);
            this.numericUpDownAutoHideOffset.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Standard-Tasks";
            // 
            // textBoxDefaultTasks
            // 
            this.textBoxDefaultTasks.Location = new System.Drawing.Point(15, 79);
            this.textBoxDefaultTasks.Name = "textBoxDefaultTasks";
            this.textBoxDefaultTasks.Size = new System.Drawing.Size(237, 20);
            this.textBoxDefaultTasks.TabIndex = 7;
            // 
            // checkBoxExperimental
            // 
            this.checkBoxExperimental.AutoSize = true;
            this.checkBoxExperimental.Location = new System.Drawing.Point(15, 300);
            this.checkBoxExperimental.Name = "checkBoxExperimental";
            this.checkBoxExperimental.Size = new System.Drawing.Size(246, 17);
            this.checkBoxExperimental.TabIndex = 17;
            this.checkBoxExperimental.Text = "Ich habe keine Angst vor Bugs und Abstürzen!";
            this.checkBoxExperimental.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOpenBATTOnTrayMenuClick);
            this.groupBox1.Controls.Add(this.checkBoxCopyOnTrayMenuClick);
            this.groupBox1.Location = new System.Drawing.Point(15, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 51);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bei Rechtsklick auf Thema in Popup-Menü";
            // 
            // checkBoxOpenBATTOnTrayMenuClick
            // 
            this.checkBoxOpenBATTOnTrayMenuClick.AutoSize = true;
            this.checkBoxOpenBATTOnTrayMenuClick.Location = new System.Drawing.Point(116, 21);
            this.checkBoxOpenBATTOnTrayMenuClick.Name = "checkBoxOpenBATTOnTrayMenuClick";
            this.checkBoxOpenBATTOnTrayMenuClick.Size = new System.Drawing.Size(99, 17);
            this.checkBoxOpenBATTOnTrayMenuClick.TabIndex = 1;
            this.checkBoxOpenBATTOnTrayMenuClick.Text = "In BATT öffnen";
            this.checkBoxOpenBATTOnTrayMenuClick.UseVisualStyleBackColor = true;
            // 
            // checkBoxCopyOnTrayMenuClick
            // 
            this.checkBoxCopyOnTrayMenuClick.AutoSize = true;
            this.checkBoxCopyOnTrayMenuClick.Location = new System.Drawing.Point(19, 21);
            this.checkBoxCopyOnTrayMenuClick.Name = "checkBoxCopyOnTrayMenuClick";
            this.checkBoxCopyOnTrayMenuClick.Size = new System.Drawing.Size(68, 17);
            this.checkBoxCopyOnTrayMenuClick.TabIndex = 0;
            this.checkBoxCopyOnTrayMenuClick.Text = "Kopieren";
            this.checkBoxCopyOnTrayMenuClick.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebugMode
            // 
            this.checkBoxDebugMode.AutoSize = true;
            this.checkBoxDebugMode.Location = new System.Drawing.Point(15, 323);
            this.checkBoxDebugMode.Name = "checkBoxDebugMode";
            this.checkBoxDebugMode.Size = new System.Drawing.Size(142, 17);
            this.checkBoxDebugMode.TabIndex = 18;
            this.checkBoxDebugMode.Text = "Debug Modus aktivieren";
            this.checkBoxDebugMode.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseBATTTime
            // 
            this.checkBoxUseBATTTime.AutoSize = true;
            this.checkBoxUseBATTTime.Location = new System.Drawing.Point(200, 40);
            this.checkBoxUseBATTTime.Name = "checkBoxUseBATTTime";
            this.checkBoxUseBATTTime.Size = new System.Drawing.Size(54, 17);
            this.checkBoxUseBATTTime.TabIndex = 5;
            this.checkBoxUseBATTTime.Text = "BATT";
            this.checkBoxUseBATTTime.UseVisualStyleBackColor = true;
            this.checkBoxUseBATTTime.CheckedChanged += new System.EventHandler(this.checkBoxUseBATTTime_CheckedChanged);
            // 
            // checkBoxSaveOnStop
            // 
            this.checkBoxSaveOnStop.AutoSize = true;
            this.checkBoxSaveOnStop.Location = new System.Drawing.Point(15, 277);
            this.checkBoxSaveOnStop.Name = "checkBoxSaveOnStop";
            this.checkBoxSaveOnStop.Size = new System.Drawing.Size(243, 17);
            this.checkBoxSaveOnStop.TabIndex = 16;
            this.checkBoxSaveOnStop.Text = "Auswertung bei \"Stop\" automatisch speichern";
            this.checkBoxSaveOnStop.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 383);
            this.Controls.Add(this.checkBoxSaveOnStop);
            this.Controls.Add(this.checkBoxUseBATTTime);
            this.Controls.Add(this.checkBoxDebugMode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxExperimental);
            this.Controls.Add(this.textBoxDefaultTasks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownAutoHideOffset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonTestQuittingMessage);
            this.Controls.Add(this.textBoxQuittingMessage);
            this.Controls.Add(this.numericUpDownOffsetRight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxQuittingMessage);
            this.Controls.Add(this.radioButtonHoursHrMin);
            this.Controls.Add(this.radioButtonHoursDecimal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Konfiguration";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConfigForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffsetRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoHideOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonHoursDecimal;
        private System.Windows.Forms.RadioButton radioButtonHoursHrMin;
        private System.Windows.Forms.CheckBox checkBoxQuittingMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownOffsetRight;
        private System.Windows.Forms.TextBox textBoxQuittingMessage;
        private System.Windows.Forms.Button buttonTestQuittingMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoHideOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDefaultTasks;
        private System.Windows.Forms.CheckBox checkBoxExperimental;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxOpenBATTOnTrayMenuClick;
        private System.Windows.Forms.CheckBox checkBoxCopyOnTrayMenuClick;
        private System.Windows.Forms.CheckBox checkBoxDebugMode;
        private System.Windows.Forms.CheckBox checkBoxUseBATTTime;
        private System.Windows.Forms.CheckBox checkBoxSaveOnStop;
    }
}