using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C969___Scheduler.Entity_Classes;

namespace C969___Scheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // sets language of login form to spanish if detected
            MessageBox.Show($"Current culture is {CultureInfo.CurrentCulture.Name}");
            if(CultureInfo.CurrentCulture.Name == "es-MX")
            {
                lblUsername.Text = "Usario:";
                lblPassword.Text = "Contraseña:";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //creates new user object based on input in text fields 
            User user = new User(txtUsername.Text, txtPassword.Text);

            try
            {
                //check user & password for a match in database 
            }
            catch
            {
                //display error message if login is invalid, DEPENDENT UPON LANGUAGE
            }

        }
    }
}
