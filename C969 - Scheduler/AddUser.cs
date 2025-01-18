using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
            btnCreateUser.Enabled = false;

            lblUsernameVal.Hide();
            lblPasswordVal.Hide(); 
        }

        private void btnCancelNewUser_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        
      

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
         
            
        }

        // have to do this on both textboxes
        // so that the create button will enable either way once it passes
        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if(txtNewPassword.Text.Length >= 5 && txtNewUsername.Text.Length >= 5)
            {
                btnCreateUser.Enabled = true;
                lblPasswordVal.Hide();
                lblUsernameVal.Hide();
            }
            else if (txtNewPassword.Text.Length >= 5)
            {
                lblPasswordVal.Hide();
            }
            else
            if (txtNewPassword.Text.Length < 5)
            {
                lblPasswordVal.Show();
                lblPasswordVal.Enabled = true;
            }
        }

        private void txtNewUsername_Leave(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Length >= 5 && txtNewUsername.Text.Length >= 5)
            {
                btnCreateUser.Enabled = true;
                lblPasswordVal.Hide();
                lblUsernameVal.Hide();
            }
            else if (txtNewUsername.Text.Length >= 5)
            {
                lblUsernameVal.Hide();
            }
            else
            {
                if (txtNewUsername.Text.Length < 5)
                {
                    lblUsernameVal.Show();
                    lblUsernameVal.Enabled = true; 
                }
                
            }
        }
    }
}
