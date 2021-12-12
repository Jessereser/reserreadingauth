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
        private readonly ReserreadingauthContext _context;
        private readonly AccountLogic _aLogic;

        public AccountController(ReserreadingauthContext context)
        {
            _context = context;
            _aLogic = new AccountLogic(new AccountData(context));
        }
        
        [HttpPost("googleAuth")]
        public async Task<ActionResult<Account>> GoogleAuthenticate(string token)
        {
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

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
