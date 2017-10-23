using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatBot.Infrastructure.Core;
using ChatBot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Controllers
{

    [Produces("application/json")]
    [Route("api/Sources")]
    public class SourceController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;
        private readonly gGMSContext _context;
        public SourceController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet]
        [Route("GetAll/{searchstring=}")]
        //[Authorize(Roles = "GetAllSource")]
        public async Task<IEnumerable<PrdSource>> GetAll(string searchstring = null)
        {
            string command = $"dbo.PRD_SOURCE_Search @p_TOP= ''";
            var result = _context.PrdSource.FromSql(command);
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(src => src.SourceCode.Contains(searchstring) ||
                                                src.SourceName.Contains(searchstring) ||
                                                src.SourceLocation.Contains(searchstring));
            }
            
            return await result.ToListAsync();
        }
        [HttpGet]
        //[Authorize(Roles = "GetSourceSearch")]
        public async Task<IEnumerable<PrdSource>> Get(string searchString = "")

        {

            //gGMSContext mycontext = new gGMSContext();
            try
            {
                var pagination = Request.Headers["Pagination"];

                if (!string.IsNullOrEmpty(pagination))
                {
                    string[] vals = pagination.ToString().Split(',');
                    int.TryParse(vals[0], out _page);
                    int.TryParse(vals[1], out _pageSize);
                }


                string command = $"dbo.PRD_SOURCE_Lst";
                var result = _context.PrdSource.FromSql(command);

                if (!String.IsNullOrEmpty(searchString))
                {
                    result = result.Where(src => src.SourceCode.Contains(searchString) || 
                                                    src.SourceName.Contains(searchString) || 
                                                    src.SourceLocation.Contains(searchString));
                }
                var totalRecord = result.Count();
                var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);



                Response.AddPagination(_page, _pageSize, totalRecord, totalPages);
                //Added these line and the issue gone!
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //IEnumerable<ListdomainObject> listPagedDomain = Mapper.Map<IEnumerable<ListdomainObject>, IEnumerable<ListdomainObject>>(domains);
                //_context.Dispose();
                //_context.Dispose();
                return await result.Skip((_page - 1) * _pageSize).Take(_pageSize).ToListAsync();
            }
            catch (Exception ex)
            {

                return new List<PrdSource>();
            }
        }
        [HttpGet("{id}")]
        [Route("GetSourceId")]
        //[Authorize(Roles = "GetSouceByID")]
        public async Task<PrdSource> GetById(string id)
        {
            try
            {
                string command = $"dbo.PRD_SOURCE_ById @SOURCE_ID='{id}' ";
                var result = await _context.PrdSource.FromSql(command).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "DeleteSource")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_SOURCE_Del @SOURCE_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa source thành công!";
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


        [HttpPost]
        //[Authorize(Roles = "AddSource")]
        public async Task<ObjectResult> Post([FromBody]PrdSource src)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command =
                    $"dbo.PRD_SOURCE_Ins @p_SOURCE_CODE = '{src.SourceCode}', @p_SOURCE_LOCATION = N'{src.SourceLocation}', @p_SOURCE_NAME = N'{src.SourceName}', @p_PRICE = {src.Price}, @p_PRICE_VAT = {src.PriceVat}, @p_VAT = {src.Vat}, @p_DISCOUNT_AMT = {src.DiscountAmt}, @p_NOTES = N'{src.Notes}', @p_RECORD_STATUS = '{src.RecordStatus}', @p_MAKER_ID = '{src.RecordStatus}', @p_DepID = N'{src.DepId}', @p_CREATE_DT = '{DateTime.Now.Date}', @p_AUTH_STATUS = '{src.AuthStatus}', @p_CHECKER_ID = '{src.CheckerId}', @p_APPROVE_DT = '{DateTime.Now.Date}', @p_EDITOR_ID = '{src.EditorId}', @p_EDIT_DT = '{DateTime.Now.Date}'";

                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm source thành công";
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
        //[Authorize(Roles ="UpdateSource")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdSource src)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_SOURCE_Upd @p_SOURCE_ID='{src.SourceId}', @p_SOURCE_CODE = '{src.SourceCode}', @p_SOURCE_LOCATION = N'{src.SourceLocation}', @p_SOURCE_NAME = N'{src.SourceName}',  @p_PRICE = {src.Price}, @p_PRICE_VAT = {src.PriceVat}, @p_VAT = {src.Vat}, @p_DISCOUNT_AMT = {src.DiscountAmt}, @p_NOTES = N'{src.Notes}', @p_RECORD_STATUS = '{src.RecordStatus}', @p_MAKER_ID = '{src.MakerId}', @p_DepID = N'{src.DepId}', @p_CREATE_DT = '{src.CreateDt}', @p_AUTH_STATUS = '{src.AuthStatus}', @p_CHECKER_ID = '{src.CheckerId}', @p_APPROVE_DT = '{src.ApproveDt}', @p_EDITOR_ID = '{src.EditorId}', @p_EDIT_DT = '{src.EditDt}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật source thành công";
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