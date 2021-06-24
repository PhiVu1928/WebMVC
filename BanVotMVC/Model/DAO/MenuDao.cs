using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class MenuDao
    {
        OnlineShop dt = null;
        public MenuDao()
        {
            dt = new OnlineShop();
        }
        public List<MenuType> listAllMenutype()
        {
            return dt.MenuTypes.ToList();
        }
        public long Insert(Menu entity)
        {
            dt.Menus.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public long CheckMenu(string name)
        {
            var check = dt.Menus.FirstOrDefault(x => x.Text == name);
            if(check == null)
            {
                return 1;
            }    
            else
            {
                return 0;
            }    
        }
        public bool UpdateMenu(Menu entity)
        {
            try
            {
                var Menu = dt.Menus.Find(entity.ID);
                Menu.Text = entity.Text;
                Menu.Link = entity.Link;
                Menu.DisplayOrder = entity.DisplayOrder;
                Menu.Status = entity.Status;
                Menu.Target = entity.Target;
                Menu.TypeID = entity.ID;
                dt.SaveChanges();
                return true;
            } catch(Exception)
            {
                return false;
            }
        }
        public bool DeleteMenu(int id)
        {
            var IdMN = dt.Menus.Find(id);
            dt.Menus.Remove(IdMN);
            dt.SaveChanges();
            return true;
        }
        public Menu MenuDetail(int id)
        {
            return dt.Menus.Find(id);
        }
        public bool ChangeStatus(long id)
        {
            var menu = dt.Menus.Find(id);
            menu.Status = !menu.Status;
            dt.SaveChanges();
            return true;
        }
        public IEnumerable<Menu> ListallMenu(string SearchString, int page, int pagesize)
        {
            IQueryable<Menu> model = dt.Menus;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Text.Contains(SearchString) || x.Text.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
    }
}
