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
using System.Data.SqlClient;
using C969___Scheduler.Database;



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
            StartPosition = FormStartPosition.CenterScreen; //centers form
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //creates new user object based on input in text fields 
            User user = new User(txtUsername.Text, txtPassword.Text);

            try
            {
                //check user & password for a match in database 
                string query = $"SELECT * FROM user WHERE userName = '{txtUsername.Text}' AND password = '{txtPassword.Text}'";
                
                // MySqlConnection conn = DBConnection.conn;
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                adapter.Fill(dtable); 

                if(dtable.Rows.Count > 0)
                {
                    MessageBox.Show("match found"); 
                    
                    // load next form here
                    MainForm mainForm = new MainForm();

                    //will close entire application when main form is closed
                    mainForm.FormClosed += (s, args) => this.Close(); 
                    mainForm.Show();
                    this.Hide(); // hides the login form so its not visible when main form is open 
                    
                }
                else
                {
                    MessageBox.Show("Invalid Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }

            }
            catch
            {
                //display error message if login is invalid, DEPENDENT UPON LANGUAGE
            
                MessageBox.Show("Error");
            }

        }
    }
}
