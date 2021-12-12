using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using reserreadingauth.common;
using ReserreadingAuth.Contract;
using reserreadingauth.Data;
using reserreadingauth.logic;
using reserreadingauth.Tests.Fakes;

namespace reserreadingauth.Tests
{
    public class AccountTests
    { 
        
        [Test]
        public void Encrypt_PasswordTest123Encrypts_True()
        {
            //Arrange test
            string password = "Test123";
            AccountLogic _aLogic = new AccountLogic();
            
            //Act test
            string result = _aLogic.Encrypt(password);
            
            //Assert test
            Assert.AreNotEqual("Test123", result);
        }
        [Test]
        public async void TakeUser_WithExistingId_True()
        {
            //Arrange test
            Account account = new Account()
            {
                Id = "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
                Username = "TestUser",
                Email = "TestEmail@gmail.com",
                Password = "TestPassword"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            Account accountEqual = await _aLogic.GetUser(account.Id);

            //Assert test

            Assert.AreEqual(account.Id, accountEqual.Id);
        }

        [Test]
        public async void TakeUser_WithNonExistingId_True()
        {
            Account account = new Account()
            {
                Id = "2318008",
                Username = "TestUser",
                Email = "TestEmail@gmail.com",
                Password = "TestPassword"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            Account accountEqual = await _aLogic.GetUser(account.Id);

            //Assert test

            Assert.AreNotEqual(account.Id, accountEqual.Id);
        }
    }
}