using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class ProdService : MongoDB.Base
    {
        [JsonProperty("id_worker")]
        public string IdWorker { get; set; }

        [JsonProperty("id_address")]
        public string IdAddress { get; set; }

        [JsonProperty("id_core_pilar")]
        public int IdCorePilar { get; set; }

        [JsonProperty("id_core_tipo_oferta")]
        public int IdCoreTipoOferta { get; set; }

        [JsonProperty("id_core_prod_serv")]
        public int IdCoreProdServ { get; set; }

        [JsonProperty("id_core_precificacao")]
        public int IdCorePrecificacao { get; set; }

        [JsonProperty("medida")]
        public string Medida { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

        [JsonProperty("date_update")]
        public DateTime DateUpdate { get; set; }


    }
}
