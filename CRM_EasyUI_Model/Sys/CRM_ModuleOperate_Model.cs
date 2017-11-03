using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_ModuleOperate_Model
    {
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "操作名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "操作码")]
        public string KeyCode { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "所属模块")]
        public string ModuleId { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "是否验证")]
        public bool IsValid { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "排序号")]
        public int Sort { get; set; }

        [Display(Name = "是否选择")]
        public bool IsSelect { get; set; }

    }
}

