namespace C969___Scheduler
{
    partial class AddUser
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
            this.txtNewUsername = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnCancelNewUser = new System.Windows.Forms.Button();
            this.lblUsernameVal = new System.Windows.Forms.Label();
            this.lblPasswordVal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNewUsername
            // 
            this.txtNewUsername.Location = new System.Drawing.Point(126, 51);
            this.txtNewUsername.Name = "txtNewUsername";
            this.txtNewUsername.Size = new System.Drawing.Size(133, 20);
            this.txtNewUsername.TabIndex = 0;
            this.txtNewUsername.Leave += new System.EventHandler(this.txtNewUsername_Leave);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(126, 102);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(133, 20);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.Leave += new System.EventHandler(this.txtNewPassword_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(216, 159);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(75, 23);
            this.btnCreateUser.TabIndex = 4;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // btnCancelNewUser
            // 
            this.btnCancelNewUser.Location = new System.Drawing.Point(76, 159);
            this.btnCancelNewUser.Name = "btnCancelNewUser";
            this.btnCancelNewUser.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewUser.TabIndex = 5;
            this.btnCancelNewUser.Text = "Cancel";
            this.btnCancelNewUser.UseVisualStyleBackColor = true;
            this.btnCancelNewUser.Click += new System.EventHandler(this.btnCancelNewUser_Click);
            // 
            // lblUsernameVal
            // 
            this.lblUsernameVal.AutoSize = true;
            this.lblUsernameVal.Enabled = false;
            this.lblUsernameVal.ForeColor = System.Drawing.Color.Red;
            this.lblUsernameVal.Location = new System.Drawing.Point(123, 74);
            this.lblUsernameVal.Name = "lblUsernameVal";
            this.lblUsernameVal.Size = new System.Drawing.Size(197, 13);
            this.lblUsernameVal.TabIndex = 6;
            this.lblUsernameVal.Text = "Username must be at least 5 characters.";
            // 
            // lblPasswordVal
            // 
            this.lblPasswordVal.AutoSize = true;
            this.lblPasswordVal.Enabled = false;
            this.lblPasswordVal.ForeColor = System.Drawing.Color.Red;
            this.lblPasswordVal.Location = new System.Drawing.Point(123, 125);
            this.lblPasswordVal.Name = "lblPasswordVal";
            this.lblPasswordVal.Size = new System.Drawing.Size(195, 13);
            this.lblPasswordVal.TabIndex = 7;
            this.lblPasswordVal.Text = "Password must be at least 5 characters.";
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 217);
            this.Controls.Add(this.lblPasswordVal);
            this.Controls.Add(this.lblUsernameVal);
            this.Controls.Add(this.btnCancelNewUser);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtNewUsername);
            this.Name = "AddUser";
            this.Text = "Add New User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewUsername;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnCancelNewUser;
        private System.Windows.Forms.Label lblUsernameVal;
        private System.Windows.Forms.Label lblPasswordVal;
    }
}