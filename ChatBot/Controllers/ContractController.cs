using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ChatBot.Infrastructure.Core;
using ChatBot.Models;
using ChatBot.ViewModels;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{

    [Produces("application/json")]
    [Route("api/Contract")]
    public class ContractController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;
        private readonly gGMSContext _context;
        public ContractController(gGMSContext context)
        {
            this._context = context;
        }

        [HttpPost]
        [Route("lookup")]
        //[Authorize(Roles="LookUpContract")]
        public async Task<IEnumerable<CmsContractMaster>> LookUp([FromBody]CmsContractMaster model) 
        {
            string cmd = "";

            try
            {
                var pagination = Request.Headers["Pagination"];

                if (!string.IsNullOrEmpty(pagination))
                {
                    string[] vals = pagination.ToString().Split(',');

                    int.TryParse(vals[0], out _page);

                    int.TryParse(vals[1], out _pageSize);
                }

                cmd = "dbo.CMS_CONTRACT_MASTER_Search";

                if (model != null)
                {
                    cmd +=
                        $" @p_CONTRACT_CODE='{model.ContractCode ?? ""}', @p_Status='{model.Status ?? ""}', @p_CUSTOMER_CODE='{model.CustomerCode}', @p_CUSTOMER_NAME='{model.CustomerName}', @p_CONTRACT_TYPE='{model.CONTRACT_TYPE}'";
                    if (model.SignContractDt != null)
                    {
                        cmd += $",@p_SignContractDT='{model.SignContractDt.Value.ToString("d")}'";
                    }
                   
                }


                var result = _context.CmsContractMaster.FromSql(cmd);


                var totalRecord = result.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

                Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

                return await result.Skip((_page - 1) * _pageSize).Take(_pageSize).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        //[Authorize(Roles ="GetContractBySearchString")]
        public async Task<IEnumerable<CmsContractMaster>> Get(string searchString)
        {
            string cmd = "";

            try
            {
                var page = Request.Headers["Pagination"];

                if (!string.IsNullOrEmpty(page))
                {
                    string[] vals = page.ToString().Split(',');

                    int.TryParse(vals[0], out _page);

                    int.TryParse(vals[1], out _pageSize);
                }

                cmd = $"dbo.CMS_CONTRACT_MASTER_Lst";

                var result = _context.CmsContractMaster.FromSql(cmd);

                if (!string.IsNullOrEmpty(searchString))
                {
                    result = result.Where(n => n.ContractCode.Contains(searchString)
                                               || n.Notes.Contains(searchString)
                                               || n.CustomerCode.Contains(searchString)
                                               || n.CustomerName.Contains(searchString));
                }

                var totalRecord = result.Count();

                var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

                Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

                return await result.Skip((_page - 1) * _pageSize).Take(_pageSize).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("getbycode")]
        //[Authorize(Roles ="GetContractByCode")]
        public async Task<CmsContractMaster> GetByCode(string code)
        {

            try
            {

                var cmd = $"dbo.CMS_CONTRACT_MASTER_Search @p_CONTRACT_CODE = '{code}'";


                var result = await _context.CmsContractMaster.FromSql(cmd).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        [HttpGet("{id}")]
        [Route("GetContactById")]
        //[Authorize(Roles ="GetContractByID")]
        public async Task<CmsContractMaster> GetById(string id)
        {
            try
            {
                string command = $"dbo.CMS_CONTRACT_MASTER_ById @CONTRACT_ID='{id}' ";
                var result = await _context.CmsContractMaster.FromSql(command).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("{id}")]
        [Route("GetContractDtByContractId")]
        //[Authorize(Roles ="GetContractDTByContractID")]
        public async Task<IEnumerable<CmsContractDt>> GetContractDtByContractId(string id)
        {
            try
            {
                string command = $"dbo.CMS_CONTRACT_DT_Search @p_CONTRACT_ID={id}";
                var result = _context.CmsContractDt.FromSql(command);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeleteContract")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();

            try
            {

                string command = $"dbo.CMS_CONTRACT_MASTER_Del @CONTRACT_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa hợp đồng thành công!";
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
        //[Authorize(Roles ="CreateContract")]
        public async Task<ObjectResult> Post([FromBody]ContractViewModel contractVM)
        {
            GenericResult rs = new GenericResult();

            CmsContractMaster contr = contractVM.Contract;
            IEnumerable<CmsContractDt> contr_dt = contractVM.ContractDetails;

            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {
                XElement parent = null;

                if (contr_dt != null)
                {
                    parent = new XElement("Root");
                    foreach (CmsContractDt dt in contr_dt)
                    {
                        XElement child = new XElement("CMS_Contract_dt",
                            new XElement("CONTRACT_ID", dt.ContractId),
                            new XElement("PRODUCT_ID", dt.ProductId),
                            new XElement("NOTES", dt.Notes),
                            new XElement("RECORD_STATUS", dt.RecordStatus),
                            new XElement("MAKER_ID", dt.MakerId),
                            new XElement("DepID", dt.DepId),
                            new XElement("CREATE_DT", dt.CreateDt.Value.ToString("d")),
                            new XElement("AUTH_STATUS", dt.AuthStatus),
                            new XElement("CHECKER_ID", dt.CheckerId),
                            new XElement("APPROVE_DT", dt.ApproveDt.Value.ToString("d")),
                            new XElement("EDITOR_ID", dt.EditorId),
                            new XElement("EDIT_DT", dt.EditDt.Value.ToString("d"))
                        );
                        parent.Add(child);
                    }
                }

                string command = $"dbo.CMS_CONTRACT_MASTER_Ins @p_CONTRACT_CODE	= '{contr.ContractCode}',@p_CUSTOMER_ID = '{contr.CustomerId}',@p_Value = {contr.Value}, @p_ExpContract = {contr.ExpContract}, @p_SignContractDT = '{contr.SignContractDt.Value.ToString("d")}', @p_ChargeDT = '{contr.ChargeDt.Value.ToString("d")}', @p_MonthCharge = {contr.MonthCharge}, @p_Status = N'{contr.Status}', @p_NOTES = N'{contr.Notes}', @p_RECORD_STATUS = '{contr.RecordStatus}', @p_MAKER_ID= '{contr.MakerId}', @p_DepID = '{contr.DepId}', @p_CREATE_DT = '{contr.CreateDt.Value.ToString("d")}', @p_AUTH_STATUS = '{contr.AuthStatus}', @p_CHECKER_ID = '{contr.CheckerId}', @p_APPROVE_DT = '{contr.ApproveDt.Value.ToString("d")}', @p_EDITOR_ID = '{contr.EditorId}', @p_EDIT_DT = '{contr.EditDt.Value.ToString("d")}', @p_XmlTemp = N'{contr.XmlTemp}', @p_DataTemp = N'{contr.DataTemp}', @p_DebitBalance = {contr.DebitBalance ?? 0}, @p_PaidAMT = {contr.PaidAmt ?? 0}, @p_DepositAccountBeforLd = {contr.DepositAccountBeforLd ?? 0}, @p_OptimalAMT = {contr.OptimalAmt ?? 0}, @p_SeoAMT = {contr.SeoAmt ?? 0}, @p_DebitMaintainFee = {contr.DebitMaintainFee ?? 0}, @p_DepositAccount = {contr.DepositAccount ?? 0}, @p_DepositLiquidation = {contr.DepositLiquidation ?? 0}, @p_IsFirstFee = {contr.IsFirstFee ?? false}, @p_IsLandingPage = {contr.IsLandingPage ?? false}, @p_TypeGoogle = N'{contr.TypeGoogle}', @p_CONTRACT_TYPE='{contr.CONTRACT_TYPE}', @CONTRACT_DT = '{parent}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm hợp đồng thành công";
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
        //[Authorize(Roles ="UpdateContract")]
        public async Task<ObjectResult> Put(string id, [FromBody]ContractViewModel contractVM)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();

            CmsContractMaster contr = contractVM.Contract;
            IEnumerable<CmsContractDt> contr_dt = contractVM.ContractDetails;

            try
            {
                XElement parent = null;
                if (contr_dt != null)
                {
                    parent = new XElement("Root");
                    foreach (CmsContractDt dt in contr_dt)
                    {
                        XElement child = new XElement("CMS_Contract_dt",
                            new XElement("CONTRACT_ID", dt.ContractId),
                            new XElement("PRODUCT_ID", dt.ProductId),
                            new XElement("NOTES", dt.Notes),
                            new XElement("RECORD_STATUS", dt.RecordStatus),
                            new XElement("MAKER_ID", dt.MakerId),
                            new XElement("DepID", dt.DepId),
                            new XElement("CREATE_DT", dt.CreateDt.Value.ToString("d")),
                            new XElement("AUTH_STATUS", dt.AuthStatus),
                            new XElement("CHECKER_ID", dt.CheckerId),
                            new XElement("APPROVE_DT", dt.ApproveDt.Value.ToString("d")),
                            new XElement("EDITOR_ID", dt.EditorId),
                            new XElement("EDIT_DT", dt.EditDt.Value.ToString("d"))
                        );
                        parent.Add(child);
                    }
                }

                string command = $"dbo.CMS_CONTRACT_MASTER_Upd @p_CONTRACT_ID = '{contr.ContractId}', @p_CONTRACT_CODE	= '{contr.ContractCode}',@p_CUSTOMER_ID = '{contr.CustomerId}',@p_Value = {contr.Value}, @p_ExpContract = {contr.ExpContract}, @p_SignContractDT = '{contr.SignContractDt.Value.ToString("d")}', @p_ChargeDT = '{contr.ChargeDt.Value.ToString("d")}', @p_MonthCharge = {contr.MonthCharge}, @p_Status = N'{contr.Status}', @p_NOTES = N'{contr.Notes}', @p_RECORD_STATUS = '{contr.RecordStatus}', @p_MAKER_ID= '{contr.MakerId}', @p_DepID = '{contr.DepId}', @p_CREATE_DT = '{contr.CreateDt.Value.ToString("d")}', @p_AUTH_STATUS = '{contr.AuthStatus}', @p_CHECKER_ID = '{contr.CheckerId}', @p_APPROVE_DT = '{contr.ApproveDt.Value.ToString("d")}', @p_EDITOR_ID = '{contr.EditorId}', @p_EDIT_DT = '{contr.EditDt.Value.ToString("d")}', @p_XmlTemp = '{contr.XmlTemp}', @p_DataTemp = '{contr.DataTemp}', @p_DebitBalance = {contr.DebitBalance ?? 0}, @p_PaidAMT = {contr.PaidAmt ?? 0}, @p_DepositAccountBeforLd = {contr.DepositAccountBeforLd ?? 0}, @p_OptimalAMT = {contr.OptimalAmt ?? 0}, @p_SeoAMT = {contr.SeoAmt ?? 0}, @p_DebitMaintainFee = {contr.DebitMaintainFee ?? 0}, @p_DepositAccount = {contr.DepositAccount ?? 0}, @p_DepositLiquidation = {contr.DepositLiquidation ?? 0}, @p_IsFirstFee = {contr.IsFirstFee ?? false}, @p_IsLandingPage = {contr.IsLandingPage ?? false}, @p_TypeGoogle = N'{contr.TypeGoogle}', @p_CONTRACT_TYPE='{contr.CONTRACT_TYPE}', @CONTRACT_DT = '{parent}'";

                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật hợp đồng thành công";
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