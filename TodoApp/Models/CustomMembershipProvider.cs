using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Web.Security;

namespace TodoApp.Models
{
    // 抽象クラスなのでメソッドを実装する必要がある
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        // ユーザー認証のバリデーション
        public override bool ValidateUser(string username, string password)
        {
            // まずDBに接続する為のコンテキストクラスを生成する
            using(var db = new TodoesContext())
            {
                // DBに格納されたハッシュ値を比較するように変更
                string hash = this.GeneratePasswordHash(username, password);

                var user = db.Users
                    .Where(u => u.UserName == username && u.Password == hash)
                    .FirstOrDefault();

                if(user != null)
                {
                    return true;
                }
            }

            // あとで削除
            if("admin".Equals(username) && "password".Equals(password)){
                return true;
            }

            return false;
        }

        // パスワードを強くするメソッド
        public string GeneratePasswordHash(string username, string password)
        {
            // ユーザー名にソルトを追加
            string rawSalt = $"secret_{username}";
            var sha256 = new SHA256CryptoServiceProvider();
            // ComputeHashはバイトの文字列を引数にとるので変換してあげる
            var salt = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawSalt));

            // さらにパスワードのハッシュ化
            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pdkdf2.GetBytes(32);

            return Convert.ToBase64String(hash); // バイトの配列を文字列に変換
        }
    }
}