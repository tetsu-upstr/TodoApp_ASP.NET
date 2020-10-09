using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace TodoApp.Models
{

    // クラス名は必ず単数形
    public class Todo
    {
        public int Id { get; set; } // Idは自動的に主キーになる
        public string Summary  { get; set; }
        public string Detail { get; set; }
        public DateTime Limit { get; set; }
        public bool Done { get; set; }
    }
}