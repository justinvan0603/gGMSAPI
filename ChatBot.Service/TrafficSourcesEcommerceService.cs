using System;
using System.Collections.Generic;
using System.Linq;
using ChatBot.Common;
using ChatBot.Data.Respositories;
using ChatBot.Model.Models;
using Remotion.Linq.Utilities;

namespace ChatBot.Service
{
    public interface ITrafficSourcesEcommerceService
    {

        IEnumerable<TrafficSourcesEcommerce> GetAll();

        IEnumerable<TrafficSourcesEcommerce> GetAll(string keyword);
        TrafficSourcesEcommerce GetById(int id);


        IQueryable<TrafficSourcesEcommerce> GetTrafficSourcesEcommerceByProjectId(string id, string keyword);

       

        void Add(TrafficSourcesEcommerce botDomain);

        void Update(TrafficSourcesEcommerce botDomain);

        void Delete(int id);

        void Save();

        int GetVersionFinal(string projectId);

        void RemoveVersionOld(string projectId);

        List<StatisticsTrafficSourcesEcommerceViewModel> CountViewTrafficSourcesEcommerce(string keyword);
    }
    public class TrafficSourcesEcommerceService : ITrafficSourcesEcommerceService
    {

        //  IUnitOfWork _unitOfWork;
        readonly ITrafficSourcesEcommerceRepository _botDomainRepository;

        public TrafficSourcesEcommerceService(ITrafficSourcesEcommerceRepository botDomainRepository)
        {
            _botDomainRepository = botDomainRepository;
        }

        public int GetVersionFinal(string projectId)
        {
            return _botDomainRepository.GetVersionFinal(projectId);
        }
        public IEnumerable<TrafficSourcesEcommerce> GetAll()
        {
            return _botDomainRepository.GetAll().Where(x => x.RECORD_STATUS=="1");
        }

        public TrafficSourcesEcommerce GetById(int id)
        {
            return _botDomainRepository.GetSingle(id);
        }

        public IQueryable<TrafficSourcesEcommerce> GetTrafficSourcesEcommerceByProjectId(string id, string keyword)
        {
          //  var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id).OrderByDescending(x => x.TRANSACTIONREVENUE);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.DOMAIN.Contains(keyword));
            return result;
        }
        public List<StatisticsTrafficSourcesEcommerceViewModel> CountViewTrafficSourcesEcommerce(string keyword)
        {
            //var result = _botDomainRepository.GetAllIQueryable().Where(x =>
            //    x.RECORD_STATUS == "1").ToList();
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1").GroupBy(x => x.SOURCE).ToList();

            double sum = 0;
            double productdetailviews=0;
            double timeonpage = 0;
            List<StatisticsTrafficSourcesEcommerceViewModel> statisticsViewModelList = new List<StatisticsTrafficSourcesEcommerceViewModel>();


           

            List<ProductPerformace> list = new List<ProductPerformace>();
            double countPageviews = 0;
            foreach (var itemGroup in result)
            {
                
                StatisticsTrafficSourcesEcommerceViewModel statisticsViewModel =
                    new StatisticsTrafficSourcesEcommerceViewModel();
                // Nested foreach is required to access group items.
                foreach (var item in itemGroup)
                {
                   
                    statisticsViewModel.SOURCE = item.SOURCE;
                    statisticsViewModel.PAGEVIEWS += Double.Parse(item.PAGEVIEWS);
                    //item.QUANTITY_ADDED_TO_CART += Double.Parse(item.QUANTITY_ADDED_TO_CART);
                    
                    //    Console.WriteLine("\t{student.LastName}, {student.FirstName}");
                }
                countPageviews += statisticsViewModel.PAGEVIEWS;
                statisticsViewModelList.Add(statisticsViewModel);
            }
            
            foreach (var item in statisticsViewModelList)
            {
                item.PERCENT = Math.Round(item.PAGEVIEWS * 100/countPageviews,2);
                item.SUM_PAGEVIEW = countPageviews.ToString("#");
            }
            //foreach (var item in result)
            //{
            //    StatisticsTrafficSourcesEcommerceViewModel statisticsViewModel = new StatisticsTrafficSourcesEcommerceViewModel
            //    {
            //        //statisticsViewModel.SOURCE = item.SOURCE;
            //        //statisticsViewModel.PAGEVIEWS += Double.Parse(item.PAGEVIEWS);
            //        //statisticsViewModel.TRANSACTIONREVENUE += Double.Parse(item.TRANSACTIONREVENUE);
                    

            //        SOURCE = item.SOURCE,
            //        PAGEVIEWS = Double.Parse(item.PAGEVIEWS),
            //        //TRANSACTIONS =
            //        //    (productdetailviews / 10).ToString("#,##");
            //};
            //    // statisticsViewModel.TRANSACTIONREVENUE = item.TRANSACTIONREVENUE;
            //    // productdetailviews = item.TRANSACTIONREVENUE;

           
            //    statisticsViewModelList.Add(statisticsViewModel);
            //}
            //statisticsViewModel.TRANSACTIONREVENUE =
            //    (productdetailviews / 10).ToString("#,##");
            //statisticsViewModel.TIMEONPAGE =
            //    (timeonpage / (10*result.Count())).ToString("#,##");

            return statisticsViewModelList;
           // return null;
        }

        public void Add(TrafficSourcesEcommerce botDomain)
        {
            _botDomainRepository.Add(botDomain);
        }

        public void Update(TrafficSourcesEcommerce botDomain)
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

        public IEnumerable<TrafficSourcesEcommerce> GetAll(string keyword)
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