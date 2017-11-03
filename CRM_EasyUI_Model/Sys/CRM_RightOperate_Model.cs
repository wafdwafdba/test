using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_RightOperate_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Display(Name = "是否验证")]
        public bool IsValid { get; set; }

        [Display(Name = "操作码")]
        public string KeyCode { get; set; }

        [Display(Name = "角色模块ID")]
        public string RightID { get; set; }


    }
}

