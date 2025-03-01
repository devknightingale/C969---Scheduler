using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler
{
    public partial class ReportForm : Form
    {
        public ReportForm(string user)
        {
            InitializeComponent();
            
        }

        private void btnMonthlyApptsReport_Click(object sender, EventArgs e)
        {
            List<Appointment> userAppointments = Helper.GetSchedule();
            string reportString = $"{Helper.userNameValue} Appointments:\n\n";

            // lambda to simplify foreach statement
            userAppointments.ForEach(appt => 
            {
                reportString += "\n\nStart: " + appt.start + Environment.NewLine;

            });
            txtReport.Text = reportString;

        }

        private void btnAppointmentType_Click(object sender, EventArgs e)
        {
            

            
        }
    }
}
