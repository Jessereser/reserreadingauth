using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using reserreadingauth.Data;
using reserreadingauth.common;
using reserreadingauth.logic;

namespace reserreadingauth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountLogic _aLogic;

        public AccountController(ReserreadingauthContext context)
        {
            _aLogic = new AccountLogic(new AccountData(context));
        }
        
        [HttpPost("googleAuth")]
        public async Task<ActionResult<Account>> GoogleAuthenticate(string token)
        {
            Console.WriteLine("entered");
            Account account = await _aLogic.GoogleAuth(token);
            
            return account;
        }
        
        [HttpPost]
        public async Task<ActionResult<Account>> Register(Account account, string confPassword)
        {
            Account checkAccount = await _aLogic.Register(account, confPassword);
            if (checkAccount.Id == null)
            {
                return BadRequest();
            }
            return checkAccount;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> Login(string email, string password)
        {
            Account account = await _aLogic.Login(new Account()
            {
                Email = email,
                Password = password,
            });
            return account;
        }
        
        [HttpGet("all")]
        public async Task<ActionResult<List<Account>>> GetAll()
        {
            return await _aLogic.GetAll();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            Account account =  await _aLogic.GetUser(id);
            if (account.Id != id)
            {
                return NotFound();
            }
            return account;
        }
        
        
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }*/
        
    }
}
