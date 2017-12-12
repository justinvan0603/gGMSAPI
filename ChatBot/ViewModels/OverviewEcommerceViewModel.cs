using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatBot.ViewModels
{

    public class OverviewEcommerceViewModel
    {
        public int OVERVIEW_ECOMMERCE_ID { get; set; }
        public int PROJECT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ITEM_REVENUE { get; set; }

        public string PRODUCT_DETAIL_VIEWS { get; set; }

        public string QUANTITY_ADDED_TO_CART { get; set; }

        public string QUANTITY_CHECKED_OUT { get; set; }



        public string RECORD_STATUS { get; set; }
        public string AUTH_STATUS { get; set; }
        public string MAKER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string CHECKER_ID { get; set; }
        public DateTime? APPROVE_DT { get; set; }
        public string EDITOR_ID { get; set; }
        public DateTime? EDIT_DT { get; set; }
    }
    //public partial class formattedJson
    //{
    //    [JsonProperty("formattedJson")]
    //    public Welcome FormattedJson { get; set; }

    //    [JsonProperty("oldWeekEntry")]
    //    public string oldWeekEntry { get; set; }
    //}

    public partial class Welcome
    {
        [JsonProperty("formattedJson")]
        public FormattedJson FormattedJson { get; set; }

        [JsonProperty("project")]
        public Dictionary<string, string> Project { get; set; }
    }
    public partial class FormattedJson
    {
        [JsonProperty("reports")]
        public Report[] Reports { get; set; }
    }

    public partial class Report
    {
        [JsonProperty("columnHeader")]
        public ColumnHeader ColumnHeader { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("maximums")]
        public Metric[] Maximums { get; set; }

        [JsonProperty("minimums")]
        public Metric[] Minimums { get; set; }

        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public Row[] Rows { get; set; }

        [JsonProperty("totals")]
        public Metric[] Totals { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("dimensions")]
        public string[] Dimensions { get; set; }

        [JsonProperty("metrics")]
        public Metric[] Metrics { get; set; }
    }

    public partial class Metric
    {
        [JsonProperty("values")]
        public string[] Values { get; set; }
    }

    public partial class ColumnHeader
    {
        [JsonProperty("dimensions")]
        public string[] Dimensions { get; set; }

        [JsonProperty("metricHeader")]
        public MetricHeader MetricHeader { get; set; }
    }

    public partial class MetricHeader
    {
        [JsonProperty("metricHeaderEntries")]
        public MetricHeaderEntry[] MetricHeaderEntries { get; set; }
    }

    public partial class MetricHeaderEntry
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    //public partial class Welcome
    //{
    //    public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
    //}

    //public class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //    };
    //}
}
