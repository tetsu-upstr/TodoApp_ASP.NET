using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace TodoApp.Models
{

    // クラス名は必ず単数形
    public class Todo
    {
        public int Id { get; set; } // Idは自動的に主キーになる
        
        [DisplayName("概要")] // アノテーションの設定だけでラベルをつけることができる
        public string Summary  { get; set; }

        [DisplayName("詳細")]
        public string Detail { get; set; }

        [DisplayName("期限")]
        public DateTime Limit { get; set; }

        [DisplayName("完了")]
        public bool Done { get; set; }
    }
}