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
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/PrdPlugins")]
    public class PrdPluginsController : Controller
    {
        private readonly gGMSContext _context;
        public PrdPluginsController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet]
        [Route("GetAll/{searchstring=}")]
        //[Authorize(Roles = "GetAllPlugin")]
        public async Task<IEnumerable<PrdPlugin>> GetAll(string searchstring = null)
        {

            var result =
                 _context.PrdPlugin.FromSql("PRD_PLUGIN_Search @p_TOP=''");

            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(plugin => plugin.PluginCode.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginName.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginLocation.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginDescription.ToLower().Contains(searchstring.ToLower()));
            }
            return await result.ToListAsync();
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{searchstring=}")]
        //[Authorize(Roles = "GetPluginBySearchAndPaging")]
        public async Task<IActionResult> Get(int? page, int? pageSize, string searchstring = null)
        {

            PaginationSet<PrdPlugin> pagedSet = new PaginationSet<PrdPlugin>();

            var result =
                 _context.PrdPlugin.FromSql("PRD_PLUGIN_Search @p_TOP=''");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(plugin => plugin.PluginCode.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginName.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginLocation.ToLower().Contains(searchstring.ToLower()) ||
                                        plugin.PluginDescription.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdPlugin>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpGet("{id}")]
        [Route("GetPluginById")]
        //[Authorize(Roles ="GetPluginById")]
        public async Task<PrdPlugin> GetById(string id)
        {
            try
            {
                string command = $"dbo.PRD_PLUGIN_ById @PLUGIN_ID='{id}' ";
                var result = await _context.PrdPlugin.FromSql(command).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeletePlugin")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_PLUGIN_Del @PLUGIN_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa plugin thành công!";
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


        /*
         * @p_PLUGIN_CODE	varchar(15)  = NULL,
@p_PLUGIN_LOCATION	nvarchar(1000)  = NULL,
@p_PLUGIN_NAME	nvarchar(200)  = NULL,
@p_PLUGIN_DESCRIPTION	nvarchar(1000)  = NULL,
@p_NOTES	nvarchar(500)  = NULL,
@p_RECORD_STATUS	varchar(1)  = NULL,
@p_MAKER_ID	varchar(15)  = NULL,
@p_DepID	ntext = NULL,
@p_CREATE_DT	VARCHAR(20) = NULL,
@p_AUTH_STATUS	varchar(1)  = NULL,
@p_CHECKER_ID	varchar(15)  = NULL,
@p_APPROVE_DT	VARCHAR(20) = NULL,
@p_EDITOR_ID	varchar(15)  = NULL,
@p_EDIT_DT	VARCHAR(20) = NULL
@p_PRICE DECIMAL(18, 0) = NULL, 
@p_PRICE_VAT DECIMAL(18, 0) = NULL, 
@p_VAT DECIMAL(5, 2) = NULL, 
@p_DISCOUNT_AMT DECIMAL(18, 0) = NULL,
         */


        [HttpPost]
        //[Authorize(Roles ="CreatePlugin")]
        public async Task<ObjectResult> Post([FromBody]PrdPlugin plugin)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_PLUGIN_Ins @p_PLUGIN_CODE='{plugin.PluginCode}',@p_PLUGIN_NAME=N'{plugin.PluginName}',@p_PLUGIN_LOCATION=N'{plugin.PluginLocation}',@p_PLUGIN_DESCRIPTION=N'{plugin.PluginDescription}',@p_NOTES='',@p_RECORD_STATUS='1',@p_MAKER_ID='{plugin.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@p_PRICE='{plugin.Price}',@p_PRICE_VAT='{plugin.PriceVat}',@p_VAT='{plugin.Vat}',@p_DISCOUNT_AMT='{plugin.DiscountAmt}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm plugin thành công";
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



        [HttpPut("{id}")]
        //[Authorize(Roles ="UpdatePlugin")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdPlugin plugin)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_PLUGIN_Upd @p_PLUGIN_ID='{plugin.PluginId}', @p_PLUGIN_CODE='{plugin.PluginCode}',@p_PLUGIN_NAME=N'{plugin.PluginName}',@p_PLUGIN_LOCATION=N'{plugin.PluginLocation}',@p_PLUGIN_DESCRIPTION=N'{plugin.PluginDescription}',@p_NOTES='{plugin.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{plugin.MakerId}',@p_CREATE_DT='{plugin.CreateDt}',@p_AUTH_STATUS='U',@p_CHECKER_ID='{plugin.CheckerId}',@p_APPROVE_DT='{plugin.ApproveDt}',@p_EDITOR_ID='{plugin.EditorId}',@p_EDIT_DT='{DateTime.Now.Date}',@p_PRICE='{plugin.Price}',@p_PRICE_VAT='{plugin.PriceVat}',@p_VAT='{plugin.Vat}',@p_DISCOUNT_AMT='{plugin.DiscountAmt}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật Plugin thành công";
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