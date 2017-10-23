using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using ChatBot.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using ChatBot.ViewModels;
using System.Threading;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ChatBot.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using testCloneOnLinux.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/PrdProducts")]
    public class PrdProductsController : Controller
    {
        private IHostingEnvironment _env;
        private readonly gGMSContext _context;
        public PrdProductsController(gGMSContext context)
        {
            this._context = context;            
        }
        //public PrdProductsController(gGMSContext context, IHostingEnvironment env)
        //{
        //    this._context = context;
        //    this._env = env;
        //}
        [HttpGet]
        [Route("GetProductCode")]
        //[Authorize(Roles="GetProductByCode")]
        public async Task<string> GetProductCode()
        {
            try
            {
                DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.PRD_PRODUCT_MASTER_GenCode";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_Kind", "PRD_PRODUCT_MASTER_CODE"));
                cmd.Parameters.Add(new SqlParameter("@p_IsShow", "Y"));
                cmd.Parameters.Add(new SqlParameter("@p_KeyGen", SqlDbType.VarChar, 15) { Direction = ParameterDirection.Output });
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                await cmd.ExecuteNonQueryAsync();

                string templateCode = cmd.Parameters["@p_KeyGen"].Value.ToString();
                cmd.Connection.Close();
                return templateCode;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
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
        //       // Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
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
        //private async Task<bool> DeleteMySQLDatabase(string dbname)
        //{
        //    try
        //    {
        //        string command = $"dbo.CM_ALLCODE_Search @p_CDNAME='MYSQL_CONN', @p_TOP = ''";
        //        var result = await this._context.CmAllcode.FromSql(command).ToListAsync();
        //        DatabaseConnect mySqlConnector = new DatabaseConnect(result[0].Content);
        //        string deleteDatabaseScript = $"DROP DATABASE IF EXISTS `{dbname}`";
        //        if(await mySqlConnector.ExecuteCommand(deleteDatabaseScript))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
                
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //[HttpGet("CreateUserSQL")]
        //public bool CreateUserSQL()
        //{
        //    return true;
        //}
        ////[HttpGet]
        ////[Route("GenerateMySQLDatabase/{productCode}")]
        //public async Task<bool> GenerateMySQLDatabase(string productCode, string sourceLocation)
        //{
        //    try
        //    {
        //        string command = $"dbo.CM_ALLCODE_Search @p_CDNAME='MYSQL_CONN', @p_TOP = ''";
        //        var result = await this._context.CmAllcode.FromSql(command).ToListAsync();
        //        DatabaseConnect mySqlConnector = new DatabaseConnect(result[0].Content);
                
        //        //string createDatabaseScript = $"DROP DATABASE IF EXISTS `{productCode}`;  CREATE DATABASE IF NOT EXISTS `{productCode}`;  CREATE USER '{mysqlUsername}'@'localhost' IDENTIFIED BY '{mysqlPassword}'; GRANT ALL PRIVILEGES ON  {productCode}. * TO '{mysqlUsername}'@'localhost';FLUSH PRIVILEGES; USE `{productCode}`;";
        //        string createDatabaseScript = $"DROP DATABASE IF EXISTS `{productCode}`;  CREATE DATABASE IF NOT EXISTS `{productCode}`  ; USE `{productCode}`;";
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
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }


        //}

        //[HttpPost]
        //[Route("GenerateSourcecode")]
        //public async Task<ObjectResult> GenerateSourcecode([FromBody] PrdProductMaster product)
        //{
        //    GenericResult rs = new GenericResult();
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(product.ProductLocation))
        //        {
        //            if (Directory.Exists(product.ProductLocation))
        //            {
        //                DeleteFolder(product.ProductLocation);
        //                //rs.Message = "Source của sản phẩm này đã được khởi tạo!";
        //                //rs.Succeeded = true;
        //                //ObjectResult objRessult = new ObjectResult(rs);
        //                //return objRessult;
        //            }
        //        }
        //        //string path = "D:/ABC/EP";
        //        //if (!Directory.Exists(path))
        //        //    Directory.CreateDirectory(path);
        //        //return null;

        //        string detailCommand = $"dbo.PRD_PRODUCT_DT_Search @p_PRODUCT_ID = '{product.ProductId}', @p_TOP = ''";
        //        List<PrdProductDt> listProductDetail = await this._context.PrdProductDt.FromSql(detailCommand).ToListAsync();
        //        List<PrdTemplate> listTemplate = new List<PrdTemplate>();
        //        List<PrdPlugin> listPlugin = new List<PrdPlugin>();
        //        List<PrdSource> listSource = new List<PrdSource>();

        //        foreach (var item in listProductDetail)
        //        {
        //            if (item.Type.Equals("S"))
        //            {
        //                string sourceCommand = $"dbo.PRD_SOURCE_ById @SOURCE_ID = '{item.ComponentId}'";
        //                var result = await this._context.PrdSource.FromSql(sourceCommand).SingleAsync();
        //                listSource.Add(result);

        //            }
        //            else if (item.Type.Equals("T"))
        //            {
        //                string templateCommand = $"dbo.PRD_TEMPLATE_ById @TEMPLATE_ID = '{item.ComponentId}'";
        //                var result = await this._context.PrdTemplate.FromSql(templateCommand).SingleAsync();
        //                listTemplate.Add(result);
        //            }
        //            else if (item.Type.Equals("P"))
        //            {
        //                string pluginCommand = $"dbo.PRD_PLUGIN_ById @PLUGIN_ID = '{item.ComponentId}'";
        //                var result = await this._context.PrdPlugin.FromSql(pluginCommand).SingleAsync();
        //                listPlugin.Add(result);
        //            }
        //        }

        //        //Create Folder First
        //        if (!Directory.Exists(product.ProductLocation))
        //        {
        //            Directory.CreateDirectory(product.ProductLocation);
        //        }
        //        //var updateCommand = $"dbo.PRD_PRODUCT_MASTER_Upd  ";
        //        //var updateResult = this._context.Database.ExecuteSqlCommandAsync(,CancellationToken.None);
        //        //Copy selected source
        //        if (Directory.Exists(listSource[0].SourceLocation))
        //        {
        //            string destinationPath = Path.GetFullPath(product.ProductLocation);
        //            this.Copy(listSource[0].SourceLocation, destinationPath);
        //            //foreach (string dirPath in Directory.GetDirectories(listSource[0].SourceLocation, "*",SearchOption.AllDirectories))
        //            //    Directory.CreateDirectory(dirPath.Replace(listSource[0].SourceLocation, destinationPath));
        //            //foreach (string newPath in Directory.GetFiles(listSource[0].SourceLocation, "*.*",SearchOption.AllDirectories))
        //            //    System.IO.File.Copy(newPath, newPath.Replace(listSource[0].SourceLocation, destinationPath), true);
        //        }
        //        //Copy plugin
        //        foreach (var item in listPlugin)
        //        {
        //            string pluginFolderName = Path.GetFileName(item.PluginLocation);

        //            string destinationPath = Path.GetFullPath(product.ProductLocation + "/wp-content/plugins/" + pluginFolderName);
        //            Directory.CreateDirectory(destinationPath);
        //            this.Copy(item.PluginLocation, destinationPath);
        //            //foreach (string dirPath in Directory.GetDirectories(item.PluginLocation, "*", SearchOption.AllDirectories))
        //            //    Directory.CreateDirectory(dirPath.Replace(item.PluginLocation, destinationPath));
        //            //foreach (string newPath in Directory.GetFiles(item.PluginLocation, "*.*", SearchOption.AllDirectories))
        //            //    System.IO.File.Copy(newPath, newPath.Replace(item.PluginLocation, destinationPath), true);
        //        }
        //        ////Copy Template
        //        foreach (var item in listTemplate)
        //        {
        //            string templateFolderName = Path.GetFileName(item.TemplateLocation);
        //            string destinationPath = Path.GetFullPath(product.ProductLocation + "/wp-content/themes/" + templateFolderName);
        //            Directory.CreateDirectory(destinationPath);
        //            this.Copy(item.TemplateLocation, destinationPath);
        //            //foreach (string dirPath in Directory.GetDirectories(item.TemplateLocation, "*", SearchOption.AllDirectories))
        //            //    Directory.CreateDirectory(dirPath.Replace(item.TemplateLocation, destinationPath));
        //            //foreach (string newPath in Directory.GetFiles(item.TemplateLocation, "*.*", SearchOption.AllDirectories))
        //            //    System.IO.File.Copy(newPath, newPath.Replace(item.TemplateLocation, destinationPath), true);
        //        }
        //        //Generate MySQL Database for Wordpress
        //        if (!(await this.GenerateMySQLDatabase(product.ProductCode, listSource[0].SourceLocation)))
        //        {
        //            rs.Message = "Có lỗi khi khởi tạo Database cho Website";
        //            rs.Succeeded = true;
        //            ObjectResult obResult = new ObjectResult(rs);
        //            return obResult;
        //        }

        //        rs.Message = "Đã khởi tạo source thành công";
        //        rs.Succeeded = true;
        //        ObjectResult objRes = new ObjectResult(rs);
        //        return objRes;
        //    }
        //    catch (Exception ex)
        //    {
        //        rs.Message = "Lỗi " + ex.Message;
        //        rs.Succeeded = false;
        //        ObjectResult objRes = new ObjectResult(rs);
        //        return objRes;
        //    }
        //}


        [HttpGet]
        [Route("GetAll/{searchstring=}")]
        //[Authorize(Roles = "GetAllProduct")]
        public async Task<IEnumerable<PrdProductMaster>> GetAllProduct(string searchstring = null)
        {
            try
            {
                var result = await
                     _context.PrdProductMaster.FromSql("PRD_PRODUCT_MASTER_Search @p_TOP=''").ToListAsync();
                if (!String.IsNullOrEmpty(searchstring))
                {
                    result = result.Where(prd => prd.ProductName.ToLower().Contains(searchstring) ||
                                            prd.ProductCode.ToLower().Contains(searchstring) ||
                                            prd.ProductLocation.ToLower().Contains(searchstring.ToLower())).ToList();
                }
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        [HttpGet("{page:int=0}/{pageSize=12}/{searchstring=}")]
       // [Authorize(Roles = "ViewProduct")]
        //[Authorize(Roles ="GetSearchProduct")]
        public  async Task<IActionResult> Get(int? page, int? pageSize, string searchstring = null)
        {

            PaginationSet<PrdProductMaster> pagedSet = new PaginationSet<PrdProductMaster>();

            var result =
                 _context.PrdProductMaster.FromSql("PRD_PRODUCT_MASTER_Search @p_TOP=''");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(prd => prd.ProductName.ToLower().Contains(searchstring) ||
                                        prd.ProductCode.ToLower().Contains(searchstring) ||
                                        prd.ProductLocation.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdProductMaster>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpGet("published/{page:int=0}/{pageSize=12}")]
        //[Authorize(Roles ="GetProductsPublished")]
        public async Task<IActionResult> GetPublished(int? page, int? pageSize)
        {

            PaginationSet<PrdProductMaster> pagedSet = new PaginationSet<PrdProductMaster>();

            var result =
                _context.PrdProductMaster.FromSql("PRD_PRODUCT_MASTER_LIST_PUBLISH");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdProductMaster>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }

        [HttpPost]
        //[Authorize("CreateProduct")]
        public async Task<ObjectResult> Post([FromBody]PrdProductViewModel prdProductViewModel)
        {
            GenericResult rs = new GenericResult();
            try
            {
                

                List<PrdProductDt> listProductDetail = new List<PrdProductDt>();
                foreach (var item in prdProductViewModel.ListPlugins)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "P";
                    productDetail.ComponentId = item.PluginId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListSources)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "S";
                    productDetail.ComponentId = item.SourceId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListTemplates)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "T";
                    productDetail.ComponentId = item.TemplateId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                XElement xmlProductDetail = new XElement("Root");
                foreach (var item in listProductDetail)
                {
                    XElement childElement = new XElement("PrdProductDt", new XElement("PRODUCT_ID", item.ProductId),
                                                        new XElement("TYPE", item.Type),
                                                        new XElement("COMPONENT_ID", item.ComponentId),
                                                        new XElement("NOTES", item.Notes),
                                                        new XElement("RECORD_STATUS",item.RecordStatus),
                                                        new XElement("MAKER_ID",prdProductViewModel.PrdProduct.MakerId),
                                                        new XElement("CREATE_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")),
                                                        new XElement("AUTH_STATUS", "U"),
                                                        new XElement("CHECKER_ID", item.CheckerId),
                                                        new XElement("APPROVE_DT", null),
                                                        new XElement("EDITOR_ID", item.EditorId),
                                                        new XElement("EDIT_DT", null));
                    xmlProductDetail.Add(childElement);
                }
                XElement xmlCategoryDetail = new XElement("Root");
                
                var listInsertCategoryDetail = prdProductViewModel.ListSelectedCategory.Where(dt => dt.data.RECORD_STATUS.Equals("1")).ToList();
                foreach (var item in listInsertCategoryDetail)
                {
                    XElement childElement = new XElement("PrdCategoryDt", new XElement("CATEGORY_ID",item.data.CATEGORY_ID),
                                                                       new XElement("PRODUCT_ID", 0),
                                                                       new XElement("NOTES", item.data.NOTES),
                                                                       new XElement("RECORD_STATUS", item.data.RECORD_STATUS),
                                                                       new XElement("AUTH_STATUS", "U"),
                                                                       new XElement("MAKER_ID", prdProductViewModel.PrdProduct.MakerId),
                                                                       new XElement("CREATE_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")),
                                                                       new XElement("CHECKER_ID", item.data.CHECKER_ID),
                                                                       new XElement("APPROVE_DT", null),
                                                                       new XElement("EDITOR_ID", item.data.EDITOR_ID),
                                                                       new XElement("EDIT_DT", null));
                    xmlCategoryDetail.Add(childElement);
                }

                string command = $"dbo.PRD_PRODUCT_MASTER_Ins @p_PRODUCT_CODE='{prdProductViewModel.PrdProduct.ProductCode}',@p_PRODUCT_NAME=N'{prdProductViewModel.PrdProduct.ProductName}',@p_PRODUCT_LOCATION=N'{prdProductViewModel.PrdProduct.ProductLocation}',@p_PRODUCT_TYPE=N'{prdProductViewModel.PrdProduct.ProductType}', @p_PRICE={prdProductViewModel.PrdProduct.Price??0}, @p_PRICE_VAT={prdProductViewModel.PrdProduct.PriceVat??0}, @p_VAT={prdProductViewModel.PrdProduct.Vat??0}, @p_DISCOUNT_AMT={prdProductViewModel.PrdProduct.DiscountAmt??0}, @p_SCRIPTS=N'{prdProductViewModel.PrdProduct.Scripts}',@p_NOTES=N'{prdProductViewModel.PrdProduct.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{prdProductViewModel.PrdProduct.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@PRODUCT_DT='{xmlProductDetail}',@CATEGORY_DT='{xmlCategoryDetail}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm sản phẩm thành công";
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


        [HttpPost]
        [Route("ProductNotes")]
        [AllowAnonymous]
        public async Task<ObjectResult> PostNotes([FromBody]PrdProductViewModel prdProductViewModel)
        {
            GenericResult rs = new GenericResult();
            try
            {


                List<PrdProductDt> listProductDetail = new List<PrdProductDt>();
                foreach (var item in prdProductViewModel.ListPlugins)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "P";
                    productDetail.ComponentId = item.PluginId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListSources)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "S";
                    productDetail.ComponentId = item.SourceId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListTemplates)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "T";
                    productDetail.ComponentId = item.TemplateId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                XElement xmlProductDetail = new XElement("Root");
                foreach (var item in listProductDetail)
                {
                    XElement childElement = new XElement("PrdProductDt", new XElement("PRODUCT_ID", item.ProductId),
                        new XElement("TYPE", item.Type),
                        new XElement("COMPONENT_ID", item.ComponentId),
                        new XElement("NOTES", item.Notes),
                        new XElement("RECORD_STATUS", item.RecordStatus),
                        new XElement("MAKER_ID", prdProductViewModel.PrdProduct.MakerId),
                        new XElement("CREATE_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")),
                        new XElement("AUTH_STATUS", "U"),
                        new XElement("CHECKER_ID", item.CheckerId),
                        new XElement("APPROVE_DT", null),
                        new XElement("EDITOR_ID", item.EditorId),
                        new XElement("EDIT_DT", null));
                    xmlProductDetail.Add(childElement);
                }
                XElement xmlCategoryDetail = new XElement("Root");

                var listInsertCategoryDetail = prdProductViewModel.ListSelectedCategory.Where(dt => dt.data.RECORD_STATUS.Equals("1")).ToList();
                foreach (var item in listInsertCategoryDetail)
                {
                    XElement childElement = new XElement("PrdCategoryDt", new XElement("CATEGORY_ID", item.data.CATEGORY_ID),
                        new XElement("PRODUCT_ID", 0),
                        new XElement("NOTES", item.data.NOTES),
                        new XElement("RECORD_STATUS", item.data.RECORD_STATUS),
                        new XElement("AUTH_STATUS", "U"),
                        new XElement("MAKER_ID", prdProductViewModel.PrdProduct.MakerId),
                        new XElement("CREATE_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")),
                        new XElement("CHECKER_ID", item.data.CHECKER_ID),
                        new XElement("APPROVE_DT", null),
                        new XElement("EDITOR_ID", item.data.EDITOR_ID),
                        new XElement("EDIT_DT", null));
                    xmlCategoryDetail.Add(childElement);
                }

                string command = $"dbo.PRD_PRODUCT_MASTER_NOTES_Ins @p_PRODUCT_CODE='{prdProductViewModel.PrdProduct.ProductId}', @p_PRODUCT_NAME=N'{prdProductViewModel.PrdProduct.ProductName}', @p_PRODUCT_LOCATION=N'{prdProductViewModel.PrdProduct.ProductLocation}', @p_PRODUCT_TYPE=N'{prdProductViewModel.PrdProduct.ProductType}', @p_PRICE={prdProductViewModel.PrdProduct.Price??0}, @p_PRICE_VAT={prdProductViewModel.PrdProduct.PriceVat??0}, @p_VAT={prdProductViewModel.PrdProduct.Vat??0}, @p_DISCOUNT_AMT={prdProductViewModel.PrdProduct.DiscountAmt??0}, @p_SCRIPTS=N'{prdProductViewModel.PrdProduct.Scripts}', @p_NOTES=N'{prdProductViewModel.PrdProduct.Notes}', @p_RECORD_STATUS='1', @p_MAKER_ID='{prdProductViewModel.PrdProduct.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@PRODUCT_DT='{xmlProductDetail}', @CATEGORY_DT='{xmlCategoryDetail}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm sản phẩm thành công";
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



        [HttpGet]
        [Route("GetProductById/{id}")]
        //[Authorize(Roles = "GetProductById")]
        public async Task<PrdProductViewModel> GetProductById(string id)
        {
            try
            {
                PrdProductViewModel prdProductViewModel = new PrdProductViewModel();
                string command = $"dbo.PRD_PRODUCT_MASTER_ById @PRODUCT_ID = '{id}'";
                var product = await this._context.PrdProductMaster.FromSql(command).SingleOrDefaultAsync();
                var productDetailCommand = $"dbo.PRD_PRODUCT_DT_Search @p_PRODUCT_DT_ID='',@p_PRODUCT_ID='{product.ProductId}',@p_TYPE='',@p_COMPONENT='',@p_NOTES='',@p_RECORD_STATUS='',@p_MAKER_ID='',@p_CREATE_DT='',@p_AUTH_STATUS='',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@p_TOP='' ";
                var listProductDetail = await this._context.PrdProductDt.FromSql(productDetailCommand).ToListAsync();


                if (product != null)
                {
                    string productCategoryDtCommand = $"dbo.PRD_CATEGORY_DT_Search @p_PRODUCT_ID = '{id}', @p_TOP=''";
                    var listCategoryDt = await this._context.PrdCategoryDt.FromSql(productCategoryDtCommand).ToListAsync();
                    string categoryMasterSearchCommand = $"dbo.PRD_CATEGORY_MASTER_Search @p_TOP=''";
                    var listCategoryMaster = await this._context.PrdCategoryMaster.FromSql(categoryMasterSearchCommand).ToListAsync();

                    listCategoryMaster.ForEach(cat => cat.RECORD_STATUS = "0");
                    if (listCategoryDt != null)
                    {
                        if (listCategoryDt.Count > 0)
                        {
                            foreach (var item in listCategoryMaster)
                            {
                                   
                                item.RECORD_STATUS = listCategoryDt.Any(dt => dt.CATEGORY_ID.Equals(item.CATEGORY_ID))? "1":"0";
                            }
                        }
                    }
                    listCategoryMaster = listCategoryMaster.OrderBy(cat => cat.CATEGORY_LEVEL.Value).ToList();

                    List<TreeViewItem> treeViewData = new List<TreeViewItem>();
                    Dictionary<string, List<TreeViewItem>> listTemp = new Dictionary<string, List<TreeViewItem>>();
                    prdProductViewModel.ListSelectedCategory = new List<TreeViewItem>();
                    foreach (var item in listCategoryMaster)
                    {
                        if (String.IsNullOrEmpty(item.PARENT_ID))
                        {
                            TreeViewItem treeItem = new TreeViewItem();
                            treeItem.id = item.CATEGORY_ID;
                            treeItem.data = item;
                            treeItem.name = item.CATEGORY_NAME;
                            treeViewData.Add(treeItem);
                            if (item.RECORD_STATUS.Equals("1"))
                                prdProductViewModel.ListSelectedCategory.Add(treeItem);
                            listTemp.Add(treeItem.data.CATEGORY_ID, treeItem.children);

                        }
                        else
                        {

                            TreeViewItem treeItem = new TreeViewItem();
                            treeItem.id = item.CATEGORY_ID;
                            treeItem.data = item;
                            treeItem.name = item.CATEGORY_NAME;
                            if (item.RECORD_STATUS.Equals("1"))
                                prdProductViewModel.ListSelectedCategory.Add(treeItem);
                            listTemp[item.PARENT_ID].Add(treeItem);
                            listTemp.Add(treeItem.data.CATEGORY_ID, treeItem.children);


                        }
                    }


                    prdProductViewModel.ListCategory = treeViewData;
                    prdProductViewModel.PrdProduct = product;
                    var pluginCommand = $"dbo.PRD_PLUGIN_Search @p_TOP='' ";
                    prdProductViewModel.ListPlugins = await _context.PrdPlugin.FromSql(pluginCommand).ToListAsync();
                    var sourceCommand = $"dbo.PRD_SOURCE_Search @p_TOP=''";
                    var templateCommand = $"dbo.PRD_TEMPLATE_Search @p_TOP=''";
                    prdProductViewModel.ListSources = await _context.PrdSource.FromSql(sourceCommand).ToListAsync();
                    prdProductViewModel.ListTemplates = await _context.PrdTemplate.FromSql(templateCommand).ToListAsync();

                    prdProductViewModel.ListSources.ForEach(item => item.RecordStatus = "0");
                    prdProductViewModel.ListTemplates.ForEach(item => item.RecordStatus = "0");
                    prdProductViewModel.ListPlugins.ForEach(item => item.RecordStatus = "0");
                    //prdProductViewModel.ListPlugins=prdProductViewModel.ListPlugins.Where(plg => !listProductDetail.Exists(dt => dt.ComponentId.Equals(plg.PluginId) && dt.Type.Equals("P"))).ToList();

                    //prdProductViewModel.ListTemplates=prdProductViewModel.ListTemplates.Where(temp => !listProductDetail.Exists(dt => dt.ComponentId.Equals(temp.TemplateId) && dt.Type.Equals("T"))).ToList();

                    //prdProductViewModel.ListSources=prdProductViewModel.ListSources.Where(src => !listProductDetail.Exists(dt => dt.ComponentId.Equals(src.SourceId) && dt.Type.Equals("S"))).ToList();
                    foreach (var item in listProductDetail)
                    {
                        if (item.Type.Equals("P"))
                        {

                            prdProductViewModel.ListPlugins.Single(plg => plg.PluginId.Equals(item.ComponentId)).RecordStatus = item.RecordStatus;

                        }
                        if (item.Type.Equals("S"))
                        {
                            prdProductViewModel.ListSources.Single(src => src.SourceId.Equals(item.ComponentId)).RecordStatus = item.RecordStatus;

                        }
                        if (item.Type.Equals("T"))
                        {
                            prdProductViewModel.ListTemplates.Single(src => src.TemplateId.Equals(item.ComponentId)).RecordStatus = item.RecordStatus;

                        }
                    }
                    // prdProductViewModel.ListPlugins =
                }
                prdProductViewModel.ListPlugins.RemoveAll(plg => plg.RecordStatus.Equals("0"));
                prdProductViewModel.ListSources.RemoveAll(src => src.RecordStatus.Equals("0"));
                prdProductViewModel.ListTemplates.RemoveAll(temp => temp.RecordStatus.Equals("0"));
                return prdProductViewModel;
            }
            catch (Exception ex)
            {
                return new PrdProductViewModel();
            }
        }
       


        [HttpPut("{id}")]
        //[Authorize(Roles ="UpdateProduct")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdProductViewModel prdProductViewModel)
        {

            GenericResult rs = new GenericResult();
            try
            {
                string productByIdCommand = $"dbo.PRD_PRODUCT_MASTER_ById @PRODUCT_ID = '{prdProductViewModel.PrdProduct.ProductId}'";
                var productResult = await this._context.PrdProductMaster.FromSql(productByIdCommand).SingleAsync();


                List<PrdProductDt> listProductDetail = new List<PrdProductDt>();
                foreach (var item in prdProductViewModel.ListPlugins)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "P";
                    productDetail.ComponentId = item.PluginId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListSources)
                {
                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "S";
                    productDetail.ComponentId = item.SourceId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListTemplates)
                {

                    PrdProductDt productDetail = new PrdProductDt();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "T";
                    productDetail.ComponentId = item.TemplateId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                XElement xmlProductDetail = new XElement("Root");
                foreach (var item in listProductDetail)
                {
                    XElement childElement = new XElement("PrdProductDt", new XElement("PRODUCT_ID", item.ProductId),
                                                        new XElement("TYPE", item.Type),
                                                        new XElement("COMPONENT_ID", item.ComponentId),
                                                        new XElement("NOTES", item.Notes),
                                                        new XElement("RECORD_STATUS", item.RecordStatus),
                                                        new XElement("MAKER_ID", item.MakerId),
                                                        new XElement("CREATE_DT", item.CreateDt != null? item.CreateDt.Value.Date.ToString("dd/MM/yyyy"):null),
                                                        new XElement("AUTH_STATUS", "U"),
                                                        new XElement("CHECKER_ID", item.CheckerId),
                                                        new XElement("APPROVE_DT",null),
                                                        new XElement("EDITOR_ID", prdProductViewModel.PrdProduct.EditorId),
                                                        new XElement("EDIT_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")));
                    xmlProductDetail.Add(childElement);
                }


                XElement xmlCategoryDetail = new XElement("Root");

                var listInsertCategoryDetail = prdProductViewModel.ListSelectedCategory.Where(dt => dt.data.RECORD_STATUS.Equals("1")).ToList();
                foreach (var item in listInsertCategoryDetail)
                {
                    XElement childElement = new XElement("PrdCategoryDt", new XElement("CATEGORY_ID", item.data.CATEGORY_ID),
                                                                       new XElement("PRODUCT_ID", prdProductViewModel.PrdProduct.ProductId),
                                                                       new XElement("NOTES", item.data.NOTES),
                                                                       new XElement("RECORD_STATUS", "1"),
                                                                       new XElement("AUTH_STATUS", "U"),
                                                                       new XElement("MAKER_ID", prdProductViewModel.PrdProduct.MakerId),
                                                                       new XElement("CREATE_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")),
                                                                       new XElement("CHECKER_ID", item.data.CHECKER_ID),
                                                                       new XElement("APPROVE_DT", null),
                                                                       new XElement("EDITOR_ID", item.data.EDITOR_ID),
                                                                       new XElement("EDIT_DT", DateTime.Now.Date.ToString("dd/MM/yyyy")));
                    xmlCategoryDetail.Add(childElement);
                }

                string command = $"dbo.PRD_PRODUCT_MASTER_Upd @p_PRODUCT_ID = '{prdProductViewModel.PrdProduct.ProductId}', @p_PRODUCT_CODE='{prdProductViewModel.PrdProduct.ProductCode}',@p_PRODUCT_NAME=N'{prdProductViewModel.PrdProduct.ProductName}',@p_PRODUCT_LOCATION=N'{prdProductViewModel.PrdProduct.ProductLocation}',@p_PRODUCT_TYPE=N'{prdProductViewModel.PrdProduct.ProductType}', @p_PRICE={prdProductViewModel.PrdProduct.Price??0}, @p_PRICE_VAT={prdProductViewModel.PrdProduct.PriceVat??0}, @p_VAT={prdProductViewModel.PrdProduct.Vat??0}, @p_DISCOUNT_AMT={prdProductViewModel.PrdProduct.DiscountAmt??0}, @p_SCRIPTS=N'{prdProductViewModel.PrdProduct.Scripts}',@p_NOTES=N'{prdProductViewModel.PrdProduct.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{prdProductViewModel.PrdProduct.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@PRODUCT_DT='{xmlProductDetail}',@CATEGORY_DT='{xmlCategoryDetail}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);


                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật sản phẩm thành công";
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


        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeleteProduct")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                string command = $"dbo.PRD_PRODUCT_MASTER_Del @PRODUCT_ID={id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Xóa sản phẩm thành công!";
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

    }

}