using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Models
{
    public class Filter
    {
        public int[] pilar { get; set; }
        public int[] type_offer { get; set; }
        public Location location_user { get; set; }
        public int distance_filter { get; set; }
     
    }
}
