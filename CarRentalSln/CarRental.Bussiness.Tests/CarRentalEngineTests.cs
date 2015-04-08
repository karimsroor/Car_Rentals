using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Bussiness.Entities;
using Moq;
using CarRental.DataContracts;
using Core.Common.Contracts;
using CarRental.Bussiness.Engines;

namespace CarRental.Bussiness.Tests
{
    [TestClass]
    public class CarRentalEngineTests
    {
        [TestMethod]
        public void IsCarCurrentlyRented_any_account()
        {
            Rental rental = new Rental()
            {
                CarId = 1
            };
            Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
            mockRentalRepository.Setup(obj=> obj.GetCurrentRentalByCar(1)).Returns (rental);

            Mock<IDataRepositoryFactory> mockDataRepositoyFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoyFactory.Setup(obj => obj.GetDataRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

            CarRentalEngine engine = new CarRentalEngine(mockDataRepositoyFactory.Object);

            bool try1 = engine.IsCarCurrentlyRented(1);
            bool try2 = engine.IsCarCurrentlyRented(2);

            Assert.IsFalse(try2);
            Assert.IsFalse(try1);


        }
    }
}
