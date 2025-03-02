namespace C969___Scheduler
{
    partial class ReportForm
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
            this.reportGroupBoxLabel = new System.Windows.Forms.GroupBox();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.btnAppointmentType = new System.Windows.Forms.Button();
            this.btnCustomerAppts = new System.Windows.Forms.Button();
            this.cbMonths = new System.Windows.Forms.ComboBox();
            this.cbCustomers = new System.Windows.Forms.ComboBox();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.btnUserScheduleReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.reportGroupBoxLabel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportGroupBoxLabel
            // 
            this.reportGroupBoxLabel.Controls.Add(this.txtReport);
            this.reportGroupBoxLabel.Location = new System.Drawing.Point(234, 50);
            this.reportGroupBoxLabel.Name = "reportGroupBoxLabel";
            this.reportGroupBoxLabel.Size = new System.Drawing.Size(435, 251);
            this.reportGroupBoxLabel.TabIndex = 0;
            this.reportGroupBoxLabel.TabStop = false;
            this.reportGroupBoxLabel.Text = "Reports";
            // 
            // txtReport
            // 
            this.txtReport.Location = new System.Drawing.Point(6, 25);
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.Size = new System.Drawing.Size(419, 200);
            this.txtReport.TabIndex = 0;
            // 
            // btnAppointmentType
            // 
            this.btnAppointmentType.Location = new System.Drawing.Point(133, 19);
            this.btnAppointmentType.Name = "btnAppointmentType";
            this.btnAppointmentType.Size = new System.Drawing.Size(65, 21);
            this.btnAppointmentType.TabIndex = 2;
            this.btnAppointmentType.Text = "Run";
            this.btnAppointmentType.UseVisualStyleBackColor = true;
            this.btnAppointmentType.Click += new System.EventHandler(this.btnAppointmentType_Click);
            // 
            // btnCustomerAppts
            // 
            this.btnCustomerAppts.Location = new System.Drawing.Point(133, 18);
            this.btnCustomerAppts.Name = "btnCustomerAppts";
            this.btnCustomerAppts.Size = new System.Drawing.Size(65, 21);
            this.btnCustomerAppts.TabIndex = 3;
            this.btnCustomerAppts.Text = "Run";
            this.btnCustomerAppts.UseVisualStyleBackColor = true;
            this.btnCustomerAppts.Click += new System.EventHandler(this.btnCustomerAppts_Click);
            // 
            // cbMonths
            // 
            this.cbMonths.FormattingEnabled = true;
            this.cbMonths.Location = new System.Drawing.Point(6, 19);
            this.cbMonths.Name = "cbMonths";
            this.cbMonths.Size = new System.Drawing.Size(122, 21);
            this.cbMonths.TabIndex = 4;
            // 
            // cbCustomers
            // 
            this.cbCustomers.FormattingEnabled = true;
            this.cbCustomers.Location = new System.Drawing.Point(5, 19);
            this.cbCustomers.Name = "cbCustomers";
            this.cbCustomers.Size = new System.Drawing.Size(122, 21);
            this.cbCustomers.TabIndex = 6;
            // 
            // cbUsers
            // 
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(6, 19);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(121, 21);
            this.cbUsers.TabIndex = 5;
            // 
            // btnUserScheduleReport
            // 
            this.btnUserScheduleReport.Location = new System.Drawing.Point(133, 19);
            this.btnUserScheduleReport.Name = "btnUserScheduleReport";
            this.btnUserScheduleReport.Size = new System.Drawing.Size(65, 21);
            this.btnUserScheduleReport.TabIndex = 1;
            this.btnUserScheduleReport.Text = "Run";
            this.btnUserScheduleReport.UseVisualStyleBackColor = true;
            this.btnUserScheduleReport.Click += new System.EventHandler(this.btnUserScheduleReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUserScheduleReport);
            this.groupBox1.Controls.Add(this.cbUsers);
            this.groupBox1.Location = new System.Drawing.Point(26, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 53);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Schedule Report";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAppointmentType);
            this.groupBox2.Controls.Add(this.cbMonths);
            this.groupBox2.Location = new System.Drawing.Point(26, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monthly Appointments Report";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCustomers);
            this.groupBox3.Controls.Add(this.btnCustomerAppts);
            this.groupBox3.Location = new System.Drawing.Point(26, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 53);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Customer Appointments Report";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 317);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reportGroupBoxLabel);
            this.Name = "ReportForm";
            this.Text = "Reports";
            this.reportGroupBoxLabel.ResumeLayout(false);
            this.reportGroupBoxLabel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox reportGroupBoxLabel;
        private System.Windows.Forms.Button btnAppointmentType;
        private System.Windows.Forms.Button btnCustomerAppts;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.ComboBox cbCustomers;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.Button btnUserScheduleReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox cbMonths;
    }
}