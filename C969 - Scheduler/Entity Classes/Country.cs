﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969___Scheduler.Entity_Classes
{
    public class Country
    {
        public int countryId {  get; set; }
        public string country { get; set; }
        public DateTime createDate { get ; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }
    }
}
