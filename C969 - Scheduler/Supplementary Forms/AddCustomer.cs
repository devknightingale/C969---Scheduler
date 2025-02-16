using C969___Scheduler.Entity_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Scheduler.Supplementary_Forms
{
    public partial class AddCustomer : Form
    {
        public AddCustomer(DataGridView dgv)
        {
            InitializeComponent();

            //setup the variables ahead of time
            string lastName;
            string firstName;
            string address; 
            string city;
            string country;
            string state;
            int zipcode; 
            string phoneNumber;  


        }

        private void btnCancelCustomer_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSubmitCustomer_Click(object sender, EventArgs e)
        {
            Helper.validateTextboxes(this);

            //FIX ME: Put the add customer function here 
            Customer newCustomer = new Customer();

            // set all the variables for customer 

            // Add city to database 
            Helper.addCity();
            // Add country to database 
            Helper.addCountry();
            // Add address to database 
            Helper.addAddress(); 
            // Add customer to database 
            Helper.addCustomer(newCustomer);

            // Debug stuff 
            // For future me: 
            // The masked textboxes appear to count the "prompt" characters as input by default 
            
            MessageBox.Show($"Length of zip is now {txtZip.Text.Length}\n Zip mask completed is {txtZip.MaskCompleted}\nZip mask full is {txtZip.MaskFull}\nLength of phone number is {txtPhone.Text.Length}");

            
        }

        
    }
}
