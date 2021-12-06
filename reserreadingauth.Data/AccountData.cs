using System;
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
        
        public async Task<bool> GoogleAuthInsertAccount(Account account)
        {
            await _context.Accounts.AddAsync(account); 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GoogleAuthSelectAData(string username, string email)
        {
            Account googleAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == username && x.Email == email);
            return googleAccount;
        }
        
    }
}