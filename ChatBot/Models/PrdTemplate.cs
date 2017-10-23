using System;
using System.Collections.Generic;

namespace ChatBot.Models
{
    public  class PrdTemplate
    {
        public string TemplateId { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateLocation { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceVat { get; set; }
        public decimal? Vat { get; set; }
        public decimal? DiscountAmt { get; set; }
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

        public string IMAGES_PATH { get; set; }
        public string DEMO_LINK { get; set; }
    }
}
