using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Data.Converter.Contract;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Data.Converter.Implementations
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if(origin is null) return null;

            return new User
            {
                UserName = origin.UserName,
                Password = origin.Password,
                FullName = "teste"

            };
        }

        public List<User> Parse(List<UserVO> origin)
        {
            if(origin is null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public UserVO Parse(User origin)
        {
            if(origin is null) return null;

            return new UserVO
            {
                UserName = origin.UserName,
                Password = origin.Password
            };
        }

        public List<UserVO> Parse(List<User> origin)
        {
            if(origin is null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}