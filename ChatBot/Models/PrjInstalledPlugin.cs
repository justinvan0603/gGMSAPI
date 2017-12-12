using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    [Table("PRJ_INSTALLED_PLUGIN")]
    public class PrjInstalledPlugin
    {
    
        public string PLUGIN_ID { get; set; }

        public string PROJECT_ID { get; set; }
        public string PROJECT_NAME { get; set; }
        public string SUBDOMAIN { get; set; }
        public string IS_CHECKED { get; set; }
    }
}
