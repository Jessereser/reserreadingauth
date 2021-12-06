using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reserreadingauth.common;
using ReserreadingAuth.Contract;
using reserreadingauth.Data;

namespace reserreadingauth.logic
{
    public class AccountLogic
    {
        private IAccountData _aDal; 
        public AccountLogic(IAccountData aData)
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
            Account googleAccount = await _aDal.GetGoogleAuthDataAsync(token);
            Account checkGoogleAccount = await _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
            if (checkGoogleAccount.Id != null)
            {
                googleAccount = await _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
                return googleAccount;
            }
            else
            {
                googleAccount.Id = Guid.NewGuid().ToString();
                await _aDal.GoogleAuthInsertAccount(googleAccount);
                googleAccount = await _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
                return googleAccount;
            }
        }
    }
}