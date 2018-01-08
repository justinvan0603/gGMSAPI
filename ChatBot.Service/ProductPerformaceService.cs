using System;
using System.Collections.Generic;
using System.Linq;
using ChatBot.Common;
using ChatBot.Data.Respositories;
using ChatBot.Model.Models;

namespace ChatBot.Service
{
    public interface IProductPerformaceService
    {

        IEnumerable<ProductPerformace> GetAll();

        IEnumerable<ProductPerformace> GetAll(string keyword);
        ProductPerformace GetById(int id);


        IQueryable<ProductPerformace> GetOverviewEcommerceByProjectId(string id, string keyword);

       

        void Add(ProductPerformace botDomain);

        void Update(ProductPerformace botDomain);

        void Delete(int id);

        void Save();

        int GetVersionFinal(string projectId);

        void RemoveVersionOld(string projectId);

     //   StatisticsViewModel CountViewOverviewEcommerce(string keyword);
    }
    public class ProductPerformaceService : IProductPerformaceService
    {

        //  IUnitOfWork _unitOfWork;
        readonly IProductPerformaceRepository _botDomainRepository;

        public ProductPerformaceService(IProductPerformaceRepository botDomainRepository)
        {
            _botDomainRepository = botDomainRepository;
        }

        public int GetVersionFinal(string projectId)
        {
            return _botDomainRepository.GetVersionFinal(projectId);
        }
        public IEnumerable<ProductPerformace> GetAll()
        {
            return _botDomainRepository.GetAll().Where(x => x.RECORD_STATUS=="1");
        }

        public ProductPerformace GetById(int id)
        {
            return _botDomainRepository.GetSingle(id);
        }

        public IQueryable<ProductPerformace> GetOverviewEcommerceByProjectId(string id, string keyword)
        {
          //  var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id).OrderByDescending(x => x.ITEM_REVENUE);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.PRODUCT_NAME.Contains(keyword));
            return result;
        }
        //public StatisticsViewModel CountViewOverviewEcommerce(string keyword)
        //{
        //    var result = _botDomainRepository.GetAllIQueryable().Where(x =>
        //        x.RECORD_STATUS == "1").ToList();

        //    StatisticsViewModel statisticsViewModel = new StatisticsViewModel();
        //    double sum=0;
        //    foreach (var item in result)
        //    {
        //        statisticsViewModel.SUM_ITEM_REVENUE += Double.Parse(item.ITEM_REVENUE);
        //        statisticsViewModel.SUM_PRODUCT_DETAIL_VIEWS += Double.Parse(item.PRODUCT_DETAIL_VIEWS);


        //        statisticsViewModel.SUM_QUANTITY_ADDED_TO_CART += Double.Parse(item.QUANTITY_ADDED_TO_CART);
        //        statisticsViewModel.SUM_QUANTITY_CHECKED_OUT += Double.Parse(item.QUANTITY_CHECKED_OUT);
        //    }


        //    double sumGroup = 0;
        //    var result2 = _botDomainRepository.GetAllIQueryable().Where(x =>
        //       x.RECORD_STATUS == "1").GroupBy(x=>x.PRODUCT_NAME).ToList();

        //    List<ProductPerformace> list = new List<ProductPerformace>();
        //    foreach (var itemGroup in result2)
        //    {
               
        //        // Nested foreach is required to access group items.
        //        foreach (var item in itemGroup)
        //        {
        //            item.ITEM_REVENUE += Double.Parse(item.ITEM_REVENUE);
        //            item.PRODUCT_DETAIL_VIEWS += Double.Parse(item.PRODUCT_DETAIL_VIEWS);
        //            item.QUANTITY_ADDED_TO_CART += Double.Parse(item.QUANTITY_ADDED_TO_CART);
        //            list.Add(item);
        //        //    Console.WriteLine("\t{student.LastName}, {student.FirstName}");
        //        }
        //    }

        //    statisticsViewModel.SUM_LISTPRODUCT_OVERVIEWE_COMMERCE = list;
        //    return statisticsViewModel;
        //    //var version = GetVersionFinal();
        //    //var result = _botDomainRepository.GetAllIQueryable().Where(x =>
        //    //  x.RECORD_STATUS == "1" && x.VERSION_INT == version).OrderByDescending(x => x.ITEM_REVENUE);
        //}

        public void Add(ProductPerformace botDomain)
        {
            _botDomainRepository.Add(botDomain);
        }

        public void Update(ProductPerformace botDomain)
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

        public IEnumerable<ProductPerformace> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _botDomainRepository.GetMulti(x => x.PRODUCT_NAME.Contains(keyword));
            else
                return _botDomainRepository.GetAll();
        }

        public void RemoveVersionOld(string projectId)
        {
            _botDomainRepository.RemoveVersionOld(projectId);
        }
    }
}