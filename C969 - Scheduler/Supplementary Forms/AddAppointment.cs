using C969___Scheduler.Entity_Classes;
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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm";


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


           
             
            // need to get both user and customer Ids... somehow
            // use sql command to find user matching username, then grab its id? 

            string apptUser = cbApptUser.SelectedIndex.ToString();
            string apptTitle = txtTitle.Text;
            string apptDescription = txtDescription.Text;
            string apptLocation = cbApptUser.SelectedIndex.ToString();
            //string apptContact = "not needed"; 
            string apptType = cbApptType.SelectedIndex.ToString();
            DateTime startTime = dateTimePicker1.Value.ToUniversalTime();
            DateTime endTime = dateTimePicker1.Value.AddMinutes(30).ToUniversalTime();
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
    }
}
