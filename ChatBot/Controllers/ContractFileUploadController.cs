using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Infrastructure.Core;
using ChatBot.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/contractuploadfile")]
    public class ContractFileUploadController : Controller
    {
        private readonly gGMSContext _db;

        private IHostingEnvironment _env;

        public ContractFileUploadController(gGMSContext context, IHostingEnvironment env)
        {
            this._db = context;
            this._env = env;
        }

        [HttpGet]
        [Route("get")]
        //[Authorize(Roles="GetListFileOfContract")]
        public async Task<ObjectResult> Get(int? page, int? pageSize, string searchString)
        {
            PaginationSet<CmsContractFileUpload> pagedSet = new PaginationSet<CmsContractFileUpload>();

            var result =
                 _db.CmsContractFileUpload.FromSql("dbo.CMS_CONTRACT_FILE_UPLOAD_Lst");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(msg => msg.FILE_NAME.Contains(searchString) ||
                                        msg.FILE_TYPE.Contains(searchString) ||
                                        msg.NOTES.ToLower().Contains(searchString.ToLower()) ||
                                        msg.PATH.ToLower().Contains(searchString.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<CmsContractFileUpload>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpGet]
        [Route("getbyid")]
        //[Authorize(Roles ="GetListFileByIDvsType")]
        public async Task<ObjectResult> GetById(string id, string type)
        {
            GenericResult gr = new GenericResult();

            try
            {
                string command = $"dbo.CMS_CONTRACT_FILE_UPLOAD_ById @ID ='{id}', @TYPE='{type}'";
                var result = _db.CmsContractFileUpload.FromSql(command).ToList();
                gr.Message = "Get thành công danh sách file!";
                gr.Data = result;
                gr.Succeeded = true;
            }
            catch (Exception ex)
            {
                gr.Data = ex;
                gr.Succeeded = false;
                gr.Message = "Lỗi " + ex.Message;
            }


            ObjectResult obj = new ObjectResult(gr);
            return obj;
        }


        [HttpPost]
        //[Authorize(Roles ="AddContractFiles")]
        public async Task<ObjectResult> Post([FromBody] List<CmsContractFileUpload> contractFiles)
        {
            GenericResult gr = new GenericResult();

            XElement parent = null;

            parent = new XElement("Root");

            if (contractFiles != null)
            {
                foreach (CmsContractFileUpload item in contractFiles)
                {
                    XElement child = new XElement("CMS_contract_upload",
                        new XElement("CONTRACT_ID", item.CONTRACT_ID),
                        new XElement("TYPE", item.TYPE),
                        new XElement("FILE_NAME", item.FILE_NAME),
                        new XElement("FILE_SIZE", item.FILE_SIZE),
                        new XElement("FILE_TYPE", item.FILE_TYPE),
                        new XElement("NOTES", item.NOTES),
                        new XElement("RECORD_STATUS", item.RECORD_STATUS),
                        new XElement("AUTH_STATUS", item.AUTH_STATUS),
                        new XElement("MAKER_ID", item.MAKER_ID),
                        new XElement("CREATE_DT", item.CREATE_DT.Value.ToString("d")),
                        new XElement("CHECKER_ID", item.CHECKER_ID),
                        new XElement("APPROVE_DT", item.APPROVE_DT.Value.ToString("d")),
                        new XElement("EDITOR_ID", item.EDITOR_ID),
                        new XElement("EDIT_DT", item.EDIT_DT.Value.ToString("d")));

                    parent.Add(child);
                }

                var cmd = $"dbo.CMS_CONTRACT_FILE_UPLOAD_InsWithXML @data = '{parent}'";

                var result = await _db.Database.ExecuteSqlCommandAsync(cmd, CancellationToken.None);

                if (result > 0)
                {
                    gr.Message = "Insert thành công!";
                    gr.Data = contractFiles;
                    gr.Succeeded = true;
                }
                else
                {
                    gr.Message = "Insert dữ liệu không thành công!";
                    gr.Succeeded = false;
                }

            }
            else
            {
                gr.Message = "Dữ liệu rỗng";
                gr.Succeeded = false;
                gr.Data = null;
            }

            ObjectResult obj = new ObjectResult(gr);
            return obj;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles ="UpdateContractFiles")]
        public async Task<ObjectResult> Put(string id, [FromBody] List<CmsContractFileUpload> contractFiles)
        {
            GenericResult gr = new GenericResult();

            XElement parent = null;

            parent = new XElement("Root");

            if (contractFiles != null)
            {
                foreach (CmsContractFileUpload item in contractFiles)
                {
                    XElement child = new XElement("CMS_contract_upload",
                        new XElement("CONTRACT_ID", item.CONTRACT_ID),
                        new XElement("TYPE", item.TYPE),
                        new XElement("FILE_NAME", item.FILE_NAME),
                        new XElement("FILE_SIZE", item.FILE_SIZE),
                        new XElement("FILE_TYPE", item.FILE_TYPE),
                        new XElement("NOTES", item.NOTES),
                        new XElement("RECORD_STATUS", item.RECORD_STATUS),
                        new XElement("AUTH_STATUS", item.AUTH_STATUS),
                        new XElement("MAKER_ID", item.MAKER_ID),
                        new XElement("CREATE_DT", item.CREATE_DT.Value.ToString("d")),
                        new XElement("CHECKER_ID", item.CHECKER_ID),
                        new XElement("APPROVE_DT", item.APPROVE_DT.Value.ToString("d")),
                        new XElement("EDITOR_ID", item.EDITOR_ID),
                        new XElement("EDIT_DT", item.EDIT_DT.Value.ToString("d")));

                    parent.Add(child);
                }

                var cmd = $"dbo.CMS_CONTRACT_FILE_UPLOAD_UpdWithXML @p_CONTRACT_ID = '{id}', @data = '{parent}'";

                var result = await _db.Database.ExecuteSqlCommandAsync(cmd, CancellationToken.None);

                if (result > 0)
                {
                    gr.Message = "Update thành công!";
                    gr.Data = contractFiles;
                    gr.Succeeded = true;
                }
                else
                {
                    gr.Message = "Update dữ liệu không thành công!";
                    gr.Succeeded = false;
                }

            }
            else
            {
                gr.Message = "Dữ liệu rỗng";
                gr.Succeeded = false;
                gr.Data = null;
            }

            ObjectResult obj = new ObjectResult(gr);
            return obj;

        }

        [HttpPost]
        [Route("contractuploadfile")]
        //[Authorize(Roles ="UploadContractFiles")]
        public async Task<ObjectResult> UploadContractFiles()
        {
            GenericResult gr = new GenericResult();

            var files = Request.Form.Files;
            var contractID = Request.Form["CONTRACT_ID"];
            var rootPath = _env.ContentRootPath;
            var pathRoot = Path.Combine(rootPath, "Container\\Contract");
            var folderPath = Path.Combine(pathRoot, contractID);

            List<CmsContractFileUpload> data = new List<CmsContractFileUpload>();

            if (files != null)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var item in files)
                {
                    if (item != null && item.Length > 0)
                    {
                        var finalPath = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-tt") + item.FileName);
                        using (var stream = new FileStream(finalPath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);

                            FileInfo info = new FileInfo(finalPath);

                            var t = new CmsContractFileUpload()
                            {
                                CONTRACT_ID = contractID,
                                FILE_NAME = info.Name,
                                PATH = info.FullName,
                                FILE_SIZE = info.Length,
                                FILE_TYPE = info.Extension,
                                AUTH_STATUS = "A",
                                RECORD_STATUS = "1",
                                CREATE_DT = DateTime.Now,
                                TYPE = "C"
                            };

                            data.Add(t);
                        }
                    }
                }
                gr.Message = "Upload file thành công!";
                gr.Succeeded = true;
                gr.Data = data;
            }
            else
            {
                gr.Message = "Không có files được tải lên!";
                gr.Data = "";
                gr.Succeeded = false;
            }
            ObjectResult obj = new ObjectResult(gr);
            return obj;
        }


        [HttpPost]
        [Route("uploadfile")]
        //[Authorize(Roles ="UploadFilesToContract")]
        public async Task<ObjectResult> UploadFiles()
        {
            GenericResult gr = new GenericResult();

            var files = Request.Form.Files;
            var contractID = Request.Form["CONTRACT_ID"];
            var rootPath = _env.ContentRootPath;
            var pathRoot = Path.Combine(rootPath, "\\Container\\ContractFilesFromCustomer");
            var folderPath = Path.Combine(pathRoot, contractID);

            List<CmsContractFileUpload> data = new List<CmsContractFileUpload>();

            if (files != null)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var item in files)
                {
                    if (item != null && item.Length > 0)
                    {
                        var finalPath = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-tt") + item.FileName);
                        using (var stream = new FileStream(finalPath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);

                            FileInfo info = new FileInfo(finalPath);

                            var t = new CmsContractFileUpload()
                            {
                                CONTRACT_ID = contractID,
                                FILE_NAME = info.Name,
                                PATH = info.FullName,
                                FILE_SIZE = info.Length,
                                FILE_TYPE = info.Extension,
                                AUTH_STATUS = "A",
                                RECORD_STATUS = "1",
                                CREATE_DT = DateTime.Now,
                                TYPE = "O"
                            };

                            data.Add(t);
                        }
                    }
                }
                gr.Message = "Upload file thành công!";
                gr.Succeeded = true;
                gr.Data = data;
            }
            else
            {
                gr.Message = "Không có files được tải lên!";
                gr.Data = "";
                gr.Succeeded = false;
            }
            ObjectResult obj = new ObjectResult(gr);
            return obj;
        }
    }
}