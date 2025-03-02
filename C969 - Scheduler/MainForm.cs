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
            Helper.ApptAlert(dgvAppointments, Helper.userNameValue); 

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
                cbTimePeriod.Enabled = true;
                apptCalendar.Enabled = true; 
            }
            else
            {
                Helper.LoadCustomerGrid(dgvAppointments);
                cbTimePeriod.Enabled = false;
                apptCalendar.Enabled = false; 
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)


        {
            if (comboBox1.SelectedIndex == 0)
            {
                AddAppointment addAppt = new AddAppointment(dgvAppointments);
                addAppt.Show();
            }
            else
            {
                AddCustomer addCustomer = new AddCustomer(dgvAppointments);
                addCustomer.Show(); 
            }
            

        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Helper.LoadAppointmentGrid(dgvAppointments);
            }
            else
            {
                Helper.LoadCustomerGrid(dgvAppointments); 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedIndex == 0)
            {// Appointments is selected 
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
            else
            {// Customers is selected 
                if (!dgvAppointments.CurrentRow.Selected)
                {
                    MessageBox.Show("Please select a customer to delete.");
                }
                else
                {

                    try
                    {
                        int customerId = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                        // confirmation window 
                        DialogResult result = MessageBox.Show("Are you sure you want to delete the selected customer and their associated appointments? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            Helper.deleteCustomer(customerId);

                            // refreshes data grid
                            Helper.LoadCustomerGrid(dgvAppointments);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error when deleting customer: {ex}", "Error", MessageBoxButtons.OK);
                    }

                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (!dgvAppointments.CurrentRow.Selected)
                {
                    MessageBox.Show("Please select an appointment to modify.");
                }
                else
                {

                    int apptId = (int)dgvAppointments.CurrentRow.Cells[0].Value;
                    AddAppointment addAppt = new AddAppointment(dgvAppointments, apptId);                    
                    addAppt.Show();
                }
            }
            else
            {
                if (!dgvAppointments.CurrentRow.Selected)
                {
                    MessageBox.Show("Please select a customer to modify.");
                }
                else
                {

                    int customerId = (int)dgvAppointments.CurrentRow.Cells[0].Value;                    
                    AddCustomer addCustomer = new AddCustomer(dgvAppointments, customerId);                  
                    addCustomer.Show();
                }
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
                Helper.BoldDates(todaysDate, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd 23:59:59");
                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Weekly")
            {
                
                Helper.GetWeekDates(todaysDate, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd 23:59:59");

                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Monthly")
            {
                int monthNum = todaysDate.Month;
                int yearNum = todaysDate.Year;    
                int daysNum = DateTime.DaysInMonth(yearNum, monthNum);

                DateTime startOfMonth = Convert.ToDateTime(yearNum.ToString() + "-" + monthNum.ToString() + "-01");
                DateTime endOfMonth = Convert.ToDateTime(yearNum.ToString() + "-" + monthNum.ToString() + "-" + daysNum);

                Helper.BoldMonthDates(startOfMonth, endOfMonth, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd") + " 23:59:59";
                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);

            }
            else
            {
                apptCalendar.RemoveAllBoldedDates();
                // have to call the refresh in order to actually update it for this one 
                apptCalendar.UpdateBoldedDates();
                Helper.LoadAppointmentGrid(dgvAppointments); 
            }
        }

        private void apptCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {

            DateTime selectedDay = apptCalendar.SelectionStart;

            if (cbTimePeriod.SelectedItem.ToString() == "Daily")
            {
                Helper.BoldDates(selectedDay, this);

                string yearNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(0, 4);
                string dayNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(8, 2);
                string monthNum = selectedDay.ToString("yyyy-MM-dd HH:mm:ss").Substring(5, 2);
                string startDate =  yearNum + "-" + monthNum + "-" + dayNum + " 00:00:00";
                string endDate = yearNum + "-" + monthNum + "-" + dayNum + " 23:59:59";

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
            }
            else if (cbTimePeriod.SelectedItem.ToString() == "Monthly")
            {
                int monthNum = apptCalendar.SelectionStart.Month;
                int yearNum = apptCalendar.SelectionStart.Year;
                int daysNum = DateTime.DaysInMonth(yearNum, monthNum);

                DateTime startOfMonth = Convert.ToDateTime(yearNum.ToString() + "-" + monthNum.ToString() + "-01 00:00:00");
                DateTime endOfMonth = Convert.ToDateTime(yearNum.ToString() + "-" + monthNum.ToString() + "-" + daysNum + " 23:59:59");
                

                Helper.BoldMonthDates(startOfMonth, endOfMonth, this);
                string startDate = apptCalendar.BoldedDates[0].ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = apptCalendar.BoldedDates[apptCalendar.BoldedDates.Length - 1].ToString("yyyy-MM-dd") + " 23:59:59";
                
                Helper.LoadAppointmentGrid(dgvAppointments, startDate, endDate);
            }
            else
            {
                //Remove all bold dates and simply show all appointments in the grid. 
                apptCalendar.RemoveAllBoldedDates();
                apptCalendar.UpdateBoldedDates(); 
            }
        }

        private void addTestUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm reports = new ReportForm(Helper.userNameValue);
            reports.Show(); 
        }
    }
}