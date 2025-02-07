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

            if (cbTimePeriod.SelectedItem.ToString() == "Daily")
            {
                // FIX ME: Check if DAILY is selected, then load appointment grid for that DAYS appointment that is selected on the calendar control
                MessageBox.Show($"Selected is {cbTimePeriod.SelectedItem.ToString()}");

                for (DateTime selectedDates = apptCalendar.SelectionRange.Start; selectedDates <= apptCalendar.SelectionRange.End; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                apptCalendar.BoldedDates = boldedSelection.ToArray();
                apptCalendar.UpdateBoldedDates();

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Weekly")
            {
                //   There has to be a more concise way to do this: 
                // Try grabbing the week number from the calendar somehow, then bolding that week? if possible. Might be less code involved 

                // FIX ME: Check if WEEKLY is selected, then load appointment grid for that WEEKS appointment that is selected on the calendar control
                MessageBox.Show($"Selected is {cbTimePeriod.SelectedItem.ToString()}");

                // switch case for day of week?
                // ex: checks day of week, then sets range of dates to select based on the day of the week 
                if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(0);
                    DateTime endDate = today.AddDays(6);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();

                }
                else if (DateTime.Today.DayOfWeek != DayOfWeek.Monday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-1);
                    DateTime endDate = today.AddDays(5);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }
                else if (DateTime.Today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-2);
                    DateTime endDate = today.AddDays(4);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }
                else if (DateTime.Today.DayOfWeek != DayOfWeek.Wednesday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-3);
                    DateTime endDate = today.AddDays(3);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }
                else if (DateTime.Today.DayOfWeek != DayOfWeek.Thursday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-4);
                    DateTime endDate = today.AddDays(2);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }
                else if (DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                {
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-5);
                    DateTime endDate = today.AddDays(1);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }
                else
                {
                    // this would be saturday 
                    DateTime today = DateTime.Now;
                    DateTime startDate = today.AddDays(-6);
                    DateTime endDate = today.AddDays(0);

                    //testing this 
                    for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                    {
                        boldedSelection.Add(selectedDates);
                    }
                    apptCalendar.BoldedDates = boldedSelection.ToArray();
                    apptCalendar.UpdateBoldedDates();
                }

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Monthly")
            {
                // FIX ME: Check if MONTHLY is selected, then load appointment grid for that MONTHS appointment that is selected on the calendar control
                MessageBox.Show($"Selected is {cbTimePeriod.SelectedItem.ToString()}");
            }
            else
            {
                // FIX ME: this is the default grid that shows all appointments. 
                MessageBox.Show($"Selected is {cbTimePeriod.SelectedItem.ToString()}");
            }
        }

        private void apptCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            // FIX ME: needs to clear all previously bolded dates, then call the function to bold dates based on the selection in the time period drop down. 
        }
    }
}