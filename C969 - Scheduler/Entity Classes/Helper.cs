﻿using C969___Scheduler.Database;
using C969___Scheduler.Supplementary_Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler.Entity_Classes
{
    public class Helper
    {
        public static string userNameValue { get; set; }

        public static int userIdValue { get; set; }

        public static int apptIdValue { get; set; }

        public static List<DateTime> boldedSelection = new List<DateTime>();
        /**********************/
        /***** DATA  GRID *****/
        /**********************/
        public static string GetLocalZone()
        {
            return TimeZone.CurrentTimeZone.StandardName;
        }
        public static void LoadAppointmentGrid(DataGridView dgv)
        {
            string apptQuery = "";


            try
            {

                apptQuery = $"SELECT appointment.appointmentId as 'Appointment ID', appointment.title as 'Title', appointment.location as 'Location', appointment.type as 'Type', appointment.start as 'Appointment Time',  customer.CustomerName as 'Customer', user.userName as 'Consultant' FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId INNER JOIN user ON appointment.userId = user.userId";

                MySqlCommand apptCmd = new MySqlCommand(apptQuery, DBConnection.conn);
                MySqlDataAdapter appAdapter = new MySqlDataAdapter(apptCmd);
                DataTable apptTable = new DataTable();
                appAdapter.Fill(apptTable);

                // Converts time displays on appointment grid to local timezone 

                BindingSource apptBindingSource = new BindingSource();
                apptBindingSource.DataSource = apptTable;
                dgv.DataSource = apptBindingSource;

                dgv.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm tt";

                int columnIndex = 4;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv[columnIndex, i].Value = Convert.ToDateTime(dgv[columnIndex, i].Value.ToString()).ToLocalTime();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error when filling data grid: {ex}", "ERROR", MessageBoxButtons.OK);
            }
        }

        public static void LoadAppointmentGrid(DataGridView dgv, string startDate, string endDate)
        {
            string apptQuery = "";


            try
            {
                DateTime startDateTime = Convert.ToDateTime(startDate);
                DateTime endDateTime = Convert.ToDateTime(endDate);
                // should update this query to a join to grab customer name instead of customer id 
                apptQuery = $"SELECT appointment.appointmentId as 'Appointment ID', appointment.title as 'Title', appointment.location as 'Location', appointment.type as 'Type', appointment.start as 'Appointment Time',  customer.CustomerName as 'Customer', user.userName as 'Consultant' FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId INNER JOIN user ON appointment.userId = user.userId WHERE appointment.start BETWEEN '{startDate}' AND '{endDate}'";

                MySqlCommand apptCmd = new MySqlCommand(apptQuery, DBConnection.conn);
                MySqlDataAdapter appAdapter = new MySqlDataAdapter(apptCmd);
                DataTable apptTable = new DataTable();
                appAdapter.Fill(apptTable);

                // Converts time displays on appointment grid to local timezone 

                BindingSource apptBindingSource = new BindingSource();
                apptBindingSource.DataSource = apptTable;
                dgv.DataSource = apptBindingSource;

                dgv.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm tt";

                int columnIndex = 4;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv[columnIndex, i].Value = Convert.ToDateTime(dgv[columnIndex, i].Value.ToString()).ToLocalTime();
                }


            }
            catch (Exception ex)
            {
                // for some reason this throws an exception when enabled despite the grid working? 
                MessageBox.Show($"Error when filling data grid: {ex}", "ERROR", MessageBoxButtons.OK);
            }
        }
        public static void LoadCustomerGrid(DataGridView dgv)
        {
            try
            {
                string customerQuery = $"SELECT customer.customerId as 'Customer ID', customer.customerName as 'Customer', address.address as 'Address', address.phone as 'Phone Number' FROM customer LEFT JOIN address ON customer.addressId = address.addressId ORDER BY customer.customerName";

                MySqlCommand customerCmd = new MySqlCommand(customerQuery, DBConnection.conn);
                MySqlDataAdapter customerAdapter = new MySqlDataAdapter(customerCmd);
                DataTable customerTable = new DataTable();
                customerAdapter.Fill(customerTable);

                BindingSource customerBindingSource = new BindingSource();
                customerBindingSource.DataSource = customerTable;
                dgv.DataSource = customerBindingSource;


            }
            catch
            {
                MessageBox.Show("Failed to load customers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /**********************/
        /***** PROCEDURES *****/
        /**********************/


        // APPOINTMENT PROCEDURES 

        public static int addAppointment(Appointment appt)
        {

            int apptId = -1;

            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();

                cmd.CommandText = $"INSERT INTO appointment(customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdateBy) VALUES(@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, @createDate, @createdBy, @lastUpdateBy);" + "SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1";
                cmd.Parameters.AddWithValue("@customerId", appt.customerId);
                cmd.Parameters.AddWithValue("@userId", appt.userId);
                cmd.Parameters.AddWithValue("@title", appt.title);
                cmd.Parameters.AddWithValue("@description", appt.description);
                cmd.Parameters.AddWithValue("@location", appt.location);
                cmd.Parameters.AddWithValue("@contact", appt.contact);
                cmd.Parameters.AddWithValue("@type", appt.type);
                cmd.Parameters.AddWithValue("@url", appt.url);
                cmd.Parameters.AddWithValue("@start", appt.start);
                cmd.Parameters.AddWithValue("@end", appt.end);
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now.ToUniversalTime());
                cmd.Parameters.AddWithValue("@createdBy", Helper.userNameValue);
                cmd.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
                apptId = (int)cmd.ExecuteScalar();


            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error when adding appointment: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return apptId;

        }
        public static bool checkOverlapAppt(string apptStartTime, string apptEndTime)
        {
            // checks for overlapping appointments 


            string queryCheckOverlap = $"SELECT * FROM appointment WHERE end >= '{apptStartTime}' and start <= '{apptEndTime}'";
            MySqlCommand cmdCheckOverlap = new MySqlCommand(queryCheckOverlap, DBConnection.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdCheckOverlap);
            DataTable dtCheckOverlap = new DataTable();
            adapter.Fill(dtCheckOverlap);

            if (dtCheckOverlap.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static void ApptAlert(DataGridView dgvAppointments, string username)

        {
            // test

            foreach (DataGridViewRow row in dgvAppointments.Rows)
            {
                DateTime currentTime = DateTime.UtcNow;
                DateTime apptStartTime = DateTime.Parse(row.Cells[4].Value.ToString()).ToUniversalTime();

                TimeSpan checkDiff = currentTime - apptStartTime;
                if (checkDiff.TotalMinutes >= -15 && checkDiff.TotalMinutes < 1)
                {
                    string apptTime = Convert.ToDateTime(row.Cells[4].Value).ToString("hh:mm tt");
                    string apptCustomer = row.Cells[5].Value.ToString();
                    MessageBox.Show($"You have an appointment at {apptTime} with {apptCustomer}.", "Appointment Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        public static int deleteAppointment(int apptId)
        {

            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = "DELETE FROM appointment WHERE appointmentId = @appointmentId";
                cmd.Parameters.AddWithValue("@appointmentId", apptId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when deleting appointment: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 0;
        }



        // CUSTOMER PROCEDURES 


        public static int addCity()
        {
            // FIX ME: Add city to database, return city id 
            return 0;
        }
        public static int addCountry()
        {
            // FIX ME: Add country to database, return country id
            return 0;
        }
        public static int addAddress()
        {
            // FIX ME: add address to database, return address id 
            // note: must use country/city id for this 
            return 0;
        }
        public static int addCustomer(Customer customer)
        {
            // FIX ME: add customer to database. Must be done LAST after city/country/address 
            // return customer id 
            return 0;
        }

        public static void updateCustomer()
        {
            // FIX ME: Update customer function goes here 
        }

        public static int deleteCustomer(int customerId)
        {
            
            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = "DELETE FROM appointment WHERE customerId = @customerId;" + "DELETE FROM customer WHERE customerId = @customerId";
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when deleting customer: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 0;
        }

        public static bool validateTextboxes(AddCustomer customerForm)
        {
            bool textValidated; 
            
            if (string.IsNullOrWhiteSpace(customerForm.txtLastName.Text))
            {
                textValidated = false; 
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtFirstName.Text))
            {
                textValidated = false; 
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtAddress1.Text))
            {
                textValidated = false;
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtCity.Text))
            {
                textValidated = false;
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtState.Text))
            {
                textValidated = false;
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtZip.Text) || !customerForm.txtZip.MaskFull)
            {
                
                textValidated = false;
            }
            else if (string.IsNullOrWhiteSpace(customerForm.txtCountry.Text))
            {
                textValidated = false;
            }            
            else if (string.IsNullOrWhiteSpace(customerForm.txtPhone.Text) || !customerForm.txtPhone.MaskFull)
            {

                textValidated = false;
            }
            else
            {
                textValidated = true;
            }

            if (textValidated == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("One or more textboxes failed to validate. Please try again.", "Error", MessageBoxButtons.OK);
                return false;

            }
            
        }

        // CALENDAR PROCEDURES 

        
        public static void BoldDates(DateTime dayToBold, MainForm mainForm)
        {
            // Bolds a single day 
            mainForm.apptCalendar.RemoveAllBoldedDates();
            boldedSelection.Clear();
            boldedSelection.Add(dayToBold);
            mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
            
        }

        public static void BoldMonthDates(DateTime startMonth, DateTime endMonth, MainForm mainForm)
        {
            mainForm.apptCalendar.RemoveAllBoldedDates();
            boldedSelection.Clear();
            for (DateTime selectedDates = startMonth; selectedDates <= endMonth; selectedDates = selectedDates.AddDays(1))
            {
                boldedSelection.Add(selectedDates);
            }
            mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
            mainForm.apptCalendar.UpdateBoldedDates();
        }



        // Get Week 
        public static void GetWeekDates(DateTime selectedDate, MainForm mainForm)
        {
            // could use some cleaning perhaps 
            mainForm.apptCalendar.RemoveAllBoldedDates();
            boldedSelection.Clear();

            if (selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
               
                DateTime startDate = selectedDate.AddDays(0);
                DateTime endDate = selectedDate.AddDays(6);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();

                
            }
            else if (selectedDate.DayOfWeek == DayOfWeek.Monday)
            {
                
                DateTime startDate = selectedDate.AddDays(-1);
                DateTime endDate = selectedDate.AddDays(5);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
            else if (selectedDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                
                DateTime startDate = selectedDate.AddDays(-2);
                DateTime endDate = selectedDate.AddDays(4);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
            else if (selectedDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                
                DateTime startDate = selectedDate.AddDays(-3);
                DateTime endDate = selectedDate.AddDays(3);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
            else if (selectedDate.DayOfWeek == DayOfWeek.Thursday)
            {
                
                DateTime startDate = selectedDate.AddDays(-4);
                DateTime endDate = selectedDate.AddDays(2);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
            else if (selectedDate.DayOfWeek == DayOfWeek.Friday)
            {
                
                DateTime startDate = selectedDate.AddDays(-5);
                DateTime endDate = selectedDate.AddDays(1);

                //testing this 
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
            else
            {
                // this would be saturday 
               
                DateTime startDate = selectedDate.AddDays(-6);
                DateTime endDate = selectedDate;
                
                for (DateTime selectedDates = startDate; selectedDates <= endDate; selectedDates = selectedDates.AddDays(1))
                {
                    boldedSelection.Add(selectedDates);
                }
                mainForm.apptCalendar.BoldedDates = boldedSelection.ToArray();
                mainForm.apptCalendar.UpdateBoldedDates();
            }
        }
    }

    
}
