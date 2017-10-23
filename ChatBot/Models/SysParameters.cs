using System;
using System.Collections.Generic;

namespace ChatBot.Models
{
    public partial class SysParameters
    {
        public decimal Id { get; set; }
        public string ParaKey { get; set; }
        public string ParaValue { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public string RecordStatus { get; set; }
        public string MakerId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string AuthStatus { get; set; }
        public string CheckerId { get; set; }
        public DateTime? ApproveDt { get; set; }
    }
}
