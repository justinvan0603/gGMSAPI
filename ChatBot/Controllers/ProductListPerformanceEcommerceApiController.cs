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

    public class ProductListPerformanceEcommerceApiController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;

        IProductListPerformanceEcommerceService _ProductListPerformanceEcommerceService;
        readonly ILoggingRepository _loggingRepository;

        private readonly gGMSContext _context;
        public ProductListPerformanceEcommerceApiController(IProductListPerformanceEcommerceService ProductListPerformanceEcommerceService, ILoggingRepository loggingRepository, gGMSContext context)
        {
            _ProductListPerformanceEcommerceService = ProductListPerformanceEcommerceService;
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
        [Route("GetProductListPerformanceEcommerceByProjectId/{id}")]
        public IEnumerable<ProductListPerformanceEcommerce> GetProductListPerformanceEcommerceByProjectId(string id, string searchString)
        {
            //    var product = _ProductListPerformanceEcommerceService.GetAll();
            var page = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(page))
            {
                string[] vals = page.ToString().Split(',');

                int.TryParse(vals[0], out _page);

                int.TryParse(vals[1], out _pageSize);
            }


            var ProductListPerformanceEcommerce = _ProductListPerformanceEcommerceService.GetProductListPerformanceEcommerceByProjectId(id, searchString);

            var totalRecord = ProductListPerformanceEcommerce.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

            Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

            return ProductListPerformanceEcommerce.Skip((_page - 1) * _pageSize).Take(_pageSize);

            //return ProductListPerformanceEcommerce;
        }

       

        [HttpPost("PostProductListPerformanceEcommerce")]
        public IActionResult PostProductListPerformanceEcommerce([FromBody]Welcome welcome)
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

                var version = _ProductListPerformanceEcommerceService.GetVersionFinal(project["PROJECT_ID"]);

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

                    decimal moeny = Decimal.Parse(values[0], System.Globalization.NumberStyles.Any);
                    //  var moeny= Double.Parse(values[0], System.Globalization.NumberStyles.Float);

                    //string[] moeny = values[0].ToString().Split('E');
                    //var x = Double.Parse(moeny[0])*(10^moeny)
                    ProductListPerformanceEcommerce newProductListPerformanceEcommerce = new ProductListPerformanceEcommerce
                    {
                        PRODUCTLIST_PERFORMANCE_ECOMMERCE_ID = 0,
                      //  OVERVIEW_ECOMMERCE_ID = 0,
                        PRODUCTLIST = productName,
                        //  PRODUCT_NAME = productName,
                        //     ITEM_REVENUE = moeny.ToString(),
                        ITEM_REVENUE = values[0],
                        PRODUCT_DETAIL_VIEWS = values[1],
                        QUANTITY_ADDED_TO_CART = values[2],
                        QUANTITY_CHECKED_OUT = values[3],
                        CREATE_DT = DateTime.Now,
                        RECORD_STATUS = "1",
                        VERSION_INT = version + 1,
                        DOMAIN =  project["DOMAIN"],
                 //       VERSION = (version + 1).ToString(),

                        PROJECT_ID = project["PROJECT_ID"]
                    };
                    _ProductListPerformanceEcommerceService.Add(newProductListPerformanceEcommerce);
                    _ProductListPerformanceEcommerceService.Save();
                }

                //ProductListPerformanceEcommerce _newProductListPerformanceEcommerce = PropertyCopy.Copy<ProductListPerformanceEcommerce, DomainViewModel>(ProductListPerformanceEcommerce);


                //_newProductListPerformanceEcommerce.CREATE_DT = DateTime.Now;
                //_newProductListPerformanceEcommerce.PROJECT_ID = 1;





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
