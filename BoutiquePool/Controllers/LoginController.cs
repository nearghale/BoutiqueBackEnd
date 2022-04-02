using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiquePool.Models.Configurations.MongoDB;
using BoutiquePool.Services;
using Microsoft.AspNetCore.Mvc;
using Unieco.Helpers;
namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Purchase> purchaseRepository;


        protected LoginService loginService;

        public LoginController(DatabaseSettings databaseSettings)
        {
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "cad_person");
            workerRepository = new Repositories.MongoDB.PersistentRepository<Entities.Worker>(databaseSettings, "cad_worker");
            prodServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.ProdService>(databaseSettings, "cad_prod_serv");
            purchaseRepository = new Repositories.MongoDB.PersistentRepository<Entities.Purchase>(databaseSettings, "sis_purchase");


            loginService = new LoginService(personRepository, workerRepository, prodServiceRepository, purchaseRepository);
        }

      

        [HttpPost("descriptografar/{id}")]

        public ActionResult DescriptografarOnlyPerson(string id)
        {
            var person = personRepository.FirstOrDefault(p => p.id == id);

          
                var senhaDescrip = CriptoHelper.Decrypt(person.Password);
                person.Password = senhaDescrip;
                personRepository.Update(person.id, person);


            return Ok();
        }

        [HttpPost("criptografar/{id}")]

        public ActionResult CriptografarOnlyPerson(string id)
        {
            var person = personRepository.FirstOrDefault(p => p.id == id);


            var senhaDescrip = CriptoHelper.Encrypt(person.Password);
            person.Password = senhaDescrip;
            personRepository.Update(person.id, person);


            return Ok();
        }



        [HttpPost]
        public ActionResult<Models.PersonInformation> Login(Models.Login login)
        {

            var person = personRepository.FirstOrDefault(c => c.Username == login.user_name);
     

            if (person == null)
                return this.NotFound(new
                {
                    error = "O nome de usuário inserido não corresponde a nenhuma conta",
                    title = "USERNAME_NOT_FOUND",
                    message = "O nome de usuário inserido não estão associadas a nenhuma conta, então não conseguiremos autenticar",
                    status = 404,
                    instance = "/login"
                });


            if(CriptoHelper.Decrypt(person.Password) != login.password)
                return this.NotFound(new
                {
                    error = "A senha inserida está incorreta",
                    title = "PASSWORD_INCORRECT",
                    message = "A senha inserida está incorreta, então não conseguiremos autenticar",
                    status = 404,
                    instance = "/login"
                });




            person.DateLastLogin = DateTime.Now;

            personRepository.Update(person.id, person);

            return loginService.Login(person);
            
        }

        [HttpGet("get-information-person/{id}")]
        public ActionResult<Models.PersonInformation> GetInformationsPerson(string id)
        {

            return loginService.GetPersonInformationPerson(id);

        }


    }
}