using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities.TableAux
{
    public class ServEstabService : MongoDB.Base
    {

        [JsonProperty("id_prod_service")]
        public string IdProdService { get; set; }

        [JsonProperty("id_core_tipo_servico_estabelecimento")]
        public int IdCoreTipoServicoEstabelecimento { get; set; }
    }
}
