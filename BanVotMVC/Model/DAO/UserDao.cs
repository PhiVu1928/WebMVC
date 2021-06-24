
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using PagedList.Mvc;
using Common;

namespace Model.DAO
{
    public class UserDao
    {
        OnlineShop dt = null;
        public UserDao()
        {
            dt = new OnlineShop();
        }
        public int CheckProduct(string name)
        {
            var check =  dt.Products.FirstOrDefault(x => x.Name == name);
            if(check == null)
            {
                return 1;
            }    
            else
            {
                return 0;
            }                
        }
        public long InsertProduct(Product entity)
        {
            dt.Products.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public bool ChageStatus(long ID)
        {
            var user = dt.Users.Find(ID);
            user.Status = !user.Status;
            dt.SaveChanges();
            return user.Status;
        }
        public long InsertContent(Content entity)
        {
            dt.Contents.Add(entity);
            dt.SaveChanges();
            return entity.ID;
        }
        public bool UpdateProduct(Product entity)
        {
            try
            {
                var product = dt.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.MoreImages = entity.MoreImages;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                product.IncludedVAT = entity.IncludedVAT;
                product.Quantity = entity.Quantity;
                product.Detail = entity.Detail;
                product.Warranty = entity.Warranty;
                product.CreatedDate = DateTime.Now;
                dt.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateContent(Content entity)
        {
            try
            {
                var content = dt.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.Detail = entity.Detail;
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool DeleteProduct(int id)
        {
            try
            {
                var idPD = dt.Products.Find(id);
                dt.Products.Remove(idPD);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }    
        public bool DeleteContent(int id)
        {
            try
            {
                var idtt = dt.Contents.Find(id);
                dt.Contents.Remove(idtt);
                dt.SaveChanges();
                return true;

            } catch (Exception)
            {
                return false;
            }           
        }
        public Content ViewContentDetail(int id)
        {
            return dt.Contents.Find(id);
        }
        public long Insert(User entity)
        {           
                dt.Users.Add(entity);
                dt.SaveChanges();
                return entity.ID;                             
        }
        public bool Update(User entity)
        {
            try
            {
                var user = dt.Users.Find(entity.ID);
                user.Name = entity.Name;
                if(!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.GroupID = entity.GroupID;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }            
        }
        public bool Delete(int id)
        {
            try
            {
                var user = dt.Users.Find(id);
                dt.Users.Remove(user);
                dt.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
       
        public IEnumerable<User> ListallPaging(string SearchString, int page, int pagesize)
        {
            IQueryable<User> model = dt.Users;
            if(!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.UserName.Contains(SearchString) || x.Name.Contains(SearchString));
            }    
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public IEnumerable<Content> ListallContent(string SearchString, int page, int pagesize)
        {
            IQueryable<Content> model = dt.Contents;
            if (!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString) || x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public IEnumerable<Product> ListallProduct(string SearchString, int page, int pagesize)
        {
            IQueryable<Product> model = dt.Products;
            if(!string.IsNullOrEmpty(SearchString))
            {
                model = model.Where(x => x.Name.Contains(SearchString) || x.Name.Contains(SearchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
        public User GetById(string username)
        {
            return dt.Users.SingleOrDefault(x => x.UserName == username);
        }
        public User ViewDetail(int Id)
        {
            return dt.Users.Find(Id);
        }
        public Product ViewProductDetail(int id)
        {
            return dt.Products.Find(id);
        }
        public int checkContent(string name)
        {
            var chk = dt.Contents.SingleOrDefault(x => x.Name == name);
            if(chk == null)
            {
                return 0;
            }    
            else
            {
                return 1;
            }    
        }
        public bool CheckUserName(string UserName)
        {
            return dt.Users.Count(x => x.UserName == UserName) > 0;
        }
        public bool CheckEmail(string Email)
        {
            return dt.Users.Count(x => x.Email == Email) > 0;
        }
        public int check(string username)
        {
            var chk = dt.Users.SingleOrDefault(x => x.UserName == username);
            if(chk == null)
            {
                return 0;
            }    
            else
            {
                return 1;
            }    
        }

        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = dt.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
        public List<string> GetListCredential(string UserName)
        {
            var user = dt.Users.Single(x => x.UserName == UserName);
            var data = (from a in dt.Credentials
                        join b in dt.UserGroups on a.UserGroupID equals b.ID
                        join c in dt.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
        public List<UserGroup> ListAllGroup()
        {
            return dt.UserGroups.OrderBy(x => x.ID).ToList();
        }
    }
}
