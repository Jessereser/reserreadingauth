using System;
using System.Threading.Tasks;
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
        public async Task Encrypt_PasswordTestPasswordEncryptAreNotEqual_True()
        {
            //Arrange test
            string password = "TestPassword";
            AccountLogic _aLogic = new AccountLogic();
            
            //Act test
            string result = await _aLogic.Encrypt(password);
            Console.WriteLine(result);
            
            //Assert test
            Assert.AreNotEqual("TestPassword", result);
        }
        
        [Test]
        public async Task Encrypt_PasswordTestPasswordEncryptAreEqual_True()
        {
            //Arrange test
            string password = "TestPassword";
            AccountLogic _aLogic = new AccountLogic();
            
            //Act test
            string result = await _aLogic.Encrypt(password);
            Console.WriteLine(result);
            
            //Assert test
            Assert.AreEqual("7bcf9d89298f1bfae16fa02ed6b61908fd2fa8de45dd8e2153a3c47300765328", result);
        }
        
        [Test]
        public async Task TakeUser_WithExistingId_True()
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
        public async Task TakeUser_WithNonExistingId_True()
        {
            //Arrange test
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

        [Test]
        public async Task Login_withCorrectCredentials_True()
        {
            //Arrange test
            Account account = new Account()
            {
                Email = "TestEmail@gmail.com",
                Password = "TestPassword"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            account = await _aLogic.Login(account);
            
            //Assert test
            Assert.AreNotEqual(account.Id, null);
        }
        
        [Test]
        public async Task Login_withWrongPassword_True()
        {
            //Arrange test
            Account account = new Account()
            {
                Email = "TestEmail@gmail.com",
                Password = "null"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            account = await _aLogic.Login(account);
            
            //Assert test
            Assert.AreEqual(account.Id, null);
        }
        
        [Test]
        public async Task Login_withWrongEmail_True()
        {
            //Arrange test
            Account account = new Account()
            {
                Email = "null",
                Password = "TestPassword"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            account = await _aLogic.Login(account);
            
            //Assert test
            Assert.AreEqual(account.Id, null);
        }
        
        [Test]
        public async Task Login_withWrongCredentials_True()
        {
            //Arrange test
            Account account = new Account()
            {
                Email = "null",
                Password = "null"
            };
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());

            //Act test

            account = await _aLogic.Login(account);
            
            //Assert test
            Assert.AreEqual(account.Id, null);
        }
    }
}