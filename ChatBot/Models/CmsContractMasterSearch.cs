using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class CmsContractMasterSearch
    {
        [Key]
        public string CONTRACT_ID { get; set; }
        public string CONTRACT_CODE { get; set; }
        public string CUSTOMER_ID { get; set; }
        public decimal? Value { get; set; }
        public int? ExpContract { get; set; }
        public DateTime? SignContractDt { get; set; }
        public DateTime? ChargeDt { get; set; }
        public double? MonthCharge { get; set; }
        public string Status { get; set; }
        public string NOTES { get; set; }
        public string RECORD_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public string DepId { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string AUTH_STATUS { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }
        public string XmlTemp { get; set; }
        public string DataTemp { get; set; }
        public decimal? DebitBalance { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? DepositAccountBeforLd { get; set; }
        public decimal? OptimalAmt { get; set; }
        public decimal? SeoAmt { get; set; }
        public decimal? DebitMaintainFee { get; set; }
        public decimal? DepositAccount { get; set; }
        public decimal? DepositLiquidation { get; set; }
        public bool? IsFirstFee { get; set; }
        public bool? IsLandingPage { get; set; }
        public string TypeGoogle { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_CODE { get; set; }
    }
}
