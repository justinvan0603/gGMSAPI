using System.Linq;
using ChatBot.Data.Infrastructure;
using ChatBot.Model.Models;

namespace ChatBot.Data.Respositories
{
    public interface ITrafficSourcesEcommerceRepository : IRepositoryBase<TrafficSourcesEcommerce>
    {
        //public void GetKhuyenMaiByKhoaHocId(int khoahocId)
        //{
        //    var result = DbContext.TrafficSourcesEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.TrafficSourcesEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
        int GetVersionFinal(string projectId);
        void RemoveVersionOld(string projectId);
    }

    public class TrafficSourcesEcommerceRepository : RepositoryBase<TrafficSourcesEcommerce>, ITrafficSourcesEcommerceRepository
    {
        public TrafficSourcesEcommerceRepository(ChatBotDbContext context)
            : base(context)
        { }

        public int GetVersionFinal(string projectId)
        {
          //  var result = DbContext.TrafficSourcesEcommerces.SelectOrderByDescending(x => x.VERSION_INT);
            var query = from u in DbContext.TrafficSourcesEcommerces.Where(x=>x.RECORD_STATUS=="1" &&x.PROJECT_ID==projectId)
                        orderby u.VERSION_INT descending
                select u;
            var version = query.FirstOrDefault();
            if (version == null)
                return 0;
                return version.VERSION_INT;
        }

        public void RemoveVersionOld(string projectId)
        {
            //  var result = DbContext.TrafficSourcesEcommerces.SelectOrderByDescending(x => x.VERSION_INT);
            DbContext.TrafficSourcesEcommerces.Where(x => x.RECORD_STATUS == "1" &&x.PROJECT_ID==projectId).ToList().ForEach(x =>
            {
                x.RECORD_STATUS = "0";
            });
            DbContext.SaveChanges();
        }
        // public int GetVersionFinal();
        //{
        //    var result = DbContext.TrafficSourcesEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.TrafficSourcesEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
    }
}
