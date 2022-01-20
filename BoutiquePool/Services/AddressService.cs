using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class AddressService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Address> _addressRepository;

        public AddressService(Repositories.MongoDB.PersistentRepository<Entities.Address> addressRepository)
        {

            _addressRepository = addressRepository;
           

        }

        public Entities.Address Create(string idWorker, Entities.Address address)
        {
            var newAddress = new Entities.Address();


            newAddress.IdWorker = idWorker;
            newAddress.CEP = address.CEP;
            newAddress.City = address.City;
            newAddress.Complement = address.Complement;
            newAddress.District = address.District;
            newAddress.Number = address.Number;
            newAddress.State = address.State;
            newAddress.Street = address.Street;
            newAddress.Lat = address.Lat;
            newAddress.Lon = address.Lon;
            newAddress.DateRegister = DateTime.Now;
            newAddress.Active = true;


            return _addressRepository.Create(newAddress);


        }
        public void Delete(Entities.Address address)
        {
            _addressRepository.Remove(address);

        }

        public List<Entities.Address> GetAddressByIdWorker(string idWorker)
        {
            return _addressRepository.Find(a => a.IdWorker == idWorker);
        }


    }
}
