using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/AllCode")]
    public class AllCodeController : Controller
    {
        private readonly gGMSContext _context;
        public AllCodeController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{cdname}")]
        public async Task<IEnumerable<CmAllcode>> Get(string cdname)
        {
            try
            {
                string command = $"dbo.CM_ALLCODE_Search @p_CDNAME='{cdname}', @p_TOP = ''";
                var result = await _context.CmAllcode.FromSql(command).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}