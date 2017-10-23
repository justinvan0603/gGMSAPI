using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
namespace ChatBot.ViewModels
{
    public class PrdProductNotesViewModel
    {
        public PrdProductMasterNotes PrdProduct { get; set; }
        public List<PrdSource> ListSources { get; set; }
        public List<PrdTemplate> ListTemplates { get; set; }
        public List<PrdPlugin> ListPlugins { get; set; }
        public List<TreeViewItem> ListCategory { get; set; }
        public List<TreeViewItem> ListSelectedCategory { get; set; }
    }
}
