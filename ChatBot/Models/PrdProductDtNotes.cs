using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class PrdProductDtNotes
    {
        public int ProductDtId { get; set; }
        public string ProductId { get; set; }
        public string Type { get; set; }
        public string ComponentId { get; set; }
        public string Notes { get; set; }
        public string RecordStatus { get; set; }
        public string MakerId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string AuthStatus { get; set; }
        public string CheckerId { get; set; }
        public DateTime? ApproveDt { get; set; }
        public string EditorId { get; set; }
        public DateTime? EditDt { get; set; }
    }

}
