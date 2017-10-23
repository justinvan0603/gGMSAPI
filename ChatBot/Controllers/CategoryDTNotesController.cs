using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Models;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/CategoryDT")]
    public class CategoryDTNotesController : Controller
    {
        private readonly gGMSContext _context;

        public CategoryDTNotesController(gGMSContext context)
        {
            this._context = context;
        }


        [HttpGet]
        //[Authorize(Roles ="GetCategoryDTBySearchString")]
        public async Task<IEnumerable<PrdCategoryDtNotes>> Get(string searchString = null)
        {
            try
            {
                string cmd = $"dbo.PRD_CATEGORY_DT_Search @TOP = ''";
                var rel = _context.PrdCategoryDtNoteses.FromSql(cmd);

                return rel;
            }
            catch (Exception ex)
            {
                return new List<PrdCategoryDtNotes>();
            }
        }

        [Route("GetProductsByCategory")]
        //[Authorize(Roles="GetProductViewModelByCategory")]
        public async Task<IEnumerable<PrdProductViewModel>> GetProductsByCategory(string id)
        {
            List<PrdProductViewModel> list = new List<PrdProductViewModel>();

            try
            {
                string cmd = $"dbo.PRD_CATEGORY_DT_Search @p_CATEGORY_ID='{id}'";
                var rel = _context.PrdCategoryDt.FromSql(cmd);

                if (!(rel.Count() > 0))
                {
                    list.Add(new PrdProductViewModel()
                    {
                        ListCategory = new List<TreeViewItem>(),
                        ListPlugins = new List<PrdPlugin>(),
                        ListSelectedCategory = new List<TreeViewItem>(),
                        ListSources = new List<PrdSource>(),
                        ListTemplates = new List<PrdTemplate>(),
                        PrdProduct = new PrdProductMaster()
                    });
                }
                foreach (PrdCategoryDt dt in rel)
                {

                    PrdProductsController con = new PrdProductsController(_context);

                    PrdProductViewModel pv = await con.GetProductById(dt.PRODUCT_ID);

                    list.Add(pv);
                }

            }
            catch (Exception ex)
            {
            }

            return list;
        }
    }
}