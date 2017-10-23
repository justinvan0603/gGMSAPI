using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace ChatBot.Models
{
    public class PrdTemplatePostObject
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
        public List<IFormFile> ListImages { get; set; }
    }
}
