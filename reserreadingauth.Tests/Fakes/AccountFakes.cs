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

        public async Task<Account> SelectAccount(string accountId)
        {
            Account account = new Account()
            {
                Id = "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
                Username = "TestUser",
                Email = "TestEmail@gmail.com",
                Password = "TestPassword"
            };
            return account;
        }
    }
}