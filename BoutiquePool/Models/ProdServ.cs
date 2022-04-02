using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Models
{
    public class ProdServ
    {

      
        public string id_worker { get; set; }

        public string id_address { get; set; }

        public int id_core_pilar { get; set; }

        public int id_core_tipo_oferta { get; set; }

        public int id_core_prod_serv { get; set; }

        public int id_core_precificacao { get; set; }

        public string medida { get; set; }

        public string valor { get; set; }
        public string descricao { get; set; }

        public string cad_prod_serv_other { get; set; }
        public string cad_offer_category_other { get; set; }
        public string cad_service_group_other { get; set; }

        public double lat { get; set; }

        public double lon { get; set; }

        public bool active { get; set; }

        public DateTime date_register { get; set; }

        public DateTime date_update { get; set; }

        public List<int> itens_serv_estab { get; set; }

        public List<int> itens_enquadra { get; set; }




    }
}
