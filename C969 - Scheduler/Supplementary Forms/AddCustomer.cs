using C969___Scheduler.Database;
using C969___Scheduler.Entity_Classes;
using MySql.Data.MySqlClient;
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
        public bool isExistingCustomer = false; 
        public AddCustomer(DataGridView dgv)
        {
            InitializeComponent();


        }

        public AddCustomer(DataGridView dgv, int customerId)
        {
            InitializeComponent();
            isExistingCustomer = true; 
            Helper.customerIdValue = customerId;

            string queryCustomerUpdate = $"SELECT customer.customerName, customer.addressId, customer.active, customer.createDate, customer.createdBy, address.addressId, address.address, address.address2, address.postalCode, address.phone, city.cityId, city.city, country.countryId, country.country FROM customer INNER JOIN address ON customer.addressId = address.addressId INNER JOIN city on address.cityId = city.cityId INNER JOIN country on city.countryId = country.countryId WHERE customerId = {customerId}";
            MySqlCommand cmdCustomerUpdate = new MySqlCommand(queryCustomerUpdate, DBConnection.conn);
            MySqlDataReader readerCustomerUpdate = cmdCustomerUpdate.ExecuteReader();
            readerCustomerUpdate.Read(); 

            int addressId = (int)readerCustomerUpdate["addressId"];
            int cityId = (int)readerCustomerUpdate["cityId"];
            int countryId = (int)readerCustomerUpdate["countryId"];

            // update all dependencies first 
            string[] splitNames = readerCustomerUpdate["customerName"].ToString().Split(' ');
            txtFirstName.Text = splitNames[0];
            txtLastName.Text = splitNames[1];
            txtAddress1.Text = (string)readerCustomerUpdate["address"];
            txtAddress2.Text = (string)readerCustomerUpdate["address2"];
            txtCity.Text = (string)readerCustomerUpdate["city"];
            txtZip.Text = (string)readerCustomerUpdate["postalCode"];
            txtCountry.Text = (string)readerCustomerUpdate["country"];
            txtPhone.Text = (string)readerCustomerUpdate["phone"];

            readerCustomerUpdate.Close(); 


        }

        private void btnCancelCustomer_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSubmitCustomer_Click(object sender, EventArgs e)
        {
           
            if (Helper.validateTextboxes(this))
            {
                if (!isExistingCustomer) 
                    // Adds new customer 
                {
                    // Adds country to database 
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


                    Address address = new Address();
                    address.address1 = txtAddress1.Text;

                    if (String.IsNullOrWhiteSpace(txtAddress2.Text.ToString()))
                    {
                        address.address2 = "N/A";
                    }
                    else
                    {
                        address.address2 = txtAddress2.Text;
                    }
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
                    newCustomer.customerName = txtFirstName.Text + " " + txtLastName.Text;
                    newCustomer.addressId = address.addressId;
                    newCustomer.active = 1;
                    newCustomer.createDate = DateTime.UtcNow;
                    newCustomer.createdBy = Helper.userNameValue;
                    newCustomer.lastUpdate = DateTime.UtcNow;
                    newCustomer.lastUpdateBy = Helper.userNameValue;
                    newCustomer.customerId = Helper.addCustomer(newCustomer);
                    this.Close();
                }
                else
                {
                    // update customer 
                    Helper.UpdateCustomer(Helper.customerIdValue, this);
                    this.Close(); 
                }
            }

                

        }

       
    }
}
