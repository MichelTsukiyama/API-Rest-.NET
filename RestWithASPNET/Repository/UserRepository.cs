using System.Text;
using System;
using System.Security.Cryptography;
using RestWithASPNET.Context;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using System.Linq;

namespace RestWithASPNET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO userVO)
        {
            var pass = ComputeHash(userVO.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => u.UserName.Equals(userVO.UserName) && (u.Password.Equals(pass)));
        }

        public User RefreshUserToken(User user)
        {
            if(!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));
            if(result is not null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private object ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public User Create(User user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            user.Password = (string)pass;

            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (System.Exception)
            {
                throw;
            };
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName.Equals(userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals(userName));

            if(user is null) return false;

            user.RefreshToken = null;

            _context.SaveChanges();
            return true;
        }
    }
}