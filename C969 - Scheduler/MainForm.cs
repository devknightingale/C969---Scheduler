using C969___Scheduler.Database;
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
        public MainForm()
        {
            InitializeComponent();
            List<string> comboBoxItems = new List<string>();
            comboBoxItems.Add("Appointments");
            comboBoxItems.Add("Customers");

            comboBox1.DataSource = comboBoxItems;
            comboBox1.SelectedIndex = 0;

            

            //maximizes main form
            WindowState = FormWindowState.Maximized;


            // this loads the default appointment view. the handling for the customer/appointment grid switch is below
            // there has to be a way to put these into methods and just call them... 
            LoadAppointmentGrid(); 
            

            // FOR THE COMBO BOX FUNCTIONALITY: 
            // Want to make it so that "Appointments" is the default value, and thus default data grid shown 
            // but when "Customers" is selected, the data grid changes to the customer dgv view. 
            // Buttons such as Add/Update/Delete will have to be coded such that it checks which type of 
            // item it is grabbing from the data grid before opening the Add/Update/Delete forms. 
            // But this will keep me from having to make like 17 forms 
            // Should I do GUI first or start the coding for Appointments Add/Update/Delete functionality?


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                LoadAppointmentGrid(); 
            }
            else
            {
                LoadCustomerGrid(); 
            }
            
        }
    }
}
