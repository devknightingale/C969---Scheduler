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
            this.btnMonthlyApptsReport = new System.Windows.Forms.Button();
            this.btnAppointmentType = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.reportGroupBoxLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportGroupBoxLabel
            // 
            this.reportGroupBoxLabel.Controls.Add(this.txtReport);
            this.reportGroupBoxLabel.Location = new System.Drawing.Point(224, 29);
            this.reportGroupBoxLabel.Name = "reportGroupBoxLabel";
            this.reportGroupBoxLabel.Size = new System.Drawing.Size(459, 251);
            this.reportGroupBoxLabel.TabIndex = 0;
            this.reportGroupBoxLabel.TabStop = false;
            this.reportGroupBoxLabel.Text = "Reports";
            // 
            // btnMonthlyApptsReport
            // 
            this.btnMonthlyApptsReport.Location = new System.Drawing.Point(26, 43);
            this.btnMonthlyApptsReport.Name = "btnMonthlyApptsReport";
            this.btnMonthlyApptsReport.Size = new System.Drawing.Size(92, 40);
            this.btnMonthlyApptsReport.TabIndex = 1;
            this.btnMonthlyApptsReport.Text = "User Appointments";
            this.btnMonthlyApptsReport.UseVisualStyleBackColor = true;
            this.btnMonthlyApptsReport.Click += new System.EventHandler(this.btnMonthlyApptsReport_Click);
            // 
            // btnAppointmentType
            // 
            this.btnAppointmentType.Location = new System.Drawing.Point(26, 96);
            this.btnAppointmentType.Name = "btnAppointmentType";
            this.btnAppointmentType.Size = new System.Drawing.Size(92, 40);
            this.btnAppointmentType.TabIndex = 2;
            this.btnAppointmentType.Text = "Appointment Type";
            this.btnAppointmentType.UseVisualStyleBackColor = true;
            this.btnAppointmentType.Click += new System.EventHandler(this.btnAppointmentType_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 150);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 40);
            this.button3.TabIndex = 3;
            this.button3.Text = "Pay Period";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // txtReport
            // 
            this.txtReport.Location = new System.Drawing.Point(21, 34);
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.Size = new System.Drawing.Size(419, 200);
            this.txtReport.TabIndex = 0;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAppointmentType);
            this.Controls.Add(this.btnMonthlyApptsReport);
            this.Controls.Add(this.reportGroupBoxLabel);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.reportGroupBoxLabel.ResumeLayout(false);
            this.reportGroupBoxLabel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox reportGroupBoxLabel;
        private System.Windows.Forms.Button btnMonthlyApptsReport;
        private System.Windows.Forms.Button btnAppointmentType;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtReport;
    }
}