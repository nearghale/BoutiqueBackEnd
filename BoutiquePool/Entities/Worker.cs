using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class Worker : MongoDB.Base
    {

        [JsonProperty("id_user")]
        public string IdUser { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("social_reason")]
        public string SocialReason { get; set; }

        [JsonProperty("cpf_cnpj")]
        public string CPFCNPJ { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("phone_corp")]
        public string PhoneCorp { get; set; }

        [JsonProperty("whatsapp_number")]
        public string WhatsappNumber { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }

        [JsonProperty("email_corp")]
        public string EmailCorp { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("instagram")]
        public string Instagram { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

        [JsonProperty("date_update")]
        public DateTime DateUpdate { get; set; }


    }
}
