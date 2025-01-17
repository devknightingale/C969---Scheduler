using System.Runtime.CompilerServices;

namespace C969___Scheduler
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appointmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.evaluatorShortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTestUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valerieVolkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.josephFeltonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.viewToolStripMenuItem,
            this.evaluatorShortcutsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.customerRecordsToolStripMenuItem,
            this.appointmentsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.usersToolStripMenuItem.Text = "Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // customerRecordsToolStripMenuItem
            // 
            this.customerRecordsToolStripMenuItem.Name = "customerRecordsToolStripMenuItem";
            this.customerRecordsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.customerRecordsToolStripMenuItem.Text = "Customers";
            // 
            // appointmentsToolStripMenuItem
            // 
            this.appointmentsToolStripMenuItem.Name = "appointmentsToolStripMenuItem";
            this.appointmentsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.appointmentsToolStripMenuItem.Text = "Appointments";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvAppointments.Location = new System.Drawing.Point(175, 24);
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(625, 426);
            this.dgvAppointments.TabIndex = 1;
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(449, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // evaluatorShortcutsToolStripMenuItem
            // 
            this.evaluatorShortcutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTestUsersToolStripMenuItem});
            this.evaluatorShortcutsToolStripMenuItem.Name = "evaluatorShortcutsToolStripMenuItem";
            this.evaluatorShortcutsToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.evaluatorShortcutsToolStripMenuItem.Text = "Evaluator Shortcuts";
            // 
            // addTestUsersToolStripMenuItem
            // 
            this.addTestUsersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.valerieVolkToolStripMenuItem,
            this.josephFeltonToolStripMenuItem});
            this.addTestUsersToolStripMenuItem.Name = "addTestUsersToolStripMenuItem";
            this.addTestUsersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addTestUsersToolStripMenuItem.Text = "Add Test Users";
            // 
            // valerieVolkToolStripMenuItem
            // 
            this.valerieVolkToolStripMenuItem.Name = "valerieVolkToolStripMenuItem";
            this.valerieVolkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.valerieVolkToolStripMenuItem.Text = "Valerie Volk";
            // 
            // josephFeltonToolStripMenuItem
            // 
            this.josephFeltonToolStripMenuItem.Name = "josephFeltonToolStripMenuItem";
            this.josephFeltonToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.josephFeltonToolStripMenuItem.Text = "Joseph Felton";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerRecordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appointmentsToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem evaluatorShortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTestUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valerieVolkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem josephFeltonToolStripMenuItem;
    }
}