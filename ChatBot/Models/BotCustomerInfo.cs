using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_CUSTOMERINFO")]
    public class BotCustomerInfo
    {
        [Key]
        public int CUSTOMER_ID { get; set; }
        public Nullable<int> DOMAIN_ID { get; set; }
        public string DOMAIN_NAME { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }
    }
}
