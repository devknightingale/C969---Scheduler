using C969___Scheduler.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        /**********************/
        /***** DATA  GRID *****/
        /**********************/

        public static void LoadAppointmentGrid(DataGridView dgv)
        {
            try
            {
                // should update this query to a join to grab customer name instead of customer id 
                string apptQuery = $"SELECT appointment.appointmentId as 'Appointment ID', appointment.title as 'Title', appointment.location as 'Location', appointment.type as 'Type', appointment.start as 'Appointment Time',  customer.CustomerName as 'Customer', user.userName as 'Consultant' FROM appointment INNER JOIN customer ON appointment.customerId = customer.customerId INNER JOIN user ON appointment.userId = user.userId";

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
    }

    
}
