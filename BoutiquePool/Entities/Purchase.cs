using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class Purchase : MongoDB.Base
    {

        [JsonProperty("id_person")]
        public string IDPerson { get; set; }

        [JsonProperty("id_worker")]
        public string IDWorker { get; set; }

        [JsonProperty("id_product")]
        public string IDProduct { get; set; }

        [JsonProperty("id_address")]
        public string IDAddress { get; set; }

        [JsonProperty("whatsapp")]
        public int Whatsapp { get; set; }

        [JsonProperty("site")]
        public int Site { get; set; }

        [JsonProperty("facebook")]
        public int Facebook { get; set; }

        [JsonProperty("phone")]
        public int Phone { get; set; }
       
        [JsonProperty("instagram")]
        public int Instagram { get; set; }

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

      

    }
}
