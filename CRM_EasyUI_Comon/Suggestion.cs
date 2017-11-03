using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupThink.CRM.Common
{
    public static class Suggestion
    {
        /// <summary>
        /// 请选择要操作的记录
        /// </summary>
        public static string PlaseChooseToOperatingRecords { get { return "请选择要操作的记录"; } }

        /// <summary>
        /// 您确定要注销系统吗
        /// </summary>
        public static string AreYouLogOff { get { return "您确定要注销系统吗？"; } }
        /// <summary>
        /// 取消
        /// </summary>
        public static string Cancel { get { return "取消"; } }
        /// <summary>
        /// 不能操作已经审核的记录
        /// </summary>
        public static string CanNotOperationHasTheAudit { get { return "不能操作已经审核的记录"; } }
        /// <summary>
        /// 审核
        /// </summary>
        public static string Check { get { return "审核"; } }
        /// <summary>
        /// 审核失败
        /// </summary>
        public static string CheckFail { get { return "审核失败"; } }
        /// <summary>
        /// 审核成功
        /// </summary>
        public static string CheckSucceed { get { return "审核成功"; } }
        /// <summary>
        /// 清理离线任务
        /// </summary>
        public static string ClearLossJobs { get { return "清理离线任务"; } }
        /// <summary>
        /// 关闭
        /// </summary>
        public static string Close { get { return "关闭"; } }
        /// <summary>
        /// 关闭全部
        /// </summary>
        public static string CloseAll { get { return "关闭全部"; } }
        /// <summary>
        /// 关闭左侧标签
        /// </summary>
        public static string CloseLeft { get { return "关闭左侧标签"; } }
        /// <summary>
        /// 除此之外全部关闭
        /// </summary>
        public static string CloseOther { get { return "除此之外全部关闭"; } }
        /// <summary>
        /// 关闭右侧标签
        /// </summary>
        public static string CloseRight { get { return "关闭右侧标签"; } }
        /// <summary>
        /// 创建
        /// </summary>
        public static string Create { get { return "创建"; } }
        /// <summary>
        /// 删除
        /// </summary>
        public static string Delete { get { return "删除"; } }
        /// <summary>
        /// 删除失败
        /// </summary>
        public static string DeleteFail { get { return "删除失败"; } }
        /// <summary>
        /// 删除成功
        /// </summary>
        public static string DeleteSucceed { get { return "删除成功"; } }
        /// <summary>
        /// 详细
        /// </summary>
        public static string Details { get { return "详细"; } }
        /// <summary>
        /// 不可用
        /// </summary>
        public static string Disable { get { return "不可用"; } }
        /// <summary>
        /// 编辑
        /// </summary>
        public static string Edit { get { return "编辑"; } }
        /// <summary>
        /// 修改失败
        /// </summary>
        public static string EditFail { get { return "修改失败"; } }
        /// <summary>
        /// 修改成功
        /// </summary>
        public static string EditSucceed { get { return "修改成功"; } }
        /// <summary>
        /// 导出
        /// </summary>
        public static string Export { get { return "导出"; } }
        /// <summary>
        /// 创建失败
        /// </summary>
        public static string InsertFail { get { return "创建失败"; } }
        /// <summary>
        /// 创建成功
        /// </summary>
        public static string InsertSucceed { get { return "创建成功"; } }
        /// <summary>
        /// 加载中
        /// </summary>
        public static string Loading { get { return "加载中"; } }
        /// <summary>
        /// 登录成功,您可以继续操作
        /// </summary>
        public static string LoginSucceed { get { return "登录成功,您可以继续操作！"; } }
        /// <summary>
        /// 安全退出
        /// </summary>
        public static string LogOff { get { return "安全退出"; } }
        /// <summary>
        /// 没有作任何修改
        /// </summary>
        public static string NoAnyChanges { get { return "没有作任何修改"; } }
        /// <summary>
        /// 操作
        /// </summary>
        public static string Operate { get { return "操作"; } }
        /// <summary>
        /// 主键重复
        /// </summary>
        public static string PrimaryRepeat { get { return "主键重复"; } }
        /// <summary>
        /// 查询
        /// </summary>
        public static string Query { get { return "查询"; } }
        /// <summary>
        /// 刷新
        /// </summary>
        public static string Reload { get { return "刷新"; } }
        /// <summary>
        /// 返回
        /// </summary>
        public static string Return { get { return "返回"; } }
        /// <summary>
        /// 保存
        /// </summary>
        public static string Save { get { return "保存"; } }
        /// <summary>
        /// 选择
        /// </summary>
        public static string Select { get { return "选择"; } }
        /// <summary>
        /// 设置失败
        /// </summary>
        public static string SetFail { get { return "设置失败"; } }
        /// <summary>
        /// 设置成功
        /// </summary>
        public static string SetSucceed { get { return "设置成功"; } }
        /// <summary>
        /// 切换主题,系统将重新加载
        /// </summary>
        public static string SwitchingSkin { get { return "切换主题,系统将重新加载?"; } }
        /// <summary>
        /// 提示
        /// </summary>
        public static string Tip { get { return "提示"; } }
        /// <summary>
        /// 反审核
        /// </summary>
        public static string UnCheck { get { return "反审核"; } }
        /// <summary>
        /// 反审核失败
        /// </summary>
        public static string UnCheckFail { get { return "反审核失败"; } }
        /// <summary>
        /// 反审核成功
        /// </summary>
        public static string UnCheckSucceed { get { return "反审核成功"; } }
        /// <summary>
        /// 反选
        /// </summary>
        public static string UnSelect { get { return "反选"; } }
        /// <summary>
        /// 更新
        /// </summary>
        public static string Update { get { return "更新"; } }
        /// <summary>
        /// 更新失败
        /// </summary>
        public static string UpdateFail { get { return "更新失败"; } }
        /// <summary>
        /// 更新成功
        /// </summary>
        public static string UpdateSucceed { get { return "更新成功"; } }
        /// <summary>
        /// 上传
        /// </summary>
        public static string UpLoad { get { return "上传"; } }
        /// <summary>
        /// 用户登录超时,请重新登录
        /// </summary>
        public static string UserTimeOver { get { return "用户登录超时,请重新登录"; } }
        /// <summary>
        /// 您好,欢迎您
        /// </summary>
        public static string Welcome { get { return "您好,欢迎您"; } }
        /// <summary>
        /// 一次只能操作一条记录
        /// </summary>
        public static string YouCanOnlyOperateARecord { get { return "一次只能操作一条记录"; } }
        /// <summary>
        /// 您确定要删除所选记录吗
        /// </summary>
        public static string YouWantToDeleteTheSelectedRecords { get { return "您确定要删除所选记录吗？"; } }

        /// <summary>
        /// 您确定要启用所选记录吗
        /// </summary>
        public static string YouWantToEnableTheSelectedRecords { get { return "您确定要启用所选记录吗？"; } }

        /// <summary>
        /// 您确定要禁用所选记录吗
        /// </summary>
        public static string YouWantToDisableTheSelectedRecords { get { return "您确定要禁用所选记录吗？"; } }

        /// <summary>
        /// 如需一次删除多条数据请用批量删除功能
        /// </summary>
        public static string ToDeleteMultipleRecords { get { return "如需一次删除多条数据请用批量删除功能！"; } }

        /// <summary>
        /// 您确定要设置该记录为默认吗
        /// </summary>
        public static string YouWantToSetDefault { get { return "您确定要设置该记录为默认吗？"; } }

        /// <summary>
        /// EDM邮件提交失败
        /// </summary>
        public static string EDMEmailSubmitFail { get { return "EDM邮件提交失败"; } }
        /// <summary>
        /// EDM邮件提交成功
        /// </summary>
        public static string EDMEmailSubmitSucceed { get { return "EDM邮件提交成功"; } }

        /// <summary>
        /// 邮件配置数据不存在
        /// </summary>
        public static string EmailConfigurationDataDoesNotExist { get { return "邮件配置数据不存在"; } }
        /// <summary>
        /// 提交中包含非法字符
        /// </summary>
        public static string SubmitContainsIllegalCharacters { get { return "提交中包含非法字符！例如：'/><"; } }

        /// <summary>
        /// 已存在同名记录
        /// </summary>
        public static string TheExistingRecordsWithTheSame { get { return "已存在同名记录"; } }

        /// <summary>
        /// 操作失败
        /// </summary>
        public static string OperationFail { get { return "操作失败"; } }
        /// <summary>
        /// 操作成功
        /// </summary>
        public static string OperationSucceed { get { return "操作成功"; } }

        /// <summary>
        /// 登录信息丢失
        /// </summary>
        public static string LoginInformationLoss { get { return "请求数据出错，您的登录信息已丢失－请重新登录！"; } }



    }
}