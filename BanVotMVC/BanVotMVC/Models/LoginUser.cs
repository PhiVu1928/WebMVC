using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanVotMVC.Models
{
    public class LoginUser
    {
        [Key]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống")]
        public string Username { set; get; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string Password { set; get; }
    }
}