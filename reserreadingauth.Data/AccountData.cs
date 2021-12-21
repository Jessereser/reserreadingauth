using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using reserreadingauth.common;
using ReserreadingAuth.Contract;

namespace reserreadingauth.Data
{
    public class AccountData : IAccountData
    {
        private readonly ReserreadingauthContext _context;
        public AccountData(ReserreadingauthContext context)
        {
            _context = context;
        }
        
        public async Task <Account> GetGoogleAuthDataAsync (string token)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
            if (validPayload != null)
            {
                Account googleAccount = new Account();
                googleAccount.Email = validPayload.Email;
                googleAccount.Username = validPayload.Name;
                return googleAccount;
            }
            Account nepAccount = new Account();
            nepAccount.Email = null;
            return nepAccount;
        }
        public async Task<Account> GoogleAuthInsertAccount(Account account)
        {
            await _context.Accounts.AddAsync(account); 
            await _context.SaveChangesAsync();
            return account;
        }
        public async Task<Account> GoogleAuthSelectAData(string username, string email)
        {
            Account googleAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == username && x.Email == email);
            return googleAccount;
        }
        public async Task<Account> InsertAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> SelectAll()
        {
            List<Account> accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }
        
        
        public async Task<Account> Login(Account account)
        {
            try
            {
                account = await _context.Accounts.FirstOrDefaultAsync(x =>
                    x.Email == account.Email && x.Password == account.Password);
                return account;
            }
            catch
            {
                return account;
            }
        }
        public async Task<Account> SelectAccount(string accountId)
        {
            Account account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == accountId);
            return account;
        }
    }
}