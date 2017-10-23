using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBot.Models
{
    public partial class CmsContractMaster
    {
        public string ContractId { get; set; }
        public string ContractCode { get; set; }
        public string CustomerId { get; set; }
        public decimal? Value { get; set; }
        public int? ExpContract { get; set; }
        public DateTime? SignContractDt { get; set; }
        public DateTime? ChargeDt { get; set; }
        public double? MonthCharge { get; set; }
        public string Status { get; set; }
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

        public string CONTRACT_TYPE { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

    }
}
