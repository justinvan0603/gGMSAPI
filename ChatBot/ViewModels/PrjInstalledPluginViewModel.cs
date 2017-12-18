using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
namespace ChatBot.ViewModels
{
    public class PrjInstalledPluginViewModel
    {
        public PrdPlugin PrdPlugin { get; set; }
        public PrjInstalledPlugin PrjInstalledPlugin { get; set; }
        public PrjProjectMaster PrjProjectMaster { get; set; }
        public bool IsChecked { get; set; }
    }
}
