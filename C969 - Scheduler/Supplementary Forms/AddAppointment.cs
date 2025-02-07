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
        public bool isExistingAppointment = false; 
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


            List<string> apptTypeList = new List<string>()
            {
                "Consultation", "Presentation", "Scrum"
            };

            cbApptType.DataSource = apptTypeList;
            cbApptType.SelectedIndex = 0; 

            // APPOINTMENT CONSULTANT COMBO BOX             
            string queryUsers = "SELECT DISTINCT userName FROM user ORDER BY userName ASC";
            MySqlCommand cmdUsers = new MySqlCommand(queryUsers, DBConnection.conn);
            MySqlDataReader readerUsers = cmdUsers.ExecuteReader();

            while (readerUsers.Read())
            {
                cbApptUser.Items.Add(readerUsers["userName"]);

            }
            readerUsers.Close();
            cbApptUser.SelectedIndex = 0;

            


            // APPOINTMENT LOCATION COMBO BOX 
            List<string> listLocation = new List<string>
            {
                "In Office", "Virtual", "Phone"
            };

            cbApptLocation.DataSource = listLocation;
//            cbApptLocation.SelectedIndex = 0;



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
            isExistingAppointment = true; 
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

                // APPOINTMENT TYPE COMBO BOX 
                string queryApptType = "SELECT DISTINCT type FROM appointment ORDER BY type ASC"; 
                MySqlCommand cmdApptType = new MySqlCommand(queryApptType, DBConnection.conn);
                MySqlDataReader readerApptType = cmdApptType.ExecuteReader();

                while (readerApptType.Read())
                {
                    cbApptType.Items.Add(readerApptType["type"].ToString());
                }
                readerApptType.Close();

                // APPOINTMENT CONSULTANT COMBO BOX
                
                string queryUpdateUsers = "SELECT DISTINCT userName FROM user ORDER BY userName ASC";
                MySqlCommand cmdUpdateUsers = new MySqlCommand(queryUpdateUsers, DBConnection.conn);
                MySqlDataReader readerUpdateUsers = cmdUpdateUsers.ExecuteReader();
                

                while (readerUpdateUsers.Read())
                {
                    cbApptUser.Items.Add(readerUpdateUsers["userName"].ToString());

                }
                readerUpdateUsers.Close();


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

                

                
                // SETTING ALL THE VALUES IN THE FORM FROM THE APPTID
                
                // First step: pull Date and time into separate variables for the picker boxes 
                string dateString = apptToUpdate.start.ToLocalTime().ToString().Substring(0,9);
                string timeString = apptToUpdate.start.ToLocalTime().ToString().Substring(9);

                
                //MessageBox.Show($"{dateString}\n{timeString}");
                datePicker.Value = Convert.ToDateTime(dateString);
                timePicker.Value = Convert.ToDateTime(timeString);

                // Set appointment type to match that of appointment to update
                cbApptType.SelectedIndex = cbApptType.FindString(apptToUpdate.type.ToString());

                // Set consultant according to who is the user on the appointment 
                // Have to grab username first 

                string queryUsernameMatch = $"SELECT user.userName FROM appointment INNER JOIN user ON user.userId = appointment.userId WHERE appointmentId = {apptId}";
                MySqlCommand cmdUsernameMatch = new MySqlCommand(queryUsernameMatch, DBConnection.conn);
                string username = (string)cmdUsernameMatch.ExecuteScalar();
                cbApptUser.SelectedIndex = (int)cbApptUser.FindStringExact(username.ToString());

                // APPOINTMENT LOCATION COMBO BOX 

                // APPOINTMENT LOCATION COMBO BOX 
                string queryUpdateLocation = "SELECT DISTINCT location FROM appointment ORDER BY location ASC";
                MySqlCommand cmdUpdateLocation = new MySqlCommand(queryUpdateLocation, DBConnection.conn);
                MySqlDataReader readerUpdateLocation = cmdUpdateLocation.ExecuteReader();


                while (readerUpdateLocation.Read())
                {
                    cbApptLocation.Items.Add(readerUpdateLocation["location"].ToString());

                }
                readerUpdateLocation.Close();


                // Set location according to appointment location 
                cbApptLocation.SelectedIndex = (int)cbApptLocation.FindString(apptToUpdate.location.ToString());
                
                // Textboxes
                txtDescription.Text = apptToUpdate.description.ToString();
                txtTitle.Text = apptToUpdate.title.ToString();

                // Set the ID value to be called for the Submit button 
                Helper.apptIdValue = apptId;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when showing Update form: {ex}", "Error", MessageBoxButtons.OK);
            }


            // TIME PICKER 
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.ShowUpDown = true;

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
            if (isExistingAppointment == true)
            {
                //MessageBox.Show("This is for the update only.");
                // need to maybe use Helper to create an appointment i can reference? 

                
                // get customer Id 
                string queryGetCustomerId = $"SELECT customer.customerId FROM customer WHERE customerName = '{cbCustomerList.SelectedItem.ToString()}'";
                MySqlCommand cmdGetCustomerId = new MySqlCommand(queryGetCustomerId, DBConnection.conn);
                int custId = (int)cmdGetCustomerId.ExecuteScalar();

                // get userId 
                string queryUserId = $"SELECT user.userId FROM user WHERE userName = '{cbApptUser.SelectedItem.ToString()}'";
                MySqlCommand cmdGetUserId = new MySqlCommand(queryUserId, DBConnection.conn);
                int userId = (int)cmdGetUserId.ExecuteScalar();

                // set up the DateTime for the start parameter 
                string updateTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.ToString("hh:mm tt");
                string updateEndTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.AddMinutes(30).ToString("hh:mm tt");
                DateTime updateStartTime = DateTime.Parse(updateTimeString).ToUniversalTime(); 
                DateTime updateEndTime = DateTime.Parse(updateEndTimeString).ToUniversalTime();
                string queryUpdateAppointmentSubmit = $"UPDATE appointment SET customerId = @custId, userId = {userId}, title = '{txtTitle.Text.ToString()}', description = '{txtDescription.Text.ToString()}', location = '{cbApptLocation.SelectedItem.ToString()}', type = '{cbApptType.SelectedItem.ToString()}', start = {updateStartTime}, end = {updateEndTime}, lastUpdate = NOW(), lastUpdateBy = '{Helper.userNameValue}' WHERE appointmentId = {Helper.apptIdValue}";



                // Parameters.. 

                try
                {
                    MySqlCommand cmdUpdateAppt = DBConnection.conn.CreateCommand();
                    cmdUpdateAppt.CommandText = $"UPDATE appointment SET customerId = @custId, userId = @userId, title = @title, description = @description, location = @location, type = @type, start = @start, end = @end, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE appointmentId = @apptId;" + "SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1";
                    cmdUpdateAppt.Parameters.AddWithValue("@apptId", Helper.apptIdValue);
                    cmdUpdateAppt.Parameters.AddWithValue("@custId", custId);
                    cmdUpdateAppt.Parameters.AddWithValue("@userId", userId);
                    cmdUpdateAppt.Parameters.AddWithValue("@title", txtTitle.Text);
                    cmdUpdateAppt.Parameters.AddWithValue("@description", txtDescription.Text);
                    cmdUpdateAppt.Parameters.AddWithValue("@location", cbApptLocation.SelectedItem.ToString());
                    cmdUpdateAppt.Parameters.AddWithValue("@type", cbApptType.SelectedItem.ToString());
                    cmdUpdateAppt.Parameters.AddWithValue("@start", updateStartTime);
                    cmdUpdateAppt.Parameters.AddWithValue("@end", updateEndTime);
                    cmdUpdateAppt.Parameters.AddWithValue("@lastUpdate", DateTime.Now.ToUniversalTime());
                    cmdUpdateAppt.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
                    cmdUpdateAppt.ExecuteScalar();

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error with updating appointment: {ex}", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                Appointment appt = new Appointment();

                // GET CUSTOMER ID
                string queryCustomerId = $"SELECT customerId FROM customer WHERE customerName = '{cbCustomerList.SelectedItem.ToString()}'";
                MySqlCommand customerIdCmd = new MySqlCommand(queryCustomerId, DBConnection.conn);
                appt.customerId = (int)customerIdCmd.ExecuteScalar();

                // GET USER ID FROM LOGGED IN USER 
                // THIS IS INCORRECT: it is grabbing the currently logged in user instead of the user that is selected in the combo box. 
                string query = $"SELECT userId FROM user WHERE userName = '{cbApptUser.SelectedItem.ToString()}'";
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
                string endTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.AddMinutes(30).ToString("hh:mm tt");
                appt.end = DateTime.Parse(endTimeString).ToUniversalTime();

                // these arent in the class definition so they can be set this way instead
                DateTime createDate = DateTime.Now.ToUniversalTime();
                string createdBy = Helper.userNameValue;
                string lastUpdateBy = Helper.userNameValue;




                Helper.addAppointment(appt);
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
