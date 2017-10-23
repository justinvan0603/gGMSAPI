using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("CMS_CONTRACT_FILE_UPLOAD")]
    public class CmsContractFileUpload
    {
        [Key]
        public int ID { get; set; }
        public string CONTRACT_ID { get; set; }
        public string TYPE { get; set; }
        public string FILE_NAME { get; set; }
        public string PATH { get; set; }
        public decimal? FILE_SIZE { get; set; }
        public string FILE_TYPE { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }

        public string MAKER_ID { get; set; }



    }
}
