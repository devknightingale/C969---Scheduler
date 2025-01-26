using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace C969___Scheduler.Supplementary_Forms
{
    public partial class AddAppointment : Form
    {
        public AddAppointment()
        {
            InitializeComponent();

            /*************************/
            /***** FORM CONTROLS *****/
            /*************************/


            // CUSTOMER COMBO BOX 
            string queryCustomers = "SELECT customerName FROM customer ORDER BY customerName ASC";
            MySqlCommand cmdCustomers = new MySqlCommand(queryCustomers, DBConnection.conn);
            MySqlDataReader reader = cmdCustomers.ExecuteReader();

            while (reader.Read())
            {
                cbCustomerList.Items.Add(reader["customerName"].ToString());
                
            }
            reader.Close();
            cbCustomerList.SelectedIndex = 0; 



            // TIME PICKER 
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.ShowUpDown = true;


            // APPOINTMENT TYPE COMBO BOX 
            List<string> listApptType = new List<string>()
            {
                "Consultation", "Presentation", "Scrum"
            };

            cbApptType.DataSource = listApptType;
            cbApptType.SelectedIndex = 0;


            // APPOINTMENT CONSULTANT COMBO BOX 
            // Should change this to a list of User objects so i can access the user Id
            // but to do that will need sql command right? 
            List<string> listUser = new List<string>
            {
                "test", "admin"
            };

            cbApptUser.DataSource = listUser;
            cbApptUser.SelectedIndex = 0;


            // APPOINTMENT LOCATION COMBO BOX 
            List<string> listLocation = new List<string>
            {
                "In Office", "Virtual", "Phone"
            };

            cbApptLocation.DataSource = listLocation;
            cbApptLocation.SelectedIndex = 0;

            // CUSTOMER COMBO BOX 

            // will need to create a combo box via using sql to build the list of customer objects from the database 


            /*************************/
            /*** END FORM CONTROLS ***/
            /*************************/


            /*************************/
            /*****   VARIABLES   *****/
            /*************************/

            // NOTE: THIS NEEDS TO BE CONVERTED TO UTC TIME 
            // DATETIMEPICKER NEEDS TO BE DISPLAYED IN LOCAL TIME THOUGH? NEEDS TESTING


            

            string apptTitle = txtTitle.Text;
            string apptDescription = txtDescription.Text;
            string apptLocation = cbApptUser.SelectedIndex.ToString(); 
            string apptType = cbApptType.SelectedIndex.ToString();
            string startTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.ToString("hh:mm tt");
            DateTime startTime = DateTime.Parse(startTimeString).ToUniversalTime();
            DateTime endTime = timePicker.Value.AddMinutes(30).ToUniversalTime();
            DateTime createDate = DateTime.Now.ToUniversalTime();
            string createdBy = Helper.userNameValue;
            string lastUpdateBy = Helper.userNameValue; 
            
            // for the created by etc things, you need to pass the logged in user to the addappointment form 
            // cant figure out how to pass to add form - it cant be accessed in the button click that opens the add form? 

            /*************************/
            /****  END VARIABLES  ****/
            /*************************/



            // EXAMPLE SQL COMMAND FOR ADDING APPOINTMENT: 
            // NOTE: REMEMBER TIMES NEED TO BE CONVERTED TO UTC FOR THE DATABASE 

            // INSERT INTO appointment(customerId, userId, title, description, location, contact, url, type, start, end, createDate, createdBy, lastUpdateBy)
            // VALUES(1, 1, "test appt", "testing appointment add", "Cincinnati", "Contact", "N/A", "Consultation", CAST("2025-01-20 12:00:00" as DATETIME), CAST("2025-01-20 12:15:00" as DATETIME), NOW(), "test", "test")

            // THOUGHTS: 
            // Use one form "ADDAPPOINTMENT" for both adding and updating appointment. 
            // Perhaps two constructors? One that does not take an argument for the add form, one that takes appointment id for update 



        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {


            Appointment appt = new Appointment();


            // GET CUSTOMER ID
            string queryCustomerId = $"SELECT customerId FROM customer WHERE customerName = '{cbCustomerList.SelectedItem.ToString()}'";
            MySqlCommand customerIdCmd = new MySqlCommand(queryCustomerId, DBConnection.conn);
            appt.customerId = (int)customerIdCmd.ExecuteScalar();




            // GET USER ID FROM LOGGED IN USER 
            string query = $"SELECT userId FROM user WHERE userName = '{Helper.userNameValue}'";
            MySqlCommand userIdCmd = new MySqlCommand(query, DBConnection.conn);
            appt.userId = (int)userIdCmd.ExecuteScalar();


            // ALL OTHER APPOINTMENT INFO 

            appt.title = txtTitle.Text;
            appt.description = txtDescription.Text;
            appt.location = cbApptLocation.SelectedItem.ToString();
            appt.contact = "not needed"; 
            appt.type = cbApptType.SelectedItem.ToString();
            appt.url = "www.example.com"; 
            string startTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.ToString("hh:mm tt");
            appt.start = DateTime.Parse(startTimeString).ToUniversalTime();
            appt.end = timePicker.Value.AddMinutes(30).ToUniversalTime();
            
            // these arent in the class definition so they can be set this way instead
            DateTime createDate = DateTime.Now.ToUniversalTime();
            string createdBy = Helper.userNameValue;
            string lastUpdateBy = Helper.userNameValue;

            Helper.addAppointment(appt); 

            // Seems like the controls display in local time but can be converted to UTC for storing in the database 
        }

        private void cbCustomerList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MessageBox.Show($"Selected item is {cbCustomerList.SelectedItem.ToString()}");
            
        }

        private void cbApptLocation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MessageBox.Show($"Selected item is {cbApptLocation.SelectedItem.ToString()}");
        }
    }
}
