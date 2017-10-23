using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Infrastructure.Core;
using Microsoft.AspNetCore.Authorization;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.DotNet.PlatformAbstractions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/PrdTemplates")]
    public class PrdTemplatesController : Controller
    {
        private readonly gGMSContext _context;

        private IHostingEnvironment _env;
        public PrdTemplatesController(gGMSContext context, IHostingEnvironment env)
        {
            this._context = context;
            _env = env;
        }
        //[HttpGet]
        //[Route("GetObjectStore")]
        //public  object GetObjectStore()
        //{
        //    DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

        //    cmd.CommandText = "dbo.TEST";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection.Open();
        //    var result=   cmd.ExecuteReader();
        //    cmd.Connection.Close();
        //    return result;
        //}
        [HttpGet]
        [Route("GetTemplateCode")]
        //[Authorize(Roles ="GetTemplateByCode")]
        public async Task<string> GetTemplateCode()
        {
            try
            {
                DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.PRD_TEMPLATE_GenCode";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_Kind", "PRD_TEMPLATE_CODE"));
                cmd.Parameters.Add(new SqlParameter("@p_IsShow", "Y"));
                cmd.Parameters.Add(new SqlParameter("@p_KeyGen", SqlDbType.VarChar,15) { Direction = ParameterDirection.Output });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                await cmd.ExecuteNonQueryAsync();
                
                string templateCode = cmd.Parameters["@p_KeyGen"].Value.ToString();
                cmd.Connection.Close();
                return templateCode;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        [HttpGet]
        [Route("GetAll/{searchstring=}")]
        //[Authorize(Roles ="GetAllTemplate")]
        public async Task<IEnumerable<PrdTemplate>> GetAll(string searchstring = null)
        {
            var result =
                 _context.PrdTemplate.FromSql("PRD_TEMPLATE_Search @p_TOP=''");
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(msg => msg.TemplateName.Contains(searchstring) ||
                                        msg.TemplateLocation.Contains(searchstring) ||
                                        msg.TemplateCode.ToLower().Contains(searchstring.ToLower()));
            }
            return await result.ToListAsync();
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{searchstring=}")]
        //[Authorize(Roles ="GetBySearchAndPaging")]
        public  IActionResult Get(int? page, int? pageSize,string searchstring = null)
        {

            PaginationSet<PrdTemplate> pagedSet = new PaginationSet<PrdTemplate>();

            var result =
                 _context.PrdTemplate.FromSql("PRD_TEMPLATE_Search @p_TOP=''");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(msg => msg.TemplateName.Contains(searchstring) ||
                                        msg.TemplateLocation.Contains(searchstring) ||
                                        msg.TemplateCode.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdTemplate>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpGet("{id}")]
        [Route("GetTemplateById")]
        //[Authorize(Roles ="GetTemplateById")]
        public async Task<PrdTemplate> GetById(string id)
        {
            try
            {
                string command = $"dbo.PRD_TEMPLATE_ById @TEMPLATE_ID='{id}' ";
                var result = await _context.PrdTemplate.FromSql(command).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeleetTemplate")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_TEMPLATE_Del @TEMPLATE_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa template thành công!";
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
        [Route("UpdateImageFile")]
        //[Authorize(Roles ="UploadImageToTemplate")]
        public async Task<ObjectResult> UpdateImageFile()
        {
            GenericResult rs = new GenericResult();

            var files = Request.Form.Files;

            string currentImagesPath = Request.Form["CurrentImagesPath"].ToString();
            if (!String.IsNullOrEmpty(currentImagesPath))
            {
                string[] listImagesPath = currentImagesPath.Split(';');
                for (int i = 0; i < listImagesPath.Length; i++)
                {
                    if (System.IO.File.Exists(listImagesPath[i]))
                    {
                        System.IO.File.Delete(listImagesPath[i]);
                    }
                }
            }
            try
            {
                long size = files.Sum(f => f.Length);


                var rootPath = _env.WebRootPath;

                var filePath = Path.Combine(rootPath, "TemplateImages");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
               

                int count = 1;

                string templateCode = Request.Form["TemplateCode"];

                var finalPath = Path.Combine(filePath, templateCode);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }

                string imagesURL = "";
                if (files.Count == 0)
                {
                    rs.Message = "Không đính kèm ảnh!";
                    rs.Succeeded = false;
                    ObjectResult t = new ObjectResult(rs);
                    return t;
                }
                foreach (var formFile in files)
                {

                    if (formFile.Length > 0)
                    {
                        if (formFile.Length > 15000000)
                        {
                            rs.Message = "File " + formFile.Name + "Vượt quá kích thước cho phép";
                            rs.Succeeded = false;
                            ObjectResult objr = new ObjectResult(rs);
                            return objr;
                        }

                        var extension = Path.GetExtension(formFile.FileName);

                        if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                        {
                            rs.Message = "File ảnh phải có định dạng JPG hoặc PNG!";
                            rs.Succeeded = false;
                            ObjectResult objr = new ObjectResult(rs);
                            return objr;
                        }
                        var path = Path.Combine(finalPath, templateCode + "-" + count + extension);
                        count++;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await formFile.CopyToAsync(stream);

                            path = path.Remove(path.IndexOf(rootPath, StringComparison.CurrentCultureIgnoreCase),
                                rootPath.Length);

                            path = path.Replace('\\', '/');

                            imagesURL += path + ";";
                            //count++;
                        }
                    }
                }

                rs.Message = "Upload ảnh thành công!";
                rs.Succeeded = true;
                rs.Data = imagesURL;
                ObjectResult objRes = new ObjectResult(rs);
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


        [HttpPost]
        [Route("PostImageFile")]
        //[Authorize(Roles ="AddImageToTemplate")]
        public async Task<ObjectResult> PostImageFile()
        {
            GenericResult rs = new GenericResult();
            
            var files = Request.Form.Files;
            try
            {
                long size = files.Sum(f => f.Length);

                var rootPath = _env.WebRootPath;

                var filePath = Path.Combine(rootPath, "TemplateImages");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }


                int count = 1;

                string templateCode = Request.Form["TemplateCode"];

                var finalPath = Path.Combine(filePath, templateCode);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }

                string imagesURL = "";
                foreach (var formFile in files)
                {

                    if (formFile.Length > 0)
                    {
                        if (formFile.Length > 15000000)
                        {
                            rs.Message = "File " + formFile.Name + "Vượt quá kích thước cho phép";
                            rs.Succeeded = false;
                            ObjectResult objr = new ObjectResult(rs);
                            return objr;
                        }
                        var extension = Path.GetExtension(formFile.FileName);
                        if (!extension.Equals(".jpg") && !extension.Equals(".png"))
                        {
                            rs.Message = "File ảnh phải có định dạng JPG hoặc PNG!";
                            rs.Succeeded = false;
                            ObjectResult objr = new ObjectResult(rs);
                            return objr;
                        }
                        var path = Path.Combine(finalPath, templateCode +"-" + count + extension);
                        count++;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            
                            await formFile.CopyToAsync(stream);

                            path = path.Remove(path.IndexOf(rootPath, StringComparison.CurrentCultureIgnoreCase),
                                rootPath.Length);

                            path = path.Replace('\\', '/');

                            imagesURL += path + ";";
                            //count++;
                        }
                    }
                }

                rs.Message = "Upload ảnh thành công!";
                rs.Data = imagesURL;
                rs.Succeeded = true;
                ObjectResult objRes = new ObjectResult(rs);
                return objRes;
            }
            catch(Exception ex)
            {
                rs.Message = "Lỗi " + ex.Message;
                rs.Succeeded = false;
                ObjectResult objRes = new ObjectResult(rs);
                //context.Dispose();
                return objRes;
            }
        }

        [HttpPost]
        //[Authorize(Roles ="CreateTemplate")]
        public async Task<ObjectResult> Post([FromBody]PrdTemplate template)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_TEMPLATE_Ins @p_TEMPLATE_CODE='{template.TemplateCode}',@p_TEMPLATE_NAME=N'{template.TemplateName}',@p_TEMPLATE_LOCATION=N'{template.TemplateLocation}',@p_NOTES=N'{template.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{template.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@p_PRICE='{template.Price}',@p_PRICE_VAT='{template.PriceVat}',@p_VAT='{template.Vat}',@p_DISCOUNT_AMT='{template.DiscountAmt}',@p_IMAGES_PATH='{template.IMAGES_PATH}',@p_DEMO_LINK='{template.DEMO_LINK}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm template thành công";
                    rs.Succeeded = true;
                    rs.Data = template;
                }
                else
                {
                    rs.Message = "Có lỗi xảy ra trong quá trình thêm!";
                    rs.Succeeded = false;
                    rs.Data = new object();
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
        //[Authorize(Roles ="UpdateTemplate")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdTemplate template)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_TEMPLATE_Upd @p_TEMPLATE_ID='{template.TemplateId}', @p_TEMPLATE_CODE='{template.TemplateCode}',@p_TEMPLATE_NAME=N'{template.TemplateName}',@p_TEMPLATE_LOCATION=N'{template.TemplateLocation}',@p_NOTES=N'{template.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{template.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='{template.CheckerId}',@p_APPROVE_DT='{template.ApproveDt}',@p_EDITOR_ID='{template.EditorId}',@p_EDIT_DT='{DateTime.Now.Date}',@p_PRICE='{template.Price}',@p_PRICE_VAT='{template.PriceVat}',@p_VAT='{template.Vat}',@p_DISCOUNT_AMT='{template.DiscountAmt}',@p_IMAGES_PATH='{template.IMAGES_PATH}',@p_DEMO_LINK='{template.DEMO_LINK}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Data = template;
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật template thành công";
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