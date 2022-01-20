using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class ProdServ : MongoDB.Base
    {
        [JsonProperty("id_core_produto_servico")]
        public int IdCoreProdutoServico { get; set; }

        [JsonProperty("fg_tipo_venda")]
        public int FgTipoVenda { get; set; }

        [JsonProperty("fg_pilar_tudo")]
        public int FgPilarTudo { get; set; }

        [JsonProperty("fg_pilar_energia")]
        public int FgPilarEnergia { get; set; }

        [JsonProperty("fg_pilar_hidrico")]
        public int FgPilarHidrico { get; set; }

        [JsonProperty("fg_pilar_residuos")]
        public int FgPilarResiduos { get; set; }

        [JsonProperty("fg_pilar_sustentavel")]
        public int FgPilarSustentavel { get; set; }

        [JsonProperty("st_core_produto_servico")]
        public string StCoreProdutoServico { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

    }
}
