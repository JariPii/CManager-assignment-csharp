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
        public void DeleteCustomer_WithEmptyGuid_ReturnFalse()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>());

            var service = new CustomerService(mockCustomerRepo.Object);

            var result = service.DeleteCustomer(Guid.Empty);

            Assert.False(result);
            mockCustomerRepo.Verify(r => r.GetAllCustomers(), Times.Never);
        }

        [Fact]
    }
}
