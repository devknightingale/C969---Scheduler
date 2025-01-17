using C969___Scheduler.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();

            


            // in order to grab the appointments for the default dgv view, 
            // maybe use a sql query on initiation to grab the appointments, then fill the dgv with it?
            try
            {
                string userQuery = $"SELECT userName, createDate, lastUpdate FROM user";

                MySqlCommand userCmd = new MySqlCommand(userQuery, DBConnection.conn);
                MySqlDataAdapter userAdapter = new MySqlDataAdapter(userCmd);
                DataTable userTable = new DataTable();
                userAdapter.Fill(userTable);

                BindingSource userBindingSource = new BindingSource();
                userBindingSource.DataSource = userTable;
                dgvUsers.DataSource = userBindingSource;

                
            }
            catch
            {
                MessageBox.Show("ERROR WITH THE DATAGRID");
            }

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form AddUserForm = new AddUser();
            AddUserForm.Show(); 
        }
    }
}
