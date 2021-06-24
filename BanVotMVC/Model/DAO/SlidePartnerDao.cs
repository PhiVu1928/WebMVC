using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class SlidePartnerDao
    {
        OnlineShop dt = null;
        public SlidePartnerDao()
        {
            dt = new OnlineShop();
        }
        public long Insert(SlidePartner entity)
        {
            dt.SlidePartners.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public bool Update(SlidePartner entity)
        {
            try
            {
                var slide = dt.SlidePartners.Find(entity.ID);
                slide.Name = entity.Name;
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.ModifiedDate = DateTime.Now;
                slide.Status = entity.Status;
                dt.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var IdSlide = dt.SlidePartners.Find(id);
                dt.SlidePartners.Remove(IdSlide);
                dt.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public long Check(string name)
        {
            var namePartner = dt.SlidePartners.Where(x => x.Name == name);
            if (namePartner != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public SlidePartner SlidePartnerDetail(long id)
        {
            return dt.SlidePartners.Find(id);
        }
        public List<SlidePartner> ListAllSlidePartner()
        {
            var model = dt.SlidePartners.OrderByDescending(x => x.DisplayOrder);
            return model.ToList();
        }
        public IEnumerable<SlidePartner> ListallSlidePartners(string SearchString, int page, int pagesize)
        {
            IQueryable<SlidePartner> model = dt.SlidePartners;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString) || x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
    }
}
