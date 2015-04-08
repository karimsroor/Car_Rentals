using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CarRental.Bussiness.Entities;
using Core.Common.Exceptions;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CarRental.Bussiness.Contracts
{   [ServiceContract]
    public interface IInventoryService
    {
       [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(int carId);
        
        [OperationContract]
        Car[] GetAllCars();

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    Car UpdateCar (Car car);

    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Allowed)]
    void DeleteCar(int carId);

    [OperationContract]
    Car[] GetAvilableCars(DateTime pickupdate, DateTime returndate);


    }
}
