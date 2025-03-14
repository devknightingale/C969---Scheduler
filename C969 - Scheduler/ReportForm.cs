﻿using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace C969___Scheduler
{
    public partial class ReportForm : Form
    {
        public ReportForm(string user)
        {
            InitializeComponent();

            // sets user for the user schedule function 
            List<string> users = new List<string>();            
            MySqlCommand cmd = DBConnection.conn.CreateCommand();
            cmd.CommandText = "SELECT userName FROM user"; 
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cbUsers.Items.Add(reader["userName"].ToString());
            }
            reader.Close();


            // sets combobox for months 
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            
            foreach (string month in months)
            {
                cbMonths.Items.Add(month); 
            }

            // sets combobox for customers 
            MySqlCommand cmdCust = DBConnection.conn.CreateCommand();
            cmdCust.CommandText = "SELECT customerName FROM customer";
            MySqlDataReader rdrCust = cmdCust.ExecuteReader(); 
            while (rdrCust.Read())
            {
                cbCustomers.Items.Add(rdrCust["customerName"].ToString());
            }
            rdrCust.Close(); 
        }

        private void btnUserScheduleReport_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedItem == null)
            {
                MessageBox.Show("You must select a user first.");
            }
            else
            {
                string name = cbUsers.SelectedItem.ToString();
                List<Appointment> userAppointments = Helper.GetSchedule(name);

                string reportString = $"{name} Appointments:" + Environment.NewLine + Environment.NewLine;

                // lambda to simplify foreach statement
                userAppointments.ForEach(appt =>
                {
                    reportString += "\n\nStart: " + appt.start + "  Type: " + appt.type + "  Title: " + appt.title + Environment.NewLine;

                });
                txtReport.Text = reportString;
            }
            

        }

        private void btnAppointmentType_Click(object sender, EventArgs e)
        {
            if (cbMonths.SelectedItem == null)
            {
                MessageBox.Show("You must select a month first.");
            }
            else
            {
                string month = cbMonths.SelectedItem.ToString();
                List<Appointment> monthlyAppts = Helper.GetMonthlyAppointments(month, this);
                Dictionary<string, int> typeCount = new Dictionary<string, int>();
                monthlyAppts.ForEach(appt =>
                {
                    if (typeCount.ContainsKey(appt.type))
                    {
                        typeCount[appt.type] += 1;
                    }
                    else
                    {
                        typeCount.Add(appt.type, 1);
                    }
                });

                string reportString = $"{month} Appointment Types:" + Environment.NewLine + Environment.NewLine;

                foreach(KeyValuePair<string, int> type in typeCount)
                {
                    reportString += "\n\nType: " + type.Key + " Amount: " + type.Value + Environment.NewLine; 
                }
                
                txtReport.Text = reportString;

            }
        }

        private void btnCustomerAppts_Click(object sender, EventArgs e)
        {
            if (cbCustomers.SelectedItem == null)
            {
                MessageBox.Show("You must select a customer first.");
            }
            else
            {
                string name = cbCustomers.SelectedItem.ToString();
                List<Appointment> customerAppts = Helper.GetCustomerAppointments(name);

                string reportString = $"{name} Appointments:" + Environment.NewLine + Environment.NewLine;

                // lambda to simplify foreach statement
                customerAppts.ForEach(appt =>
                {
                    reportString += "\n\nStart: " + appt.start + "  Type: " + appt.type + "  Title: " + appt.title + Environment.NewLine;

                });
                txtReport.Text = reportString;
            }
        }
    }
}
