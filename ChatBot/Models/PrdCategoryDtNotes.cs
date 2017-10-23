using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class PrdCategoryDtNotes
    {
        public string CATEGORY_ID { get; set; }
        public string PRODUCT_ID { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }
    }
}
