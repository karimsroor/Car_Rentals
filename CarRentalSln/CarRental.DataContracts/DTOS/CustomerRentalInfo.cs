using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Bussiness.Entities;
using System.Runtime.Serialization;

namespace CarRental.DataContracts
{
    public class CustomerRentalInfo
    {
        public Account Customer { get; set; }
        public Car Car { get; set; }
        public Rental Rental { get; set; }
    }
}
