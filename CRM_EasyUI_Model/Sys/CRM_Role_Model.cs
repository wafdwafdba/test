using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Role_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string Name { get; set; }

        [Display(Name = "说明")]
        public string Des { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "部门")]
        public int DeptId { get; set; }

        [Display(Name = "部门名称")]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "启用状态")]
        public bool Enable { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "上级")]
        public int ParentId { get; set; }

        [Display(Name = "复制角色权限")]
        public int? CopyRoleID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "层级")]
        public int Level { get; set; }

        [Display(Name = "是否选择")]
        public bool IsSelect { get; set; }

        public string state { get; set; }
    }
}

