using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatBot.Models;
using ChatBot.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using ChatBot.ViewModels;

namespace ChatBot.Controllers
{
    [Produces("application/json")]
    [Route("api/BotScenario")]
    public class BotScenarioController : Controller
    {
        private readonly gGMSContext _context;
        public BotScenarioController(gGMSContext context)
        {
            this._context = context;
        }
        [HttpGet("{page:int=0}/{pageSize=12}/{username=}/{searchstring=}")]
        //[Authorize(Roles = "GetPluginBySearchAndPaging")]
        public  IActionResult Get(int? page, int? pageSize,string username, string searchstring = null)
        {

            PaginationSet<BotScenario> pagedSet = new PaginationSet<BotScenario>();

            var result =
                 _context.BotScenarios.FromSql($"dbo.BOT_SCENARIO_Search @p_TOP='', @p_USER_NAME = '{username}'");

            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            if (!String.IsNullOrEmpty(searchstring))
            {
                result = result.Where(botdomain => botdomain.NAME.ToLower().Contains(searchstring.ToLower()) ||
                                        botdomain.DOMAIN_NAME.ToLower().Contains(searchstring.ToLower()));
            }

            var totalRecord = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / pageSize.Value);

            var messages = result.Skip((currentPage) * currentPageSize).Take(currentPageSize).ToList();
            Response.AddPagination(currentPage, currentPageSize, totalRecord, totalPages);

            pagedSet = new PaginationSet<BotScenario>()
            {
                Page = currentPage,
                TotalCount = totalRecord,
                TotalPages = totalPages,
                Items = messages
            };

