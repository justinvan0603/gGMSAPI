using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ChatBot.Models
{
    [Table("PRJ_PROJECT_MASTER")]
    public class PrjProjectMaster
    {
        [Key]
        public string PROJECT_ID { get; set; }
        public string PROJECT_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public DateTime? BEGIN_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public DateTime? ESTIMATE_DATE { get; set; }
        public DateTime? COMPLETION_DATE { get; set; }
        public string STATE { get; set; }
        public string CONTRACT_ID { get; set; }
        public string CONTRACT_CODE { get; set; }
        public string CONTRACT_TYPE { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }
        public string MYSQL_USERNAME { get; set; }
        public string MYSQL_PASSWORD { get; set; }
        public string DATABASE_NAME { get; set; }
        public string DOMAIN { get; set; }
        public string SUB_DOMAIN { get; set; }
        public string VIEW_ID { get; set; }
        //public string OPERATION_STATE { get; set; }

    }
}
