using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ChatBot.Models
{
    public class PrdCategoryMaster
    {
        [Key]
        public string CATEGORY_ID { get; set; }
        public string TYPE_ID { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string PARENT_ID { get; set; }
        public string IS_LEAF { get; set; }
        public int? CATEGORY_LEVEL { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }
        public string PARENT_NAME { get; set; }
        public string PARENT_CODE { get; set; }
    }
}
