using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Models
{
    public class TreeViewItem
    {
        public string id { get; set; }
        public PrdCategoryMaster data { get; set; }
        public List<TreeViewItem> children { get; set; }
     
        public string name { get; set; }
        //public string icon { get; set; }
        //public bool Checked { get; set; }
        public TreeViewItem ()
        {
            this.data = new PrdCategoryMaster();
            this.children = new List<TreeViewItem>();
           
           // this.icon = "fa-file-image-o";
        }
    }
}
