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
    [Route("api/BotCustomer")]
    public class BotCustomerController : Controller
    {
        private readonly gGMSContext _context;
        public BotCustomerController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{searchstring=}")]
        //[Authorize(Roles = "GetPluginBySearchAndPaging")]
        public async Task<IActionResult> Get(int? page, int? pageSize, string searchstring = null)
        {

            PaginationSet<BotCustomerInfo> pagedSet = new PaginationSet<BotCustomerInfo>();

            var result =
                 _context.BotCustomerInfos.FromSql("dbo.BOT_CUSTOMERINFO_Search @p_TOP=''");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(customerinfo => customerinfo.DOMAIN_NAME.ToLower().Contains(searchstring.ToLower()) ||
                                        customerinfo.NAME.ToLower().Contains(searchstring.ToLower()) ||
                                        customerinfo.EMAIL.ToLower().Contains(searchstring.ToLower()) ||
                                        customerinfo.PHONE.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<BotCustomerInfo>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeletePlugin")]
        public async Task<ObjectResult> Delete(int id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.BOT_CUSTOMERINFO_Del @CUSTOMER_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa thông tin khách hàng tiềm năng thành công!";
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
        public async Task<ObjectResult> Put(int id, [FromBody]BotCustomerInfo botCustomerInfo)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.BOT_CUSTOMERINFO_Upd @p_CUSTOMER_ID={id}, @p_DOMAIN_ID={botCustomerInfo.DOMAIN_ID}, @p_DOMAIN_NAME='{botCustomerInfo.DOMAIN_NAME}',@p_NAME = '{botCustomerInfo.NAME}',@p_EMAIL='{botCustomerInfo.EMAIL}',@p_PHONE='{botCustomerInfo.PHONE}',@p_RECORD_STATUS={botCustomerInfo.RECORD_STATUS}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật Thông tin khách hàng tiềm năng thành công";
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
    }
}