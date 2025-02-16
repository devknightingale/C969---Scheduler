using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969___Scheduler.Entity_Classes
{
    public class Customer
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public int addressId { get ; set; }
        public bool active { get; set; }
        public string createdby { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdate { get; set; }
    }
}
