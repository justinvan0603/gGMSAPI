using System;
using System.Collections.Generic;

namespace ChatBot.Models
{
    public partial class CmsCustomerMaster
    {
        public string CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string ContractId { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public decimal? Value { get; set; }
        public int? ExpContract { get; set; }
        public DateTime? SignContractDt { get; set; }
        public DateTime? ChargeDt { get; set; }
        public string Status { get; set; }
        public string DepId { get; set; }
        public string Notes { get; set; }
        public string RecordStatus { get; set; }
        public string MakerId { get; set; }
        public DateTime? CreateDt { get; set; }
        public string AuthStatus { get; set; }
        public string CheckerId { get; set; }
        public DateTime? ApproveDt { get; set; }
        public string EditorId { get; set; }
        public DateTime? EditDt { get; set; }
        public string XmlTemp { get; set; }
        public string DataTemp { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }
}
