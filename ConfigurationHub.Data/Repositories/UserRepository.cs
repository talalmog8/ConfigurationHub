using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Configuration.Data;
using ConfigurationHub.Core.DI;
using ConfigurationHub.Domain.Auth;

namespace ConfigurationHub.Data.Repositories
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetUsers(int skip, int take);
        User GetById(int id);
        User Register(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
        long Count();
    }

    [Scoped]
    public class UserService : IUserService
    {
        private readonly ConfigurationContext _context;

        public UserService(ConfigurationContext context)
        {
            _context = context;
        }

        ~UserService()
        {
            _context.Dispose();
        }

        public User Authenticate(string username, string password)
        {
            User user;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("username or password aren't valid", $"{nameof(password)},{nameof(username)}");
            if ((user = _context.Users.SingleOrDefault(x => x.Username == username)) is null)
                throw new ArgumentException($"User with named: {username} doesn't exist");
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new InvalidOperationException("Credentials could not be verified");

            return user;
        }

        public IEnumerable<User> GetUsers(int skip  = 0, int take = 5)
        {
            return _context.Users
                .Skip(skip)
                .Take(take);
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Register(User user, string password)
        {
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new ArgumentException($"Username: {user.Username} already exists", nameof(user.Username));

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new InvalidOperationException("User not found");

            if (userParam.Username != user.Username)
            {
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new ArgumentException($"Username: {userParam.Username} is already taken");
            }

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password Is Invalid");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            userParam.PasswordHash = passwordHash;
            userParam.PasswordSalt = passwordSalt;

            _context.Users.Update(userParam);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user;
            if ((user = _context.Users.Find(id)) != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public long Count()
        {
            return _context.Users.LongCount();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64 || storedSalt.Length != 128)
                return false;

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                    return false;
            }

            return true;
        }
    }
}
