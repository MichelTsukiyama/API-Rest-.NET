using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userVO);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
        User Create(UserVO userVO);
        User RefreshUserInfo(User user);
    }
}