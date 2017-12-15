using System.Linq;
using ChatBot.Data.Infrastructure;
using ChatBot.Model.Models;

namespace ChatBot.Data.Respositories
{
    public interface IPageBehaviorEcommerceRepository : IRepositoryBase<PageBehaviorEcommerce>
    {
        //public void GetKhuyenMaiByKhoaHocId(int khoahocId)
        //{
        //    var result = DbContext.PageBehaviorEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.PageBehaviorEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
        int GetVersionFinal(string projectId);
    }

    public class PageBehaviorEcommerceRepository : RepositoryBase<PageBehaviorEcommerce>, IPageBehaviorEcommerceRepository
    {
        public PageBehaviorEcommerceRepository(ChatBotDbContext context)
            : base(context)
        { }

        public int GetVersionFinal(string projectId)
        {
          //  var result = DbContext.PageBehaviorEcommerces.SelectOrderByDescending(x => x.VERSION_INT);
            var query = from u in DbContext.PageBehaviorEcommerces.Where(x=>x.RECORD_STATUS=="1" &&x.PROJECT_ID==projectId)
                        orderby u.VERSION_INT descending
                select u;
            var version = query.FirstOrDefault();
            if (version == null)
                return 0;
                return version.VERSION_INT;
        }
        // public int GetVersionFinal();
        //{
        //    var result = DbContext.PageBehaviorEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.PageBehaviorEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
    }
}
