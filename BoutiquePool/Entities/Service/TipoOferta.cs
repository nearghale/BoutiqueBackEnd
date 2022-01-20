using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class TipoOferta : MongoDB.Base
    {
        [JsonProperty("id_core_tipo_oferta")]
        public int IdCoreTipoOferta { get; set; }

        [JsonProperty("st_core_tipo_oferta")]
        public string StCoreTipoOferta { get; set; }


    }
}
