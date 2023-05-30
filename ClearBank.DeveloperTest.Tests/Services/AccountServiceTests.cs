using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestClass]
    public class AccountServiceTests
    {
        private Mock<IAccountDataStore> _accountDataStoreMock;
        private AccountService _accountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountDataStoreMock = new Mock<IAccountDataStore>();
            _accountService = new AccountService(_accountDataStoreMock.Object);
        }

        [TestMethod]
        public void GetAccount_AccountNumber_ReturnsAccount()
        {
            // Arrange
            var accountNumber = "12345678";
            var expectedAccount = new Account { AccountNumber = accountNumber };

            _accountDataStoreMock.Setup(ds => ds.GetAccount(accountNumber)).Returns(expectedAccount);

            // Act
            var actualAccount = _accountService.GetAccount(accountNumber);

            // Assert
            Assert.AreEqual(expectedAccount, actualAccount);
        }

        [TestMethod]
        public void DeductFromBalance_UpdatesAccountBalance()
        {
            // Arrange
            var account = new Account { Balance = 500 };
            var amountToDeduct = 100;

            // Act
            _accountService.DeductFromBalance(account, amountToDeduct);

            // Assert
            Assert.AreEqual(400, account.Balance);
            _accountDataStoreMock.Verify(ds => ds.UpdateAccount(account), Times.Once);
        }
    }
}
