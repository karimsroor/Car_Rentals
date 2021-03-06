﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CarRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Car UpdateCar(Car car);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCar(int carId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(int carId);

        [OperationContract]
        Car[] GetAllCars();

        [OperationContract]
        Car[] GetAvailableCars(DateTime pickupDate, DateTime returnDate);

        #region Async operations

        Task<Car> UpdateCarAsync(Car car);

        Task DeleteCarAsync(int carId);

        Task<Car> GetCarAsync(int carId);

        Task<Car[]> GetAllCarsAsync();

        Task<Car[]> GetAvailableCarsAsync(DateTime pickupDate, DateTime returnDate);

        #endregion
    }
}
