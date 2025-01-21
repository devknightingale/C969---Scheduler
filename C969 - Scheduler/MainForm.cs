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
        public void LoadAppointmentGrid()
        {
            try
            {
                string apptQuery = $"SELECT appointmentId, customerId, title, start FROM appointment";

                MySqlCommand apptCmd = new MySqlCommand(apptQuery, DBConnection.conn);
                MySqlDataAdapter appAdapter = new MySqlDataAdapter(apptCmd);
                DataTable apptTable = new DataTable();
                appAdapter.Fill(apptTable);

                BindingSource apptBindingSource = new BindingSource();
                apptBindingSource.DataSource = apptTable;
                dgvAppointments.DataSource = apptBindingSource;

                dgvAppointments.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm tt";
            }
            catch
            {
                MessageBox.Show("ERROR WITH THE DATAGRID");
            }
        }
        public void LoadCustomerGrid()
        {
            try
            {
                string customerQuery = $"SELECT customer.customerId, customer.customerName, address.address, address.phone FROM customer LEFT JOIN address ON customer.addressId = address.addressId ORDER BY customer.customerName";

                MySqlCommand customerCmd = new MySqlCommand(customerQuery, DBConnection.conn);
                MySqlDataAdapter customerAdapter = new MySqlDataAdapter(customerCmd);
                DataTable customerTable = new DataTable();
                customerAdapter.Fill(customerTable);

                BindingSource customerBindingSource = new BindingSource();
                customerBindingSource.DataSource = customerTable;
                dgvAppointments.DataSource = customerBindingSource;


            }
            catch
            {
                MessageBox.Show("Failed to load customers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public MainForm(User activeUser)
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
            LoadAppointmentGrid();

            //testing that user log in works 
            lblUsername.Text = activeUser.username; 
            MessageBox.Show($"Logged in user is currently {activeUser.username}");
        }

        string activeName; 
        public void RetrieveData(Object objPassedFromParent)
        {
            objPassedFromParent = objPassedFromParent.ToString();
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
                LoadAppointmentGrid(); 
            }
            else
            {
                LoadCustomerGrid(); 
            }
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddAppointment addAppt = new AddAppointment();
            addAppt.Show(); 
        }
    }
}
