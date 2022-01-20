using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class Person : MongoDB.Base
    {

        [JsonProperty("type_user")]
        public string TypeUser { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("cell_number")]
        public string CellNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
       
        [JsonProperty("user_name")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("services")]
        public int Services { get; set; }

        [JsonProperty("services_accessed")]
        public int ServicesAccessed { get; set; }

        [JsonProperty("services_contact")]
        public int ServicesContact { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

        [JsonProperty("date_update")]
        public DateTime DateUpdate { get; set; }

        [JsonProperty("date_last_login")]
        public DateTime DateLastLogin { get; set; }


    }
}
