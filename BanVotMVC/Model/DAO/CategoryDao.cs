using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class CategoryDao
    {
        OnlineShop dt = null;
        public CategoryDao()
        {
            dt = new OnlineShop();
        }
        public List<Category> ListAll()
        {
            return dt.Categories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewCategorybyID(long CategoryId)
        {
            return dt.ProductCategories.Find(CategoryId);
        }
        public List<ProductCategory> ListAllProduct()
        {
            return dt.ProductCategories.Where(x => x.Status == true && x.ParentID != null ).ToList();
        }
        public List<ProductCategory> listCategory()
        {
            return dt.ProductCategories.OrderBy(x => x.DisplayOrder > 0).ToList();
        }
        public List<ProductCategory> listAllparent()
        {
            return dt.ProductCategories.Where(x => x.ParentID == null).ToList();
        }
        public long InsertProductCategory(ProductCategory entity)
        {
            dt.ProductCategories.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public long CheckProductCategory(string name)
        {
            var check = dt.ProductCategories.FirstOrDefault(x => x.Name == name);
            if(check == null)
            {
                return 1;
            }    
            else
            {
                return 0;
            }                
        }
        public bool UpdateProductCategory(ProductCategory entity)
        {
            try
            {
                var ProductCategory = dt.ProductCategories.Find(entity.ID);
                ProductCategory.Name = entity.Name;
                ProductCategory.MetaTitle = entity.MetaTitle;
                ProductCategory.ParentID = entity.ParentID;
                ProductCategory.CreatedDate = DateTime.Now;
                dt.SaveChanges();
                return true;
            } catch(Exception)
            {
                return false;
            }
        }
        public bool DeleteProductCategory(int id)
        {
            try
            {
                var idPC = dt.ProductCategories.Find(id);
                dt.ProductCategories.Remove(idPC);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }            
        }
        public IEnumerable<ProductCategory> ListallProductCategory(string SearchString, int page, int pagesize)
        {
            IQueryable<ProductCategory> model = dt.ProductCategories;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString) || x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public ProductCategory ViewProductCategoryDetail(int id)
        {
            return dt.ProductCategories.Find(id);
        }
        public bool ChageStatus(long ID)
        {
            var productCategory = dt.ProductCategories.Find(ID);
            productCategory.Status = !productCategory.Status;
            dt.SaveChanges();
            return productCategory.Status;
        }
    }
}
