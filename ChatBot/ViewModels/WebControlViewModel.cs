using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
namespace ChatBot.ViewModels
{
    public class WebControlViewModel
    {
        public CwWebControl CwWebControl { get; set; }
        public PrjProjectMaster PrjProjectMaster { get; set; }
        public WebControlViewModel()
        {
            this.CwWebControl = new CwWebControl();
            this.PrjProjectMaster = new PrjProjectMaster();
        }
    }
}
