using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_ANSWER")]
    public class BotAnswer
    {
        [Key]
        public int ANSWER_ID { get; set; }
        public string CONTENT { get; set; }
        public Nullable<int> QUESTION_ID { get; set; }
        public Nullable<int> PREVANSWER_ID { get; set; }
        public Nullable<bool> IS_END { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }
        public Nullable<int> LEVEL { get; set; }
        public Nullable<int> FORM_ID { get; set; }
        public string FORM_NAME { get; set; }

    }
}
