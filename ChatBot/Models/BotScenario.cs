using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_SCENARIO")]
    public class BotScenario
    {
        [Key]
        public int SCENARIO_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> DOMAIN_ID { get; set; }
        public string DOMAIN_NAME { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }
    }
}
