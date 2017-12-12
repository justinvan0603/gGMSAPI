using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Infrastructure.Core;
using ChatBot.ViewModels;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using ChatBot.ViewModels;
using System.Threading;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/WebControl")]
    public class WebControlController : Controller
    {
        private readonly gGMSContext _context;
        public WebControlController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{searchstring=}")]
        // [Authorize(Roles = "ViewProduct")]
        //[Authorize(Roles ="GetSearchProduct")]
        public  async Task<IActionResult> Get(int? page, int? pageSize, string searchstring = null)
        {

            PaginationSet<WebControlViewModel> pagedSet = new PaginationSet<WebControlViewModel>();

            var result =
                 _context.PrjProjectMaster.FromSql("dbo.PRJ_PROJECT_MASTER_Search @p_TOP=''");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(prj => prj.PROJECT_CODE.ToLower().Contains(searchstring) ||
                                        prj.PROJECT_NAME.ToLower().Contains(searchstring) ||
                                        prj.SUB_DOMAIN.ToLower().Contains(searchstring.ToLower()) ||
                                        prj.DOMAIN.ToLower().Contains(searchstring.ToLower()));
            }
            
            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);


            var webControlResult = _context.CwWebControls.FromSql("dbo.CW_WEB_CONTROL_Search @p_TOP=''").ToList();

            List<WebControlViewModel> listWebControlViewModel = new List<WebControlViewModel>();
            List<PrjProjectMaster> listProject = result.ToList();
            foreach(var item in listProject)
            {
                WebControlViewModel newItem = new WebControlViewModel();
                newItem.PrjProjectMaster = item;
                newItem.CwWebControl = webControlResult.SingleOrDefault(wc => wc.PROJECT_ID.Equals(item.PROJECT_ID));
                if (newItem.CwWebControl == null)
                    newItem.CwWebControl = new CwWebControl();
                newItem.CwWebControl.OPERATION_STATE = "1";
                newItem.CwWebControl.OPERATION_NAME = "Đang hoạt động";
                listWebControlViewModel.Add(newItem);
            }


            var messages = listWebControlViewModel.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<WebControlViewModel>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "UpdateCustomer")]
        public async Task<ObjectResult> Put(string id, [FromBody]WebControlViewModel webControlViewModel)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.CW_WEB_CONTROL_Upd @p_PROJECT_ID = '{webControlViewModel.CwWebControl.PROJECT_ID}', @p_OPERATION_STATE = '{webControlViewModel.CwWebControl.OPERATION_STATE}',@p_RECORD_STATUS='1',@p_AUTH_STATUS='U',@p_MAKER_ID='{webControlViewModel.CwWebControl.MAKER_ID}',@p_CREATE_DT='{webControlViewModel.CwWebControl.CREATE_DT}',@p_EDITOR_ID='{webControlViewModel.CwWebControl.EDITOR_ID}',@p_EDIT_DT='{DateTime.Now.Date}', @p_NOTES = '{webControlViewModel.CwWebControl.NOTES}'";
                //string command = $"dbo.CMS_CUSTOMER_MASTER_Upd @p_CUSTOMER_CODE='{customer.CustomerCode}',@p_CUSTOMER_NAME='{customer.CustomerName}',@p_COMPANY_NAME='{customer.CompanyName}',@p_ADDRESS='{customer.Address}',@p_TAX_CODE='{customer.TaxCode}',@p_Email='{customer.Email}',@p_PhoneNumber='{customer.PhoneNumber}',@p_RECORD_STATUS='1',@p_AUTH_STATUS='U',@p_MAKER_ID='{customer.MakerId}',@p_CREATE_DT='{customer.CreateDt}',@p_EDITOR_ID='{customer.EditorId}',@p_EDIT_DT='{DateTime.Now.Date}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật trạng thái Website thành công";
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