using System;
using Xunit;

namespace Domain.Tests
{
    public class ContractTests
    {
        [Fact]
        public void When_CreatingNewContract_Expect_Successfully()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            // Act

            var contract = new Contract(
                subject,
                address,
                price,
                clientId);

            //Assert

            Assert.Equal(subject, contract.Subject);
            Assert.Equal(address, contract.Address);
            Assert.Equal(price, contract.Price);
            Assert.Equal(clientId, contract.ClientId);
        }

        [Fact]
        public void When_AddEmployee_Expect_EmployeeWasAddedToCollection()
        {
            // Arrenge

            var name = "name";

            var surname = "surname";

            var positionId = 0;

            var employee = new Employee(
                name, 
                surname, 
                positionId);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            // Act

            contract.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(contract.Employees);
        }

        [Fact]
        public void When_AddWorkPlan_Expect_PlanWasAddedToCollection()
        {
            // Arrenge

            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            var workPlan = new WorkPlan(
                dateStart, 
                dateFinish, 
                contractId);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            // Act

            contract.AddWorkPlan(workPlan);

            // Assert

            Assert.NotEmpty(contract.WorkPlans);
        }

        [Fact]
        public void When_ChangeSubject_Expect_ChangeSubjectWasAddedToContract()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            var newSubject = "newSubject";

            // Act

            contract.ChangeSubject(newSubject);

            //Assert

            Assert.Equal(newSubject, contract.Subject);
        }

        [Fact]
        public void When_ChangeAddress_Expect_ChangeAddressWasAddedToContract()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            var newAddress = "newAddress";

            // Act

            contract.ChangeAddress(newAddress);

            //Assert

            Assert.Equal(newAddress, contract.Address);
        }

        [Fact]
        public void When_ChangePrice_Expect_ChangePriceWasAddedToContract()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            var newPrice = 1;

            // Act

            contract.ChangePrice(newPrice);

            //Assert

            Assert.Equal(newPrice, contract.Price);
        }

        [Fact]
        public void When_ChangeClientId_Expect_ChangeClientIdWasAddedToContract()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            var newClientId = 1;

            // Act

            contract.ChangeClientId(newClientId);

            //Assert

            Assert.Equal(newClientId, contract.ClientId);
        }
    }
}
