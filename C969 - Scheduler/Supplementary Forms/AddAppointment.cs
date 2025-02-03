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
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace C969___Scheduler.Supplementary_Forms
{
    public partial class AddAppointment : Form
    {
        public AddAppointment(DataGridView dgv)
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
            //cbCustomerList.SelectedIndex = 0; 



            // TIME PICKER 
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.ShowUpDown = true;


            // APPOINTMENT TYPE COMBO BOX 
            //List<string> listApptType = new List<string>()
            //{
                //"Consultation", "Presentation", "Scrum"
            //};

            //cbApptType.DataSource = listApptType;
            //cbApptType.SelectedIndex = 0;

            string queryApptType = "SELECT type FROM appointment ORDER BY type ASC";
            MySqlCommand cmdApptType = new MySqlCommand(queryApptType, DBConnection.conn);
            MySqlDataReader readerApptType = cmdApptType.ExecuteReader();

            while (readerApptType.Read())
            {
                cbApptType.Items.Add(readerApptType["type"].ToString());

            }
            readerApptType.Close();


            // APPOINTMENT CONSULTANT COMBO BOX             
            string queryUsers = "SELECT userName FROM user ORDER BY userName ASC";
            MySqlCommand cmdUsers = new MySqlCommand(queryUsers, DBConnection.conn);
            MySqlDataReader readerUsers = cmdUsers.ExecuteReader();

            while (readerUsers.Read())
            {
                cbApptUser.Items.Add(readerUsers["userName"].ToString());

            }
            readerUsers.Close();
            cbApptUser.SelectedIndex = 0;

            


            // APPOINTMENT LOCATION COMBO BOX 
            List<string> listLocation = new List<string>
            {
                "In Office", "Virtual", "Phone"
            };

            cbApptLocation.DataSource = listLocation;
            cbApptLocation.SelectedIndex = 0;



            /*************************/
            /*** END FORM CONTROLS ***/
            /*************************/


            /*************************/
            /*****   VARIABLES   *****/
            /*************************/


            /*************************/
            /****  END VARIABLES  ****/
            /*************************/

        }


        /*************************/
        /****** UPDATE FORM ******/
        /*************************/


        public AddAppointment(DataGridView dgv, int apptId)
        {
            InitializeComponent();

            /*************************/
            /***** FORM CONTROLS *****/
            /*************************/
            try
            {
                // CUSTOMER COMBO BOX 
                string queryCustomers = "SELECT customerName FROM customer ORDER BY customerName ASC";
                MySqlCommand cmdCustomers = new MySqlCommand(queryCustomers, DBConnection.conn);
                MySqlDataReader reader = cmdCustomers.ExecuteReader();

                while (reader.Read())
                {
                    cbCustomerList.Items.Add(reader["customerName"].ToString());

                }
                reader.Close();

                // Testing combo appt type adding 
                string queryApptType = "SELECT DISTINCT type FROM appointment ORDER BY type ASC"; 
                MySqlCommand cmdApptType = new MySqlCommand(queryApptType, DBConnection.conn);
                MySqlDataReader readerApptType = cmdApptType.ExecuteReader();

                while (readerApptType.Read())
                {
                    cbApptType.Items.Add(readerApptType["type"].ToString());
                }
                readerApptType.Close(); 
                
                string queryApptUpdate = $"SELECT *, customer.customerName FROM appointment INNER JOIN customer ON customer.customerId = appointment.customerId WHERE appointmentId = {apptId}";
                MySqlCommand cmdApptUpdate = new MySqlCommand(queryApptUpdate, DBConnection.conn);
                MySqlDataReader readerApptUpdate = cmdApptUpdate.ExecuteReader();

                while (readerApptUpdate.Read())
                {                   
                    cbCustomerList.SelectedIndex = (int)cbCustomerList.FindStringExact(readerApptUpdate["customerName"].ToString());
                    
                        
                }


                Appointment apptToUpdate = new Appointment();

                apptToUpdate.customerId = (int)readerApptUpdate["customerId"];
                apptToUpdate.userId = (int)readerApptUpdate["userId"];
                apptToUpdate.title = (string)readerApptUpdate["title"];
                apptToUpdate.description = (string)readerApptUpdate["description"];
                apptToUpdate.location = (string)readerApptUpdate["location"];
                apptToUpdate.type = (string)readerApptUpdate["type"];
                apptToUpdate.url = (string)readerApptUpdate["url"];
                apptToUpdate.start = Convert.ToDateTime(readerApptUpdate["start"].ToString());
                apptToUpdate.end = Convert.ToDateTime(readerApptUpdate["end"].ToString());
                apptToUpdate.createDate = Convert.ToDateTime(readerApptUpdate["createDate"].ToString());
                apptToUpdate.createdBy = (string)readerApptUpdate["createdBy"];
                apptToUpdate.lastUpdate = Convert.ToDateTime(readerApptUpdate["lastUpdate"].ToString());
                apptToUpdate.lastUpdateBy = (string)readerApptUpdate["lastUpdateBy"];

                


                readerApptUpdate.Close();

                cbApptType.SelectedIndex = cbApptType.FindString(apptToUpdate.type.ToString());

                

                MessageBox.Show($"Appointment type is {apptToUpdate.type} of type {apptToUpdate.type.GetType()}\ncbItems type is {cbApptType.Items.GetType()}");
                // First step: pull Date and time into separate variables for the picker boxes 
                string dateString = apptToUpdate.start.ToLocalTime().ToString().Substring(0,9);
                string timeString = apptToUpdate.start.ToLocalTime().ToString().Substring(10);

                //MessageBox.Show($"{dateString}\n{timeString}");
                datePicker.Value = Convert.ToDateTime(dateString);
                timePicker.Value = Convert.ToDateTime(timeString);

                


                // this is not working properly 
                //int testIndex = (int)cbApptType.FindStringExact(apptToUpdate.type);
                //MessageBox.Show($"{testIndex}");

                


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when showing Update form: {ex}", "Error", MessageBoxButtons.OK);
            }

            


            // TIME PICKER 
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.ShowUpDown = true;


            

            // APPOINTMENT CONSULTANT COMBO BOX             
            string queryUsers = "SELECT userName FROM user ORDER BY userName ASC";
            MySqlCommand cmdUsers = new MySqlCommand(queryUsers, DBConnection.conn);
            MySqlDataReader readerUsers = cmdUsers.ExecuteReader();

            while (readerUsers.Read())
            {
                cbApptUser.Items.Add(readerUsers["userName"].ToString());

            }
            readerUsers.Close();
            cbApptUser.SelectedIndex = 0;




            // APPOINTMENT LOCATION COMBO BOX 
            List<string> listLocation = new List<string>
            {
                "In Office", "Virtual", "Phone"
            };

            cbApptLocation.DataSource = listLocation;
            cbApptLocation.SelectedIndex = 0;



            /*************************/
            /*** END FORM CONTROLS ***/
            /*************************/


            /*************************/
            /*****   VARIABLES   *****/
            /*************************/


            /*************************/
            /****  END VARIABLES  ****/
            /*************************/

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
            this.Close(); 
            // how to force datagridview to update when add appointment is done?
            


            // Seems like the controls display in local time but can be converted to UTC for storing in the database 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
