using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatBot.Model.Abstract
{
    public abstract class ApproveAuditable : IApproveAuditable
    {
        [DefaultValue("1")]
        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }

        //[DefaultValue(true)]
        //public bool Status
        //{
        //    set;
        //    get;
        //}
    }
}