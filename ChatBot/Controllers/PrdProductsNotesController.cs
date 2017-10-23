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
    [Route("api/PrdProductsNotes")]
    public class PrdProductsNotesController : Controller
    {
        private IHostingEnvironment _env;
        private readonly gGMSContext _context;
        public PrdProductsNotesController(gGMSContext context)
        {
            this._context = context;            
        }
   
        [HttpGet]
        [Route("GetAll/{searchstring=}")]
        //[Authorize(Roles = "GetAllProductNotes")]
        public async Task<IEnumerable<PrdProductMasterNotes>> GetAllProduct(string searchstring = null)
        {
            try
            {
                var result = await
                     _context.PrdProductMasterNoteses.FromSql("PRD_PRODUCT_MASTER_NOTES_Search @p_TOP=''").ToListAsync();
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
        //[Authorize(Roles ="GetSearchProductNotes")]
        public  async Task<IActionResult> Get(int? page, int? pageSize, string searchstring = null)
        {

            PaginationSet<PrdProductMasterNotes> pagedSet = new PaginationSet<PrdProductMasterNotes>();

            var result =
                 _context.PrdProductMasterNoteses.FromSql("PRD_PRODUCT_MASTER_NOTES_Search @p_TOP=''");

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

            pagedSet = new PaginationSet<PrdProductMasterNotes>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }


        [HttpGet]
        [Route("GetProductById/{id}")]
        //[Authorize(Roles = "GetProductById")]
        public async Task<PrdProductNotesViewModel> GetProductById(string id)
        {
            try
            {
                PrdProductNotesViewModel prdProductViewModel = new PrdProductNotesViewModel();
                string command = $"dbo.PRD_PRODUCT_MASTER_NOTES_ById @PRODUCT_ID = '{id}'";
                var product = await this._context.PrdProductMasterNoteses.FromSql(command).SingleOrDefaultAsync();
                var productDetailCommand = $"dbo.PRD_PRODUCT_DT_NOTES_Search @p_PRODUCT_DT_ID='',@p_PRODUCT_ID='{product.ProductId}',@p_TYPE='',@p_COMPONENT='',@p_NOTES='',@p_RECORD_STATUS='',@p_MAKER_ID='',@p_CREATE_DT='',@p_AUTH_STATUS='',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@p_TOP='' ";
                var listProductDetail = await this._context.PrdProductDt.FromSql(productDetailCommand).ToListAsync();


                if (product != null)
                {
                    string productCategoryDtCommand = $"dbo.PRD_CATEGORY_DT_NOTES_Search @p_PRODUCT_ID = '{id}', @p_TOP=''";
                    var listCategoryDt = await this._context.PrdCategoryDtNoteses.FromSql(productCategoryDtCommand).ToListAsync();
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
                return new PrdProductNotesViewModel();
            }
        }
       


        [HttpPut("{id}")]
        //[Authorize(Roles ="UpdateProduct")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdProductNotesViewModel prdProductViewModel)
        {

            GenericResult rs = new GenericResult();
            try
            {
                string productByIdCommand = $"dbo.PRD_PRODUCT_MASTER_NOTES_ById @PRODUCT_ID = '{prdProductViewModel.PrdProduct.ProductId}'";
                var productResult = await this._context.PrdProductMasterNoteses.FromSql(productByIdCommand).SingleAsync();


                List<PrdProductDtNotes> listProductDetail = new List<PrdProductDtNotes>();
                foreach (var item in prdProductViewModel.ListPlugins)
                {
                    PrdProductDtNotes productDetail = new PrdProductDtNotes();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "P";
                    productDetail.ComponentId = item.PluginId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListSources)
                {
                    PrdProductDtNotes productDetail = new PrdProductDtNotes();
                    productDetail.ProductDtId = 0;
                    productDetail.ProductId = "";
                    productDetail.Type = "S";
                    productDetail.ComponentId = item.SourceId;
                    productDetail.RecordStatus = item.RecordStatus;
                    listProductDetail.Add(productDetail);
                }
                foreach (var item in prdProductViewModel.ListTemplates)
                {

                    PrdProductDtNotes productDetail = new PrdProductDtNotes();
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

                string command = $"dbo.PRD_PRODUCT_MASTER_NOTES_Upd @p_PRODUCT_ID = '{prdProductViewModel.PrdProduct.ProductId}', @p_PRODUCT_CODE='{prdProductViewModel.PrdProduct.ProductCode}',@p_PRODUCT_NAME=N'{prdProductViewModel.PrdProduct.ProductName}',@p_PRODUCT_LOCATION=N'{prdProductViewModel.PrdProduct.ProductLocation}',@p_PRODUCT_TYPE=N'{prdProductViewModel.PrdProduct.ProductType}', @p_PRICE={prdProductViewModel.PrdProduct.Price??0}, @p_PRICE_VAT={prdProductViewModel.PrdProduct.PriceVat??0}, @p_VAT={prdProductViewModel.PrdProduct.Vat??0}, @p_DISCOUNT_AMT={prdProductViewModel.PrdProduct.DiscountAmt??0}, @p_SCRIPTS=N'{prdProductViewModel.PrdProduct.Scripts}',@p_NOTES=N'{prdProductViewModel.PrdProduct.Notes}',@p_RECORD_STATUS='1',@p_MAKER_ID='{prdProductViewModel.PrdProduct.MakerId}',@p_CREATE_DT='{DateTime.Now.Date}',@p_AUTH_STATUS='U',@p_CHECKER_ID='',@p_APPROVE_DT='',@p_EDITOR_ID='',@p_EDIT_DT='',@PRODUCT_DT='{xmlProductDetail}',@CATEGORY_DT='{xmlCategoryDetail}'";
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

                string command = $"dbo.PRD_PRODUCT_MASTER_NOTES_Del @PRODUCT_ID={id}";
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