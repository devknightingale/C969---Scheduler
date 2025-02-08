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
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            List<string> comboBoxItems = new List<string>()
            {
                "Appointments", "Customers"
            };

            comboBox1.DataSource = comboBoxItems;
            comboBox1.SelectedIndex = 0;


            // Time period selection for appointment screen 
            // Make sure to remember to hide this for the customer grid, then re-enable for appointment 
            List<string> cbTimePeriodItems = new List<string>()
            {
                "ALL", "Daily", "Weekly", "Monthly"
            };
            cbTimePeriod.DataSource = cbTimePeriodItems;
            cbTimePeriod.SelectedIndex = 0;

            //maximizes main form
            //WindowState = FormWindowState.Maximized;


            // this loads the default appointment view. 
            Helper.LoadAppointmentGrid(dgvAppointments);

            //testing that user log in works 
            lblUsername.Text = Helper.userNameValue;
            // MessageBox.Show($"Logged in user is currently {Helper.userNameValue}");


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
            // technically it should not be possible to not have a row selected since the application starts with a row selected automatically, but just in case... 
            if (!dgvAppointments.CurrentRow.Selected)
            {
                MessageBox.Show("Please select an appointment to delete.");
            }
            else
            {

                try
                {
                    int apptId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                    // confirmation window 
                    DialogResult result = MessageBox.Show("Are you sure you want to delete the selected appointment? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Helper.deleteAppointment(apptId);

                        // refreshes data grid
                        Helper.LoadAppointmentGrid(dgvAppointments);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when deleting appointment: {ex}", "Error", MessageBoxButtons.OK);
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!dgvAppointments.CurrentRow.Selected)
            {
                MessageBox.Show("Please select an appointment to modify.");
            }
            else
            {

                int apptId = (int)dgvAppointments.CurrentRow.Cells[0].Value;



                // pull up add appointment form here but need to fill text boxes first? 
                AddAppointment addAppt = new AddAppointment(dgvAppointments, apptId);
                //need to create an addAppt form with a constructor taking an Appointment as an argument in order to prefill the textboxes 
                addAppt.Show();
            }
        }

        private void cbTimePeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<DateTime> boldedSelection = new List<DateTime>();
            DateTime selectedDate = apptCalendar.SelectionStart;
            
            DateTime todaysDate = DateTime.Now; 
            // Have the calendar default to current day/week/month depending on which is selected here 

            if (cbTimePeriod.SelectedItem.ToString() == "Daily")
            {
                // FIX ME: Default to select current day. 
                
                Helper.BoldDates(todaysDate, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd 23:59:59");
                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Weekly")
            {
                              
                

                // Bold current week only. Use GetWeekDates() to grab the dates. 
                
                Helper.GetWeekDates(todaysDate, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd 23:59:59");
                // FIX ME: Check if WEEKLY is selected, then load appointment grid for that WEEKS appointment that is selected on the calendar control

                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Monthly")
            {
                // FIX ME: Check if MONTHLY is selected, then load appointment grid for that MONTHS appointment that is selected on the calendar control
                
                int monthNum = todaysDate.Month;
                int yearNum = todaysDate.Year;    
                int daysNum = DateTime.DaysInMonth(yearNum, monthNum);

                string startOfMonth = yearNum.ToString() + "-" + monthNum.ToString() + "-01";
                string endOfMonth = yearNum.ToString() + "-" + monthNum.ToString() + "-" + daysNum;

            }
            else
            {
                // FIX ME: this is the default grid that shows all appointments. 
                MessageBox.Show($"Selected is {cbTimePeriod.SelectedItem.ToString()}");
            }
        }

        private void apptCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {

            DateTime selectedDay = apptCalendar.SelectionStart;
            

            // FIX ME: Should call different versions of the BoldDates() function depending on if the selection is daily, monthly, or weekly. 
            // FIX ME: LoadAppointmentGrid() needs rewriting to account for different versions of the grid ex day week or month. the only change will be in the SQL statement. figure out how to implement this maybe pass in an int, which corresponds to the version ex 1 = daily, 2 = weekly, 3 = monthly, then in LoadAppointmentGrid(), just check for that number and use the appropriate version  
            if (cbTimePeriod.SelectedItem.ToString() == "Daily")
            {
                Helper.BoldDates(selectedDay, this);

                string yearNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(0, 4);
                string dayNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(8, 2);
                string monthNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(5, 2);
                string startDate =  yearNum + "-" + monthNum + "-" + dayNum + " 00:00:00";
                string endDate = yearNum + "-" + monthNum + "-" + dayNum + " 23:59:59";
                //FIX ME: LoadAppointmentGrid for the DAY SELECTED only 

                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Weekly")
            {
                Helper.GetWeekDates(selectedDay, this);
                
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd HH:mm:ss");
                string yearNum = startDate.Substring(0, 4);
                string dayNum = startDate.Substring(8, 2);
                string monthNum = startDate.Substring(5, 2);


                startDate = yearNum + "-" + monthNum + "-" + dayNum + " 00:00:00";
                endDate = yearNum + "-" + endDate.Substring(5,2) + "-" + endDate.Substring(8,2) + " 23:59:59";
                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);
                //FIX ME: LoadAppointmentGrid for the WEEK SELECTED only 
            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Monthly")
            {
                //FIX ME: Make the BoldDates(month) function, then call it here
                // will need to get current month, and daysinmonth, then assign those to selectionstart/selection end. pass in start, end, mainform 
                //Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);
                //FIX ME: LoadAppointmentGrid for the MONTH SELECTED only 
            }
            else
            {
                //Remove all bold dates and simply show all appointments in the grid. 
                apptCalendar.RemoveAllBoldedDates();
            }
        }

    }
}