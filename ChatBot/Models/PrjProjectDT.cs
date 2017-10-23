using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("PRJ_PROJECT_DT")]
    public class PrjProjectDT
    {
        public string PROJECT_ID { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public string PROJECT_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string STATE { get; set; }
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
