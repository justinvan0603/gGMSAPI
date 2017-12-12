using System;
using System.Collections.Generic;
using System.Linq;
using ChatBot.Data.Respositories;
using ChatBot.Model.Models;

namespace ChatBot.Service
{
    public interface IProductListPerformanceEcommerceService
    {

        IEnumerable<ProductListPerformanceEcommerce> GetAll();

        IEnumerable<ProductListPerformanceEcommerce> GetAll(string keyword);
        ProductListPerformanceEcommerce GetById(int id);

        IQueryable<ProductListPerformanceEcommerce> GetProductListPerformanceEcommerceByProjectId(string id, string keyword);

        void Add(ProductListPerformanceEcommerce botDomain);

        void Update(ProductListPerformanceEcommerce botDomain);

        void Delete(int id);

        void Save();

        int GetVersionFinal(string projectId);
    }
    public class ProductListPerformanceEcommerceService : IProductListPerformanceEcommerceService
    {

        //  IUnitOfWork _unitOfWork;
        readonly IProductListPerformanceEcommerceRepository _botDomainRepository;

        public ProductListPerformanceEcommerceService(IProductListPerformanceEcommerceRepository botDomainRepository)
        {
            _botDomainRepository = botDomainRepository;
        }

        public int GetVersionFinal(string projectId)
        {
            return _botDomainRepository.GetVersionFinal(projectId);
        }
        public IEnumerable<ProductListPerformanceEcommerce> GetAll()
        {
            return _botDomainRepository.GetAll().Where(x => x.RECORD_STATUS=="1");
        }

        public ProductListPerformanceEcommerce GetById(int id)
        {
            return _botDomainRepository.GetSingle(id);
        }

        public IQueryable<ProductListPerformanceEcommerce> GetProductListPerformanceEcommerceByProjectId(string id, string keyword)
        {
            var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id && x.VERSION_INT == version);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.PRODUCTLIST.Contains(keyword));
            return result;
        }


        public void Add(ProductListPerformanceEcommerce botDomain)
        {
            _botDomainRepository.Add(botDomain);
        }

        public void Update(ProductListPerformanceEcommerce botDomain)
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

        public IEnumerable<ProductListPerformanceEcommerce> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _botDomainRepository.GetMulti(x => x.PRODUCTLIST.Contains(keyword));
            else
                return _botDomainRepository.GetAll();
        }
    }
}