using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Models
{
    public class PersonInformation
    {

        public string id { get; set; }

        public string type_user { get; set; }

        public string image { get; set; }
        
        public string name { get; set; }

        public string cell_number { get; set; }

        public string email { get; set; }

        public int services { get; set; }

        public int services_accessed { get; set; }

        public int services_contact { get; set; }


    }
}
