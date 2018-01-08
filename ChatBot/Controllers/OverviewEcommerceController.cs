using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBot.Data.Infrastructure;
using ChatBot.Infrastructure.Core;
using ChatBot.Infrastructure.Mappings;
using ChatBot.Model.Models;
using ChatBot.Service;
using ChatBot.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Controllers
{
    //[Route("api/[controller]")]
    public class OverviewEcommerceController : Controller
    {
        IProductPerformaceService _botDomainService;
        readonly ILoggingRepository _loggingRepository;

        public OverviewEcommerceController(IProductPerformaceService botDomainService, ILoggingRepository loggingRepository)
        {
            _botDomainService = botDomainService;
            _loggingRepository = loggingRepository;
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

       
    }
}