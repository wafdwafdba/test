using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Right_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Display(Name = "模块ID")]
        public string ModulID { get; set; }

        [Display(Name = "权限")]
        public bool RightFlag { get; set; }

        [Display(Name = "角色ID")]
        public int RoleID { get; set; }


    }
}

