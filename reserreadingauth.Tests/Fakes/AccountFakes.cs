using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Account> Login(Account account)
        {
            Account DBAccount = new Account()
            {
                Id = "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
                Username = "TestUser",
                Email = "TestEmail@gmail.com",
                Password = "7bcf9d89298f1bfae16fa02ed6b61908fd2fa8de45dd8e2153a3c47300765328"
            };
            if(DBAccount.Email == account.Email && DBAccount.Password == account.Password)
            {
                return DBAccount;
            }
            else
            {
                return new Account();
            }
        }
    }
}