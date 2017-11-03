using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_RoleUser_Model
    {
        [Display(Name = "角色")]
        public int RoleId { get; set; }

        [Display(Name = "用户")]
        public string UserId { get; set; }

        [Display(Name = "部门")]
        public int? DeptId { get; set; }
    }
}

