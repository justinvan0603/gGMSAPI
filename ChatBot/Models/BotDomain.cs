using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_DOMAIN")]
    public class BotDomain
    {
        [Key]
        public int DOMAIN_ID { get; set; }
        public string DOMAIN { get; set; }
        public string USER_NAME { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }
    }
}
