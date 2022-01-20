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
    public class OpeningHoursController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Address> addressRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> openingHoursRepository;
        protected OpeningHoursService openingHoursService;

        public OpeningHoursController(DatabaseSettings databaseSettings)
        {
            openingHoursRepository = new Repositories.MongoDB.PersistentRepository<Entities.OpeningHours>(databaseSettings, "opening_hours");
            addressRepository = new Repositories.MongoDB.PersistentRepository<Entities.Address>(databaseSettings, "address");

            openingHoursService = new OpeningHoursService(openingHoursRepository, addressRepository);

        }

        [HttpPost("{idAdress}")]
        public ActionResult<Entities.OpeningHours> Create(string idAdress, Entities.OpeningHours openingHours)
        {
            
            var address = addressRepository.FirstOrDefault(a => a.id == idAdress);
            if (address == null)
                return this.NotFound(new
                {
                    error = "O id inserido, não corresponde a nenhum endereço",
                    title = "ADDRESS_NOT_FOUND",
                    message = "O id inserido, não corresponde a nenhum endereço, então não conseguiremos obter os dados",
                    status = 404,
                    instance = "/openingHours/{idAddress}"
                });


            return openingHoursService.Create(idAdress, openingHours);
        }

    }
}