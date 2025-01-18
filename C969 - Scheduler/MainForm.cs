using C969___Scheduler.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
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
            WindowState = FormWindowState.Maximized;
            

            // in order to grab the appointments for the default dgv view, 
            // maybe use a sql query on initiation to grab the appointments, then fill the dgv with it?
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
            

            // COMBO BOX 
            // Drop down list of users in the application, starts on the default value of "ALL" 
            // need to create a binding source of users - will this work for the evaluator?


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

       

    }
}
