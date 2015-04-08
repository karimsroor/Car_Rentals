using Core.Common.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using CarRental.Bussiness.Common;
using Core.Common.Core;
using CarRental.Bussiness.Contracts;
using CarRental.Bussiness.Entities;
using CarRental.DataContracts;
using Core.Common.Exceptions;
using System.ServiceModel;
using System.Runtime.Serialization;
using System;
using System.Security.Permissions;
using CarRental.Common;


namespace CarRental.Bussiness.Managers
{
    [ServiceBehavior (InstanceContextMode= InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase,IInventoryService
    {   [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

         [Import]
         IBusinessEngineFactory _BusinessEngineFactory;

    public InventoryManager()
    {
        ObjectBase.Container.SatisfyImportsOnce(this);
    }

    public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
    {
        _DataRepositoryFactory = dataRepositoryFactory;
    }

    public InventoryManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
    {
        _DataRepositoryFactory = dataRepositoryFactory;
        _BusinessEngineFactory = businessEngineFactory;
    }

   [PrincipalPermission(SecurityAction.Demand,Role= Security.CarRentalAdminRole)]
   [PrincipalPermission (SecurityAction.Demand,Role= Security.CarRentalUser)]
    public Car GetCar(int carId)
    {
        return ExecuteFaultHandledOperation (()=>{
       
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
            Car carEntity = carRepository.Get(carId);
            if (carEntity == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("car with IF {0} not avilable", carId));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }
            return carEntity;
        });
    }


   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
    public Car[] GetAllCars()
    {
        ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
        IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();

        IEnumerable<Car> cars = carRepository.Get();
        IEnumerable<Rental> rentedcars = rentalRepository.Get();

        foreach (Car car in cars)
        {
            Rental rentedCar = rentedcars.Where(item => item.CarId == car.CarId).FirstOrDefault();
            car.CurrentlyRented = (rentedcars != null);
        }

        return cars.ToArray();
        
    }


   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
    [OperationBehavior(TransactionScopeRequired = true)]
    public Car UpdateCar(Car car)
    {
        return ExecuteFaultHandledOperation(() =>
        {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

            Car updatedEntity = null;

            if (car.CarId == 0)
                updatedEntity = carRepository.Add(car);
            else
                updatedEntity = carRepository.Update(car);

            return updatedEntity;
        });
    }

   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
    [OperationBehavior(TransactionScopeRequired = true)]
    public void DeleteCar(int carId)
    {
         ExecuteFaultHandledOperation(() =>
        {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

            carRepository.Remove(carId);
        });
    }

   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
   [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
    public Car[] GetAvilableCars(DateTime pickupdate, DateTime returndate)
    {
        ICarRepository carReposiotry = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
        IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
        IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
        ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngine<ICarRentalEngine>();

        IEnumerable<Car> allCars= carReposiotry.Get();
        IEnumerable<Rental> rentedCars=rentalRepository.GetCurrentlyRentedCars();
        IEnumerable<Reservation> reservedCars = reservationRepository.Get();

        List<Car> avilablecars= new List<Car>();

        foreach (Car car in allCars)
        {
            if (carRentalEngine.IsCarAvilableForRental(car.CarId, pickupdate, returndate, rentedCars, reservedCars))
                avilablecars.Add(car);
        }
        return allCars.ToArray();

    }


    }
}
