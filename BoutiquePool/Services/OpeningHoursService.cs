using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class OpeningHoursService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> _openingHoursRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Address> _addressRepository;



        public OpeningHoursService(Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> openingHoursRepository, Repositories.MongoDB.PersistentRepository<Entities.Address> addresstRepository)
        {

            _openingHoursRepository = openingHoursRepository;
            _addressRepository = addresstRepository;



        }

        public Entities.OpeningHours Create(string idAdress, Entities.OpeningHours openingHours)
        {
            var newOpeningHours = new Entities.OpeningHours();

            newOpeningHours.IdAdress = idAdress;
            newOpeningHours.AllDays = openingHours.AllDays;
            newOpeningHours.DateRegister = DateTime.Now;
            newOpeningHours.DayFriday = openingHours.DayFriday;
            newOpeningHours.DayMonday = openingHours.DayMonday;
            newOpeningHours.DaySaturday = openingHours.DaySaturday;
            newOpeningHours.DaySunday = openingHours.DaySunday;
            newOpeningHours.DayThursday = openingHours.DayThursday;
            newOpeningHours.DayTuesday = openingHours.DayTuesday;
            newOpeningHours.DayWednesday = openingHours.DayWednesday;
            newOpeningHours.HourEndFriday = openingHours.HourEndFriday;
            newOpeningHours.HourEndMonday = openingHours.HourEndMonday;
            newOpeningHours.HourEndSaturday = openingHours.HourEndSaturday;
            newOpeningHours.HourEndSunday = openingHours.HourEndSunday;
            newOpeningHours.HourEndThursday = openingHours.HourEndThursday;
            newOpeningHours.HourEndTuesday = openingHours.HourEndTuesday;
            newOpeningHours.HourEndWednesday = openingHours.HourEndWednesday;
            newOpeningHours.Hours24 = openingHours.Hours24;
            newOpeningHours.HourBeginFriday = openingHours.HourBeginFriday;
            newOpeningHours.HourBeginMonday = openingHours.HourBeginMonday;
            newOpeningHours.HourBeginSaturday = openingHours.HourBeginSaturday;
            newOpeningHours.HourBeginSunday = openingHours.HourBeginSunday;
            newOpeningHours.HourBeginThursday = openingHours.HourBeginThursday;
            newOpeningHours.HourBeginTuesday = openingHours.HourBeginTuesday;
            newOpeningHours.HourBeginWednesday = openingHours.HourBeginWednesday;
            newOpeningHours.MondayFriday = openingHours.MondayFriday;
            newOpeningHours.MondaySaturday = openingHours.MondaySaturday;
            newOpeningHours.SaturdaySunday = openingHours.SaturdaySunday;

            return _openingHoursRepository.Create(newOpeningHours);

        }
     
        public Entities.OpeningHours GetOpeningHours(string idAddress)
        {
            return _openingHoursRepository.FirstOrDefault(o => o.IdAdress == idAddress);
        }


    }
}
