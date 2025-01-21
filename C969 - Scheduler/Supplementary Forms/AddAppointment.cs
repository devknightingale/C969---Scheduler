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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm";


            // EXAMPLE SQL COMMAND FOR ADDING APPOINTMENT: 
            // NOTE: REMEMBER TIMES NEED TO BE CONVERTED TO UTC FOR THE DATABASE 

            // INSERT INTO appointment(customerId, userId, title, description, location, contact, url, type, start, end, createDate, createdBy, lastUpdateBy)
            // VALUES(1, 1, "test appt", "testing appointment add", "Cincinnati", "Contact", "N/A", "Consultation", CAST("2025-01-20 12:00:00" as DATETIME), CAST("2025-01-20 12:15:00" as DATETIME), NOW(), "test", "test")
        }
    }
}
