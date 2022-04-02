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
    public class WorkerController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;

        protected WorkerService workerService;
        protected MediaService mediaService;


        public WorkerController(DatabaseSettings databaseSettings, S3Configuration s3Configuration, Credentials credentials)
        {
            workerRepository = new Repositories.MongoDB.PersistentRepository<Entities.Worker>(databaseSettings, "cad_worker");
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "cad_person");
            mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "aux_media");


            workerService = new WorkerService(workerRepository, personRepository);
            mediaService = new MediaService(mediaRepository, credentials, s3Configuration);


        }

        [HttpPost("{idUser}/{typeUser}")]
        public ActionResult<Entities.Worker> Create(string idUser, string typeUser, Entities.Worker worker)
        {
            var newWorker = workerRepository.FirstOrDefault(w => w.CPFCNPJ == worker.CPFCNPJ);
            if (newWorker != null && newWorker.CPFCNPJ != "00000000000000")
                return Unauthorized(new
                {
                    error = "O cnpj inserido, já corresponde a uma conta",
                    title = "WORKER_ALREADY_EXISTS",
                    message = "O cpf inserido, já corresponde a uma conta, então não conseguiremos cadastrar uma nova conta com esse cnpj",
                    status = 404,
                    instance = "/worker/{idUser}"
                });

            return workerService.Create(idUser, typeUser, worker);
        }

        [HttpPost("send-picture/{idWorker}")]
        public ActionResult<Entities.Media> SendFile(string idWorker, IFormFile file)
        {

            var worker = workerRepository.FirstOrDefault(w => w.id == idWorker);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O id inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O id inserido, não está associado a nenhuma conta, então não conseguiremos obter a conta",
                    status = 401,
                    instance = "/worker/send-picture/{idWorker}"
                });


            var info = new FileInfo(file.FileName);
            var extension = info.Extension.ToLower().Replace(".", "");
            var type = mediaService.GetTypeByExtension(extension);
            if (type == Entities.Media.Types.Unknown)
            {
                return BadRequest("INVALID_EXTENSIONS");
            }


            var media = mediaService.Create(file.OpenReadStream(), type, extension, idWorker);
            worker.Image = media.Url;

            workerRepository.Update(worker.id, worker);

            return media;

        }

        [HttpGet("verify-already-exists-worker/{text}")]
        public ActionResult<bool> VerifyAlreadyExists(string text)
        {
             if(text == "00000000000000")
                return false;
            

            var worker = workerRepository.FirstOrDefault(w => w.CPFCNPJ == text);
            if (worker != null)
                return true;

            return false;

        }

        [HttpGet]
        public ActionResult<List<Entities.Worker>> GetAll()
        {
            var workers = workerRepository.Find(w => true);
            return workers;
        }


        [HttpPut("{cpfcnpj}")]
        public ActionResult Update(string cpfcnpj, Models.WorkerUpdate workerModel)
        {

            var worker = workerRepository.FirstOrDefault(w => w.CPFCNPJ == cpfcnpj);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O cnpj inserido, não corresponde a nenhum fornecedor ou vendedor",
                    title = "WORKER_NOT_FOUND",
                    message = "O cpf inserido, não corresponde a nenhum fornecedor ou vendedor, então não conseguiremos obter os dados",
                    status = 404,
                    instance = "/worker/{cpfcnpj}"
                });

            workerService.Update(worker, workerModel);
            return Ok();

        }

        [HttpDelete("{cpfcnpj}")]
        public ActionResult Delete(string cpfcnpj)
        {

            var worker = workerRepository.FirstOrDefault(w => w.CPFCNPJ == cpfcnpj);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O cnpj inserido, não corresponde a nenhum fornecedor ou vendedor",
                    title = "WORKER_NOT_FOUND",
                    message = "O cpf inserido, não corresponde a nenhum fornecedor ou vendedor, então não conseguiremos obter os dados",
                    status = 404,
                    instance = "/worker/{cpfcnpj}"
                });

            workerService.Delete(worker);
            return Ok();

        }

        [HttpGet("{cpfcnpj}")]
        public ActionResult<Entities.Worker> Get(string cpfcnpj)
        {

            var worker = workerRepository.FirstOrDefault(w => w.CPFCNPJ == cpfcnpj);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O cnpj inserido, não corresponde a nenhum fornecedor ou vendedor",
                    title = "WORKER_NOT_FOUND",
                    message = "O cpf inserido, não corresponde a nenhum fornecedor ou vendedor, então não conseguiremos obter os dados",
                    status = 404,
                    instance = "/worker/{cpfcnpj}"
                });

            return workerService.GetWorker(cpfcnpj);
            
        }
    }
}