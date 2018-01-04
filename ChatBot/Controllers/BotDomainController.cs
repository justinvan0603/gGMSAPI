using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using ChatBot.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/BotDomain")]
    public class BotDomainController : Controller
    {
        private readonly gGMSContext _context;
        public BotDomainController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{username=}/{searchstring=}")]
        //[Authorize(Roles = "GetPluginBySearchAndPaging")]
        public async Task<IActionResult> Get(int? page, int? pageSize,string username, string searchstring = null)
        {

            PaginationSet<BotDomain> pagedSet = new PaginationSet<BotDomain>();

            var result =
                 _context.BotDomains.FromSql($"dbo.BOT_DOMAIN_Search @p_TOP='', @p_USERNAME= '{username}'");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(botdomain => botdomain.DOMAIN.ToLower().Contains(searchstring.ToLower()) ||
                                        botdomain.USER_NAME.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<BotDomain>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }
        [HttpGet]
        [Route("GetByUsername/{username}")]
        public async Task<IEnumerable<BotDomain>> GetByUsername(string username)
        {
            try
            {
                return this._context.BotDomains.Where(domain => domain.USER_NAME.Equals(username)).ToList();

            }
            catch(Exception ex)
            {
                return new List<BotDomain>();
            }
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeletePlugin")]
        public async Task<ObjectResult> Delete(int id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.BOT_DOMAIN_Del @DOMAIN_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa domain thành công!";
                }
                else
                {
                    rs.Succeeded = false;
                    rs.Message = "Đã có lỗi xảy ra!";
                }
                //return result;
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }
            catch (Exception ex)
            {
                rs.Succeeded = false;
                rs.Message = "Lỗi: " + ex.Message;
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }

        }
        [HttpPut("{id}")]
        //[Authorize(Roles ="UpdatePlugin")]
        public async Task<ObjectResult> Put(int id, [FromBody]BotDomain botDomain)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.BOT_DOMAIN_Upd @p_DOMAIN_ID={id}, @p_DOMAIN='{botDomain.DOMAIN}',@p_USERNAME='{botDomain.USER_NAME}',@p_RECORD_STATUS = {botDomain.RECORD_STATUS}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật domain thành công";
                }
                else
                {
                    rs.Succeeded = false;
                    rs.Message = "Đã có lỗi xảy ra";
                }
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }
            catch (Exception ex)
            {
                rs.Succeeded = false;
                rs.Message = ex.Message;
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }
        }

        [HttpPost]
        public async Task<ObjectResult> Post([FromBody]BotDomain botDomain)
        {
            GenericResult rs = new GenericResult();
            try
            {
                string command = $"dbo.BOT_DOMAIN_Ins @p_DOMAIN = '{botDomain.DOMAIN}',@p_RECORD_STATUS =1, @p_USERNAME='{botDomain.USER_NAME}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm domain thành công";
                    rs.Succeeded = true;
                }
                else
                {
                    rs.Message = "Có lỗi xảy ra trong quá trình thêm!";
                    rs.Succeeded = false;
                }
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;

            }
            catch (Exception ex)
            {
                rs.Message = "Lỗi " + ex.Message;
                rs.Succeeded = false;
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }
        }
    }
}
