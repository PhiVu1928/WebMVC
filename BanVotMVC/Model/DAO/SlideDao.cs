using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class SlideDao
    {
        OnlineShop dt = null;
        public SlideDao()
        {
            dt = new OnlineShop();
        }
        public long InsertSlide(Slide entity)
        {
            dt.Slides.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public bool UpdateSlide(Slide entity)
        {
            try
            {
                var slide = dt.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.Status = entity.Status;
                slide.Name = entity.Name;
                slide.Price = entity.Price;
                slide.PromotionPrice = entity.PromotionPrice;
                slide.MoreImage = entity.MoreImage;
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool DeleteSlide(int id)
        {
            try
            {
                var IdSl = dt.Slides.Find(id);
                dt.Slides.Remove(IdSl);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
       
        public Slide ViewSlideDeltail(int id)
        {
            return dt.Slides.Find(id);
        }
        public long CheckSlide(string name)
        {
            var check = dt.Slides.FirstOrDefault(x => x.Name == name);
            if(check == null)
            {
                return 1;
            }    
            else
            {
                return 0;
            }    
        }
        public IEnumerable<Slide> ListAllSlide(string SearchString, int page, int pagesize)
        {
            IQueryable<Slide> model = dt.Slides;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString) || x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public List<Slide> SlideShow()
        {
            return dt.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
