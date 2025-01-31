using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using C969___Scheduler.Supplementary_Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler
{
    public partial class MainForm : Form
    {
       
        public MainForm()
        {
            InitializeComponent();
            List<string> comboBoxItems = new List<string>();
            comboBoxItems.Add("Appointments");
            comboBoxItems.Add("Customers");

            comboBox1.DataSource = comboBoxItems;
            comboBox1.SelectedIndex = 0;

            //maximizes main form
            //WindowState = FormWindowState.Maximized;
            

            // this loads the default appointment view. 
            Helper.LoadAppointmentGrid(dgvAppointments);

            //testing that user log in works 
            lblUsername.Text = Helper.userNameValue; 
            // MessageBox.Show($"Logged in user is currently {Helper.userNameValue}");

            
        }
       
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ensures the entire application is closed when main form is closed
            Application.Exit(); 
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // changes the grid from customers to appointments and vice versa based on dropdown selection
            if (comboBox1.SelectedIndex == 0)
            {
                Helper.LoadAppointmentGrid(dgvAppointments); 
            }
            else
            {
                Helper.LoadCustomerGrid(dgvAppointments); 
            }
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            
            AddAppointment addAppt = new AddAppointment(dgvAppointments);
            addAppt.Show();
            
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            Helper.LoadAppointmentGrid(dgvAppointments);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // technically it should not be possible to not have a row selected since the application starts with a row selected automatically, but just in case... 
            if (!dgvAppointments.CurrentRow.Selected)
            {
                MessageBox.Show("Please select an appointment to delete."); 
            }
            else
            {
                
                int apptId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                // confirmation window 
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected appointment? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo); 

                if (result == DialogResult.Yes)
                {                   
                    Helper.deleteAppointment(apptId);

                    // refreshes data grid
                    Helper.LoadAppointmentGrid(dgvAppointments);
                }
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!dgvAppointments.CurrentRow.Selected)
            {
                MessageBox.Show("Please select an appointment to modify.");
            }
            else
            {

                int apptId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                // pull up add appointment form here but need to fill text boxes first? 
                AddAppointment addAppt = new AddAppointment(dgvAppointments);
                //need to create an addAppt form with a constructor taking an Appointment as an argument in order to prefill the textboxes 
                addAppt.Show();
            }
        }
    }
}
