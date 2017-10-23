using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChatBot.Infrastructure.Core;
using ChatBot.Models;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;
        private readonly gGMSContext _context;
        public CategoryController(gGMSContext context)
        {
            this._context = context;
        }

        [HttpGet("getlistexists/{page:int=0}/{pageSize=12}/{searchString=}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListExists(int? page, int? pageSize, string searchString = null)
        {
            PaginationSet<PrdCategoryMaster> pagedSet = new PaginationSet<PrdCategoryMaster>();

            string cmd = "";

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            int.TryParse(searchString, out int level);

            cmd = $"dbo.PRD_CATEGORY_MASTER_LIST_EXISTS";

            var result = _context.PrdCategoryMaster.FromSql(cmd);

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(
                    n => n.CATEGORY_CODE.Contains(searchString)
                    || n.CATEGORY_NAME.Contains(searchString)
                    || n.NOTES.Contains(searchString)
                    || n.PARENT_NAME.Contains(searchString) 
                    || n.PARENT_CODE.Contains(searchString));
            }

            var totalRecord = result.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();

            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdCategoryMaster>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);

        }

        [HttpGet("{page:int=0}/{pageSize=12}/{searchString=}")]
        //[Authorize(Roles ="GetCategoryBySearchString")]
        public async Task<IActionResult> Get(int? page, int? pageSize, string searchString = null)
        {
            PaginationSet<PrdCategoryMaster> pagedSet = new PaginationSet<PrdCategoryMaster>();

            string cmd = "";

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            int.TryParse(searchString, out int level);

            if (string.IsNullOrEmpty(searchString))
            {
                cmd = $"dbo.PRD_CATEGORY_MASTER_Lst";
            }
            else
            {
                cmd =
                    $"dbo.PRD_CATEGORY_MASTER_Search @p_TYPE_ID = '{searchString}', @p_CATEGORY_CODE = '{searchString}', @p_CATEGORY_NAME = N'{searchString}', @p_PARENT_ID = '{searchString}', @p_IS_LEAF = '{searchString}', @p_CATEGORY_LEVEL = {level}, @p_NOTES = N'{searchString}', @p_RECORD_STATUS = '{searchString}', @p_AUTH_STATUS = '{searchString}', @p_MAKER_ID = '{searchString}', @p_CREATE_DT = '', @p_CHECKER_ID = '{searchString}', @p_APPROVE_DT = '', @p_EDITOR_ID = '{searchString}', @p_EDIT_DT = ''";
            }



            var result = _context.PrdCategoryMaster.FromSql(cmd);


            var totalRecord = result.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();

            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<PrdCategoryMaster>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);

        }

        [HttpGet("{id}")]
        [Route("GetCategoryById")]
        //[Authorize(Roles ="GetCategoryById")]
        public async Task<PrdCategoryMaster> GetById(string id)
        {
            try
            {
                string command = $"dbo.PRD_CATEGORY_MASTER_ById @CATEGORY_ID='{id}' ";
                var result = await _context.PrdCategoryMaster.FromSql(command).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeleteCategory")]
        public async Task<ObjectResult> Delete(string id)
        {
            GenericResult rs = new GenericResult();

            try
            {

                string command = $"dbo.PRD_CATEGORY_MASTER_Del @CATEGORY_ID={id}";
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
        //[Authorize(Roles ="CreateCategory")]
        public async Task<ObjectResult> Post([FromBody]PrdCategoryMaster category)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {
                if (!string.IsNullOrEmpty(category.PARENT_ID))
                {
                    category.IS_LEAF = "Y";
                }
                else
                {
                    category.IS_LEAF = "N";
                    category.CATEGORY_LEVEL = 1;
                }

                string command = $"dbo.PRD_CATEGORY_MASTER_Ins @p_TYPE_ID = '{category.TYPE_ID}', @p_CATEGORY_CODE = '{category.CATEGORY_CODE}', @p_CATEGORY_NAME = N'{category.CATEGORY_NAME}', @p_PARENT_ID = '{category.PARENT_ID}', @p_IS_LEAF = '{category.IS_LEAF}', @p_CATEGORY_LEVEL = {category.CATEGORY_LEVEL}, @p_NOTES = N'{category.NOTES}', @p_RECORD_STATUS = '{category.RECORD_STATUS}', @p_AUTH_STATUS = '{category.AUTH_STATUS}', @p_MAKER_ID = '{category.MAKER_ID}', @p_CREATE_DT = '{category.CREATE_DT.Value.ToString("d")}', @p_CHECKER_ID = '{category.CHECKER_ID}', @p_APPROVE_DT = '{category.APPROVE_DT.Value.ToString("d")}', @p_EDITOR_ID = '{category.EDITOR_ID}', @p_EDIT_DT = '{category.EDIT_DT.Value.ToString("d")}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);


                //return result;
                if (result != -1)
                {
                    rs.Message = "Thêm lĩnh vực thành công";
                    rs.Succeeded = true;
                }
                else if (result == -2)
                {
                    rs.Succeeded = false;
                    rs.Message = "Trùng mã lĩnh vực, yêu cầu chọn mã khác!";
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
        //[Authorize(Roles = "UpdateCategory")]
        public async Task<ObjectResult> Put(string id, [FromBody]PrdCategoryMaster category)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {
                if (!string.IsNullOrEmpty(category.PARENT_ID))
                {
                    category.IS_LEAF = "Y";
                }
                else
                {
                    category.IS_LEAF = "N";
                    category.CATEGORY_LEVEL = 1;
                }
                string command = $"dbo.PRD_CATEGORY_MASTER_Upd @p_CATEGORY_ID='{category.CATEGORY_ID}', @p_TYPE_ID = '{category.TYPE_ID}', @p_CATEGORY_CODE = '{category.CATEGORY_CODE}', @p_CATEGORY_NAME = N'{category.CATEGORY_NAME}', @p_PARENT_ID = '{category.PARENT_ID}', @p_IS_LEAF = '{category.IS_LEAF}', @p_CATEGORY_LEVEL = {category.CATEGORY_LEVEL}, @p_NOTES = N'{category.NOTES}', @p_RECORD_STATUS = '{category.RECORD_STATUS}', @p_AUTH_STATUS = '{category.AUTH_STATUS}', @p_MAKER_ID = '{category.MAKER_ID}', @p_CREATE_DT = '{category.CREATE_DT}', @p_CHECKER_ID = '{category.CHECKER_ID}', @p_APPROVE_DT = '{category.APPROVE_DT}', @p_EDITOR_ID = '{category.EDITOR_ID}', @p_EDIT_DT = '{category.EDIT_DT}'";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                if (result > 0)
                {
                    rs.Succeeded = true;
                    rs.Message = "Cập nhật lĩnh vực thành công";
                }
                else if (result == -2)
                {
                    rs.Succeeded = false;
                    rs.Message = "Trùng mã lĩnh vực, yêu cầu chọn mã khác!";
                }
                else
                {
                    rs.Succeeded = false;
                    rs.Message = "Đã có lỗi xảy ra ";
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
        [HttpGet]
        [Route("GetCategoryTree")]
        //[Authorize(Roles ="GetTreeViewCategory")]
        public async Task<IEnumerable<TreeViewItem>> GetCategoryTree()
        {
            try
            {
                string command = $"dbo.PRD_CATEGORY_MASTER_Search @p_TOP=''";
                var result = await _context.PrdCategoryMaster.FromSql(command).ToListAsync();
                result = result.OrderBy(cat => cat.CATEGORY_LEVEL.Value).ToList();
                result.ForEach(cat => cat.RECORD_STATUS = "0");
                List<TreeViewItem> treeViewData = new List<TreeViewItem>();
                Dictionary<string, List<TreeViewItem>> listTemp = new Dictionary<string, List<TreeViewItem>>();
                foreach (var item in result)
                {
                    if (String.IsNullOrEmpty(item.PARENT_ID))
                    {
                        TreeViewItem treeItem = new TreeViewItem();
                        treeItem.id = item.CATEGORY_ID;
                        treeItem.data = item;
                        treeItem.name = item.CATEGORY_NAME;
                        treeViewData.Add(treeItem);
                        listTemp.Add(treeItem.data.CATEGORY_ID, treeItem.children);

                    }
                    else
                    {

                        TreeViewItem treeItem = new TreeViewItem();
                        treeItem.id = item.CATEGORY_ID;
                        treeItem.data = item;
                        treeItem.name = item.CATEGORY_NAME;
                        listTemp[item.PARENT_ID].Add(treeItem);
                        listTemp.Add(treeItem.data.CATEGORY_ID, treeItem.children);
                    }

                }
                return treeViewData;
                //return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("IsEndNode/{id}")]
        [AllowAnonymous]
        public async Task<ObjectResult> IsEndNode(string id)
        {
            GenericResult rs = new GenericResult();

            if (string.IsNullOrEmpty(id))
            {
                rs.Data = false;
                rs.Succeeded = false;
            }
            else
            {
                rs.Data = true;
                rs.Succeeded = true;
                try
                {
                    var cmd = $"dbo.PRD_CATEGORY_MASTER_Search @p_PARENT_ID='{id}'";

                    var list = _context.PrdCategoryMaster.FromSql(cmd);

                    if (list.Count() > 0)
                    {
                        rs.Data = false;
                        rs.Succeeded = true;
                        rs.Message = "Chưa chọn lĩnh vực cấp con cho lĩnh vực";
                    }
                }
                catch (Exception e)
                {
                    rs.Message = "Lỗi " + e.Message;
                    rs.Data = false;
                    rs.Succeeded = false;
                }
            }
            ObjectResult obj = new ObjectResult(rs);

            return obj;
        }

    }
}