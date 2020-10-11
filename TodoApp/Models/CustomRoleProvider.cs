using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TodoApp.Models
{
    // 抽象クラス
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        // 指定されたユーザーが所属するロールを配列で返す
        public override string[] GetRolesForUser(string username)
        {
            using(var db = new TodoesContext())
            {
                var user = db.Users
                    .Where(u => u.UserName == username)
                    .FirstOrDefault();

                if(user != null)
                {
                    // selectで、ユーザーのロールそれぞれについて探索する
                    // 戻り値が配列なので、ToArray()
                    return user.Roles.Select(role => role.RoleName).ToArray();
                }
            }

            return new string[] { "Users" };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // ユーザーがロールに所属しているかどうかを返す
        public override bool IsUserInRole(string username, string roleName)
        {
            // 上に記述したメソッドで取得したユーザーがロールに含まれるかチェックする
            string[] roles = this.GetRolesForUser(username);
            return roles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}