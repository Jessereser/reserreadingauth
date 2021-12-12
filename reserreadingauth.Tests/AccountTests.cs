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
            AccountLogic _aLogic = new AccountLogic(new AccountFakes());
            
            //Act test
            string result = _aLogic.Encrypt(password);
            
            //Assert test
            Assert.AreNotEqual("Test123", result);
        }
    }
}