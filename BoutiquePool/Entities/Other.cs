using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BoutiquePool.Entities
{
    public class Other: MongoDB.Base
    {
        [JsonProperty("id_core_pilar")]
        public int IdCorePilar { get; set; }

        [JsonProperty("id_core_tipo_oferta")]
        public int IdCoreTipoOferta { get; set; }

        [JsonProperty("id_prod_service")]
        public string IdProdService { get; set; }

        [JsonProperty("cad_prod_serv")]
        public string CadProdServ { get; set; }

        [JsonProperty("cad_offer_category")]
        public string CadOfferCategory { get; set; }

        [JsonProperty("cad_service_group")]
        public string CadServiceGroup { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
