using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Data.Infrastructure;
using ChatBot.Infrastructure.Core;
using ChatBot.Model.Models;
using ChatBot.Models;
using ChatBot.Service;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]

    public class PageBehaviorEcommerceApiController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;

        IPageBehaviorEcommerceService _PageBehaviorEcommerceService;
        readonly ILoggingRepository _loggingRepository;

        private readonly gGMSContext _context;
        public PageBehaviorEcommerceApiController(IPageBehaviorEcommerceService PageBehaviorEcommerceService, ILoggingRepository loggingRepository, gGMSContext context)
        {
            _PageBehaviorEcommerceService = PageBehaviorEcommerceService;
            _loggingRepository = loggingRepository;
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<PrjProjectMaster>> GetAsync(string searchString)
        {
            string cmd = "";

            try
            {
                //var page = Request.Headers["Pagination"];

                //if (!string.IsNullOrEmpty(page))
                //{
                //    string[] vals = page.ToString().Split(',');

                //    int.TryParse(vals[0], out _page);

                //    int.TryParse(vals[1], out _pageSize);
                //}

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

              //  var totalRecord = result.Count();

             //   var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

             //   Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

               // return await result.Skip((_page - 1) * _pageSize).Take(_pageSize).ToListAsync();
                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
    //    [HttpGet("{id}")]
        [HttpGet]
        [Route("GetPageBehaviorEcommerceByProjectId/{id}")]
        public IEnumerable<PageBehaviorEcommerce> GetPageBehaviorEcommerceByProjectId(string id, string searchString)
        {
            //    var product = _PageBehaviorEcommerceService.GetAll();
            var page = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(page))
            {
                string[] vals = page.ToString().Split(',');

                int.TryParse(vals[0], out _page);

                int.TryParse(vals[1], out _pageSize);
            }


            var PageBehaviorEcommerce = _PageBehaviorEcommerceService.GetPageBehaviorEcommerceByProjectId(id, searchString);


            //foreach (var item in PageBehaviorEcommerce)
            //{
            //    //var x = Decimal.Parse(item.EXIT_RATE);

            //    //double after1 = Math.Round(before1, 1,
            //    //    MidpointRounding.AwayFromZero);

            //    //item.EXIT_RATE = Math.Round(x, 1).ToString("#") + "%";

            //    var x = Decimal.Parse(item.PAGE_VALUE);
            //    item.PAGE_VALUE = Math.Round(x, 2).ToString("#, ##");
            //}
            var totalRecord = PageBehaviorEcommerce.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

            Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

            return PageBehaviorEcommerce.Skip((_page - 1) * _pageSize).Take(_pageSize);

            //return PageBehaviorEcommerce;
        }

       

        [HttpPost("PostPageBehaviorEcommerce")]
        public IActionResult PostPageBehaviorEcommerce([FromBody]Welcome welcome)
        {

            var reports = welcome.FormattedJson;
            var project = welcome.Project;
            IActionResult _result = new ObjectResult(false);
            GenericResult _addResult = null;
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var version = _PageBehaviorEcommerceService.GetVersionFinal(project["PROJECT_ID"]);

                var rows = reports.Reports[0].Data.Rows;
                if (rows == null)
                {
                    _addResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Không có dữ liệu trong 30 ngày gần nhất"
                    };
                    _result = new ObjectResult(_addResult);
                    return _result;

                }

                for (int i = 0; i < rows.Length; i++)
                {
                    var productName = rows[i].Dimensions[0];
                    var values = rows[i].Metrics[0].Values;

                 //   decimal money = Decimal.Parse(values[1]);
                    //  var moeny= Double.Parse(values[0], System.Globalization.NumberStyles.Float);

                    //string[] moeny = values[0].ToString().Split('E');
                    //var x = Double.Parse(moeny[0])*(10^moeny)
                    PageBehaviorEcommerce newPageBehaviorEcommerce = new PageBehaviorEcommerce
                    {
                        PAGE_BEHAVIOR_ECOMMERCE_ID = 0,
                    
                        PAGE_PATH = productName,
                      //  PRODUCT_NAME = productName,
                        PAGE_VIEW = values[0],
                        //  PAGE_VALUE = money.ToString(),
                        PAGE_VALUE = values[1],
                        TIME_ON_PAGE = values[2],
                        EXIT_RATE = values[3],
                        CREATE_DT = DateTime.Now,
                        RECORD_STATUS = "1",
                        VERSION_INT = version + 1,
                        DOMAIN =project["DOMAIN"],
                 //       VERSION = (version + 1).ToString(),

                        PROJECT_ID = project["PROJECT_ID"]
                    };
                    _PageBehaviorEcommerceService.Add(newPageBehaviorEcommerce);
                    _PageBehaviorEcommerceService.Save();
                }

                //PageBehaviorEcommerce _newPageBehaviorEcommerce = PropertyCopy.Copy<PageBehaviorEcommerce, DomainViewModel>(PageBehaviorEcommerce);


                //_newPageBehaviorEcommerce.CREATE_DT = DateTime.Now;
                //_newPageBehaviorEcommerce.PROJECT_ID = 1;





                _addResult = new GenericResult()
                {
                    Succeeded = true,
                    Message = "Add success."
                };
            }
            catch (Exception ex)
            {
                _addResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            _result = new ObjectResult(_addResult);
            return _result;

        }



        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
