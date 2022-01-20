using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class ServiceEstabItem : MongoDB.Base
    {
        [JsonProperty("id_service")]
        public string IdService { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("social_reason")]
        public string SocialReason { get; set; }

        [JsonProperty("category_type")]
        public List<string> CategoryType { get; set; }

        [JsonProperty("type_offer")]
        public string TypeOffer { get; set; }

        [JsonProperty("cornestone")]
        public string Cornerstone { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }





    }
}
