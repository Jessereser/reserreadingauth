using System.Threading.Tasks;
using reserreadingauth.common;
using ReserreadingAuth.Contract;

namespace reserreadingauth.Tests.Fakes
{
    public class AccountFakes : IAccountData
    {
        public async Task<Account> GetGoogleAuthDataAsync(string token)
        {
            Account account = new Account();
            account.Username = "TestUser";
            account.Email = "TestUser@gmail.com";
            return account;
        }

        public Task<Account> GoogleAuthInsertAccount(Account account)
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> GoogleAuthSelectAData(string username, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> InsertAccount(Account account)
        {
            throw new System.NotImplementedException();
        }

        public Task<Account> SelectAccount(string accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}