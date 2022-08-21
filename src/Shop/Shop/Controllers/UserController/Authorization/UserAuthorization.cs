using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UserLogic.UserAuthentication.Queries.LoginUser;
using Domain.Entities.User;
using Application.UserLogic.UserAuthentication.Commands.RegisterUser;
using Application.Common.Authentication.Cookie;
using Application.Common.Models;
namespace Shop.Controllers.UserController.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthorization : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserAuthorization(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<string> Login([FromBody]UserLoginDTO user)
        {
            var jwt = await _mediator.Send(new LoginUserQuery(user.Username, user.Password));

            Response.Cookies.Append("userToken", jwt , new Cookies().Cookie(DateTime.Now.AddDays(1)));

            return jwt;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<UserModel> Register(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
