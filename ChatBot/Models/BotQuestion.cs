using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_QUESTION")]
    public class BotQuestion
    {
        [Key]
        public int QUESTION_ID { get; set; }
        public string CONTENT { get; set; }
        public Nullable<int> QUESTION_TYPE { get; set; }
        public Nullable<int> SCENARIO_ID { get; set; }
        public Nullable<int> DOMAIN_ID { get; set; }
        public string DOMAIN_NAME { get; set; }
        public Nullable<int> PREVQUESTION_ID { get; set; }
        public Nullable<bool> IS_END { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }
        public Nullable<int> PREVANSWER_ID { get; set; }
        public Nullable<int> FORM_ID { get; set; }
        public string FORM_NAME { get; set; }
        public Nullable<int> LEVEL { get; set; }
    }
}
