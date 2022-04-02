using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class ServiceEstabView : MongoDB.Base
    {
        //product
        [JsonProperty("id_service")]
        public string IdService { get; set; }

        [JsonProperty("images_products")]
        public List<string> ImagesProducts { get; set; }

        [JsonProperty("name_service_estab")]
        public string NameServiceEstab { get; set; }

        [JsonProperty("category_type")]
        public List<string> CategoryType { get; set; }

        [JsonProperty("enquadra_type")]
        public List<string> EnquadraType { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("medida")]
        public string Medida { get; set; }

        [JsonProperty("precification")]
        public string Precification { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("cornestone")]
        public string Cornerstone { get; set; }

        [JsonProperty("type_offer")]
        public string TypeOffer { get; set; }

        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        //contact
        [JsonProperty("phone_corp")]
        public string PhoneCorp { get; set; }

        [JsonProperty("whatsapp_number")]
        public string WhatsappNumber { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("instagram")]
        public string Instagram { get; set; }

        //description and company 
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("company_description")]
        public string CompanyDescription { get; set; }

        //attendance
        [JsonProperty("all_days")]
        public bool AllDays { get; set; }

        [JsonProperty("monday_friday")]
        public bool MondayFriday { get; set; }

        [JsonProperty("monday_saturday")]
        public bool MondaySaturday { get; set; }

        [JsonProperty("saturday_sunday")]
        public bool SaturdaySunday { get; set; }

        [JsonProperty("day_monday")]
        public bool DayMonday { get; set; }

        [JsonProperty("day_tuesday")]
        public bool DayTuesday { get; set; }

        [JsonProperty("day_wednesday")]
        public bool DayWednesday { get; set; }

        [JsonProperty("day_thursday")]
        public bool DayThursday { get; set; }

        [JsonProperty("day_friday")]
        public bool DayFriday { get; set; }

        [JsonProperty("day_saturday")]
        public bool DaySaturday { get; set; }

        [JsonProperty("day_sunday")]
        public bool DaySunday { get; set; }

        [JsonProperty("hours_24")]
        public bool Hours24 { get; set; }

        [JsonProperty("hour_begin_monday")]
        public string HourBeginMonday { get; set; }

        [JsonProperty("hour_end_monday")]
        public string HourEndMonday { get; set; }

        [JsonProperty("hour_begin_tuesday")]
        public string HourBeginTuesday { get; set; }

        [JsonProperty("hour_end_tuesday")]
        public string HourEndTuesday { get; set; }

        [JsonProperty("hour_begin_wednesday")]
        public string HourBeginWednesday { get; set; }

        [JsonProperty("hour_end_wednesday")]
        public string HourEndWednesday { get; set; }

        [JsonProperty("hour_begin_thursday")]
        public string HourBeginThursday { get; set; }

        [JsonProperty("hour_end_thursday")]
        public string HourEndThursday { get; set; }

        [JsonProperty("hour_begin_friday")]
        public string HourBeginFriday { get; set; }

        [JsonProperty("hour_end_friday")]
        public string HourEndFriday { get; set; }

        [JsonProperty("hour_begin_saturday")]
        public string HourBeginSaturday { get; set; }

        [JsonProperty("hour_end_saturday")]
        public string HourEndSaturday { get; set; }

        [JsonProperty("hour_begin_sunday")]
        public string HourBeginSunday { get; set; }

        [JsonProperty("hour_end_sunday")]
        public string HourEndSunday { get; set; }

        //address
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }



        //map


        [JsonProperty("image_company")]
        public string ImageCompany { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }




    }
}
