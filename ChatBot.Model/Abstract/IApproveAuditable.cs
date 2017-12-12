using System;

namespace ChatBot.Model.Abstract
{
    public interface IApproveAuditable
    {
        //DateTime? CreatedDate { set; get; }
        //string CreatedBy { set; get; }
        //DateTime? UpdatedDate { set; get; }
        //string UpdatedBy { set; get; }
        //bool Status { set; get; }

      string RECORD_STATUS { get; set; }
      string AUTH_STATUS { get; set; }
      string MAKER_ID { get; set; }
      DateTime? CREATE_DT { get; set; }
      string CHECKER_ID { get; set; }
      DateTime? APPROVE_DT { get; set; }
      string EDITOR_ID { get; set; }
      DateTime? EDIT_DT { get; set; }

   





    }
}