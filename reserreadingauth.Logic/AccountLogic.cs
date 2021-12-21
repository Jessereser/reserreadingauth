using System;
using System.Collections.Generic;
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

        public AccountLogic()
        {
        }
        public AccountLogic(IAccountData aData)
        {
            _aDal = aData;
        }
        
        
        public async Task<string> Encrypt(string password)
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
        
        public async Task<Account> Register(Account account, string confPassword)
        {
            if (account.Password == confPassword)
            {
                account.Password = await Encrypt(account.Password);
                account.Id = Guid.NewGuid().ToString();
                return await _aDal.InsertAccount(account);
            }
            else
            {
                return new Account();
            }
        }

        public async Task<Account> Login(Account account)
        {
            account.Password = await Encrypt(account.Password);
            account =  await _aDal.Login(account);
            return account;
        }

        public async Task<Account> GetUser(string accountId)
        {
            return await _aDal.SelectAccount(accountId);
        }
        
        public async Task<List<Account>> GetAll ()
        { 
            return await _aDal.SelectAll();;
        }

        public async Task<Account> GoogleAuth(string token)
        {
            Account googleAccount = await _aDal.GetGoogleAuthDataAsync(token);
            if (googleAccount.Username != null)
            {
                Account checkGoogleAccount = await _aDal.GoogleAuthSelectAData(googleAccount.Username, googleAccount.Email);
                if (checkGoogleAccount.Id != null)
                {
                    return checkGoogleAccount;
                }
                else
                {
                    googleAccount.Id = Guid.NewGuid().ToString();
                    Account account = await _aDal.GoogleAuthInsertAccount(googleAccount);
                    return account;
                }
            }
            else
            {
                return new Account();
            }
        }
    }
}