using C969___Scheduler.Database;
using C969___Scheduler.Supplementary_Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
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
        public static int customerIdValue {  get; set; }

        public static List<DateTime> boldedSelection = new List<DateTime>();
        /**********************/
        /***** DATA  GRID *****/
        /**********************/
        public static string GetLocalZone()
        {
            return TimeZone.CurrentTimeZone.StandardName;
        }
        public static int GetUserID()
        {
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "SELECT userId FROM user WHERE userName = @userName";
            cmd.Parameters.AddWithValue("@userName", userNameValue);
            userIdValue = (int)cmd.ExecuteScalar();

            return userIdValue;
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


        public static int addCity(City city)
        {
            // Adds city to database, returns city id to use for address function
            int cityId = -1; 
            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO city(city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES(@city, @countryId, @createDate, @createdBy, @lastUpdate, @lastUpdateBy);" + "SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1";
                cmd.Parameters.AddWithValue("@city", city.city);
                cmd.Parameters.AddWithValue("@countryId", city.countryId);
                cmd.Parameters.AddWithValue("@createDate", city.createDate);
                cmd.Parameters.AddWithValue("@createdBy", city.createdBy);
                cmd.Parameters.AddWithValue("@lastUpdate", city.lastUpdate);
                cmd.Parameters.AddWithValue("@lastUpdateBy", city.lastUpdateBy);
                cityId = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {                
                MessageBox.Show($"Error when adding city: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return cityId;
        }
        public static int addCountry(Country country)
        {
            // Adds country to database, returns country id for use in other functions 
            int countryId = -1;

            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO country(country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES(@country, @createDate, @createdBy, @lastUpdate, @lastUpdateBy);" + "SELECT countryId FROM country ORDER BY countryId DESC LIMIT 1";
                cmd.Parameters.AddWithValue("@country", country.country);
                cmd.Parameters.AddWithValue("@createDate", country.createDate);
                cmd.Parameters.AddWithValue("@createdBy", country.createdBy);
                cmd.Parameters.AddWithValue("@lastUpdate", country.lastUpdate);
                cmd.Parameters.AddWithValue("@lastUpdateBy", country.lastUpdateBy);
                countryId = (int)cmd.ExecuteScalar();
                country.countryId = countryId;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Error when adding country: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return countryId;
        }
        public static int addAddress(Address address)
        {
            // FIX ME: add address to database, return address id 
            // note: must use country/city id for this 
            int addressId = -1; 
            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO address(address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES(@address, @address2, @cityId, @postalCode, @phone, @createDate, @createdBy, @lastUpdate, @lastUpdateBy);" + "SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1";
                cmd.Parameters.AddWithValue("@address", address.address1);
                cmd.Parameters.AddWithValue("@address2", address.address2);
                cmd.Parameters.AddWithValue("@cityId", address.cityId);
                cmd.Parameters.AddWithValue("@postalCode", address.postalCode);
                cmd.Parameters.AddWithValue("@phone", address.phoneNumber);
                cmd.Parameters.AddWithValue("@createDate", address.createDate); 
                cmd.Parameters.AddWithValue("@createdBy", address.createdBy);
                cmd.Parameters.AddWithValue("@lastUpdate", address.lastUpdate);
                cmd.Parameters.AddWithValue("@lastUpdateBy", address.lastUpdateBy);
                addressId = (int)cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error when adding address: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return addressId;
        }
        public static int addCustomer(Customer customer)
        {
            // FIX ME: add customer to database. Must be done LAST after city/country/address 
            // return customer id 
            int customerId = -1;
            try
            {
                MySqlCommand cmd = DBConnection.conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO customer(customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES(@customerName, @addressId, @active, @createDate, @createdBy, @lastUpdate, @lastUpdateBy);" + "SELECT customerId FROM customer ORDER BY customerId DESC LIMIT 1";
                cmd.Parameters.AddWithValue("@customerName", customer.customerName);
                cmd.Parameters.AddWithValue("@addressId", customer.addressId);
                cmd.Parameters.AddWithValue("@active", customer.active);                
                cmd.Parameters.AddWithValue("@createDate", customer.createDate);
                cmd.Parameters.AddWithValue("@createdBy", customer.createdBy);
                cmd.Parameters.AddWithValue("@lastUpdate", customer.lastUpdate);
                cmd.Parameters.AddWithValue("@lastUpdateBy", customer.lastUpdateBy);
                customerId = (int)cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error when adding customer: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return customerId;
            
        }

        public static void UpdateCountry(int countryId, AddCustomer form)
        {
            // updates country 
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "UPDATE country SET country = @country, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE countryId = @countryId;";
            cmd.Parameters.AddWithValue("@countryId", countryId);
            cmd.Parameters.AddWithValue("@country", form.txtCountry.Text);
            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
            cmd.ExecuteNonQuery(); 
        }
        public static void UpdateCity(int cityId, AddCustomer form)
        {

            // first update the country
            MySqlCommand getCountryId = DBConnection.conn.CreateCommand();
            getCountryId.CommandText = "SELECT countryId FROM city WHERE cityId = @cityId";
            getCountryId.Parameters.AddWithValue("@cityId", cityId);
            int countryId = (int)getCountryId.ExecuteScalar();
            UpdateCountry(countryId, form);


            // updates city
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "UPDATE city SET city = @city, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE cityId = @cityId;";
            cmd.Parameters.AddWithValue("@cityId", cityId);
            cmd.Parameters.AddWithValue("@city", form.txtCity.Text);
            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
            cmd.ExecuteNonQuery();
        }

        public static void UpdateAddress(int addressId, AddCustomer form)
        {
            // first update the city
            MySqlCommand getCityId = DBConnection.conn.CreateCommand();
            getCityId.CommandText = "SELECT cityId FROM address WHERE addressId = @addressId";
            getCityId.Parameters.AddWithValue("@addressId", addressId);
            int cityId = (int)getCityId.ExecuteScalar();
            UpdateCity(cityId, form);

            //updates address 
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "UPDATE address SET address = @address, address2 = @address2, postalCode = @postalCode, phone = @phone, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE addressId = @addressId;";
            cmd.Parameters.AddWithValue("@addressId", addressId);
            cmd.Parameters.AddWithValue("@address", form.txtAddress1.Text);
            if (String.IsNullOrWhiteSpace(form.txtAddress2.Text)) {
                cmd.Parameters.AddWithValue("@address2", "N/A");
            }
            else
            {
                cmd.Parameters.AddWithValue("@address2", form.txtAddress2.Text);
            }
            cmd.Parameters.AddWithValue("@postalCode", form.txtZip.Text);
            cmd.Parameters.AddWithValue("@phone", form.txtPhone.Text);
            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
            cmd.ExecuteNonQuery();
        }

        public static void UpdateCustomer(int customerId, AddCustomer form)
        {
            // first update the address
            MySqlCommand getAddressId = DBConnection.conn.CreateCommand();
            getAddressId.CommandText = "SELECT addressId FROM customer WHERE customerId = @customerId";
            getAddressId.Parameters.AddWithValue("@customerId", customerId);
            int addressId = (int)getAddressId.ExecuteScalar();
            UpdateAddress(addressId, form);

            // FIX ME: Update customer function goes here 
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "UPDATE customer SET customerName = @customerName, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE customerId = @customerId;";
            cmd.Parameters.AddWithValue("@customerId", customerId); 
            cmd.Parameters.AddWithValue("@customerName", form.txtFirstName.Text + " " + form.txtLastName.Text);
            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@lastUpdateBy", Helper.userNameValue);
            cmd.ExecuteNonQuery();
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

        public static List<Appointment> GetSchedule()
        {
            int userId = GetUserID();

            List<Appointment> apptList = new List<Appointment>();
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "SELECT customer.customerName as 'Customer Name', appointment.title as 'Title', appointment.start as 'Start' FROM appointment  LEFT JOIN customer ON customer.customerId = appointment.customerId WHERE appointment.userId = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                apptList.Add(new Appointment() {
                    start = Convert.ToDateTime(reader["start"])
                });

            }
            reader.Close();
            return apptList;
            

        }
    }

    
}
