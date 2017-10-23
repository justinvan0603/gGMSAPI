using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class PrdProductMasterNotes
    {
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductLocation { get; set; }
        public string ProductType { get; set; }
        public string Notes { get; set; }
        public string RecordStatus { get; set; }
        public string MakerId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string AuthStatus { get; set; }
        public string CheckerId { get; set; }
        public DateTime? ApproveDt { get; set; }
        public string EditorId { get; set; }
        public DateTime? EditDt { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceVat { get; set; }
        public decimal? Vat { get; set; }
        public decimal? DiscountAmt { get; set; }
        public string Scripts { get; set; }
        public string CodeView { get; set; }

    }
}
