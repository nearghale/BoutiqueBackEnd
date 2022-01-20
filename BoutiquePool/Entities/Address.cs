using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class Address : MongoDB.Base
    {
        [JsonProperty("id_worker")]
        public string IdWorker { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }


        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

        [JsonProperty("date_update")]
        public DateTime DateUpdate { get; set; }


    }
}
