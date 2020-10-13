using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)] // 重複禁止のアノテーション
        [StringLength(256)] // 文字列の長さに制限を設定しないとDB作成時にエラーになる
        [DisplayName("ユーザー名")]
        public string  UserName { get; set; }

        [Required]
        [DisplayName("パスワード")]
        public string Password { get; set; }

        [NotMapped] // マイグレーション時に作られないようにするため
        [DisplayName("ロール")]
        public List<int> RoleIds { get; set; }

        // 複数のロールに所属できるように設定する
        // ナビゲーションプロパティ(virtualの修飾子が必須）
        public virtual ICollection<Role> Roles { get; set; }

        // Todoとユーザーが紐づくように追加
        // 一人のユーザーは複数のTodoを持つのでコレクションとして保持する(1対nの関係)
        public virtual ICollection<Todo> Todoes { get; set; }
    }
}