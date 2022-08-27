using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UserLogic.UserAuthentication.Queries.LoginUser;
using Domain.Entities.User;
using Application.UserLogic.UserAuthentication.Commands.RegisterUser;
using Application.Common.Authentication.Cookie;
using Application.Common.Models;
using Application.Common.Help;
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
        public async Task<ActionResult> Login([FromBody]UserLoginDTO user)
        {
            var (response,status) = await _mediator.Send(new LoginUserQuery(user.Username, user.Password));

            Response.Cookies.Append("userToken", response, new Cookies().Cookie(DateTime.Now.AddDays(1)));

           

            return StatusCode(status,response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<UserModel> Register(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
