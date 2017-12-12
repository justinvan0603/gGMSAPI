using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("BOT_QUESTIONTYPE")]
    public class BotQuestionType
    {
        [Key]
        public int QUESTIONTYPE_ID { get; set; }
        public string QUESTION_TYPE { get; set; }
        public Nullable<int> RECORD_STATUS { get; set; }

    }
}
