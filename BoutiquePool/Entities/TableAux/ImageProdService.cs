using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities.TableAux
{
    public class ImageProdService : MongoDB.Base
    {

        [JsonProperty("id_prod_service")]
        public string IdProdService { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
