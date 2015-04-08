using CarRental.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace CarRental.DataContracts
{
    public interface IAccountRepository : IDataRepository <Account>
    {
        Account GetByLogin(string login);
    }
}
