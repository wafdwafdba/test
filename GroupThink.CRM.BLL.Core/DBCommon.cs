using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupThink.CRM.Model;
using GroupThink.CRM.Model.Sys;

namespace GroupThink.CRM.BLL.Core
{
    public class DBCommon
    {

        /// <summary>
        /// 获取部门下属用户
        /// </summary>   
        /// <param name="DeptId">部门Id</param>
        /// <param name="UserId">登录用户Id</param>
        /// <returns>是否成功</returns>
        public List<User_Model> GetUserListByDeptId(int DeptId, string UserId)
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    string sql = " UP_GetUserList_ByDeptId @DeptId,@UserId";

                    var args = new System.Data.Common.DbParameter[] {
                    new System.Data.SqlClient.SqlParameter { ParameterName = "@DeptId", Value = DeptId},
                    new System.Data.SqlClient.SqlParameter { ParameterName = "@UserId", Value = UserId}
                 };

                    List<User_Model> list = db.ExecuteStoreQuery<User_Model>(sql, args).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    ExceptionHander.WriteException(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取部门下属用户
        /// </summary>   
        /// <param name="DeptId">部门Id</param>
        /// <param name="UserId">登录用户Id</param>
        /// <returns>是否成功</returns>
        public List<string> GetUserIdListByDeptId(int DeptId, string UserId)
        {
            List<User_Model> userList = GetUserListByDeptId(DeptId, UserId);
            List<string> userIdList = new List<string>();

            foreach (User_Model model in userList)
            {
                userIdList.Add(model.UserId);
            }
            return userIdList;
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>      
        /// <returns>是否成功</returns>
        public List<DeptTree_Model> GetDeptTreeAll()
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    string sql = " select DeptId,DeptIdTree as DeptTreeId,DeptName,TreeLevel,IsLast,ParentId from dbo.fn_DeptTree_Info()";

                    List<DeptTree_Model> list = db.ExecuteStoreQuery<DeptTree_Model>(sql).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    ExceptionHander.WriteException(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取登录用户所属部门
        /// </summary>      
        /// <returns>是否成功</returns>
        public List<DeptTree_Model> GetLoginUserDept(string LoginUserId)
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    if (LoginUserId.Trim().Length == 0)
                    {
                        return null;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select d.DeptId,d.DeptIdTree as DeptTreeId,d.DeptName,d.TreeLevel,d.IsLast from (");
                    sb.AppendFormat(" select * from dbo.GroupThink_CRM_RoleUser where userId='{0}') as a", LoginUserId);
                    sb.Append(" left outer join GroupThink_CRM_Role as c ");
                    sb.Append(" on a.RoleId = c.Id ");
                    sb.Append(" join (select * from dbo.fn_DeptTree_Info()) as d ");
                    sb.Append(" on c.DeptId = d.DeptId ");

                    List<DeptTree_Model> list = db.ExecuteStoreQuery<DeptTree_Model>(sb.ToString()).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    ExceptionHander.WriteException(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取登录用户部门及下属部门
        /// </summary>   
        /// <param name="DeptId">部门Id</param>
        /// <param name="UserId">登录用户Id</param>
        /// <returns>是否成功</returns>
        public List<DeptTree_Model> GetUserListByLoginId(string LoginUserId)
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select b.DeptId,b.DeptIdTree as DeptTreeId,b.DeptName,b.TreeLevel,b.IsLast from (");
                    sb.Append(" select d.DeptId,d.DeptIdTree,d.DeptName,d.TreeLevel,d.IsLast from ( ");
                    sb.AppendFormat(" select * from dbo.GroupThink_CRM_RoleUser where userId='{0}') as a  ", LoginUserId);
                    sb.Append(" left outer join GroupThink_CRM_Role as c  on a.RoleId = c.Id  ");
                    sb.Append(" join (select * from dbo.fn_DeptTree_Info()) as d  on c.DeptId = d.DeptId ) as a ");
                    sb.Append(" left outer join dbo.fn_DeptTree_Info() as b ");
                    sb.Append(" on b.DeptIdTree like '%'+a.DeptIdTree+'%' ");

                    List<DeptTree_Model> dept_list = db.ExecuteStoreQuery<DeptTree_Model>(sb.ToString()).ToList();

                    return dept_list;
                }
                catch (Exception ex)
                {
                    ExceptionHander.WriteException(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取登录部门下属用户，可选择是否包括登录用户和同级用户
        /// </summary>   
        /// <param name="DeptId">部门Id</param>
        /// <param name="UserId">登录用户Id</param>
        /// <param name="IsIncLoginDept">是否包含登录用户所属部门</param>
        /// <returns>是否成功</returns>
        public List<User_Model> GetUserListByLoginUserId(string LoginUserId, bool IsIncLoginDept)
        {
            using (DBContainer db = new DBContainer())
            {
                try
                {
                    List<DeptTree_Model> userDept_list = GetLoginUserDept(LoginUserId);
                    if (userDept_list == null)
                    {
                        return null;
                    }

                    List<DeptTree_Model> dept_list = GetUserListByLoginId(LoginUserId);

                    if (!IsIncLoginDept)
                    {
                        List<string> DeptTreeIdList = new List<string>();

                        List<DeptTree_Model> tempDept_List = new List<DeptTree_Model>();
                        foreach (DeptTree_Model model in userDept_list)
                        {
                            if (userDept_list.Where(a => model.DeptTreeId.Contains(a.DeptTreeId)).Count() == 1)
                            {
                                tempDept_List.Add(model);
                            }
                        }
                        foreach (DeptTree_Model model in tempDept_List)
                        {
                            for (int nI = 0; nI < dept_list.Count; nI++)
                            {
                                if (model.DeptId == dept_list[nI].DeptId)
                                {
                                    dept_list.RemoveAt(nI);
                                    break;
                                }
                            }
                        }
                    }

                    if (userDept_list.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" select distinct d.Id as UserId,d.UserName from (");
                        sb.AppendFormat(" select * from  GroupThink_CRM_Role where DeptId in ({0}) ) as b", string.Join(",", userDept_list.Select(a => a.DeptId).ToList()));
                        sb.Append(" left outer join GroupThink_CRM_RoleUser as c ");
                        sb.Append(" on b.Id = c.RoleId ");
                        sb.Append(" join dbo.GroupThink_CRM_User as d ");
                        sb.Append(" on c.UserId = d.Id ");

                        List<User_Model> list = db.ExecuteStoreQuery<User_Model>(sb.ToString()).ToList();

                        return list;
                    }
                    else if (userDept_list.Count > 0 && dept_list.Count> 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" select distinct d.Id as UserId,d.UserName from (");
                        sb.AppendFormat(" select * from  GroupThink_CRM_Role where DeptId in ({0}) ) as b", string.Join(",", userDept_list.Select(a => a.DeptId).ToList()));
                        sb.Append(" left outer join GroupThink_CRM_RoleUser as c ");
                        sb.Append(" on b.Id = c.RoleId ");
                        sb.Append(" join dbo.GroupThink_CRM_User as d ");
                        sb.Append(" on c.UserId = d.Id ");
                        sb.Append("union all");
                        sb.Append(" select distinct d.Id as UserId,d.UserName from (");
                        sb.AppendFormat(" select * from  GroupThink_CRM_Role where DeptId in ({0}) ) as b", string.Join(",", dept_list.Select(a => a.DeptId).ToList()));
                        sb.Append(" left outer join GroupThink_CRM_RoleUser as c ");
                        sb.Append(" on b.Id = c.RoleId ");
                        sb.Append(" join dbo.GroupThink_CRM_User as d ");
                        sb.Append(" on c.UserId = d.Id ");
                        List<User_Model> list = db.ExecuteStoreQuery<User_Model>(sb.ToString()).ToList();

                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHander.WriteException(ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>      
        /// <returns>是否成功</returns>
        public List<DeptUserTree_Model> GetDeptUserTreeAll()
        {
            List<DeptUserTree_Model> TreeList = new List<DeptUserTree_Model>();

            using (DBContainer db = new DBContainer())
            {
                List<DeptTree_Model> list = GetDeptTreeAll();
                if (list != null)
                {
                    List<DeptUser_Model> userList = (from r in db.GroupThink_CRM_Role
                                                     join d in db.GroupThink_CRM_Dept
                                                     on r.DeptId equals d.DeptId
                                                     join ru in db.GroupThink_CRM_RoleUser
                                                     on r.Id equals ru.RoleId
                                                     join u in db.GroupThink_CRM_User
                                                     on ru.UserId equals u.Id
                                                     select new DeptUser_Model
                                                     {
                                                         DeptId = d.DeptId,
                                                         UserId = u.Id,
                                                         UserName = u.UserName
                                                     }).ToList();

                    GetDeptUserTree(TreeList, list, userList, 0);
                }
            }
            return TreeList;
        }

        /// <summary>
        /// 获取部门下直属员工，不递归
        /// </summary>
        /// <param name="DeptId">部门Id</param>
        /// <returns></returns>
        public List<DeptUser_Model> GetDeptUserByDeptId(int DeptId)
        {
            List<DeptUser_Model> userList = new List<DeptUser_Model>();
            using (DBContainer db = new DBContainer())
            {
                userList = (from r in db.GroupThink_CRM_Role
                            join d in db.GroupThink_CRM_Dept
                            on r.DeptId equals d.DeptId
                            join ru in db.GroupThink_CRM_RoleUser
                            on r.Id equals ru.RoleId
                            join u in db.GroupThink_CRM_User
                            on ru.UserId equals u.Id
                            where u.UserState == 1 && d.DeptId == DeptId
                            select new DeptUser_Model
                            {
                                DeptId = d.DeptId,
                                UserId = u.Id,
                                UserName = u.UserName
                            }).ToList();
            }
            return userList;
        }

        //递归
        private List<DeptUserTree_Model> GetDeptUserTree(List<DeptUserTree_Model> TreeList, List<DeptTree_Model> list, List<DeptUser_Model> DeptUserList, int DeptId)
        {
            foreach (DeptTree_Model model in list.Where(a => a.ParentId == DeptId))
            {
                DeptUserTree_Model treeModel = new DeptUserTree_Model();
                treeModel.DeptModel = model;

                treeModel.userList = DeptUserList.Where(a => a.DeptId == model.DeptId).ToList();

                List<DeptUserTree_Model> temp = new List<DeptUserTree_Model>();
                treeModel.subList = GetDeptUserTree(temp, list, DeptUserList, model.DeptId);

                TreeList.Add(treeModel);
            }

            return TreeList;
        }

        public List<Tree_Model> GetTree(List<Tree_Model> treeList, List<DeptUserTree_Model> list)
        {
            foreach (DeptUserTree_Model model in list)
            {
                Tree_Model tree = new Tree_Model();
                tree.id = model.DeptModel.DeptId.ToString();
                tree.text = model.DeptModel.DeptName.ToString();
                tree.Type = 2;
                tree.state = "closed";
                tree.attributes = new { Types = "Dept" };

                List<Tree_Model> treeNewList = new List<Tree_Model>();
                foreach (DeptUser_Model user in model.userList)
                {
                    Tree_Model tree_user = new Tree_Model();
                    tree_user.id = user.UserId;
                    tree_user.text = user.UserName;
                    tree_user.Type = 1;
                    tree_user.state = "open";
                    tree.attributes = new { Types = "User" };
                    treeNewList.Add(tree_user);
                }
                List<Tree_Model> treeList_temp = new List<Tree_Model>();
                List<Tree_Model> subList = GetTree(treeList_temp, model.subList);

                tree.children = treeNewList.Concat(subList).ToList();
                treeList.Add(tree);
            }
            return treeList;
        }
    }
}
