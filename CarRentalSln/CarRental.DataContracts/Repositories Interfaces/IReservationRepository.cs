using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CarRental.Bussiness.Entities;

namespace CarRental.DataContracts
{
    public interface IReservationRepository :IDataRepository<Reservation>
    {
        IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate);
        IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo();
        IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId);
    }
}
