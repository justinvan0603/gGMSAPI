using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChatBot.Infrastructure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Controllers
{
    using System.Data;
    using System.Data.SqlClient;

    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private gGMSContext _db;

        public TestController(gGMSContext db)
        {
            _db = db;
        }

        public string Get()
        {
            TestFunc();
            int a = 5;
            return "aSD";
        }
        private void TestFunc()
        {
            List<TreeViewItem> treeViewData = new List<TreeViewItem>();
            TreeViewItem item = new TreeViewItem();
            item.id = "123";
            item.data = new PrdCategoryMaster();
            item.data.CATEGORY_ID = "123";
            item.children = new List<TreeViewItem>();
            treeViewData.Add(item);
            Dictionary<string, List<TreeViewItem>> listTemp = new Dictionary<string, List<TreeViewItem>>();
            listTemp.Add(item.data.CATEGORY_ID, item.children);
            listTemp["123"].Add(new TreeViewItem());
            var tempcount = listTemp["123"].Count;
            var oricount = item.children.Count;
            listTemp["123"] = null;
            //listTemp["123"].Clear();

        }

        [HttpGet]
        [Route("Update")]
        public async Task<ObjectResult> Update()
        {
            GenericResult gr = new GenericResult();

            gr.Message = "";

            var list = _db.PrdCategoryMaster.FromSql("dbo.PRD_CATEGORY_MASTER_Lst");


            for (int i = 1; i < 4; i++)
            {
                var list1 = list.Where(n => n.CATEGORY_LEVEL.Equals(i)).ToList();

                foreach (var category in list1)
                {
                    string command =
                        $"dbo.PRD_CATEGORY_MASTER_Upd @p_CATEGORY_ID='{category.CATEGORY_ID}', @p_TYPE_ID = '{category.TYPE_ID}', @p_CATEGORY_CODE = '{category.CATEGORY_CODE}', @p_CATEGORY_NAME = N'{category.CATEGORY_NAME}', @p_PARENT_ID = '{category.PARENT_ID}', @p_IS_LEAF = '{category.IS_LEAF}', @p_CATEGORY_LEVEL = {category.CATEGORY_LEVEL}, @p_NOTES = N'{category.NOTES}', @p_RECORD_STATUS = '{category.RECORD_STATUS}', @p_AUTH_STATUS = '{category.AUTH_STATUS}', @p_MAKER_ID = '{category.MAKER_ID}', @p_CREATE_DT = '{category.CREATE_DT}', @p_CHECKER_ID = '{category.CHECKER_ID}', @p_APPROVE_DT = '{category.APPROVE_DT}', @p_EDITOR_ID = '{category.EDITOR_ID}', @p_EDIT_DT = '{category.EDIT_DT}'";
                    var result = await _db.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);

                    gr.Message += result.ToString();
                }
            }

            ObjectResult objR = new ObjectResult(gr);

            return objR;
        }

        [HttpGet]
        [Route("OnCentOS")]
        public async Task<ObjectResult> OnCentOSTask()
        {
            GenericResult gr = new GenericResult();
            try
            {

                var list = await _db.PrdCategoryMaster.FromSql("dbo.PRD_CATEGORY_MASTER_Lst").ToListAsync();

                gr.Data = list;

                gr.Message = "Get danh sách lĩnh vực thành công!";

                gr.Succeeded = true;
            }
            catch (Exception ex)
            {
                gr.Message = ex.Message;

                gr.Data = ex;

                gr.Succeeded = false;
            }


            ObjectResult objR = new ObjectResult(gr);

            return objR;
        }


        [HttpGet]
        [Route("ConnectionDirect")]
        public async Task<ObjectResult> ConnectionDirect()
        {
            GenericResult gr = new GenericResult();

            try
            {
                string stringGMSDBConnection =
                    "Server=27.0.12.24;Database=gGMS;User Id=gGMS;Password=gGMS@239;MultipleActiveResultSets=True;";

                var conn = new SqlConnection(stringGMSDBConnection);

                List<string> list = new List<string>();

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var cmd = conn.CreateCommand();

                cmd.CommandText = "exec PRD_CATEGORY_MASTER_Lst";

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var name = reader["CATEGORY_NAME"].ToString();

                    list.Add(name);

                }

                gr.Data = list;

            }
            catch (Exception ex)
            {
                gr.Message = ex.Message;

                gr.Data = ex;

                gr.Succeeded = false;
            }

            ObjectResult obj = new ObjectResult(gr);

            return obj;
        }



    }
}