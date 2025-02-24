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

            // FIX BEFORE SUBMISSION - should change the add___ functions to take in addCustomer form as an argument so I can set all the variables inside the function instead of doing it here, Would be cleaner 
            
            // Add the country first since city/address is dependent upon it (countryId as foreign key) 
            Country country = new Country();
            country.country = txtCountry.Text;
            country.createDate = DateTime.UtcNow;
            country.createdBy = Helper.userNameValue;
            country.lastUpdate = DateTime.UtcNow;
            country.lastUpdateBy = Helper.userNameValue;            
            country.countryId = Helper.addCountry(country);

            
            // Add city to database 
            City city = new City(); 
            city.city = txtCity.Text;
            city.countryId = country.countryId;
            city.createDate = DateTime.UtcNow;
            city.createdBy = Helper.userNameValue;
            city.lastUpdate = DateTime.UtcNow; 
            city.lastUpdateBy = Helper.userNameValue;             
            city.cityId = Helper.addCity(city);

            //FIX ME: Put the add address here - depends on city 
            Address address = new Address(); 
            address.address1 = txtAddress1.Text;
            //FIX ME: should address2 be optional? 
            address.address2 = txtAddress2.Text;
            address.cityId = city.cityId;
            address.postalCode = txtZip.Text;
            address.phoneNumber = txtPhone.Text;
            address.createDate = DateTime.UtcNow;
            address.createdBy = Helper.userNameValue;
            address.lastUpdate = DateTime.UtcNow;
            address.lastUpdateBy = Helper.userNameValue;
            address.addressId = Helper.addAddress(address); 

            //FIX ME: Put the add customer function here - depends on address 
            Customer newCustomer = new Customer();

            // set all the variables for customer 

            // Add customer to database 
            Helper.addCustomer(newCustomer);

            // Debug stuff 
            // For future me: 
            // The masked textboxes appear to count the "prompt" characters as input by default 
            
            MessageBox.Show($"Length of zip is now {txtZip.Text.Length}\n Zip mask completed is {txtZip.MaskCompleted}\nZip mask full is {txtZip.MaskFull}\nLength of phone number is {txtPhone.Text.Length}");

            
        }

        
    }
}
