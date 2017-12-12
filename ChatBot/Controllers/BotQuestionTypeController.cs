using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/BotQuestionType")]
    public class BotQuestionTypeController : Controller
    {
        private readonly gGMSContext _context;
        public BotQuestionTypeController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<BotQuestionType>> Get()
        {
            try
            {

                var result = this._context.BotQuestionTypes.ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}