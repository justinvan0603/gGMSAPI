using System.Linq;
using ChatBot.Data.Infrastructure;
using ChatBot.Model.Models;

namespace ChatBot.Data.Respositories
{
    public interface IOverviewEcommerceRepository : IRepositoryBase<OverviewEcommerce>
    {
        //public void GetKhuyenMaiByKhoaHocId(int khoahocId)
        //{
        //    var result = DbContext.OverviewEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.OverviewEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
        int GetVersionFinal(string projectId);
    }

    public class OverviewEcommerceRepository : RepositoryBase<OverviewEcommerce>, IOverviewEcommerceRepository
    {
        public OverviewEcommerceRepository(ChatBotDbContext context)
            : base(context)
        { }

        public int GetVersionFinal(string projectId)
        {
          //  var result = DbContext.OverviewEcommerces.SelectOrderByDescending(x => x.VERSION_INT);
            var query = from u in DbContext.OverviewEcommerces.Where(x=>x.RECORD_STATUS=="1" &&x.PROJECT_ID==projectId)
                        orderby u.VERSION_INT descending
                select u;
            var version = query.FirstOrDefault();
            if (version == null)
                return 0;
                return version.VERSION_INT;
        }
        // public int GetVersionFinal();
        //{
        //    var result = DbContext.OverviewEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.OverviewEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
    }
}
