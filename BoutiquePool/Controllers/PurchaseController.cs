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
    public class PurchaseController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Purchase> purchaseRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;



        protected PurchaseService purchaseService;



        public PurchaseController(DatabaseSettings databaseSettings)
        {
            purchaseRepository = new Repositories.MongoDB.PersistentRepository<Entities.Purchase>(databaseSettings, "sis_purchase");
            prodServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.ProdService>(databaseSettings, "cad_prod_serv");
            workerRepository = new Repositories.MongoDB.PersistentRepository<Entities.Worker>(databaseSettings, "cad_worker");
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "cad_person");


            purchaseService = new PurchaseService(purchaseRepository, prodServiceRepository, personRepository);

        }

        [HttpPost]
        public ActionResult<Entities.Purchase> Create(Entities.Purchase purchase)
        {
           
            var workerVerifySame = workerRepository.FirstOrDefault(w => w.IdUser == purchase.IDPerson);
            if(workerVerifySame != null) { 
            var productWorker = prodServiceRepository.FirstOrDefault(p => p.IdWorker == workerVerifySame.id && p.id == purchase.IDProduct );

            if (productWorker != null)
            {
                return Unauthorized(new
                {
                    error = "O id do user é o mesmo do worker",
                    title = "USER_SAME_WORKER",
                    message = "O id do user é o mesmo do worker, então não conseguiremos cadastrar uma compra para esse id",
                    status = 401,
                    instance = "/purchase"
                });
            };
            }


            return purchaseService.Create(purchase);

          
        }

        [HttpGet("get-purchase-worker/{idWorker}")]
        public ActionResult<List<Entities.Purchase>> GetAll(string idWorker)
        {
            var purchases = purchaseRepository.Find(p => p.IDWorker == idWorker);
            return purchases;
        }


        [HttpPut("{idPurchase}/{idContact}")]
        public ActionResult Update([FromRoute] string idPurchase, [FromRoute] int idContact)
        {

            var purchase = purchaseRepository.FirstOrDefault(p => p.id == idPurchase);
            if (purchase == null)
                return this.NotFound(new
                {
                    error = "Não foi possível encontrar a compra",
                    title = "PURCHASE_NOT_FOUND",
                    message = "O id da compra inserida, não foi encontrado",
                    status = 401,
                    instance = "/purchase/{idPurchase}"
                }); 

            purchaseService.Update(purchase, idContact);
            return Ok();

        }

    }
}