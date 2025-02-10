using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C969___Scheduler.Entity_Classes;
using System.Data.SqlClient;
using C969___Scheduler.Database;
using System.IO;



namespace C969___Scheduler
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            // centers login form 
            StartPosition = FormStartPosition.CenterScreen;

            // Get the timezone of the application - is this even needed here or do only on appointment section?
            // May move this elsewhere later 
            string localZone = Helper.GetLocalZone();


            // sets language of login form to spanish if detected
            if (CultureInfo.CurrentCulture.Name == "es-MX")
            {
                lblUsername.Text = "Usario:";
                lblPassword.Text = "Contraseña:";
            }



            // DEBUG MESSAGEBOX
            //MessageBox.Show($"Current culture is {CultureInfo.CurrentCulture.Name}"); //for culture info
            //MessageBox.Show($"Current timezone is {localZone}\n Time is currently {currentTime}"); // for timezone
        }

        // testing username retrieval option here
        public static string testUsername { get; set; }

        // end testing 


        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                //check user & password for a match in database 
                string query = $"SELECT * FROM user WHERE userName = '{txtUsername.Text}' AND password = '{txtPassword.Text}'";
                
                // MySqlConnection conn = DBConnection.conn;
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                adapter.Fill(dtable);


                // log file?
                string path = @"C:\Users\Public\Documents\Login_History.txt";
                // need timestamp and username 
                // how do i send this user to the next form???
                string logUser = txtUsername.Text;
                string currentTime = DateTime.Now.ToString(); 
                

                

                if (dtable.Rows.Count > 0)
                {
                    Helper.userNameValue = logUser;
                    

                    
                    //MessageBox.Show("match found");
                    if (Helper.ApptAlert())

                    {
                        List<Appointment> appts = new List<Appointment>();
                        DateTime currentUtc = DateTime.UtcNow;
                        MySqlCommand cmdNextAppt = DBConnection.conn.CreateCommand();
                        // need to join appointment and user 
                        cmdNextAppt.CommandText = $"SELECT * FROM appointment LEFT JOIN user ON appointment.userId = user.userId WHERE userName = @userName and TIMESTAMPDIFF(MINUTE, start, @currentTime) < 15";
                        cmdNextAppt.Parameters.AddWithValue("@userName", Helper.userNameValue);
                        cmdNextAppt.Parameters.AddWithValue("@currentTime", currentUtc);
                        cmdNextAppt.ExecuteNonQuery(); 

                        try
                        {
                            MySqlDataReader readerApptCheck = cmdNextAppt.ExecuteReader();

                            while (readerApptCheck.Read()) {
                                appts.Add(new Appointment()
                                {
                                    customerId = (int)readerApptCheck["customerId"],
                                    title = readerApptCheck["title"].ToString(),
                                    description = readerApptCheck["description"].ToString(),
                                    location = readerApptCheck["location"].ToString(),
                                    contact = readerApptCheck["contact"].ToString(),
                                    type = readerApptCheck["type"].ToString(),
                                    url = readerApptCheck["url"].ToString(),
                                    start = Convert.ToDateTime(readerApptCheck["start"].ToString()).ToLocalTime(),
                                    end = Convert.ToDateTime(readerApptCheck["end"].ToString()).ToLocalTime()
                                });
                            }


                            foreach (Appointment appt in appts)
                            {
                                MessageBox.Show($"You have an appointment at {appt.start.ToString("hh:mm tt")}.");
                            }
                        }
                            
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error fetching time of upcoming appointment: {ex}");
                        }

                        

                        
                    }
                    File.AppendAllText(path, $"User {logUser} logged in successfully at {currentTime}\n");
                    
                    
                    // load next form here
                    MainForm mainForm = new MainForm();
                   

                    //will close entire application when main form is closed
                    mainForm.FormClosed += (s, args) => this.Close(); 
                    
                    mainForm.Show();
                    this.Hide(); // hides the login form so its not visible when main form is open 
                    
                }
                else
                {
                    if (CultureInfo.CurrentCulture.Name == "es-MX")
                    {
                        MessageBox.Show("Inicio de sesión inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    File.AppendAllText(path, $"User {logUser} failed to login at {currentTime}\n");
                }

            }
            catch (Exception ex)
            {
                //display error message if login is invalid, DEPENDENT UPON LANGUAGE
            
                MessageBox.Show($"Error on login form: {ex}");
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { 
                btnSubmit_Click(this, new EventArgs());
            }
        }
    }
}
