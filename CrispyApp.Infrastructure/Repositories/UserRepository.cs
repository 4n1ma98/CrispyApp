using System;
using System.Security.Cryptography;
using System.Text;
using CrispyApp.Domain.Entities;
using LiteDB;

namespace CrispyApp.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        void Add(User user);
        bool AnyUsers();
        string HashPassword(string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly LiteDB.ILiteDatabase _db;
        private readonly ILiteCollection<User> _collection;

        public UserRepository(CrispyApp.Infrastructure.Data.LiteDbContext context)
        {
            _db = context.Database;
            _collection = _db.GetCollection<User>("users");
            _collection.EnsureIndex(x => x.Username, true);
        }

        public User? GetByUsername(string username)
        {
            return _collection.FindOne(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && x.IsActive);
        }

        public void Add(User user)
        {
            _collection.Insert(user);
        }

        public bool AnyUsers()
        {
            return _collection.Count() > 0;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password + "CrispySalt123");
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
