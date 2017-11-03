using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_CustomerConnect_Model
    {
        [Display(Name = "联系人ID")]
        public Guid ID { get; set; }

        [Display(Name = "联系人名称")]
        public string ContactName { get; set; }

        [Display(Name = "性别")]
        public int? Sex { get; set; }

        [Display(Name = "客户ID")]
        public Guid? CustomerID { get; set; }

        [Display(Name = "是否默认")]
        public bool? IsDefault { get; set; }

        [Display(Name = "联系电话")]
        public string Tel { get; set; }

        [Display(Name = "联系手机")]
        public string Phone { get; set; }

        [Display(Name = "传真")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "QQ")]
        public string QQ { get; set; }

        [Display(Name = "联系地址")]
        public string Address { get; set; }

        [Display(Name = "创建人ID")]
        public string CreateUserID { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "是否启用")]
        public bool? IsEnable { get; set; }


    }
}

