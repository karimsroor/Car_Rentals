using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using Core.Common;
using Core.Common.Core;
using CarRental.Bussiness.Bootstrapper;
using CarRental.Bussiness.Entities;
using CarRental.DataContracts;
using System.Collections.Generic;
using Moq;
using Core.Common.Contracts;

namespace CarRental.Data.Tests
{
    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass repoistory_test = new RepositoryTestClass();
            IEnumerable<Car> cars = repoistory_test.GetCars();
            Assert.IsTrue(cars != null);
        }

        [TestMethod]
        public void test_repository_mocking()
        {
            List<Car> cars = new List<Car>()
            {
                new Car() { CarId = 1, Description = "Mustang" },
                new Car() { CarId = 2, Description = "Corvette" }
            };

            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            RepositoryTestClass repository_test = new RepositoryTestClass(mockCarRepository.Object);
            IEnumerable<Car> ret = repository_test.GetCars();
            Assert.IsTrue(ret == cars);
        }

        [TestMethod]
        public void test_factory_mocking2()
        {
            List<Car> cars = new List<Car>()
            {
                new Car() { CarId = 1, Description = "Mustang" },
                new Car() { CarId = 2, Description = "Corvette" }
            };

            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<ICarRepository>()).Returns(mockCarRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepositoryFactory.Object);

            IEnumerable<Car> ret = factoryTest.GetCars();

            Assert.IsTrue(ret == cars);

        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Import]
        ICarRepository _carRepository;

        public IEnumerable<Car> GetCars()
            
        {
            IEnumerable<Car> cars = _carRepository.Get();
            return cars;
        }
    }

    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass( IDataRepositoryFactory  dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<Car> GetCars()
        {
            ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();

            IEnumerable<Car> cars = carRepository.Get();
            return cars;
        }

    }
}
