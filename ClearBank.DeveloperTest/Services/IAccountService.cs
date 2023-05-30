using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        Account GetAccount(string accountNumber);
        void DeductFromBalance(Account account, decimal amount);
    }
}
