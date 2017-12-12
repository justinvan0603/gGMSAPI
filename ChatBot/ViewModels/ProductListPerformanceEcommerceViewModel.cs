//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace ChatBot.ViewModels
//{

//    public partial class Welcome
//    {
//        [JsonProperty("formattedJson")]
//        public FormattedJson FormattedJson { get; set; }

//        [JsonProperty("project")]
//        public Dictionary<string, string> Project { get; set; }
//    }

//    public partial class FormattedJson
//    {
//        [JsonProperty("reports")]
//        public Report[] Reports { get; set; }
//    }

//    public partial class Report
//    {
//        [JsonProperty("columnHeader")]
//        public ColumnHeader ColumnHeader { get; set; }

//        [JsonProperty("data")]
//        public Data Data { get; set; }
//    }

//    public partial class Data
//    {
//        [JsonProperty("maximums")]
//        public Metric[] Maximums { get; set; }

//        [JsonProperty("minimums")]
//        public Metric[] Minimums { get; set; }

//        [JsonProperty("rowCount")]
//        public long RowCount { get; set; }

//        [JsonProperty("rows")]
//        public Row[] Rows { get; set; }

//        [JsonProperty("totals")]
//        public Metric[] Totals { get; set; }
//    }

//    public partial class Row
//    {
//        [JsonProperty("dimensions")]
//        public string[] Dimensions { get; set; }

//        [JsonProperty("metrics")]
//        public Metric[] Metrics { get; set; }
//    }

//    public partial class Metric
//    {
//        [JsonProperty("values")]
//        public string[] Values { get; set; }
//    }

//    public partial class ColumnHeader
//    {
//        [JsonProperty("dimensions")]
//        public string[] Dimensions { get; set; }

//        [JsonProperty("metricHeader")]
//        public MetricHeader MetricHeader { get; set; }
//    }

//    public partial class MetricHeader
//    {
//        [JsonProperty("metricHeaderEntries")]
//        public MetricHeaderEntry[] MetricHeaderEntries { get; set; }
//    }

//    public partial class MetricHeaderEntry
//    {
//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }
//    }

//}
