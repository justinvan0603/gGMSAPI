using System;
using System.Collections.Generic;
using System.Linq;
using ChatBot.Data.Respositories;
using ChatBot.Model.Models;

namespace ChatBot.Service
{
    public interface IPageBehaviorEcommerceService
    {

        IEnumerable<PageBehaviorEcommerce> GetAll();

        IEnumerable<PageBehaviorEcommerce> GetAll(string keyword);
        PageBehaviorEcommerce GetById(int id);

        IQueryable<PageBehaviorEcommerce> GetPageBehaviorEcommerceByProjectId(string id, string keyword);

        void Add(PageBehaviorEcommerce botDomain);

        void Update(PageBehaviorEcommerce botDomain);

        void Delete(int id);

        void Save();

        int GetVersionFinal(string projectId);
    }
    public class PageBehaviorEcommerceService : IPageBehaviorEcommerceService
    {

        //  IUnitOfWork _unitOfWork;
        readonly IPageBehaviorEcommerceRepository _botDomainRepository;

        public PageBehaviorEcommerceService(IPageBehaviorEcommerceRepository botDomainRepository)
        {
            _botDomainRepository = botDomainRepository;
        }

        public int GetVersionFinal(string projectId)
        {
            return _botDomainRepository.GetVersionFinal(projectId);
        }
        public IEnumerable<PageBehaviorEcommerce> GetAll()
        {
            return _botDomainRepository.GetAll().Where(x => x.RECORD_STATUS=="1");
        }

        public PageBehaviorEcommerce GetById(int id)
        {
            return _botDomainRepository.GetSingle(id);
        }

        public IQueryable<PageBehaviorEcommerce> GetPageBehaviorEcommerceByProjectId(string id, string keyword)
        {
            var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id && x.VERSION_INT == version).OrderByDescending(x => x.PAGE_VALUE);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.PAGE_PATH.Contains(keyword));
            return result;
        }


        public void Add(PageBehaviorEcommerce botDomain)
        {
            _botDomainRepository.Add(botDomain);
        }

        public void Update(PageBehaviorEcommerce botDomain)
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

        public IEnumerable<PageBehaviorEcommerce> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _botDomainRepository.GetMulti(x => x.PAGE_PATH.Contains(keyword));
            else
                return _botDomainRepository.GetAll();
        }
    }
}