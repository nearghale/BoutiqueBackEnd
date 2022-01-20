using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class PurchaseService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Purchase> _purchaseRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Person> _personRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ProdService> _prodServiceRepository;


        public PurchaseService(Repositories.MongoDB.PersistentRepository<Entities.Purchase> purchaseRepository, 
                                Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository,
                                Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository)
        {

            _purchaseRepository = purchaseRepository;
            _prodServiceRepository = prodServiceRepository;
            _personRepository = personRepository;


        }

        public Entities.Purchase Create(Entities.Purchase purchase)
        {
            var product = _prodServiceRepository.FirstOrDefault(p => p.id == purchase.IDProduct);

            
            purchase.IDAddress = product.IdAddress;
            purchase.IDWorker = product.IdWorker;
            purchase.DateRegister = DateTime.Now;
            purchase.Facebook = 0;
            purchase.Instagram = 0;
            purchase.Whatsapp = 0;
            purchase.Site = 0;
            purchase.Phone = 0;

            var personUpdate = _personRepository.FirstOrDefault(p => p.id == purchase.IDPerson);
            personUpdate.ServicesAccessed += 1;

            _personRepository.Update(personUpdate.id, personUpdate);

            return _purchaseRepository.Create(purchase);

        }


        public void Delete(Entities.Purchase purchase)
        {
            _purchaseRepository.Remove(purchase);

        }

        public void Update(Entities.Purchase purchase, int contact)
        {
            switch (contact)
            {
                case 1:
                    purchase.Facebook += 1;
                    break;

                case 2:
                    purchase.Instagram += 1;
                    break;

                case 3:
                    purchase.Whatsapp += 1;
                    break;

                case 4:
                    purchase.Site += 1;
                    break;

                case 5:
                    purchase.Phone += 1;
                    break;


            }
            var personUpdate = _personRepository.FirstOrDefault(p => p.id == purchase.IDPerson);
            personUpdate.ServicesContact += 1;

            _personRepository.Update(personUpdate.id, personUpdate);



            _purchaseRepository.Update(purchase.id, purchase);

        }
    


    }
}
