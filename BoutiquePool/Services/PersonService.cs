using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unieco.Helpers;
namespace BoutiquePool.Services
{
    public class PersonService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Person> _personRepository;

        public PersonService(Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository)
        {

            _personRepository = personRepository;
           

        }

        public Entities.Person Create(Entities.Person person)
        {
            var newPerson = new Entities.Person();


            newPerson.BirthDate = person.BirthDate;
            newPerson.TypeUser = "C";
            newPerson.Image = person.Image;
            newPerson.Email = person.Email;
            newPerson.Name = person.Name;
            newPerson.CellNumber = person.CellNumber;
            newPerson.Username = person.Username;
            newPerson.Password = CriptoHelper.Encrypt(person.Password);
            newPerson.DateRegister = DateTime.Now;
            newPerson.Active = true;


            return _personRepository.Create(newPerson);


        }
        public void Delete(Entities.Person person)
        {
            _personRepository.Remove(person);

        }

        public void Update(Entities.Person person, Models.PersonUpdate personUpdate)
        {

            person.DateUpdate = DateTime.Now;


            person.Password = person.Password;
            person.BirthDate = person.BirthDate;
            person.Email = person.Email;
            person.Image = person.Image;
            person.Name = person.Name;
            person.CellNumber = person.CellNumber;
            person.DateRegister = person.DateRegister;



            _personRepository.Update(person.id, person);

        }
        public Entities.Person GetPerson(string email)
        {
            return _personRepository.FirstOrDefault(a => a.Email == email);
        }


    }
}
