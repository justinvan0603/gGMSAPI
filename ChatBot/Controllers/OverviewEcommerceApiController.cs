﻿using System;
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

    public class OverviewEcommerceApiController : Controller
    {
        private int _page = 1;
        private int _pageSize = 10;

        IOverviewEcommerceService _overviewEcommerceService;
        readonly ILoggingRepository _loggingRepository;

        private readonly gGMSContext _context;
        public OverviewEcommerceApiController(IOverviewEcommerceService overviewEcommerceService, ILoggingRepository loggingRepository, gGMSContext context)
        {
            _overviewEcommerceService = overviewEcommerceService;
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

                var result = _context.PrjProjectMaster.FromSql(cmd).Where(x => x.NOTES != null && x.NOTES != "");

                if (!string.IsNullOrEmpty(searchString))
                {
                    result = result.Where(n => n.NOTES.Contains(searchString)
                                               || n.PROJECT_CODE.Contains(searchString)
                                               || n.PROJECT_NAME.Contains(searchString)
                                               || n.CONTRACT_CODE.Contains(searchString)
                                               
                    );
                }
                foreach (var item in result)
                {
                    item.DOMAIN = item.SUB_DOMAIN +'.'+item.DOMAIN;
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
        [Route("GetOverviewEcommerceByProjectId/{id}")]
        public IEnumerable<OverviewEcommerce> GetOverviewEcommerceByProjectId(string id, string searchString)
        {
            //    var product = _overviewEcommerceService.GetAll();
            var page = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(page))
            {
                string[] vals = page.ToString().Split(',');

                int.TryParse(vals[0], out _page);

                int.TryParse(vals[1], out _pageSize);
            }


            var overviewEcommerce = _overviewEcommerceService.GetOverviewEcommerceByProjectId(id, searchString);

            var totalRecord = overviewEcommerce.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecord / _pageSize);

            Response.AddPagination(_page, _pageSize, totalRecord, totalPages);

            return overviewEcommerce.Skip((_page - 1) * _pageSize).Take(_pageSize);

            //return overviewEcommerce;
        }

       
        [HttpPost("PostOverviewEcommerce")]
        public IActionResult Post([FromBody]Welcome welcome)
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

                var version = _overviewEcommerceService.GetVersionFinal(project["PROJECT_ID"]);
           
                var rows = reports.Reports[0].Data.Rows;
                for (int i = 0; i < rows.Length; i++)
                {
                    var productName = rows[i].Dimensions[0];
                    var values = rows[i].Metrics[0].Values;

                //    decimal moeny = Decimal.Parse(values[0], System.Globalization.NumberStyles.Any);
                  //  var moeny= Double.Parse(values[0], System.Globalization.NumberStyles.Float);

                    //string[] moeny = values[0].ToString().Split('E');
                    //var x = Double.Parse(moeny[0])*(10^moeny)
                    OverviewEcommerce newOverviewEcommerce = new OverviewEcommerce
                    {
                        OVERVIEW_ECOMMERCE_ID = 0,
                        PRODUCT_NAME = productName,
                    //    ITEM_REVENUE = moeny.ToString(),
                        ITEM_REVENUE = values[0],
                        PRODUCT_DETAIL_VIEWS = values[1],
                        QUANTITY_CHECKED_OUT = values[2],
                        QUANTITY_ADDED_TO_CART = values[3],
                        CREATE_DT = DateTime.Now,
                        RECORD_STATUS = "1",
                        VERSION_INT = version+1,
                        VERSION = (version + 1).ToString(),
                        
                        PROJECT_ID = project["PROJECT_ID"],
                        DOMAIN = project["SUB_DOMAIN"] + "."+project["DOMAIN"]
                    };
                    _overviewEcommerceService.Add(newOverviewEcommerce);
                    _overviewEcommerceService.Save();
                }

                //OverviewEcommerce _newOverviewEcommerce = PropertyCopy.Copy<OverviewEcommerce, DomainViewModel>(overviewEcommerce);


                //_newOverviewEcommerce.CREATE_DT = DateTime.Now;
                //_newOverviewEcommerce.PROJECT_ID = 1;





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
