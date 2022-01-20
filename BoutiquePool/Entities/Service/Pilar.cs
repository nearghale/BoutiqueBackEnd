using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class Pilar : MongoDB.Base
    {
        [JsonProperty("id_core_pilar")]
        public int IdCorePilar { get; set; }

        [JsonProperty("st_core_pilar")]
        public string StCorePilar { get; set; }

        [JsonProperty("color_primary")]
        public string ColorPrimary { get; set; }

        [JsonProperty("color_secundary")]
        public string ColorSecundary { get; set; }
    
        [JsonProperty("icon")]
        public string Icon { get; set; }


    }
}
