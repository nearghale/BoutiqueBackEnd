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
    public class AddressController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Address> addressRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository;
        protected AddressService addressService;

        public AddressController(DatabaseSettings databaseSettings)
        {
            addressRepository = new Repositories.MongoDB.PersistentRepository<Entities.Address>(databaseSettings, "address");
            workerRepository = new Repositories.MongoDB.PersistentRepository<Entities.Worker>(databaseSettings, "worker");

            addressService = new AddressService(addressRepository);

        }

        [HttpPost("{idWorker}")]
        public ActionResult<Entities.Address> Create(string idWorker, Entities.Address address)
        {
            var newAddress = addressRepository.FirstOrDefault(a => a.Lat == address.Lat && a.Lon == address.Lon);
            if(newAddress != null)
                return this.Unauthorized(new
                {
                    error = "Essa localização já existe",
                    title = "ADDRESS_ALREADY_EXISTS",
                    message = "Essa localização já existe em nossa base de dados, pois consta que já existe essa Latitude e Longitude do endereço informado",
                    status = 401,
                    instance = "/address/{id}"
                }); 

            var worker = workerRepository.FirstOrDefault(w => w.id == idWorker);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O ID inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O ID inserido, não está associado a nenhuma conta, então não conseguiremos cadastrar um novo endereço",
                    status = 404,
                    instance = "/address/{id}"
                });


            return addressService.Create(idWorker, address);
        }

        [HttpGet]
        public ActionResult<List<Entities.Address>> GetAll()
        {
            var address = addressRepository.Find(s => true);
            return address;
        }

        [HttpGet("verify-already-exists-address/{text}")]
        public ActionResult<bool> VerifyAlreadyExists(string text)
        {

            var address = addressRepository.FirstOrDefault(a => a.IdWorker == text);
            if (address != null)
                return true;

            return false;

        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {

            var address = addressRepository.FirstOrDefault(a => a.id == id);
            if (address == null)
                return this.NotFound(new
                {
                    error = "O ID inserido não corresponde a nenhum endereço",
                    title = "ADDRESS_NOT_FOUND",
                    message = "O ID inserido, não está associado a nenhum endereço, então não conseguiremos deletar o endereço",
                    status = 404,
                    instance = "/address/{id}"
                });
            

            addressService.Delete(address);
            return Ok();

        }

        [HttpGet("{id}")]
        public ActionResult<List<Entities.Address>> Get(string id)
        {

            var address = addressRepository.FirstOrDefault(a => a.IdWorker == id);
            if (address == null)
                return this.NotFound(new
                {
                    error = "O ID do usuário inserido não corresponde a nenhum endereço",
                    title = "ADDRESS_NOT_FOUND",
                    message = "O ID do usuário inserido, não está associado a nenhum endereço, então não conseguiremos obter o endereço",
                    status = 404,
                    instance = "/address/{id}"
                });

            return addressService.GetAddressByIdWorker(id);
            
        }
    }
}