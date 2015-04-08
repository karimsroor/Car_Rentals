using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Bussiness.Entities;
using Core.Common.Contracts;
using System.Runtime.Serialization;

namespace CarRental.Bussiness.Common
{
    public interface ICarRentalEngine : IBusinessEngine
    {
         bool IsCarAvilableForRental(int carId,DateTime pickupDate,DateTime returnDate, 
            IEnumerable<Rental> rentedCars,IEnumerable<Reservation> reservedCars) ;

         bool IsCarCurrentlyRented(int carId, int accountId);
         bool IsCarCurrentlyRented(int carId);
         
         Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);
        
    }
}
