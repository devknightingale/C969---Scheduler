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
            MessageBox.Show($"Logged in user is currently {Helper.userNameValue}");

            
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
            if (!dgvAppointments.CurrentRow.Selected)
            {
                MessageBox.Show("Please select an appointment to delete."); 
            }
            else
            {
                // this is not grabbing an appointment object. object is null 
                // what did i do wrong here?
                // find way to grab the int of cell 0 from the data grid 
                Appointment apptToDelete = dgvAppointments.CurrentRow.DataBoundItem as Appointment;
                Helper.deleteAppointment(apptToDelete.appointmentId);

            }
        }

        private void dgvAppointments_SelectionChanged(object sender, EventArgs e)
        {
            Appointment checkAppt = dgvAppointments.CurrentRow.DataBoundItem as Appointment; 

            MessageBox.Show($"Current selection id is {checkAppt.appointmentId}");
        }
    }
}
