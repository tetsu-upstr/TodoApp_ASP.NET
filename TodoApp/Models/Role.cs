using System.Collections.Generic;

namespace TodoApp.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        // UserとRoleは、n対nの関係
        public virtual ICollection<User> Users { get; set; }
    }
}