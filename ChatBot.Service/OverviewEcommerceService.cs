using System;
using System.Collections.Generic;
using System.Linq;
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
            var version = GetVersionFinal(id);
            var result = _botDomainRepository.GetAllIQueryable().Where(x =>
                x.RECORD_STATUS == "1" && x.PROJECT_ID == id && x.VERSION_INT == version);
            if (!String.IsNullOrWhiteSpace(keyword)&& keyword!="undefined")
                return result.Where(y=>y.PRODUCT_NAME.Contains(keyword));
            return result;
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
                return _botDomainRepository.GetMulti(x => x.PRODUCT_NAME.Contains(keyword));
            else
                return _botDomainRepository.GetAll();
        }
    }
}