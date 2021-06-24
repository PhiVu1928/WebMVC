using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.DAO
{
    public class ProductDao
    {
        OnlineShop dt = null;
        public ProductDao()
        {
            dt = new OnlineShop();
        }
        public IEnumerable<Product> ListallProductbyCateID(long CategoryID, int page, int pagesize)
        {
            IQueryable<Product> model = dt.Products.Where(x => x.CategoryID == CategoryID);
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public IEnumerable<Product> SearchProduct(string SearchString, int page , int pagesize)
        {
            IQueryable<Product> model = dt.Products;
            if(!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page,pagesize);
        }
        public List<string> ListName(string keyword)
        {
            return dt.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        /// <summary>
        /// lấy danh sách sản phẩm theo danh mục
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Product> ListProductbyCategoryID(long CategoryId, ref int totalRecord , int pageIndex , int pageSize )
        {
            totalRecord = dt.Products.Where(x => x.CategoryID == CategoryId).Count();
            var model = dt.Products.Where(x => x.CategoryID == CategoryId).OrderByDescending(x => x.CreatedDate).Skip((pageIndex -1)*pageSize).Take(pageSize);
            return model.ToList();
        }
        public Product Viewdetail(long Productid)
        {
            return dt.Products.Find(Productid);
        }
        public List<ProductViewModel> ProductFeature(int top)
        {
            var model = from a in dt.Products
                        join b in dt.ProductCategories
                        on a.CategoryID equals b.ID
                        where b.ParentID == 2
                        where a.CategoryID == b.ID
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Images = a.Image,
                            MoreImages = a.MoreImages,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Code = a.Code,
                            Quantity = a.Quantity,
                            CategoryID = a.CategoryID,
                            Detail = a.Detail,
                            Name = a.Name,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            CreatedDate = a.CreatedDate,
                        };
            model.Take(top);
            return model.ToList();
        }
        public List<ProductViewModel> ProductNew(int top)
        {
            var model = from a in dt.Products
                        join b in dt.ProductCategories
                        on a.CategoryID equals b.ID
                        where b.ParentID == 1
                        where a.CategoryID == b.ID
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Images = a.Image,
                            MoreImages = a.MoreImages,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Code = a.Code,
                            Quantity = a.Quantity,
                            CategoryID = a.CategoryID,
                            Detail = a.Detail,
                            Name = a.Name,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            CreatedDate = a.CreatedDate,
                        };
            return model.Take(top).ToList();
        }
    }
}
