using System;
using System.Collections.Generic;
using System.Linq;
using ChatBot.Common;
using ChatBot.Data.Respositories;
using ChatBot.Model.Models;

namespace ChatBot.Service
{
    public interface IOverviewEcommerceService
    {

        IEnumerable<OverviewEcommerce> GetAll();

        IEnumerable<OverviewEcommerce> GetAll(string keyword);
        OverviewEcommerce GetById(int id);


        IQueryable<OverviewEcommerce> GetOverviewEcommerceByProjectId(string id, string keyword);

       

        void Add(OverviewEcommerce botDomain);

        void Update(OverviewEcommerce botDomain);

        void Delete(int id);

        void Save();

        int GetVersionFinal(string projectId);

        void RemoveVersionOld(string projectId);

        StatisticsOverviewEcommerceViewModel CountViewOverviewEcommerce(string keyword);
    }
    public class OverviewEcommerceService : IOverviewEcommerceService
    {

        //  IUnitOfWork _unitOfWork;
        readonly IOverviewEcommerceRepository _botDomainRepository;

        public OverviewEcommerceService(IOverviewEcommerceRepository botDomainRepository)
        {
            _botDomainRepository = botDomainRepository;
        }

        public int GetVersionFinal(string projectId)
        {
            return _botDomainRepository.GetVersionFinal(projectId);
        }
        public IEnumerable<OverviewEcommerce> GetAll()
        {
            return _botDomainRepository.GetAll().Where(x => x.RECORD_STATUS=="1");
        }

        public OverviewEcommerce GetById(int id)
        {
            return _botDomainRepository.GetSingle(id);
        }

        public IQueryable<OverviewEcommerce> GetOverviewEcommerceByProjectId(string id, string keyword)
        {
          //  var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id).OrderByDescending(x => x.TRANSACTIONREVENUE);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.DOMAIN.Contains(keyword));
            return result;
        }
        public StatisticsOverviewEcommerceViewModel CountViewOverviewEcommerce(string keyword)
        {
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1").ToList();

            StatisticsOverviewEcommerceViewModel statisticsViewModel = new StatisticsOverviewEcommerceViewModel();
            double sum = 0;
            double productdetailviews=0;
            double timeonpage = 0;
            foreach (var item in result)
            {
                statisticsViewModel.SESSIONS += Double.Parse(item.SESSIONS);
                statisticsViewModel.PAGEVIEWS += Double.Parse(item.PAGEVIEWS);
                timeonpage += Double.Parse(item.TIMEONPAGE);
                productdetailviews += Double.Parse(item.TRANSACTIONREVENUE);

                statisticsViewModel.PRODUCTDETAILVIEWS += Double.Parse(item.PRODUCTDETAILVIEWS);
                statisticsViewModel.PRODUCTADDSTOCART += Double.Parse(item.PRODUCTADDSTOCART);
                statisticsViewModel.PRODUCTCHECKOUTS += Double.Parse(item.PRODUCTCHECKOUTS);

                statisticsViewModel.USERS += Double.Parse(item.USERS);
                statisticsViewModel.NEWS_USERS += Double.Parse(item.NEWS_USERS);
            }
            statisticsViewModel.TRANSACTIONREVENUE =
                (productdetailviews / 10).ToString("#,##");
            statisticsViewModel.TIMEONPAGE =
                (timeonpage / (10)).ToString("#,##");

            return statisticsViewModel;
           // return null;
        }

        public void Add(OverviewEcommerce botDomain)
        {
            _botDomainRepository.Add(botDomain);
        }

        public void Update(OverviewEcommerce botDomain)
        {
            _botDomainRepository.Update(botDomain);
        }

        public void Delete(int id)
        {
            _botDomainRepository.Delete(id);

        }

        public void Save()
        {
            _botDomainRepository.Commit();
        }

        public IEnumerable<OverviewEcommerce> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _botDomainRepository.GetMulti(x => x.DOMAIN.Contains(keyword));
            else
                return _botDomainRepository.GetAll();
        }

        public void RemoveVersionOld(string projectId)
        {
            _botDomainRepository.RemoveVersionOld(projectId);
        }
    }
}