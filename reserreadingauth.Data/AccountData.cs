using System;
using System.Threading.Tasks;
using Google.Apis.Auth;
using reserreadingauth.common;

namespace reserreadingauth.Data
{
    public class AccountData
    {
        private readonly ReserreadingauthContext _context;
        public AccountData(ReserreadingauthContext context)
        {
            _context = context;
        }
        
        
        private void PrintException()
        {
            Console.WriteLine("!! ERROR !! --> Not able to connect to specified database\n Possible Solution --> Connect to the school VPN\n");
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
            try
            {
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return ();
            }
            catch ()
            {
                PrintException();
            }
            return false;
                
            
        }
    }
}