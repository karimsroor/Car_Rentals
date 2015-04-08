using System;
using CarRental.Bussiness.Common;
using CarRental.Bussiness.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using CarRental.DataContracts;
using CarRental.Common;
using Core.Common.Exceptions;


namespace CarRental.Bussiness.Engines
{
    [Export(typeof(ICarRentalEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRentalEngine : ICarRentalEngine
    {
        IDataRepositoryFactory _DataRepositoryFactory;

        [ImportingConstructor]
        public CarRentalEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }


        public bool IsCarCurrentlyRented(int carId, int accountId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null && currentRental.AccountId == accountId)
                rented = true;

            return rented;
        }

        public bool IsCarCurrentlyRented(int carId)
        {
            bool rented = false;

            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Rental currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null)
                rented = true;

            return rented;
        }

        public bool IsCarAvilableForRental(int carId,DateTime pickupDate,DateTime returnDate, 
            IEnumerable<Rental> rentedCars,IEnumerable<Reservation> reservedCars)
        {
            bool avilable = true;

            Reservation reservation = reservedCars.Where(item => item.CarId == carId).FirstOrDefault();
            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
            {
                avilable = false;
            }

            if (avilable)
            {
                Rental rental = rentedCars.Where(item => item.CarId == carId).FirstOrDefault();
                if (rental != null && (pickupDate <= rental.DateDue))
                    avilable = false;
            }

            return avilable;
        }

        public Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            if (rentalDate > DateTime.Now)
                throw new UnableToRentForDateException(string.Format("Cannot rent for date {0} yet.", rentalDate.ToShortDateString()));

            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

            bool carIsRented = IsCarCurrentlyRented(carId);
            if (carIsRented)
                throw new CarCurrentlyRentedException(string.Format("Car {0} is already rented.", carId));

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
                throw new NotFoundException(string.Format("No account found for login '{0}'.", loginEmail));

            Rental rental = new Rental()
            {
                AccountId = account.AccountId,
                CarId = carId,
                DateRented = rentalDate,
                DateDue = dateDueBack
            };

            Rental savedEntity = rentalRepository.Add(rental);

            return savedEntity;
        }
    }
}
