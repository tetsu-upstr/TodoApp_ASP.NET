using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    // Administratorsのみユーザーの管理ができるようアノテーションを設定
    [Authorize(Roles = "Administrators")]
    public class UsersController : Controller
    {
        private readonly TodoesContext db = new TodoesContext();

        // ハッシュ化メソッドを使いたいのでメンバーシッププロバイダーのインスタンスを保持するように追記
        private readonly CustomMembershipProvider membershipProvider = new CustomMembershipProvider();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            // こちらの引数のロールには空の配列を返すようにセット
            this.SetRoles(new List<Role>());
            return View();
        }

        // POST: Users/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,RoleIds")] User user)
        {
            // ビューのリストボックスで選択されたロールがDBのロールに含まれていれば取得して返す
            var roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // 取得したロールをセット
                user.Roles = roles;
                // Create時にパスワードをハッシュ化して登録
                user.Password = this.membershipProvider.GeneratePasswordHash(user.UserName, user.Password);

                db.Users.Add(user);
                db.SaveChanges(); // DBに反映
                return RedirectToAction("Index");
            }

            this.SetRoles(roles); 
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            this.SetRoles(user.Roles);
            return View(user);
        }

        // POST: Users/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,RoleIds")] User user)
        {
            // Createと同様の処理
            var roles = db.Roles.Where(role => user.RoleIds.Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                // DBからユーザー情報を取得
                var dbUser = db.Users.Find(user.Id);
                if(dbUser == null)
                {
                    return HttpNotFound();
                }
                // ユーザーが入力した情報を更新
                //dbUser.UserName = user.UserName;

                // DBに格納されているパスワードがハッシュ化されていなければハッシュ化する
                if (!dbUser.Password.Equals(user.Password))
                {
                    dbUser.Password = this.membershipProvider.GeneratePasswordHash(user.UserName, user.Password);
                }

                //dbUser.Password = user.Password;
                dbUser.Roles.Clear();
                foreach(var role in roles)
                {
                    dbUser.Roles.Add(role);
                }
                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.SetRoles(roles);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Create と Edit のメソッドから共通で使用するメソッドとして定義
        // 引数に現在ユーザーが所属しているロールをセットするように修正
        private void SetRoles(ICollection<Role> userRoles)
        {

            // 配列に変換（扱いやすくするために）
            var roles = userRoles.Select(item => item.Id).ToArray();

            // リストボックスからアイテムを取り出すためにSelectListItemオブジェクトをセット
            var list = db.Roles.Select(item => new SelectListItem()
            {
                Text = item.RoleName,
                Value = item.Id.ToString(),
                // 選択状態にするには、Selected
                Selected = roles.Contains(item.Id)
            }).ToList();

            // ビューで扱うために取得したlistをVieBagにセット
            ViewBag.RoleIds = list;
        }

    }
}
