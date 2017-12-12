using System.Linq;
using ChatBot.Data.Infrastructure;
using ChatBot.Model.Models;

namespace ChatBot.Data.Respositories
{
    public interface IProductListPerformanceEcommerceRepository : IRepositoryBase<ProductListPerformanceEcommerce>
    {
        //public void GetKhuyenMaiByKhoaHocId(int khoahocId)
        //{
        //    var result = DbContext.ProductListPerformanceEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.ProductListPerformanceEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
        int GetVersionFinal(string projectId);
    }

    public class ProductListPerformanceEcommerceRepository : RepositoryBase<ProductListPerformanceEcommerce>, IProductListPerformanceEcommerceRepository
    {
        public ProductListPerformanceEcommerceRepository(ChatBotDbContext context)
            : base(context)
        { }

        public int GetVersionFinal(string projectId)
        {
          //  var result = DbContext.ProductListPerformanceEcommerces.SelectOrderByDescending(x => x.VERSION_INT);
            var query = from u in DbContext.ProductListPerformanceEcommerces.Where(x=>x.RECORD_STATUS=="1" &&x.PROJECT_ID==projectId)
                        orderby u.VERSION_INT descending
                select u;
            var version = query.FirstOrDefault();
            if (version == null)
                return 0;
                return version.VERSION_INT;
        }
        // public int GetVersionFinal();
        //{
        //    var result = DbContext.ProductListPerformanceEcommerces.OrderByDescending(x => x.CREATE_DT).First();

        //    var query = from khuyenmai in DbContext.ProductListPerformanceEcommerces.SelectOrderByDescending(t=>t.CREATE_DT);

        //}
    }
}
