using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoutiquePool.Models.Configurations.AWS;
using BoutiquePool.Models.Configurations.MongoDB;
using BoutiquePool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;

        protected PersonService personService;
        protected MediaService mediaService;


        public PersonController(DatabaseSettings databaseSettings, S3Configuration s3Configuration, Credentials credentials)
        {
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "person");
            mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "media");

            personService = new PersonService(personRepository);
            mediaService = new MediaService(mediaRepository, credentials, s3Configuration);

        }

        [HttpPost("send-picture/{idUser}")]
        public ActionResult<Entities.Media> SendFile(string idUser, IFormFile file)
        {

            var person = personRepository.FirstOrDefault(a => a.id == idUser);
            if (person == null)
                return this.NotFound(new
                {
                    error = "O id inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O id inserido, não está associado a nenhuma conta, então não conseguiremos obter a conta",
                    status = 401,
                    instance = "/person/send-picture/{idUser}"
                });


            var info = new FileInfo(file.FileName);
            var extension = info.Extension.ToLower().Replace(".", "");
            var type = mediaService.GetTypeByExtension(extension);
            if (type == Entities.Media.Types.Unknown)
            {
                return BadRequest("INVALID_EXTENSIONS");
            }


            var media = mediaService.Create(file.OpenReadStream(), type, extension, idUser);
            person.Image = media.Url;

            personRepository.Update(person.id, person);

            return media;

        }

        [HttpPost]
        public ActionResult<Entities.Person> Create(Entities.Person person)
        {
            var newPerson = personRepository.FirstOrDefault(a => a.Email == person.Email);
            if(newPerson != null)
              return this.Unauthorized(new
                {
                    error = "O e-mail inserido já existe",
                    title = "EMAIL_ALREADY_EXISTS",
                    message = "essa conta já existe em nossa base de dados, por isso não conseguimos criar uma nova conta a partir desse e-mail",
                    status = 401,
                    instance = "/person"
                }); 

            return personService.Create(person);
        }

        [HttpGet]
        public ActionResult<List<Entities.Person>> GetAll()
        {
            var persons = personRepository.Find(s => true);
            return persons;
        }


        [HttpPut("{email}")]
        public ActionResult Update(string email, Models.PersonUpdate personModel)
        {

            var person = personRepository.FirstOrDefault(a => a.Email == email);
            if (person == null)
                return this.NotFound(new
                {
                    error = "O e-mail inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O email inserido, não está associado a nenhuma conta, então não conseguiremos atualizar a conta",
                    status = 401,
                    instance = "/person/{email}"
                }); 

            personService.Update(person, personModel);
            return Ok();

        }

        [HttpDelete("{email}")]
        public ActionResult Delete(string email)
        {

            var person = personRepository.FirstOrDefault(a => a.Email == email);
            if (person == null)
                return this.NotFound(new
                {
                    error = "O e-mail inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O email inserido, não está associado a nenhuma conta, então não conseguiremos deletar a conta",
                    status = 401,
                    instance = "/person/{email}"
                }); 

            personService.Delete(person);
            return Ok();

        }

        [HttpGet("verify-already-exists-person/{text}")]
        public ActionResult<bool> VerifyAlreadyExists(string text)
        {

            var person = personRepository.FirstOrDefault(a => a.Email == text || a.CellNumber == text || a.Username == text);
            if (person != null)
                return true;

            return false;

        }



        [HttpGet("{email}")]
        public ActionResult<Entities.Person> Get(string email)
        {

            var person = personRepository.FirstOrDefault(a => a.Email == email);
            if (person == null)
                return this.NotFound(new
                {
                    error = "O e-mail inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O email inserido, não está associado a nenhuma conta, então não conseguiremos obter a conta",
                    status = 401,
                    instance = "/person/{email}"
                }); 

            return personService.GetPerson(email);
            
        }
    }
}