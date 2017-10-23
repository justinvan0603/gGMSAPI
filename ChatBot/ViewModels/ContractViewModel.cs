using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;

namespace ChatBot.ViewModels
{
    public class ContractViewModel
    {
        public CmsContractMaster Contract { get; set; }
        public IEnumerable<CmsContractDt> ContractDetails { get; set; }

        public ContractViewModel()
        {
            Contract = new CmsContractMaster();
            ContractDetails = new List<CmsContractDt>();
        }

    }
}
