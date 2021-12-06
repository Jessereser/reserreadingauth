using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using reserreadingauth.common;
using reserreadingauth.Data;

namespace reserreadingauth.logic
{
    public class AccountLogic
    {
        private AccountData _aDal; 
        public AccountLogic(AccountData aData)
        {
            _aDal = aData;
        }
        
        private string Encrypt(string password)
        {
            string plainData = password;
            string hashedData = ComputeSha256Hash(plainData);

            return hashedData;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        
        public async Task<Account> GoogleAuth(string token)
        {
            Account googleAccount = new Account();
            Account checkGoogleAccount = new Account();
            googleAccount = await _aDal.GetGoogleAuthDataAsync(token);
            checkGoogleAccount = _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
            if (checkGoogleAccount.Id != null)
            {
                googleAccount = _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
                return googleAccount;
            }
            else
            {
                _aDal.GoogleAuthInsertAccount(googleAccount.Username, googleAccount.Email);
                googleAccount = _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
                return googleAccount;
            }
        }
    }
}