using System;
using System.Collections.Generic;
using System.Text;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;
using CManager.Domain.Models;
using Moq;

namespace CManager.Tests.ServiceTests
{
    public class CustomerServiceTest
    {
        [Fact]
        public void CreateCustomer_ShouldReturnTrue_And_CallSaveOnce()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var customerList = new List<CustomerModel>();

            mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(customerList);
            mockCustomerRepo.Setup(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>())).Returns(true);

            var service = new CustomerService(mockCustomerRepo.Object);

            var result = service.CreateCustomer("Test", "Testsson", "har@epost.com", "456654456", "nånstans", "666", "downunder");

            Assert.True(result);

            mockCustomerRepo.Verify(r => r.SaveCustomers(It.Is<List<CustomerModel>>(a => a.Count == 1)), Times.Once);
        }

        [Fact]
        public void CreateCustomer_ShouldReturnFalse_WhenSaveFails()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();

            mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>());

            var service = new CustomerService(mockCustomerRepo.Object);

            var result = service.CreateCustomer("Test", "Testsson", "har@epost.com", "456654456", "nånstans", "666", "downunder");

            Assert.False(result);

        }
    }
}
