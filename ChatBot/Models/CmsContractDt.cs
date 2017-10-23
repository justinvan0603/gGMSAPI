using System;
using System.Collections.Generic;

namespace ChatBot.Models
{
    public partial class CmsContractDt
    {
        public string ContractId { get; set; }
        public string ProductId { get; set; }
        public string Notes { get; set; }
        public string RecordStatus { get; set; }
        public string MakerId { get; set; }
        public string DepId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string AuthStatus { get; set; }
        public string CheckerId { get; set; }
        public DateTime? ApproveDt { get; set; }
        public string EditorId { get; set; }
        public DateTime? EditDt { get; set; }
    }
}
