using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChatBot.Infrastructure;
using ChatBot.Infrastructure.Core;
using ChatBot.Models;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testCloneOnLinux.Models;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/project")]
    public class PrjProjectController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;

        private readonly gGMSContext _context;
        public PrjProjectController(gGMSContext context)
        {
            this._context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "GetProject")]
        public async Task<IEnumerable<PrjProjectMaster>> Get(string searchString)
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

                cmd = $"dbo.PRJ_PROJECT_MASTER_Lst";

                var result = _context.PrjProjectMaster.FromSql(cmd);

                if (!string.IsNullOrEmpty(searchString))
                {
                    result = result.Where(n => n.NOTES.Contains(searchString)
                                      || n.PROJECT_CODE.Contains(searchString)
                                      || n.PROJECT_NAME.Contains(searchString)
                                      || n.CONTRACT_CODE.Contains(searchString)
                    );
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

        [HttpPost]
        [Route("lookup")]
        //[Authorize(Roles = "LookUpProject")]
        public async Task<IEnumerable<PrjProjectMaster>> LookUp([FromBody]LookUpProject lup)
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

                cmd = "dbo.PRJ_PROJECT_MASTER_Search";

                if (lup != null)
                {
                    cmd +=
                        $" @p_PROJECT_CODE='{lup.Code ?? ""}', @p_STATE='{lup.State ?? ""}'";
                    if (lup.BeginDate != null)
                    {
                        cmd += $",@p_BEGIN_DATE='{lup.BeginDate.Value.ToString("d")}'";
                    }
                    if (lup.EndDate != null)
                    {
                        cmd += $",@p_END_DATE='{lup.EndDate.Value.ToString("d")}'";
                    }
                }
                

                var result = _context.PrjProjectMaster.FromSql(cmd);


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
        //[Authorize(Roles = "GetProjectByCode")]
        public async Task<PrjProjectMaster> GetByCode(string code)
        {
            try
            {
                var cmd = $"dbo.CMS_Project_MASTER_Search @p_PROJECT_CODE = '{code}'";

                var result = await _context.PrjProjectMaster.FromSql(cmd).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        [HttpGet("{id}")]
        [Route("GetById")]
        //[Authorize(Roles = "GetProjectById")]
        public async Task<PrjProjectMaster> GetById(string id)
        {
            try
            {
                string command = $"dbo.PRJ_PROJECT_MASTER_Search @p_PROJECT_ID='{id}' ";
                var result = await _context.PrjProjectMaster.FromSql(command).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("{id}")]
        [Route("GetProjectDtByProjectId")]
        //[Authorize(Roles="GetProjectDTByProjectID")]
        public async Task<IEnumerable<PrjProjectDT>> GetProjectDtByProjectId(string id)
        {
            try
            {
                string command = $"dbo.PRJ_PROJECT_DT_ById @PROJECT_ID='{id}'";
                var result = _context.PrjProjectDT.FromSql(command);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpDelete("{id}")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();

            try
            {

                string command = $"dbo.PRJ_PROJECT_MASTER_Del @PROJECT_ID={id}";
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
        //[Authorize(Roles="AddProject")]
        public async Task<ObjectResult> Post([FromBody]PrjProjectViewModel prjVM)
        {
            GenericResult rs = new GenericResult();

            PrjProjectMaster pro = prjVM.Project;
            IEnumerable<PrjProjectDT> proDts = prjVM.ProjectDT;

            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {
                XElement parent = null;

                if (proDts != null)
                {
                    parent = new XElement("Root");
                    foreach (PrjProjectDT dt in proDts)
                    {
                        XElement child = new XElement("PRJ_PROJECT_DT",
                            new XElement("PROJECT_ID", dt.PROJECT_ID),
                            new XElement("EMPLOYEE_ID", dt.EMPLOYEE_ID),
                            new XElement("PROJECT_CODE", dt.PROJECT_CODE),
                            new XElement("PROJECT_NAME", dt.PROJECT_NAME),
                            new XElement("EMLOYEE_CODE", dt.EMPLOYEE_CODE),
                            new XElement("EMPLOYEE_NAME", dt.EMPLOYEE_NAME),
                            new XElement("STATE", dt.STATE),
                            new XElement("NOTES", dt.NOTES),
                            new XElement("RECORD_STATUS", dt.RECORD_STATUS),
                            new XElement("MAKER_ID", dt.MAKER_ID),
                            new XElement("CREATE_DT", dt.CREATE_DT.Value.ToString("d")),
                            new XElement("AUTH_STATUS", dt.AUTH_STATUS),
                            new XElement("CHECKER_ID", dt.CHECKER_ID),
                            new XElement("APPROVE_DT", dt.APPROVE_DT.Value.ToString("d")),
                            new XElement("EDITOR_ID", dt.EDITOR_ID),
                            new XElement("EDIT_DT", dt.EDIT_DT.Value.ToString("d"))
                        );
                        parent.Add(child);
                    }
                }

                string command = $"dbo.PRJ_PROJECT_MASTER_Ins @p_PROJECT_CODE='{pro.PROJECT_CODE}', @p_PROJECT_NAME=N'{pro.PROJECT_NAME}', @p_BEGIN_DATE='{pro.BEGIN_DATE.Value.ToString("d")}', @p_END_DATE='{pro.END_DATE.Value.ToString("d")}', @p_ESTIMATE_DATE='{pro.ESTIMATE_DATE.Value.ToString("d")}', @p_COMPLETION_DATE='{pro.COMPLETION_DATE.Value.ToString("d")}', @p_STATE='{pro.STATE}', @p_CONTRACT_ID='{pro.CONTRACT_ID}', @p_CONTRACT_CODE='{pro.CONTRACT_CODE}', @p_CONTRACT_TYPE='{pro.CONTRACT_TYPE}', @p_NOTES=N'{pro.NOTES}', @p_RECORD_STATUS='{pro.RECORD_STATUS}', @p_MAKER_ID='{pro.MAKER_ID}', @p_CREATE_DT='{pro.CREATE_DT.Value.ToString("d")}', @p_AUTH_STATUS='{pro.AUTH_STATUS}', @p_CHECKER_ID='{pro.CHECKER_ID}', @p_APPROVE_DT='{pro.APPROVE_DT.Value.ToString("d")}', @p_EDITOR_ID='{pro.EDITOR_ID}', @p_EDIT_DT='{pro.EDIT_DT.Value.ToString("d")}', @DT='{parent}', @p_MYSQL_USERNAME='{pro.MYSQL_USERNAME}', @p_MYSQL_PASSWORD='{pro.MYSQL_PASSWORD}', @p_DATABASE_NAME='{pro.DATABASE_NAME}', @p_DOMAIN='{pro.DOMAIN}', @p_SUB_DOMAIN='{pro.SUB_DOMAIN}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm dự án thành công";
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
        //[Authorize(Roles="UpdateProject")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrjProjectViewModel prjVM)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();

            PrjProjectMaster pro = prjVM.Project;
            IEnumerable<PrjProjectDT> proDts = prjVM.ProjectDT;

            try
            {
                XElement parent = null;
                if (proDts != null)
                {
                    parent = new XElement("Root");
                    foreach (PrjProjectDT dt in proDts)
                    {
                        XElement child = new XElement("PRJ_PROJECT_DT",
                            new XElement("PROJECT_ID", dt.PROJECT_ID),
                            new XElement("EMPLOYEE_ID", dt.EMPLOYEE_ID),
                            new XElement("PROJECT_CODE", dt.PROJECT_CODE),
                            new XElement("PROJECT_NAME", dt.PROJECT_NAME),
                            new XElement("EMLOYEE_CODE", dt.EMPLOYEE_CODE),
                            new XElement("EMPLOYEE_NAME", dt.EMPLOYEE_NAME),
                            new XElement("STATE", dt.STATE),
                            new XElement("NOTES", dt.NOTES),
                            new XElement("RECORD_STATUS", dt.RECORD_STATUS),
                            new XElement("MAKER_ID", dt.MAKER_ID),
                            new XElement("CREATE_DT", dt.CREATE_DT.Value.ToString("d")),
                            new XElement("AUTH_STATUS", dt.AUTH_STATUS),
                            new XElement("CHECKER_ID", dt.CHECKER_ID),
                            new XElement("APPROVE_DT", dt.APPROVE_DT.Value.ToString("d")),
                            new XElement("EDITOR_ID", dt.EDITOR_ID),
                            new XElement("EDIT_DT", dt.EDIT_DT.Value.ToString("d"))
                        );
                        parent.Add(child);
                    }

                }
                string command =
                        $"dbo.PRJ_PROJECT_MASTER_Upd @p_PROJECT_ID='{pro.PROJECT_ID}', @p_PROJECT_CODE='{pro.PROJECT_CODE}', @p_PROJECT_NAME=N'{pro.PROJECT_NAME}', @p_BEGIN_DATE='{pro.BEGIN_DATE.Value.ToString("d")}', @p_END_DATE='{pro.END_DATE.Value.ToString("d")}', @p_ESTIMATE_DATE='{pro.ESTIMATE_DATE.Value.ToString("d")}', @p_COMPLETION_DATE='{pro.COMPLETION_DATE.Value.ToString("d")}', @p_STATE='{pro.STATE}', @p_CONTRACT_ID='{pro.CONTRACT_ID}', @p_CONTRACT_CODE='{pro.CONTRACT_CODE}', @p_CONTRACT_TYPE='{pro.CONTRACT_TYPE}', @p_NOTES=N'{pro.NOTES}', @p_RECORD_STATUS='{pro.RECORD_STATUS}', @p_MAKER_ID='{pro.MAKER_ID}', @p_CREATE_DT='{pro.CREATE_DT.Value.ToString("d")}', @p_AUTH_STATUS='{pro.AUTH_STATUS}', @p_CHECKER_ID='{pro.CHECKER_ID}', @p_APPROVE_DT='{pro.APPROVE_DT.Value.ToString("d")}', @p_EDITOR_ID='{pro.EDITOR_ID}', @p_EDIT_DT='{pro.EDIT_DT.Value.ToString("d")}', @DT='{parent}', @p_MYSQL_USERNAME='{pro.MYSQL_USERNAME}', @p_MYSQL_PASSWORD='{pro.MYSQL_PASSWORD}', @p_DATABASE_NAME='{pro.DATABASE_NAME}', @p_DOMAIN='{pro.DOMAIN}', @p_SUB_DOMAIN='{pro.SUB_DOMAIN}'";

                var result =
                    await _context.Database.ExecuteSqlCommandAsync(command,
                        cancellationToken: CancellationToken.None);

                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật dự án thành công";
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
        ////[HttpGet]
        ////[Route("GenerateMySQLDatabase/{productCode}")]
        
        //private async Task<bool> GenerateMySQLDatabase(string databaseName
        //    , string sourceLocation
        //    , string mySqlConnectionString
        //    , string userName
        //    , string password)
        //{
        //    try
        //    {
        //        DatabaseConnect mySqlConnector = new DatabaseConnect(mySqlConnectionString);

        //        string createDatabaseScript = $"DROP DATABASE IF EXISTS `{databaseName}`;  CREATE DATABASE IF NOT EXISTS `{databaseName}`;  CREATE USER '{userName}'@'localhost' IDENTIFIED BY '{password}'; GRANT ALL PRIVILEGES ON  {databaseName}. * TO '{userName}'@'localhost';FLUSH PRIVILEGES; USE `{databaseName}`;";

        //        //string createDatabaseScript = $"DROP DATABASE IF EXISTS `{databaseName}`;  CREATE DATABASE IF NOT EXISTS `{databaseName}`; USE `{databaseName}`; ";
        //        //string scriptTable = System.IO.File.ReadAllText(@"E:\Lib\Ky 2 nam 4\Wordpress_DB\wordpress.sql");
        //        string scriptTable = System.IO.File.ReadAllText(sourceLocation + "/scriptDB/scriptdb.sql");
        //        string fullScript = createDatabaseScript + " " + scriptTable;
        //        if (await mySqlConnector.ExecuteCommand(fullScript))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }


        //}

        //[HttpPost]
        //[Route("generatesource")]
        //public async Task<ActionResult> Post([FromBody]MyModelGen model)
        //{
        //    var isSQLSuccess = true;
        //    var messageSql = "";
        //    try
        //    {
        //        if (!Directory.Exists(model.Source))
        //        {
        //            return Json(new
        //            {
        //                Message = "Nhập sai đường dẫn nguồn",
        //                Data = "",
        //                IsSQLSuccess = false,
        //                MessageSql = ""
        //            });
        //        }
        //        if (!Directory.Exists(model.Destination))
        //        {
        //            Directory.CreateDirectory(model.Destination);
        //        }
        //        this.Copy(model.Source, model.Destination);
        //        isSQLSuccess = await this.GenerateMySQLDatabase(model.DatabaseName
        //            , model.Source
        //            , model.MySQlConnectionString
        //            , model.DatabaseUser
        //            , model.Password);

        //        if (isSQLSuccess)
        //        {
        //            messageSql = "Gen mysql thành công!";
        //        }
        //        else
        //        {
        //            messageSql = "Gen mysql thất bại!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            Message = ex.Message,
        //            Data = ex,
        //            IsSQLSuccess = isSQLSuccess,
        //            MessageSql = messageSql,
        //        });
        //    }
        //    return Json(new
        //    {
        //        Message = "Sao chép thành công!",
        //        IsSQLSuccess = isSQLSuccess,
        //        MessageSql = messageSql,
        //        Data = ""
        //    });
        //}

        //private void Copy(string sourceDirectory, string targetDirectory)
        //{
        //    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
        //    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

        //    CopyAll(diSource, diTarget);
        //}

        //private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        //{
        //    Directory.CreateDirectory(target.FullName);

        //    // Copy each file into the new directory.
        //    foreach (FileInfo fi in source.GetFiles())
        //    {
        //        // Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
        //        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        //    }

        //    // Copy each subdirectory using recursion.
        //    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        //    {
        //        DirectoryInfo nextTargetSubDir =
        //            target.CreateSubdirectory(diSourceSubDir.Name);
        //        CopyAll(diSourceSubDir, nextTargetSubDir);
        //    }
        //}
        //private void DeleteFolder(string path)
        //{
        //    System.IO.DirectoryInfo di = new DirectoryInfo(path);

        //    foreach (FileInfo file in di.GetFiles())
        //    {
        //        file.Delete();
        //    }
        //    foreach (DirectoryInfo dir in di.GetDirectories())
        //    {
        //        dir.Delete(true);
        //    }
        //    di.Delete();
        //}
    }
}