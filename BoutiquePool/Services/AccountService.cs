using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class AccountService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Account> _accountRepository;

        public AccountService(Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository)
        {

            _accountRepository = accountRepository;
           

        }

        public Entities.Account Create(Entities.Account account)
        {
            var newAccount = new Entities.Account();

            newAccount.BirthDate = account.BirthDate;
            newAccount.Email = account.Email;
            newAccount.Name = account.Name;
            newAccount.Profession = account.Profession;

            return _accountRepository.Create(newAccount);


        }
        public void Delete(Entities.Account account)
        {
            _accountRepository.Remove(account);

        }

        public void Update(Entities.Account account, Models.AccountUpdate accountUpdate)
        {
            account.Name = accountUpdate.name;
            account.BirthDate = accountUpdate.birth_date;
            account.Profession = accountUpdate.profession;

      
            _accountRepository.Update(account.id, account);

        }
        public Entities.Account GetAccount(string email)
        {
            return _accountRepository.FirstOrDefault(a => a.Email == email);
        }


    }
}
