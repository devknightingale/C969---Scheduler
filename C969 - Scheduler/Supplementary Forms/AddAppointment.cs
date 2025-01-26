using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


            // DATE TIME PICKER 

            // TIME PICKER 
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.ShowUpDown = true;


            // DATETIME COMBINER? 
            //DateTime startTime = datePicker.Value.ToUniversalTime();
            //DateTime endTime = datePicker.Value.AddMinutes(30).ToUniversalTime();
            //DateTime createDate = DateTime.Now.ToUniversalTime();

            
            

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


            // GET CUSTOMER ID FROM COMBO BOX MAYBE? 
            // load combo box with list of users sorted alphabetically? then allow drop down selection 

            
            // GET USER ID FROM LOGGED IN USER 
            string query = $"SELECT userId FROM user WHERE userName = '{UsernameHelper.userNameValue}'";
            MySqlCommand userIdCmd = new MySqlCommand(query, DBConnection.conn);
            int apptUserId = Convert.ToInt32(userIdCmd.ExecuteScalar());
            
            string apptTitle = txtTitle.Text;
            string apptDescription = txtDescription.Text;
            string apptLocation = cbApptUser.SelectedIndex.ToString(); 
            string apptType = cbApptType.SelectedIndex.ToString();
            string startTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.ToString("hh:mm tt");
            DateTime startTime = DateTime.Parse(startTimeString).ToUniversalTime();
            DateTime endTime = timePicker.Value.AddMinutes(30).ToUniversalTime();
            DateTime createDate = DateTime.Now.ToUniversalTime();
            string createdBy = UsernameHelper.userNameValue;
            string lastUpdateBy = UsernameHelper.userNameValue; 
            
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
            string startTimeString = datePicker.Value.ToString("yyyy-MM-dd") + ' ' + timePicker.Value.ToString("hh:mm tt");
            DateTime startTimeTest = DateTime.Parse(startTimeString).ToUniversalTime();


            
            // Seems like the controls display in local time but can be converted to UTC for storing in the database 
        }
    }
}
