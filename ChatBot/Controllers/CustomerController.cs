using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using ChatBot.Infrastructure.Core;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;
        private readonly gGMSContext _context;
        public CustomerController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet]
        //[Authorize(Roles = "GetAllCustomers")]
        public async Task<IEnumerable<CmsCustomerMaster>> Get(string searchString = "")

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


                string command = $"dbo.CMS_CUSTOMER_MASTER_Search @p_TOP=''";
                var result = await _context.CmsCustomerMaster.FromSql(command).ToListAsync();

                if (!String.IsNullOrEmpty(searchString))
                {
                    result = result.Where(cus => (cus.CustomerCode.ToLower().Contains(searchString.ToLower()) ||
                                                    cus.CustomerName.ToLower().Contains(searchString.ToLower()) ||
                                                    cus.PhoneNumber.ToLower().Contains(searchString) ||
                                                    cus.TaxCode.ToLower().Contains(searchString) ||
                                                    cus.Address.ToLower().Contains(searchString.ToLower()) ||
                                                    cus.Email.ToLower().Contains(searchString.ToLower()) ||
                                                    cus.CompanyName.ToLower().Contains(searchString.ToLower())
                                                    )
                                                    ).ToList();
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
                return result.Skip((_page - 1) * _pageSize).Take(_pageSize).ToList();
            }
            catch (Exception ex)
            {

                return new List<CmsCustomerMaster>();
            }
        }
        [HttpGet("{id}")]
        [Route("GetCustomerById")]
        //[Authorize(Roles = "GetCustomerById")]
        public async Task<CmsCustomerMaster> GetById(string id)
        {
            try
            {
                string command = $"dbo.CMS_CUSTOMER_MASTER_ById @CUSTOMER_ID='{id}' ";
                var result = await _context.CmsCustomerMaster.FromSql(command).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "DeleteCustomer")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.CMS_CUSTOMER_MASTER_Del @CUSTOMER_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa khách hàng thành công!";
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
        //[Authorize(Roles ="CreateCustomer")]
        public async Task<ObjectResult> Post([FromBody]CmsCustomerMaster customer)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.CMS_CUSTOMER_MASTER_Ins @p_CUSTOMER_CODE='{customer.CustomerCode}',@p_CUSTOMER_NAME=N'{customer.CustomerName}',@p_COMPANY_NAME=N'{customer.CompanyName}',@p_ADDRESS=N'{customer.Address}',@p_TAX_CODE='{customer.TaxCode}',@p_Email='{customer.Email}',@p_PhoneNumber='{customer.PhoneNumber}',@p_RECORD_STATUS='1',@p_AUTH_STATUS='U',@p_MAKER_ID='{customer.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm khách hàng thành công";
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
        //[Authorize(Roles = "UpdateCustomer")]
        public async Task<ObjectResult> Put(string id, [FromBody]CmsCustomerMaster customer)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.CMS_CUSTOMER_MASTER_Upd @p_CUSTOMER_ID='{customer.CustomerId}', @p_CUSTOMER_CODE='{customer.CustomerCode}',@p_CUSTOMER_NAME=N'{customer.CustomerName}',@p_COMPANY_NAME=N'{customer.CompanyName}',@p_ADDRESS=N'{customer.Address}',@p_TAX_CODE='{customer.TaxCode}',@p_Email='{customer.Email}',@p_PhoneNumber='{customer.PhoneNumber}',@p_RECORD_STATUS='1',@p_AUTH_STATUS='U',@p_MAKER_ID='{customer.MakerId}',@p_CREATE_DT='{customer.CreateDt}',@p_EDITOR_ID='{customer.EditorId}',@p_EDIT_DT='{DateTime.Now.Date}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật khách hàng thành công";
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