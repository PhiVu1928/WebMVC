using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.DAO
{
    public class AboutDao
    {
        OnlineShop dt = null;
        public AboutDao()
        {
            dt = new OnlineShop();
        }
        
        public long Insert(About entity)
        {
            dt.Abouts.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public bool Edit(About entity)
        {
            try
            {
                var About = dt.Abouts.Find(entity.ID);
                About.Name = entity.Name;
                About.Address = entity.Address;
                About.Detail = entity.Detail;
                About.Facebook = entity.Facebook;
                About.Description = entity.Description;
                About.ModifiedDate = DateTime.Now;
                About.Status = entity.Status;
                About.Image = entity.Image;
                dt.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var aboutid = dt.Abouts.Find(id);
                dt.Abouts.Remove(aboutid);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public List<About> Aboutdetail()
        {
            var model = dt.Abouts;
            return model.ToList();
        }
        public About ViewAboutDetail(long id)
        {
            return dt.Abouts.Find(id);
        }
    }
}
