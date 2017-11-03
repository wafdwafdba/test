using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupThink.CRM.Model.Sys
{
    public class CRM_Customer_Model
    {
        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户ID")]
        public Guid ID { get; set; }

        [Display(Name = "客户名称")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "公司型/个人型")]
        public int IsCompany { get; set; }

        [Display(Name = "客户联系人ID")]
        public Guid? CustomerConnect { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "销售员")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "共享人")]
        public string ShareID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户来源")]
        public int CustomerSource { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户类型")]
        public int CustomerType { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "行业")]
        public int CustomerBusiness { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "客户状态")]
        public int CustomerState { get; set; }

        [Display(Name = "最后跟进时间")]
        public DateTime? LastFollowTime { get; set; }

        [Display(Name = "跟进次数")]
        public int? FollowTimes { get; set; }

        [Display(Name = "是否公海客户")]
        public int? IsPublic { get; set; }

        [Display(Name = "客户产品id")]
        public int? CusProduct { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "创建人")]
        public string CreateUserID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "修改人")]
        public string ModifyUserID { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "修改时间")]
        public DateTime ModifyTime { get; set; }

        [Required(ErrorMessage = "{0}必须填写")]
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }

        #region 联系人相关model
        /// <summary>
        /// 客户联系人名称
        /// </summary>
         [Required(ErrorMessage = "{0}必须填写")]
         [Display(Name = "客户联系人名称")]
        public string ContactName { get; set; }

         [Display(Name = "是否默认")]
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }

        [Display(Name = "联系电话")]
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }

        [Display(Name = "联系手机")]
        /// <summary>
        /// 联系手机
        /// </summary>
        public string Phone { get; set; }

        [Display(Name = "Email")]
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        [Display(Name = "传真")]
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        [Display(Name = "QQ")]
        /// <summary>
        /// qq
        /// </summary>
        public string QQ { get; set; }
        #endregion
    }
}

