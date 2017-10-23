using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;

namespace ChatBot.ViewModels
{
    public class PrjProjectViewModel
    {
        public PrjProjectMaster Project { get; set; }
        public IEnumerable<PrjProjectDT> ProjectDT { get; set; }

        public PrjProjectViewModel()
        {
            Project = new PrjProjectMaster();
            ProjectDT = new List<PrjProjectDT>();
        } 
    } 
}