            return new ObjectResult(pagedSet);
        }
        [HttpGet]
        [Route("GetDetailScenario/{id}")]
        public async Task<BotScenarioViewModel> GetDetailScenario(int id)
        {
            try
            {
                BotScenarioViewModel botScenarioViewModel = new BotScenarioViewModel();
                botScenarioViewModel.BotScenario = this._context.BotScenarios.Single(sc => sc.SCENARIO_ID == id);
                botScenarioViewModel.BotQuestions = this._context.BotQuestions.Where(q => q.SCENARIO_ID == id).ToList();
                botScenarioViewModel.BotAnswers = this._context.BotAnswers.Where(a => botScenarioViewModel.BotQuestions.Any(q => q.QUESTION_ID == a.QUESTION_ID)).ToList();
                return botScenarioViewModel;
            }
            catch(Exception ex)
            {
                return new BotScenarioViewModel();
            }
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles ="DeletePlugin")]
        public async Task<ObjectResult> Delete(int id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {
                var target = this._context.BotScenarios.Single(sc => sc.SCENARIO_ID == id);
                this._context.BotScenarios.Remove(target);
                var deleteQuestion = this._context.BotQuestions.Where(question => question.SCENARIO_ID == id).ToList();
                this._context.BotAnswers.RemoveRange(this._context.BotAnswers.Where(ans => deleteQuestion.Any(qu => qu.QUESTION_ID == ans.QUESTION_ID)));
                this._context.BotQuestions.RemoveRange(this._context.BotQuestions.Where(question => question.SCENARIO_ID == id));
                this._context.SaveChanges();
                rs.Succeeded = true;
                rs.Message = "Xóa kịch bản chat thành công!";
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
        public async Task<ObjectResult> Post([FromBody]BotScenarioViewModel botScenarioViewModel)
        {
            GenericResult rs = new GenericResult();
            try
            {
                botScenarioViewModel.BotScenario.RECORD_STATUS = 1;
                this._context.BotScenarios.Add(botScenarioViewModel.BotScenario);
                this._context.SaveChanges();
                int countQuestion = botScenarioViewModel.BotQuestions.Count;
                for(int i =0; i<countQuestion; i++)
                {
                    botScenarioViewModel.BotQuestions[i].SCENARIO_ID = botScenarioViewModel.BotScenario.SCENARIO_ID;
                    botScenarioViewModel.BotQuestions[i].LEVEL = 1;
                    botScenarioViewModel.BotQuestions[i].IS_END = false;
                    botScenarioViewModel.BotQuestions[i].RECORD_STATUS = 1;
                    this._context.BotQuestions.Add(botScenarioViewModel.BotQuestions[i]);
                    this._context.SaveChanges();
                    //if (botScenarioViewModel.BotQuestions[i].QUESTION_TYPE != 1)
                    //{
                        botScenarioViewModel.BotAnswers[i].QUESTION_ID = botScenarioViewModel.BotQuestions[i].QUESTION_ID;
                        botScenarioViewModel.BotAnswers[i].LEVEL = 1;
                        botScenarioViewModel.BotAnswers[i].IS_END = true;
                        botScenarioViewModel.BotAnswers[i].RECORD_STATUS = 1;
                        this._context.BotAnswers.Add(botScenarioViewModel.BotAnswers[i]);
                        this._context.SaveChanges();
                    //}
                    
                }
                
                rs.Succeeded = true;
                rs.Message = "Thêm kịch bản chat thành công";
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
        //[Authorize(Roles ="UpdatePlugin")]
        public async Task<ObjectResult> Put(int id, [FromBody]BotScenarioViewModel botScenarioViewModel)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {

                this._context.BotScenarios.Update(botScenarioViewModel.BotScenario);
                var deleteQuestion = this._context.BotQuestions.Where(question => question.SCENARIO_ID == id).ToList();
                this._context.BotAnswers.RemoveRange(this._context.BotAnswers.Where(ans => deleteQuestion.Any(qu => qu.QUESTION_ID == ans.QUESTION_ID)));
                this._context.BotQuestions.RemoveRange(this._context.BotQuestions.Where(question => question.SCENARIO_ID == id));

                int countQuestion = botScenarioViewModel.BotQuestions.Count;
                for (int i = 0; i < countQuestion; i++)
                {
                    botScenarioViewModel.BotQuestions[i].QUESTION_ID = 0;
                    botScenarioViewModel.BotQuestions[i].SCENARIO_ID = botScenarioViewModel.BotScenario.SCENARIO_ID;
                    botScenarioViewModel.BotQuestions[i].LEVEL = 1;
                    botScenarioViewModel.BotQuestions[i].RECORD_STATUS = 1;
                    this._context.BotQuestions.Add(botScenarioViewModel.BotQuestions[i]);
                    this._context.SaveChanges();
                    //if (botScenarioViewModel.BotQuestions[i].QUESTION_TYPE != 1)
                    //{
                        botScenarioViewModel.BotAnswers[i].ANSWER_ID = 0;
                        botScenarioViewModel.BotAnswers[i].QUESTION_ID = botScenarioViewModel.BotQuestions[i].QUESTION_ID;
                        botScenarioViewModel.BotAnswers[i].LEVEL = 1;
                        botScenarioViewModel.BotAnswers[i].IS_END = true;
                        botScenarioViewModel.BotAnswers[i].RECORD_STATUS = 1;
                        this._context.BotAnswers.Add(botScenarioViewModel.BotAnswers[i]);
                        this._context.SaveChanges();
                    //}
                }
                
                rs.Succeeded = true;
                rs.Message = "Cập nhật domain thành công";
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

        [HttpPut]
        [Route("ChangeState/{id}")]
        //[Authorize(Roles ="UpdatePlugin")]
        public async Task<ObjectResult> Put(int id)
        {
            GenericResult rs = new GenericResult();
            //DEFACEWEBSITEContext context = new DEFACEWEBSITEContext();
            try
            {


                string command = $"dbo.BOT_SCENARIO_Activate @p_SCENARIO_ID = {id}";
                var result = await _context.Database.ExecuteSqlCommandAsync(command, cancellationToken: CancellationToken.None);
                rs.Succeeded = true;
                rs.Message = "Thay đổi trạng thái kịch bản thành công";
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