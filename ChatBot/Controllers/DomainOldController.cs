using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChatBot.Data.Infrastructure;
using ChatBot.Infrastructure.Core;
using ChatBot.Model.Models;
using ChatBot.Service;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatBot.Controllers
{
    [Route("api/[controller]")]
    public class DomainOldController : Controller
    {
        IBotDomainService _botDomainService;
        readonly ILoggingRepository _loggingRepository;
        public DomainOldController(IBotDomainService botDomainService, ILoggingRepository loggingRepository)
        {
            _botDomainService = botDomainService;
            _loggingRepository = loggingRepository;
        }

       // [Authorize(Roles = "ViewDomain")]
        //public IEnumerable<DomainViewModel> Get(string DOMAIN_like = "", int _page = 1, int _limit = 20, int _start = 1, int _end = 1)
        //{
        //    IEnumerable<DomainViewModel> domainVm = null;
        //    // PaginationSet<DomainViewModel> pagedSet = null;
        //    try
        //    {
        //        // int totalRow = 0;

        //        var model = _botDomainService.GetAll(DOMAIN_like);
        //        //totalRow = domains.Count();
        //        //var query = domains.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

        //        int currentPage = _page;
        //        int currentPageSize = _limit;

        //        var totalRecord = model.Count();
        //        var totalPages = (int)Math.Ceiling((double)totalRecord / _limit);

        //        var domains = model.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize);
        //        // IEnumerable<DomainViewModel> domainVm = Mapper.Map<IEnumerable<BotDomains>, IEnumerable<DomainViewModel>>(domains);
        //        domainVm = Mapper.Map<IEnumerable<BotDomains>, IEnumerable<DomainViewModel>>(domains);

        //        //pagedSet = new PaginationSet<DomainViewModel>()
        //        //{
        //        //    Page = currentPage,
        //        //    TotalCount = totalPages,
        //        //    TotalPages = (int)Math.Ceiling((decimal)totalRecord / currentPageSize),
        //        //    Items = domainVm
        //        //};
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
        //        _loggingRepository.Commit();
        //    }
        //    return domainVm;
        //}


        //[HttpGet("{id:int}", Name = "GetDomain")]
        //public DomainViewModel Get(int id)
        //{
        //    DomainViewModel domainVm = null;
        //    try
        //    {
        //        var domains = _botDomainService.GetById(id);
        //        domainVm = Mapper.Map<BotDomains, DomainViewModel>(domains);
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
        //        _loggingRepository.Commit();
        //    }
        //    return domainVm;
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody]DomainViewModel botDomain)
        //{

        //    IActionResult _result = new ObjectResult(false);
        //    GenericResult _addResult = null;
        //    try
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        //      BotDomains _newBotDomain = Mapper.Map<DomainViewModel, BotDomains>(botDomain);

        //        BotDomains _newBotDomain = PropertyCopy.Copy<BotDomains, DomainViewModel>(botDomain);


        //        _newBotDomain.CreatedDate = DateTime.Now;
        //        _newBotDomain.DOMAIN_ID = 1;

        //        _botDomainService.Add(_newBotDomain);
        //        _botDomainService.Save();



        //        _addResult = new GenericResult()
        //        {
        //            Succeeded = true,
        //            Message = "Add success."
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _addResult = new GenericResult()
        //        {
        //            Succeeded = false,
        //            Message = ex.Message
        //        };

        //        _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
        //        _loggingRepository.Commit();
        //    }

        //    _result = new ObjectResult(_addResult);
        //    return _result;
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody]DomainViewModel domainViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    BotDomains botDomainUpd = PropertyCopy.Copy<BotDomains, DomainViewModel>(domainViewModel);
        //    botDomainUpd.UpdatedDate = DateTime.Now;
        //    _botDomainService.Update(botDomainUpd);

        //    return new NoContentResult();
        //}


        //[HttpDelete("{id:int}")]
        //public IActionResult Delete(int id)
        //{
        //    IActionResult _result = new ObjectResult(false);
        //    GenericResult _removeResult = null;

        //    try
        //    {
        //        _botDomainService.Delete(id);
        //        _botDomainService.Save();

        //        _removeResult = new GenericResult()
        //        {
        //            Succeeded = true,
        //            Message = "Domain removed."
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _removeResult = new GenericResult()
        //        {
        //            Succeeded = false,
        //            Message = ex.Message
        //        };

        //        _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
        //        _loggingRepository.Commit();
        //    }

        //    _result = new ObjectResult(_removeResult);
        //    return _result;
        //}




    }
}
