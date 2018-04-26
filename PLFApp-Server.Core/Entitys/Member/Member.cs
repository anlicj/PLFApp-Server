using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLFApp.Server.Core
{
    [Table("t_Member")]
    public class Member : BaseFullEntity
    {
        [Required]
        //[RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage ="手机号格式错误")]
        [MaxLength(16)]
        public string MobilePhone { get; set; }

        [Required]
        //[RegularExpression(@"^\w{6,12},$",ErrorMessage ="密码为6至12位的数字、字母或下划线")]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string NickName { get; set; }

        [Required]
        [MaxLength(255)]
        public string HeadImageUrl { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [NotMapped]
        public string LastLoginTimeFormat
        {
            get=> LastLoginTime == null ? "" : Convert.ToDateTime(LastLoginTime).ToString(Constant.DATETIMEFORMAT);
        }

        [MaxLength(23)]
        public string  LoginIp { get; set; }

    }
}
