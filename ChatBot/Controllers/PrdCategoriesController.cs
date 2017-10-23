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
    [Route("api/PrdCategories")]
    public class PrdCategoriesController : Controller
    {
        private readonly gGMSContext _context;
        public PrdCategoriesController(gGMSContext context)
        {
            this._context = context;
        }
        
    }
}