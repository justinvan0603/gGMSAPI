using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
namespace ChatBot.ViewModels
{
    public class BotScenarioViewModel
    {
        public BotScenario BotScenario { get; set; }
        public List<BotQuestion> BotQuestions { get; set; }
        public List<BotAnswer> BotAnswers { get; set; }
    }
}
