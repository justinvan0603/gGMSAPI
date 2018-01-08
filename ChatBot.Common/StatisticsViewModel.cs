using ChatBot.Model.Models;
using System;
using System.Collections.Generic;

namespace ChatBot.Common
{
    //public class StatisticsViewModel
    //{
    //   public double SUM_ITEM_REVENUE { get; set; }
    //   public double SUM_PRODUCT_DETAIL_VIEWS { get; set; }
    //   public double SUM_QUANTITY_ADDED_TO_CART { get; set; }
    //   public double SUM_QUANTITY_CHECKED_OUT { get; set; }

    //   public List<ProductPerformace> SUM_LISTPRODUCT_OVERVIEWE_COMMERCE { get; set; }
    //}

    public class StatisticsOverviewEcommerceViewModel
    {
        public double SESSIONS { get; set; }
        public double PAGEVIEWS { get; set; }
        public string TIMEONPAGE { get; set; }
        public string TRANSACTIONREVENUE { get; set; }
               
        public double PRODUCTDETAILVIEWS { get; set; }
               
        public double PRODUCTADDSTOCART { get; set; }
               
        public double PRODUCTCHECKOUTS { get; set; }

        public double USERS { get; set; }
        public double NEWS_USERS { get; set; }

        public int COUNT_WEBSITE { get; set; }
        //  public List<ProductPerformace> SUM_LISTPRODUCT_OVERVIEWE_COMMERCE { get; set; }
    }

    public class StatisticsTrafficSourcesEcommerceViewModel
    {
        public string SOURCE { get; set; }
       // public string MEDIUM { get; set; }
      //  public string SESSIONS { get; set; }
      //  public string SESSIONDURATION { get; set; }

        public double PAGEVIEWS { get; set; }

       // public string EXITS { get; set; }

        public double TRANSACTIONS { get; set; }

        public double PERCENT { get; set; }

        public string TRANSACTIONREVENUE { get; set; }

        public string SUM_PAGEVIEW { get; set; }
        
    }

}
