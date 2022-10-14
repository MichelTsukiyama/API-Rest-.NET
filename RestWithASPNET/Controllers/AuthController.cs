using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/api/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signing")]
        public IActionResult Sigining([FromBody] UserVO userVO)
        {
            if(userVO is null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(userVO);

            if (token is null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if(tokenVO is null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(tokenVO);

            if (token is null) return BadRequest("Invalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);

            if (!result) return BadRequest("Invalid client request");

            return NoContent();
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup([FromBody] UserVO userVO)
        {
            if(userVO is null) return BadRequest("Invalid input");

            return Ok(_loginBusiness.Create(userVO));
        }

    }
}