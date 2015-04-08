using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CarRental.Bussiness.Entities;

namespace CarRental.DataContracts
{
    public interface ICarRepository :IDataRepository<Car>
    {
    }
}
