using CarRental.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace CarRental.DataContracts
{
    public interface IRentalRepository: IDataRepository <Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(int carId);
        Rental GetCurrentRentalByCar(int carId);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<Rental> GetRentalHistoryByAccount(int accountId);
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
    }
}
