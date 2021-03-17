using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiquePool.Models.Configurations.MongoDB;
using BoutiquePool.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        protected AccountService accountService;

        public AccountController(DatabaseSettings databaseSettings)
        {
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
            accountService = new AccountService(accountRepository);

        }

        [HttpPost]
        public ActionResult<Entities.Account> Create(Entities.Account account)
        {
            var newAccount = accountRepository.FirstOrDefault(a => a.Email == account.Email);
            if(newAccount != null)
                return Unauthorized("USER_ALREADY_EXISTS");

            return accountService.Create(account);
        }

        [HttpGet]
        public ActionResult<List<Entities.Account>> GetAll()
        {
            return accountRepository.Get();
        }


        [HttpPut("{email}")]
        public ActionResult Update(string email, Models.AccountUpdate accountModel)
        {

            var accountUser = accountRepository.FirstOrDefault(a => a.Email == email);
            if (accountUser == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            accountService.Update(accountUser, accountModel);
            return Ok();

        }

        [HttpDelete("{email}")]
        public ActionResult Delete(string email)
        {

            var accountUser = accountRepository.FirstOrDefault(a => a.Email == email);
            if (accountUser == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            accountService.Delete(accountUser);
            return Ok();

        }

        [HttpGet("{email}")]
        public ActionResult<Entities.Account> Get(string email)
        {

            var accountUser = accountRepository.FirstOrDefault(a => a.Email == email);
            if (accountUser == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            return accountService.GetAccount(email);
            
        }
    }
}