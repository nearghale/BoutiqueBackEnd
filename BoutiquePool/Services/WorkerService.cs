using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class WorkerService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Worker> _workerRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Person> _personRepository;


        public WorkerService(Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository, Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository)
        {

            _workerRepository = workerRepository;
            _personRepository = personRepository;



        }

        public Entities.Worker Create(string idUser, string typeUser, Entities.Worker worker)
        {
            var newWorker = new Entities.Worker();

            newWorker.IdUser = idUser;
            newWorker.Active = true;
            newWorker.CPFCNPJ = worker.CPFCNPJ;
            newWorker.Image = worker.Image;
            newWorker.DateRegister = DateTime.Now;
            newWorker.Description = worker.Description;
            newWorker.EmailCorp = worker.EmailCorp;
            newWorker.Facebook = worker.Facebook;
            newWorker.Instagram = worker.Instagram;
            newWorker.Office = worker.Office;
            newWorker.PhoneCorp = worker.PhoneCorp;
            newWorker.Site = worker.Site;
            newWorker.SocialReason = worker.SocialReason;
            newWorker.WhatsappNumber = worker.WhatsappNumber;

            var personUpdateType = _personRepository.FirstOrDefault(c => c.id == idUser);
            personUpdateType.TypeUser = typeUser;
            _personRepository.Update(idUser, personUpdateType);


            return _workerRepository.Create(newWorker);


        }
        public void Delete(Entities.Worker worker)
        {
            _workerRepository.Remove(worker);

        }

        public void Update(Entities.Worker worker, Models.WorkerUpdate workerUpdate)
        {
            worker.CPFCNPJ = workerUpdate.cpf_cnpj;
            worker.Description = workerUpdate.description;
            worker.EmailCorp = workerUpdate.email_corp;
            worker.Facebook = workerUpdate.facebook;
            worker.Image = workerUpdate.image;
            worker.Instagram = workerUpdate.instagram;
            worker.Office = workerUpdate.office;
            worker.PhoneCorp = workerUpdate.phone_corp;
            worker.Site = workerUpdate.site;
            worker.SocialReason = workerUpdate.social_reason;
            worker.WhatsappNumber = workerUpdate.whatsapp_number;
            worker.DateUpdate = DateTime.Now;

            _workerRepository.Update(worker.id, worker);

        }
        public Entities.Worker GetWorker(string cpfcnpj)
        {
            return _workerRepository.FirstOrDefault(w => w.CPFCNPJ == cpfcnpj);
        }


    }
}
