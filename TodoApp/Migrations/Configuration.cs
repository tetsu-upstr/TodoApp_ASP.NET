﻿namespace TodoApp.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using TodoApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoApp.Models.TodoesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // 追記 モデルへの列の削除といった修正も自動的にマイグレーションに反映させてもいいかどうかの設定
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TodoApp.Models.TodoesContext";
        }

        protected override void Seed(TodoApp.Models.TodoesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            User admin = new User()
            {
                Id = 1,
                UserName = "admin",
                Password = "password",
                Roles = new List<Role>()
            };

            Role administrators = new Role
            {
                Id = 1,
                RoleName = "Administrators",
                Users = new List<User>()
            };

            Role users = new Role
            {
                Id = 2,
                RoleName = "Users",
                Users = new List<User>()
            };

            // adminのパスワードをハッシュ化
            var membarshipProvieder = new CustomMembershipProvider();
            admin.Password = membarshipProvieder.GeneratePasswordHash(admin.UserName, admin.Password);

            admin.Roles.Add(administrators);
            administrators.Users.Add(admin);

            // AddOrUpdate データベースになければ登録、あれば更新するメソッド
            context.Users.AddOrUpdate(user => user.Id, new User[] { admin });
            context.Roles.AddOrUpdate(role => role.Id, new Role[] { administrators, users });
        }
    }
}
