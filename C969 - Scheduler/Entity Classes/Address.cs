using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969___Scheduler.Entity_Classes
{
    public class Address
    {
        public int addressId { get; set; } 
        public string address1 { get; set; }
        public string address2 { get; set; }
        public int cityId { get; set; }
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public DateTime createDate {  get; set; }
        public string createdBy {  get; set; }
        public DateTime lastUpdate {  get; set; }
        public string lastUpdateBy {  get; set; }




    }
}
