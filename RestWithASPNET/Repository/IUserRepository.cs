using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Repository
{
    public interface IUserRepository
    {
         User ValidateCredentials(UserVO userVO);
         User ValidateCredentials(string userName);
         bool RevokeToken(string userName);
         User Create(User user);
         User RefreshUserToken(User user);
    }
}