using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Users
{
    public class ChangeUserPwdDto
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码与确认新密码不一致, 请重新输入")]
        public string ConfirmPassword { get; set; }   
    }
}
