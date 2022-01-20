using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Entities
{
    public class OpeningHours : MongoDB.Base
    {

        [JsonProperty("id_address")]
        public string IdAdress { get; set; }

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

        [JsonProperty("date_register")]
        public DateTime DateRegister { get; set; }

        [JsonProperty("date_update")]
        public DateTime DateUpdate { get; set; }

    }
}
