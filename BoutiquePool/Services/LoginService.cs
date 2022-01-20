using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class LoginService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Person> _personRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Worker> _workerRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ProdService> _prodServiceRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Purchase> _purchaseRepository;


        public LoginService(Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository,
                            Repositories.MongoDB.PersistentRepository<Entities.Purchase> purchaseRepository)
        {

            _prodServiceRepository = prodServiceRepository;
            _purchaseRepository = purchaseRepository;
            _personRepository = personRepository;
            _workerRepository = workerRepository;


        }

        public Models.PersonInformation Login(Entities.Person person)
        {
           
            return GetInformation(person);

        }

        public Models.PersonInformation GetPersonInformationPerson(string id)
        {
            var person = _personRepository.FirstOrDefault(p => p.id == id);
            return GetInformation(person);

        }

        private Models.PersonInformation GetInformation(Entities.Person person)
        {

            var personInformation = new Models.PersonInformation();

            personInformation.id = person.id;
            personInformation.cell_number = person.CellNumber;
            personInformation.email = person.Email;
            personInformation.image = person.Image;
            personInformation.name = person.Name;
            personInformation.type_user = person.TypeUser;
            personInformation.services = person.Services;
            personInformation.services_accessed = person.ServicesAccessed;
            personInformation.services_contact = person.ServicesContact;


            if (person.TypeUser == "E")
            {
                var worker = _workerRepository.FirstOrDefault(w => w.IdUser == person.id);
                var prodServicesWorker = _prodServiceRepository.Find(p => p.IdWorker == worker.id);
                personInformation.services = prodServicesWorker.Count();
               
            }

            if (person.TypeUser == "C")
            {
                personInformation.services = 0;
             
            }


            return personInformation;



        }


    }
}
