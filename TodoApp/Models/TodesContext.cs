using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    // DBコンテキストクラスを通してデータを扱う
    public class TodoesContext : DbContext
    {
        // このプラグラムを通してデータの登録や更新を行う
        public DbSet<Todo> Todoes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        
    }
}